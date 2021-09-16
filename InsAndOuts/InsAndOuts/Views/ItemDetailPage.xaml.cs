using InsAndOuts.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace InsAndOuts.Views
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