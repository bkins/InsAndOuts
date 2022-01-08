using System;
using System.Linq;
using System.Runtime.CompilerServices;
using InsAndOuts.Models;
using InsAndOuts.Utilities;
using InsAndOuts.ViewModels;
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
            
            AssignMissingSymptomTypes();
        }

        private void AssignMissingSymptomTypes()
        {
            var symptomTypeVm = new SymptomsViewModel();

            if (symptomTypeVm.Pains.All(fields => fields.Type != null))
                return;
        
            DisplayAlert("New Symptom Types"
                       , "As of this new version, Symptoms replaced Pains and Symptom Types are required. All existing Symptoms/Pains that do not have a Symptom Type will be assigned the Symptom Type \"Pain\""
                       , "OK");

            AddPainSymptomType(symptomTypeVm);

            AddPainSymptomTypeToAllPainsWhereMissing(symptomTypeVm);
        
        }

        private static void AddPainSymptomTypeToAllPainsWhereMissing(SymptomsViewModel symptomTypeVm)
        {
            var painSymptomType = symptomTypeVm.SymptomTypes.FirstOrDefault(fields => fields.Name == "Pain");

            if (painSymptomType==null)
            {
                return;
            }

            foreach (var pain in symptomTypeVm.Pains
                                              .Where(pain => pain.Type == null))
            {
                pain.TypeId        = painSymptomType.Id;
                pain.Type          = painSymptomType;
                symptomTypeVm.Pain = pain;
                
                symptomTypeVm.Save();
            }
        }

        private static void AddPainSymptomType(SymptomsViewModel symptomTypeVm)
        {
            if (symptomTypeVm.SymptomTypes.All(fields => fields.Name != "Pain"))
            {
                symptomTypeVm.AddNewSymptomType("Pain");
            }
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

        private async void AddSymptomButton_OnClicked(object    sender
                                                 , EventArgs e)
        {
            AddSymptomButton.IsEnabled = false;
            await PageNavigation.NavigateTo(nameof(SymptomsView)
                                          , nameof(SymptomsView.ViewMode)
                                          , "ADD");

            AddSymptomButton.IsEnabled = true;
        }

        private async void EditSymptomButton_OnClicked(object    sender
                                                  , EventArgs e)
        {
            EditSymptomButton.IsEnabled = false;
            await PageNavigation.NavigateTo(nameof(SymptomsView)
                                          , nameof(SymptomsView.ViewMode)
                                          , "EDIT");

            EditSymptomButton.IsEnabled = true;
        }
        
    }
}