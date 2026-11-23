using Shamshoon_delivery.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Shamshoon_delivery.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}