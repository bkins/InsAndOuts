using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsAndOuts.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InsAndOuts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageDataView : ContentPage
    {
        public ManageDataView()
        {
            InitializeComponent();
        }

        private async void EditMeals_OnClicked(object    sender
                                        , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(MealView), nameof(MealView.ViewMode), "EDIT");
        }

        private void EditStools_OnClicked(object    sender
                                         , EventArgs e)
        {
            
        }

        private void EditPains_OnClicked(object    sender
                                        , EventArgs e)
        {
            
        }
    }
}