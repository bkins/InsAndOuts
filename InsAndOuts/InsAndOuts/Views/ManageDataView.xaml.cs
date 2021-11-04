using System;
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
            await PageNavigation.NavigateTo(nameof(MealView)
                                          , nameof(MealView.ViewMode)
                                          , "EDIT");
        }

        private async void EditStools_OnClicked(object    sender
                                              , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(StoolsView)
                                          , nameof(StoolsView.ViewMode)
                                          , "EDIT");
        }

        private async void EditPains_OnClicked(object    sender
                                             , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(PainView)
                                          , nameof(PainView.ViewMode)
                                          , "EDIT");
        }
    }
}