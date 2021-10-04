using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsAndOuts.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InsAndOuts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchView : ContentPage
    {
        private SearchViewModel ViewModel { get; } = new SearchViewModel();

        public SearchView()
        {
            InitializeComponent();

            SearchPicker.ItemsSource = ViewModel.SearchableMeals;
        }

        private void OkButton_OnClicked(object    sender
                                      , EventArgs e)
        {
            
        }
    }
}