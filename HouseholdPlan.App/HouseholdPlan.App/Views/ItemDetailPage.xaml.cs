using HouseholdPlan.App.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace HouseholdPlan.App.Views
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