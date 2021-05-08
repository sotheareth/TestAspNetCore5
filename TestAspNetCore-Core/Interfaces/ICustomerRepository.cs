using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAspNetCore_Core.Entities;

namespace TestAspNetCore_Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> RegisterCustomer(Customer order);
        Task<Customer> GetCustomerById(int CustomerId);
        Task<Object> GetCustomer();
        Task<Customer> GetCustomer(int customerId);
        Task<ICollection<Customer>> GetCustomers();
        Task<Customer> UpdateCustomer(Customer order);
        Task<Customer> Delete(Customer Customer);
        Task<int> CountNameLength(string name);
    }
}
