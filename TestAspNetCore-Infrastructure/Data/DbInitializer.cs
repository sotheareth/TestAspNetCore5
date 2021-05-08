using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using TestAspNetCore_Core.Entities;
using TestAspNetCore_Infrastructure.Data;
using WebApplicationInfrastructure.Helpers;

namespace WebApplicationInfrastructure.Data
{
    public class DbInitializer
    {

        public static void Initialize(CustomerContext context, ILogger<DbInitializer> logger)
        {
            
            var manager = new Employee
            {
                FirstName = "manager",
                LastName = "manager",
                Title = "Mr.",
                BirthDate = DateTime.Now,
                Email = "manager@gmail.com"
            };
            context.Employee.Add(manager);            

            var employee = new Employee
            {                
                FirstName = "test",
                LastName = "test",
                Title = "Mr.",
                BirthDate = DateTime.Now,
                Email = "sotheareth.ham@gmail.com"
            };
            context.Employee.Add(employee);            

            var customer = new Customer {              
                SupportRep =  employee,
                Address = new Address {                    
                    City ="test",
                    Street="test"
                },
                City = "test",
                Company = "test",
                Country = "Cambodia",                
                Email = "sothearet.ham@gmail.com",
                FirstName = "test",
                LastName = "test",
                Tag = "test"
            };
            context.Customer.Add(customer);
            context.SaveChanges();


            //Add Stored Procedures
            foreach (var file in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sql"), "*.sql"))
            {
                var content = File.ReadAllText(file);

                var sqls = content.Split("GO").Where(x => x != "").ToList();
                foreach (var sql in sqls)
                {
                    context.Database.ExecuteSqlQuery(sql, new object[0]);
                }
            }


        }
    }
}
