using Microsoft.AspNetCore.Mvc;
using Tesodev.Models;
using Tesodev.Repository;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Tesodev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var products = _orderRepository.GetOrders();
            return new OkObjectResult(products);
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get(Guid guid)
        {
            var product = _orderRepository.GetOrderByID(guid);
            return new OkObjectResult(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            using (var scope = new TransactionScope())
            {
                _orderRepository.CreateOrder(order);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = order.OrderId }, order);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Order order)
        {
            if (order != null)
            {
                using (var scope = new TransactionScope())
                {
                    _orderRepository.UpdateOrder(order);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid guid)
        {
            _orderRepository.DeleteOrder(guid);
            return new OkResult();
        }
    }
}
