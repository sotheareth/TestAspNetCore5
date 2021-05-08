using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAspNetCore_Core.Entities;

namespace TestAspNetCore_Core.Contract
{
    public interface ICustomerService
    {
        Task<Customer> RegisterCustomer(Customer customer);
        Task<Customer> GetCustomerById(int customerId);
        Task<Object> GetCustomer();
        Task<ICollection<Customer>> GetCustomers();
        Task<Customer> GetCustomer(int customerId);
        Task<Customer> UpdateCustomer(Customer customer);
        Task<Customer> Delete(int customerId);
        Task<int> CountNameLength(string name);
    }
}
