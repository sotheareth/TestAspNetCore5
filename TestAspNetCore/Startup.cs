using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using TestAspNetCore.BackgroundTask;
using TestAspNetCore.Filters;
using TestAspNetCore.Mapping;
using TestAspNetCore.Middlewares;
using TestAspNetCore.Options;
using TestAspNetCore_Core.Contract;
using TestAspNetCore_Core.Interfaces;
using TestAspNetCore_Infrastructure.Data;
using TestAspNetCore_Infrastructure.Service;
using TestAspNetCore_Infrastructure.Services;

namespace TestAspNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var jwtSetting = new JwtSetting();
            Configuration.Bind(nameof(jwtSetting), jwtSetting);
            services.AddSingleton(jwtSetting);

            services.AddMvcCore(options =>
            {
                options.Filters.Add(new ValidationFilter());
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSetting.Issuer,
                    ValidAudience = jwtSetting.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecretKey))
                };
            });

            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddDbContextPool<CustomerContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));            

            services.AddControllers()
                .AddFluentValidation(s =>
                {
                    s.RegisterValidatorsFromAssemblyContaining<Startup>();
                    s.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });

            services.Configure<MySettingsModel>(Configuration.GetSection("MySettings"));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            //services.AddScoped<ValidationFilter>();
            services.AddTransient<IFileService, FileService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddSingleton<IConnectionMultiplexer>(x => 
                ConnectionMultiplexer.Connect(Configuration.GetValue<string>("RedisConnection")));
            services.AddSingleton<ICacheService, RedisCacheService>();
            services.AddHostedService<RedisSubscriber>();
            

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestAspNetCore", Version = "v1" });
                c.ExampleFilters();

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer" , new string[0] }
                };

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
            services.AddDirectoryBrowser();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestAspNetCore v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            var contenTypeProvider = new FileExtensionContentTypeProvider();
            contenTypeProvider.Mappings.Clear();
            contenTypeProvider.Mappings[".html"] = "text/html";

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(env.ContentRootPath, "StaticFiles")),
            //    RequestPath = "/StaticFiles",
            //    ContentTypeProvider = provider
            //});

            var provider = new PhysicalFileProvider(
                Path.Combine(env.ContentRootPath, "StaticFiles")
            );
            
            var _fileServerOptions = new FileServerOptions();
            _fileServerOptions.RequestPath = "/StaticFiles";
            _fileServerOptions.StaticFileOptions.FileProvider = provider;
            _fileServerOptions.StaticFileOptions.ContentTypeProvider = contenTypeProvider;
            _fileServerOptions.EnableDirectoryBrowsing = true;
            app.UseFileServer(_fileServerOptions);

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
