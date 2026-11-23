using System;

namespace Shamshoon_delivery.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public string DeliveryTime { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
    }
}