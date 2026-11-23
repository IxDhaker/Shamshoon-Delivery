using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Shamshoon_delivery.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Shamshoon_delivery.ViewModels
{
    public class OrderTrackingViewModel : BaseViewModel
    {
        private Order currentOrder;
        public Order CurrentOrder
        {
            get => currentOrder;
            set => SetProperty(ref currentOrder, value);
        }

        // Propriétés de contrôle du Stepper visuel (les étapes vertes ou grises)
        public bool IsStep1Completed => CurrentOrder != null && CurrentOrder.StatusCode >= 1; // Validée
        public bool IsStep2Completed => CurrentOrder != null && CurrentOrder.StatusCode >= 2; // En préparation
        public bool IsStep3Completed => CurrentOrder != null && CurrentOrder.StatusCode >= 3; // En cours
        public bool IsStep4Completed => CurrentOrder != null && CurrentOrder.StatusCode >= 4; // Livré

        // Couleurs des lignes du stepper
        public Color Line1Color => IsStep2Completed ? Color.FromHex("#10B981") : Color.FromHex("#E5E7EB");
        public Color Line2Color => IsStep3Completed ? Color.FromHex("#10B981") : Color.FromHex("#E5E7EB");
        public Color Line3Color => IsStep4Completed ? Color.FromHex("#10B981") : Color.FromHex("#E5E7EB");

        // Commandes
        public ICommand LoadOrderCommand { get; }
        public ICommand CallDriverCommand { get; }
        public ICommand MessageDriverCommand { get; }
        public ICommand AdvanceStatusCommand { get; }
        public ICommand BackCommand { get; }

        public OrderTrackingViewModel()
        {
            Title = "Suivi de Commande";

            LoadOrderCommand = new Command(async () => await LoadOrderFromDbAsync());
            CallDriverCommand = new Command(async () => await OnCallDriver());
            MessageDriverCommand = new Command(async () => await OnMessageDriver());
            AdvanceStatusCommand = new Command(async () => await OnAdvanceStatus());
            BackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        // Charger la commande depuis SQLite (kima TP 3)
        public async Task LoadOrderFromDbAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                // Appel direct à notre base SQLite locale
                var order = await App.Database.GetActiveOrderAsync();
                CurrentOrder = order;
                NotifyStepperChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur chargement commande: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Permet d'avancer le statut pour la démonstration devant la prof (Validée -> Préparation -> En cours -> Livré)
        private async Task OnAdvanceStatus()
        {
            if (CurrentOrder == null) return;

            if (CurrentOrder.StatusCode < 4)
                CurrentOrder.StatusCode++;
            else
                CurrentOrder.StatusCode = 1; // Recommencer la boucle

            switch (CurrentOrder.StatusCode)
            {
                case 1:
                    CurrentOrder.StatusText = "Validée";
                    CurrentOrder.EstimatedTime = "30 min";
                    break;
                case 2:
                    CurrentOrder.StatusText = "En préparation";
                    CurrentOrder.EstimatedTime = "20 min";
                    break;
                case 3:
                    CurrentOrder.StatusText = "En cours";
                    CurrentOrder.EstimatedTime = "12 min";
                    break;
                case 4:
                    CurrentOrder.StatusText = "Livré";
                    CurrentOrder.EstimatedTime = "Arrivé !";
                    break;
            }

            // Sauvegarder la mise à jour dans SQLite (kima TP 3)
            await App.Database.SaveOrderAsync(CurrentOrder);
            NotifyStepperChanges();
        }

        private void NotifyStepperChanges()
        {
            OnPropertyChanged(nameof(CurrentOrder));
            OnPropertyChanged(nameof(IsStep1Completed));
            OnPropertyChanged(nameof(IsStep2Completed));
            OnPropertyChanged(nameof(IsStep3Completed));
            OnPropertyChanged(nameof(IsStep4Completed));
            OnPropertyChanged(nameof(Line1Color));
            OnPropertyChanged(nameof(Line2Color));
            OnPropertyChanged(nameof(Line3Color));
        }

        private async Task OnCallDriver()
        {
            if (CurrentOrder != null && !string.IsNullOrWhiteSpace(CurrentOrder.DriverPhone))
            {
                try
                {
                    PhoneDialer.Open(CurrentOrder.DriverPhone);
                }
                catch
                {
                    await Application.Current.MainPage.DisplayAlert("Appel", $"Appel vers Shamshoon: {CurrentOrder.DriverPhone}", "OK");
                }
            }
        }

        private async Task OnMessageDriver()
        {
            await Application.Current.MainPage.DisplayAlert("Message", "Discussion avec le livreur Shamshoon...", "OK");
        }
    }
}