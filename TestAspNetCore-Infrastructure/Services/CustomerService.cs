using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAspNetCore_Core.Contract;
using TestAspNetCore_Core.Entities;
using TestAspNetCore_Core.Interfaces;

namespace TestAspNetCore_Infrastructure.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository CustomerRepository, ILogger<CustomerService> logger)
        {
            _logger = logger;
            _customerRepository = CustomerRepository;
        }

        public async Task<Customer> Delete(int CustomerId)
        {
            var Customer = await _customerRepository.GetCustomerById(CustomerId);
            return await _customerRepository.Delete(Customer);
        }

        public async Task<Customer> GetCustomerById(int CustomerId)
        {
            return await _customerRepository.GetCustomerById(CustomerId);
        }

        public async Task<Object> GetCustomer()
        {
            return await _customerRepository.GetCustomer();
        }

        public async Task<Customer> RegisterCustomer(Customer Customer)
        {
            return await _customerRepository.RegisterCustomer(Customer);
        }

        public async Task<Customer> UpdateCustomer(Customer Customer)
        {
            return await _customerRepository.UpdateCustomer(Customer);
        }

        public async Task<ICollection<Customer>> GetCustomers()
        {
            return await _customerRepository.GetCustomers();
        }

        public async Task<Customer> GetCustomer(int customerId)
        {
            return await _customerRepository.GetCustomer(customerId);
        }

        public async Task<int> CountNameLength(string name)
        {
            return await _customerRepository.CountNameLength(name);
        }
    }
}
