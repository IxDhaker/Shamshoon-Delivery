using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Shamshoon_delivery.Models;

namespace Shamshoon_delivery.Data
{
    public class OrderDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public OrderDatabase(string dbPath)
        {
            // Initialisation de la connexion SQLite
            _database = new SQLiteAsyncConnection(dbPath);
            // Création de la table Order si elle n'existe pas déjà
            _database.CreateTableAsync<Order>().Wait();

            // Initialiser avec la commande de la maquette si la base est vide
            SeedDefaultOrderAsync().Wait();
        }

        // Récupérer toutes les commandes (Historique)
        public Task<List<Order>> GetOrdersAsync()
        {
            return _database.Table<Order>().ToListAsync();
        }

        // Récupérer une commande par son Id
        public Task<Order> GetOrderAsync(int id)
        {
            return _database.Table<Order>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        // Récupérer la dernière commande active (pour l'écran Suivi)
        public Task<Order> GetActiveOrderAsync()
        {
            return _database.Table<Order>()
                            .OrderByDescending(o => o.Id)
                            .FirstOrDefaultAsync();
        }

        // Insérer ou Mettre à jour une commande
        public Task<int> SaveOrderAsync(Order order)
        {
            if (order.Id != 0)
            {
                return _database.UpdateAsync(order);
            }
            else
            {
                return _database.InsertAsync(order);
            }
        }

        // Supprimer une commande
        public Task<int> DeleteOrderAsync(Order order)
        {
            return _database.DeleteAsync(order);
        }

        // Données initiales
        private async Task SeedDefaultOrderAsync()
        {
            var count = await _database.Table<Order>().CountAsync();
            if (count == 0)
            {
                var defaultOrder = new Order
                {
                    OrderNumber = "#531-9842",
                    RestaurantName = "Le Majestic",
                    StatusCode = 3,                 // 3 = "En cours" (kima fel maquette)
                    StatusText = "En cours",
                    EstimatedTime = "12 min",
                    DriverName = "Shamshoon (Livreur)",
                    DriverRating = 5.0,
                    DriverPhone = "+216 22 123 456",
                    TotalPrice = 34.500,
                    OrderDate = DateTime.Now
                };
                await _database.InsertAsync(defaultOrder);
            }
        }
    }
}