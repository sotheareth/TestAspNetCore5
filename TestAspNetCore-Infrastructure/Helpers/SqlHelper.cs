using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAspNetCore_Infrastructure.Data;

namespace WebApplicationInfrastructure.Helpers
{
    public static class SqlHelper
    {
        public static IList<T> SqlQuery<T>(this DbContext db, Func<T> targetType, string sql, params object[] parameters) where T : class
        {
            return SqlQuery<T>(db, sql, parameters);
        }

        public static IList<T> SqlQuery<T>(this DbContext db, string sql, params object[] parameters) where T : class
        {
            using (var db2 = new ContextForQueryType<T>(db.Database.GetDbConnection()))
            {
                return db2.SqlQuery<T>(sql, parameters).ToList();
            }
        }

        public static void ExecuteSqlQuery(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var concurrencyDetector = databaseFacade.GetService<IConcurrencyDetector>();

            using (concurrencyDetector.EnterCriticalSection())
            {
                var rawSqlCommand = databaseFacade
                    .GetService<IRawSqlCommandBuilder>()
                    .Build(sql, parameters);

                var paramObject = new RelationalCommandParameterObject(databaseFacade.GetService<IRelationalConnection>(), 
                    rawSqlCommand.ParameterValues, null, null, null);

                rawSqlCommand
                    .RelationalCommand
                    .ExecuteNonQuery(paramObject);
            }
        }

        private class ContextForQueryType<T> : DbContext where T : class
        {
            private readonly DbConnection connection;

            public ContextForQueryType(DbConnection connection)
            {
                this.connection = connection;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                // switch on the connection type name to enable support multiple providers
                // var name = con.GetType().Name;
                optionsBuilder.UseSqlServer(connection, options => options.EnableRetryOnFailure());

                base.OnConfiguring(optionsBuilder);
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<T>().HasNoKey();
                base.OnModelCreating(modelBuilder);
            }
        }
    }
}
