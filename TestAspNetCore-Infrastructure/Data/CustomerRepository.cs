using Microsoft.EntityFrameworkCore;
using StoredProcedureEFCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAspNetCore_Core.Entities;
using TestAspNetCore_Core.Interfaces;

namespace TestAspNetCore_Infrastructure.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _customerContext;

        public CustomerRepository(CustomerContext customerContext)
        {
            this._customerContext = customerContext;
        }

        public async Task<Customer> Delete(Customer Customer)
        {
            var result = _customerContext.Customer.Remove(Customer);
            await _customerContext.SaveChangesAsync();
            return await Task.FromResult(result.Entity);
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            return await _customerContext.Customer.SingleOrDefaultAsync(c => c.Id == customerId);                            
        }

        public async Task<ICollection<Customer>> GetCustomers()
        {
            return await _customerContext.Customer.FromSqlRaw("EXEC spGetAllCustomer").ToListAsync<Customer>();
        }

        public async Task<Object> GetCustomer()
        {
            return await _customerContext.Customer
                            .Include(c => c.Invoices)
                                .ThenInclude( d => d.InvoiceLines)
                                    .ThenInclude( t => t.Track)
                                        .ThenInclude ( m => m.MediaType)
                            .Include(e => e.SupportRep)
                            .Include( k => k.Address)
                            .SingleOrDefaultAsync(c => c.Id == 1);
        }

        public async Task<Customer> RegisterCustomer(Customer Customer)
        {
            var result = await _customerContext.Customer.AddAsync(Customer);
            await _customerContext.SaveChangesAsync();
            return await Task.FromResult(result.Entity);
        }

        public async Task<Customer> UpdateCustomer(Customer Customer)
        {
            var entity = _customerContext.Customer.Update(Customer);
            await _customerContext.SaveChangesAsync();
            return await Task.FromResult(entity.Entity);
        }

        public async Task<Customer> GetCustomer(int customerId)
        {
            //return await _customerContext.Customer.FromSqlInterpolated($"EXEC spGetCustomer {customerId}").FirstOrDefaultAsync();
            Customer result = null;
            await _customerContext.LoadStoredProc("dbo.spGetCustomer")
                .AddParam("customerId", customerId)
                .ExecAsync(async r => result = await r.FirstOrDefaultAsync<Customer>());

            return result;
        }

        public async Task<int> CountNameLength(string name)
        {
            //var parameters = new SqlParameter[] {
            //    new SqlParameter() {
            //        ParameterName = "@name",
            //        SqlDbType =  System.Data.SqlDbType.VarChar,
            //        Size = 1000,
            //        Direction = System.Data.ParameterDirection.Input,
            //        Value = name
            //    },
            //    new SqlParameter() {
            //        ParameterName = "@count",
            //        SqlDbType =  System.Data.SqlDbType.Int,
            //        Direction = System.Data.ParameterDirection.Output,
            //    }
            //};

            //await _customerContext.Database.ExecuteSqlRawAsync("[dbo].[spCountNameLength] @name, @count OUTPUT", parameters);
            //int count = Convert.ToInt32(parameters[1].Value);
            //return count;

            await _customerContext.LoadStoredProc("dbo.spCountNameLength") // no need "[dbo].[spCountNameLength] @name, @count OUTPUT"
               .AddParam("name", name)
               .AddParam("count", out IOutParam<int> count)
               .ExecNonQueryAsync();

            int result = count.Value;
            return result;
        }
    }
}
