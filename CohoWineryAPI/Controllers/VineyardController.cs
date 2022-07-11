using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using CohoWineryAPI.Data;

namespace CohoWineryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VineyardController : ControllerBase
    {
        private readonly Data.VineyardContext _context;
        private readonly ILogger<VineyardController> _logger;

        public VineyardController(Data.VineyardContext context, ILogger<VineyardController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("Products")]
        public IEnumerable<Product> GetProducts()
        {
            _logger.LogInformation("Retreiving products.");
            return _context.Products;
        }

        [HttpGet]
        [Route("Orders")]
        public IEnumerable<Order> GetOrders()
        {
            _logger.LogInformation("Retreiving orders.");
            var result = _context.Orders.Include(oi => oi.OrderItems);
            return result;
        }

        [HttpGet]
        [Route("Order")]
        public Order GetOrder(int id)
        {
            _logger.LogInformation($"Retreiving order by id {id}.");
            return _context.Orders.FirstOrDefault(o => o.Id == id);
        }

        [HttpPost]
        [Route("Order")]
        public Order CreateOrder(Order order)
        {
            _logger.LogInformation("Creating order.");
            _context.Orders.Add(order);
            _context.SaveChanges();
            _logger.LogInformation($"Created order {order.Id}");
            return order;
        }

        [HttpDelete]
        [Route("Order")]
        public void DeleteOrder(int id)
        {
            _logger.LogInformation("Deleting order.");
            var order = new Order() { Id = id };
            _context.Attach(order);
            _context.Remove(order);
            _context.SaveChanges();
        }
    }
}
