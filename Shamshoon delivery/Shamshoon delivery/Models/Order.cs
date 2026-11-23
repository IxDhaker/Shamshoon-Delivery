using System;
using SQLite;

namespace Shamshoon_delivery.Models
{
    public class Order
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string OrderNumber { get; set; }
        public string RestaurantName { get; set; }

        // Statut de la commande :
        // 1 = Validée, 2 = En préparation, 3 = En cours, 4 = Livré
        public int StatusCode { get; set; }
        public string StatusText { get; set; }       // Ex: "En cours"

        public string EstimatedTime { get; set; }

        // Données du Livreur (Shamshoon)
        public string DriverName { get; set; }
        public double DriverRating { get; set; } 
        public string DriverPhone { get; set; }
        public string DriverPhotoUrl { get; set; }

        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}