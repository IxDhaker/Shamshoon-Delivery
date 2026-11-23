using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Shamshoon_delivery.Models;
using Xamarin.Forms;

namespace Shamshoon_delivery.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        // 1. Collections observées (kima TP MAUI) pour le Data Binding
        public ObservableCollection<Category> Categories { get; }
        public ObservableCollection<Restaurant> PopularRestaurants { get; }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set => SetProperty(ref searchText, value);
        }

        // 2. Les Commandes (ICommand)
        public ICommand BannerOrderCommand { get; }
        public ICommand RestaurantSelectedCommand { get; }

        public HomeViewModel()
        {
            Title = "Shamshoon Delivery";

            Categories = new ObservableCollection<Category>();
            PopularRestaurants = new ObservableCollection<Restaurant>();

            // Navigation vers le Suivi de commande ki yenzel 3al bannière "Commander"
            BannerOrderCommand = new Command(async () => await GoToTrackingPage());
            RestaurantSelectedCommand = new Command<Restaurant>(async (resto) => await OnRestaurantSelected(resto));

            // Charger les données de la maquette
            LoadData();
        }

        private void LoadData()
        {
            // Les 6 Catégories de la maquette
            Categories.Clear();
            Categories.Add(new Category { Id = 1, Title = "Restaurants", Icon = "🍴" });
            Categories.Add(new Category { Id = 2, Title = "Épicerie", Icon = "🛒" });
            Categories.Add(new Category { Id = 3, Title = "Fast Food", Icon = "🍔" });
            Categories.Add(new Category { Id = 4, Title = "Pharmacie", Icon = "💊" });
            Categories.Add(new Category { Id = 5, Title = "Dessert", Icon = "🧁" });
            Categories.Add(new Category { Id = 6, Title = "Coursier", Icon = "🛵" });

            // Les Restaurants Populaires de la maquette
            PopularRestaurants.Clear();
            PopularRestaurants.Add(new Restaurant
            {
                Id = 1,
                Name = "Le Majestic",
                Rating = 4.8,
                DeliveryTime = "20-30 min",
                ImageUrl = "https://images.unsplash.com/photo-1517248135467-4c7edcad34c4?w=500"
            });
            PopularRestaurants.Add(new Restaurant
            {
                Id = 2,
                Name = "Shamshoon Burger",
                Rating = 4.9,
                DeliveryTime = "15-25 min",
                ImageUrl = "https://images.unsplash.com/photo-1568901346375-23c9450c58cd?w=500"
            });
            PopularRestaurants.Add(new Restaurant
            {
                Id = 3,
                Name = "Pasta & Co",
                Rating = 4.7,
                DeliveryTime = "25-35 min",
                ImageUrl = "https://images.unsplash.com/photo-1551183053-bf91a1d81141?w=500"
            });
        }

        private async Task GoToTrackingPage()
        {
            // Navigation Shell vers la page de suivi (route "tracking")
            await Shell.Current.GoToAsync("tracking");
        }

        private async Task OnRestaurantSelected(Restaurant resto)
        {
            if (resto == null) return;
            // Ki yenzel 3la restaurant, yhezou lel suivi de commande
            await Shell.Current.GoToAsync("tracking");
        }
    }
}