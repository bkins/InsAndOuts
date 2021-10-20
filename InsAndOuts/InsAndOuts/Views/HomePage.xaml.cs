using System;
using InsAndOuts.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InsAndOuts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            
            Title = @"Is & Os & Ooohs!!";
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BackgroundImageSource                    = Utilities.Configuration.BackgroundImage;
            Application.Current.Resources["Primary"] = Application.Current.Resources[Utilities.Configuration.ZoeColor];
            
        } 
        
        private async void Configuration_OnClicked(object    sender
                                                 , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(ConfigurationView));
        }
        
        private async void DailyReportCircleButton_OnClicked(object    sender
                                                           , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(DailyReportView));
        }

        private async void ChartsAndReportsCircleButton_OnClicked(object    sender
                                                                , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(ReportsView));
        }

        private async void AboutCircleButton_OnClicked(object    sender
                                                     , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(AboutView));
        }

        private async void AddMealButton_OnClicked(object    sender
                                                 , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(MealView)
                                          , nameof(MealView.ViewMode)
                                          , "ADD");
        }

        private async void EditMealButton_OnClicked(object    sender
                                                  , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(MealView)
                                          , nameof(MealView.ViewMode)
                                          , "EDIT");
        }

        private async void AddStoolButton_OnClicked(object    sender
                                                  , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(StoolsView)
                                          , nameof(StoolsView.ViewMode)
                                          , "ADD");
        }

        private async void EditStoolButton_OnClicked(object    sender
                                                   , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(StoolsView)
                                          , nameof(StoolsView.ViewMode)
                                          , "EDIT");
        }

        private async void AddPainButton_OnClicked(object    sender
                                                 , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(PainView)
                                          , nameof(PainView.ViewMode)
                                          , "ADD");
        }

        private async void EditPainButton_OnClicked(object    sender
                                                  , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(PainView)
                                          , nameof(PainView.ViewMode)
                                          , "EDIT");
        }
        
    }
}