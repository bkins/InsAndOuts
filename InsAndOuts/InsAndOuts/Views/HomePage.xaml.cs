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
            Configuration.IsEnabled = false;
            await PageNavigation.NavigateTo(nameof(ConfigurationView));
            
            Configuration.IsEnabled = true;
        }
        
        private async void DailyReportCircleButton_OnClicked(object    sender
                                                           , EventArgs e)
        {
            DailyReportCircleButton.IsEnabled = false;
            await PageNavigation.NavigateTo(nameof(DailyReportView));
            
            DailyReportCircleButton.IsEnabled = true;
        }

        private async void ChartsAndReportsCircleButton_OnClicked(object    sender
                                                                , EventArgs e)
        {
            ChartsAndReportsCircleButton.IsEnabled = false;
            await PageNavigation.NavigateTo(nameof(ReportsView));

            ChartsAndReportsCircleButton.IsEnabled = true;
        }

        private async void AboutCircleButton_OnClicked(object    sender
                                                     , EventArgs e)
        {
            AboutCircleButton.IsEnabled = false;
            await PageNavigation.NavigateTo(nameof(AboutView));

            AboutCircleButton.IsEnabled = true;
        }

        private async void AddMealButton_OnClicked(object    sender
                                                 , EventArgs e)
        {
            AddMealButton.IsEnabled = false;
            await PageNavigation.NavigateTo(nameof(MealView)
                                          , nameof(MealView.ViewMode)
                                          , "ADD");

            AddMealButton.IsEnabled = true;
        }

        private async void EditMealButton_OnClicked(object    sender
                                                  , EventArgs e)
        {
            EditMealButton.IsEnabled = false;
            await PageNavigation.NavigateTo(nameof(MealView)
                                          , nameof(MealView.ViewMode)
                                          , "EDIT");

            EditMealButton.IsEnabled = true;
        }

        private async void AddStoolButton_OnClicked(object    sender
                                                  , EventArgs e)
        {
            AddStoolButton.IsEnabled = false;
            await PageNavigation.NavigateTo(nameof(StoolsView)
                                          , nameof(StoolsView.ViewMode)
                                          , "ADD");

            AddStoolButton.IsEnabled = true;
        }

        private async void EditStoolButton_OnClicked(object    sender
                                                   , EventArgs e)
        {
            EditStoolButton.IsEnabled = false;
            await PageNavigation.NavigateTo(nameof(StoolsView)
                                          , nameof(StoolsView.ViewMode)
                                          , "EDIT");

            EditStoolButton.IsEnabled = true;
        }

        private async void AddPainButton_OnClicked(object    sender
                                                 , EventArgs e)
        {
            AddPainButton.IsEnabled = false;
            await PageNavigation.NavigateTo(nameof(PainView)
                                          , nameof(PainView.ViewMode)
                                          , "ADD");

            AddPainButton.IsEnabled = true;
        }

        private async void EditPainButton_OnClicked(object    sender
                                                  , EventArgs e)
        {
            EditPainButton.IsEnabled = false;
            await PageNavigation.NavigateTo(nameof(PainView)
                                          , nameof(PainView.ViewMode)
                                          , "EDIT");

            EditPainButton.IsEnabled = true;
        }
        
    }
}