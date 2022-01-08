using RestaurantSystemMobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace RestaurantSystemMobile.Views
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