using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TestAspNetCore.Attributes;
using TestAspNetCore.Filters;
using TestAspNetCore_Core.Contract;
using TestAspNetCore_Core.Dtos.Requests;
using TestAspNetCore_Core.Dtos.Responses;
using TestAspNetCore_Core.Entities;

namespace TestAspNetCore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
 

        public CustomersController(ILogger<CustomersController> logger,
            ICustomerService customerService,
            IMapper mapper)
        {
            _logger = logger;
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllCustomer()
        {
            var result = await _customerService.GetCustomers();
            return Ok(result);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetAllCustomer(int customerId)
        {
            var result = await _customerService.GetCustomerById(customerId);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomerResponseDto), StatusCodes.Status400BadRequest)]
        //[ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerRequestDto request)
        { 
            if (!ModelState.IsValid)
            {

            }

            var customer = _mapper.Map<Customer>(request);
            var registerCustomer = await _customerService.RegisterCustomer(customer);
            var result = _mapper.Map<CustomerResponseDto>(registerCustomer);
            return Ok(result);
        }

        [HttpGet("countNameLength")]
        public async Task<IActionResult> countNameLength([FromQuery(Name = "name")] string name)
        {
            var result = await _customerService.CountNameLength(name);
            return Ok(result);
        }
    }
}
