using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CohoWineryAPI
{
    public class VineyardContext : DbContext
    {
        public VineyardContext(DbContextOptions<VineyardContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Shimmer Shugart", Style = "Sparkling" },
                new Product { Id = 2, Name = "Snowfall", Style = "Light Bodied White" },
                new Product { Id = 3, Name = "Sturm Chardonnay", Style = "Full Bodied White" },
                new Product { Id = 4, Name = "Muscat Blanc", Style = "Aromatic White" },
                new Product { Id = 5, Name = "Doe Creek Rose", Style = "Rose" },
                new Product { Id = 6, Name = "Oregon Pinot Noir", Style = "Light Bodied Red" },
                new Product { Id = 7, Name = "Grumps Nebbiolo", Style = "Medium Bodied Red" },
                new Product { Id = 8, Name = "Texan Sunset Syrah", Style = "Full Bodied Red" },
                new Product { Id = 9, Name = "Raspberry Honey", Style = "Dessert" },
                new Product { Id = 10, Name = "Whitten's Tawny Port", Style = "Fortified" }
                );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1000, CustomerId = 52, DeliveryAddress = "900 Fibrakam Way, San Saba, TX 76877" });

            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 100, OrderId = 1000, ProductId = 4, Quantity = 1 },
                new OrderItem { Id = 101, OrderId = 1000, ProductId = 8, Quantity = 4 },
                new OrderItem { Id = 102, OrderId = 1000, ProductId = 1, Quantity = 2 }
                );
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        
        public string DeliveryAddress { get; set; }
        //Address Lines 1-4
        //Locality
        //Region
        //Postcode(or zipcode)
        //Country

        public List<OrderItem> OrderItems { get; } = new();
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
