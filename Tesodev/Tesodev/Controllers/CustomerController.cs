using Microsoft.AspNetCore.Mvc;
using Tesodev.Models;
using Tesodev.Repository;
using System;
using System.Collections.Generic;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tesodev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var products = _customerRepository.GetCustomers();
            return new OkObjectResult(products);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(Guid guid)
        {
            var product = _customerRepository.GetCustomerByID(guid);
            return new OkObjectResult(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            using (var scope = new TransactionScope())
            {
                _customerRepository.CreateCustomer(customer);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = customer.CustomerId }, customer);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Customer customer)
        {
            if (customer != null)
            {
                using (var scope = new TransactionScope())
                {
                    _customerRepository.UpdateCustomer(customer);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid guid)
        {
            _customerRepository.DeleteCustomer(guid);
            return new OkResult();
        }
    }
}
