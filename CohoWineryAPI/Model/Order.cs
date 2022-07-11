using System.Collections.Generic;

namespace CohoWineryAPI.Model
{
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

        public List<OrderItem> OrderItems { get; set;  } = new();
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}
