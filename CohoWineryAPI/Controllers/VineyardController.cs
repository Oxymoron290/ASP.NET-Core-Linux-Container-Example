using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using CohoWineryAPI.Model;

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
            return _context.Products.Select(p => new Product { Id = p.Id, Name = p.Name, Style = p.Style}).ToList();
        }

        [HttpGet]
        [Route("Orders")]
        public IEnumerable<Order> GetOrders()
        {
            _logger.LogInformation("Retreiving orders.");
            return _context.Orders.Select(o => new Order
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                DeliveryAddress = o.DeliveryAddress,
                OrderItems = o.OrderItems.Select(oi => new OrderItem
                {
                    Id = oi.Id,
                    Quantity = oi.Quantity,
                    Product = new Product
                    {
                        Id = oi.Product.Id,
                        Name = oi.Product.Name,
                        Style = oi.Product.Style,
                    }
                }).ToList()
            });
        }

        [HttpGet]
        [Route("Order")]
        public Order GetOrder(int id)
        {
            _logger.LogInformation($"Retreiving order by id {id}.");
            return _context.Orders.Select(o => new Order
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                DeliveryAddress = o.DeliveryAddress,
                OrderItems = o.OrderItems.Select(oi => new OrderItem
                {
                    Id = oi.Id,
                    Quantity = oi.Quantity,
                    Product = new Product
                    {
                        Id = oi.Product.Id,
                        Name = oi.Product.Name,
                        Style = oi.Product.Style,
                    }
                }).ToList()
            }).FirstOrDefault(o => o.Id == id);
        }

        [HttpPost]
        [Route("Order")]
        public Order CreateOrder(Order order)
        {
            _logger.LogInformation("Creating order.");
            var o = new Data.Order()
            {
                CustomerId = order.CustomerId,
                DeliveryAddress = order.DeliveryAddress
            };
            var items = order.OrderItems.Select(oi => new Data.OrderItem
            {
                Quantity = oi.Quantity,
                ProductId = oi.Product.Id,
                Order = o
            });

            _context.Orders.Add(o);
            _context.OrderItems.AddRange(items);
            _context.SaveChanges();

            order.Id = o.Id;
            order.OrderItems = o.OrderItems.Select(oi => new OrderItem
            {
                Id = oi.Id,
                Quantity = oi.Quantity,
                Product = new Product
                {
                    Id = oi.ProductId
                    // TODO: name and style.
                }
            }).ToList();
            _logger.LogInformation($"Created order {order.Id}");
            return order;
        }

        [HttpDelete]
        [Route("Order")]
        public void DeleteOrder(int id)
        {
            _logger.LogInformation("Deleting order.");
            var order = new Data.Order() { Id = id };
            _context.Attach(order);
            _context.Remove(order);
            _context.SaveChanges();
        }
    }
}
