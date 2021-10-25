using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using InsAndOuts.Models;
using InsAndOuts.Utilities;
using InsAndOuts.ViewModels;
using Syncfusion.XForms.Buttons;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SelectionChangedEventArgs = Syncfusion.SfPicker.XForms.SelectionChangedEventArgs;

namespace InsAndOuts.Views
{
    [QueryProperty(nameof(ViewMode)
                 , nameof(ViewMode))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealView : ContentPage, IQueryAttributable
    {
        public string          ViewMode        { get; set; }
        public MealsViewModel  ViewModel       { get; set; }
        public SearchViewModel SearchViewModel { get; set; }

        private bool EditMode  { get; set; }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            ViewMode  = HttpUtility.UrlDecode(query[nameof(ViewMode)]);
            
            if (ViewMode != null 
             && ViewMode.Equals("EDIT"
                              , StringComparison.CurrentCultureIgnoreCase))
            {
                EditMode               = true;

                SearchViewModel        = new SearchViewModel();

                SearchPicker.ItemsSource = SearchViewModel.SearchableMeals;
                SearchPicker.Focus();
            }

            SelectToolbarItem.IsEnabled = EditMode;
            DeleteToolbarItem.IsEnabled = EditMode;
            
            ToggleControlsEnabled();

            UpdateViewTitle();
        }

        public MealView()
        {
            //1
            InitializeComponent();
            
        }

        protected override void OnAppearing()
        {
            //3
            base.OnAppearing();
            
            ViewModel = new MealsViewModel();
            
            DescriptionRtf.AlignLeft();
            
            ResetData();

            SearchPicker.IsVisible = EditMode;
        }

        

        private void ResetData()
        {
            NameEntry.Text          = string.Empty;
            DescriptionRtf.Text     = string.Empty;
            DescriptionRtf.HtmlText = string.Empty;
            WhenDatePicker.Date     = DateTime.Today;
            WhenTimePicker.Time     = DateTime.Now.TimeOfDay;
            
            ViewModel              = new MealsViewModel();
            UpdateViewTitle();
        }
        
        private void UpdateViewTitle()
        {
            
            Title = $"{ViewMode.ToTitleCase(force: true)} Meal";
        }

        private string GetSelectDateTimeFromPickers()
        {
            var dateTimeToSave = $"{WhenDatePicker.Date.ToShortDateString()} {WhenTimePicker.Time.ToString("g", CultureInfo.CreateSpecificCulture("en-US"))}";
            return dateTimeToSave;
        }
        
        private void SaveButton_OnClicked(object    sender
                                        , EventArgs e)
        {
            if (NameEntry.Text.Contains("-"))
            {
                DisplayAlert("Invalid character"
                           , $"You cannot have hyphens ( - ) in the name of the meal.{Environment.NewLine}Meal not save."
                           , "OK");
                return;
            }
            ViewModel.Meal.Name                = NameEntry.Text;
            ViewModel.Meal.DescriptionPainText = DescriptionRtf.Text;
            ViewModel.Meal.DescriptionHtml     = DescriptionRtf.HtmlText;
            ViewModel.Meal.When                = GetSelectDateTimeFromPickers();
            
            ViewModel.Save();

            if (EditMode)
            {
                SearchViewModel = new SearchViewModel();

                SearchPicker.ItemsSource = SearchViewModel.SearchableMeals;
            }

            if (sender is SfButton button)
                SetSaveButtonSaved(button);

            ResetData();
        }
        
        private void NameEntry_OnFocused(object         sender
                                       , FocusEventArgs e)
        {
            SetSaveButtonNotSaved();
        }
        
        private void DescriptionRtf_OnFocused(object    sender
                                            , EventArgs e)
        {
            SetSaveButtonNotSaved();
        }

        private void WhenDatePicker_OnFocused(object         sender
                                            , FocusEventArgs e)
        {
            SetSaveButtonNotSaved();
        }

        private void WhenTimePicker_OnFocused(object         sender
                                            , FocusEventArgs e)
        {
            SetSaveButtonNotSaved();
        }

        private void SetSaveButtonNotSaved()
        {
            SaveButton.ShowIcon = false;
            SaveButton.Text     = "SAVE";
        }

        private void SetSaveButtonSaved(SfButton button)
        {
            button.ShowIcon = true;
            button.Text     = "SAVED";
        }
        
        private async void DeleteToolbarItem_OnClicked(object    sender
                                                   , EventArgs e)
        {
            if (! EditMode
             || ViewModel          == null
             || ViewModel.Meal?.Id == 0)
                return;

            ViewModel.Delete();
            await PageNavigation.NavigateBackwards();
        }

        private void SearchPicker_OnOkButtonClicked(object                    sender
                                                  , SelectionChangedEventArgs e)
        {
            //if (SearchPicker.SelectedItem == null
            // || IsLoading)
            //{
            //    IsLoading = false;
            //    return;
            //}

            ViewModel = new MealsViewModel(SearchPicker.SelectedItem.ToString());

            if (ViewModel.Meal == null)
            {
                DisplayAlert("Meal not found"
                           , "Something went wrong while retrieving the meal."
                           , "OK");
                return;
            }

            NameEntry.Text           = ViewModel.Meal.Name;
            DescriptionRtf.HtmlText  = ViewModel.Meal.DescriptionHtml;
            WhenDatePicker.Date      = ViewModel.Meal.WhenToDateTime();
            WhenTimePicker.Time      = ViewModel.Meal.WhenToTimeSpan();

            ToggleControlsEnabled();

            SearchPicker.IsVisible   = false;
        }

        private void SearchPicker_OnCancelButtonClicked(object                    sender
                                                      , SelectionChangedEventArgs e)
        {
            ToggleControlsEnabled();

            SearchPicker.IsVisible = false;
        }

        private void SelectToolbarItem_OnClicked(object    sender
                                                   , EventArgs e)
        {
            ToggleControlsEnabled();

            SearchPicker.IsVisible   = true;
        }

        private void ToggleControlsEnabled()
        {
            SaveButton.IsEnabled     = ! SaveButton.IsEnabled;
            WhenDatePicker.IsEnabled = ! WhenDatePicker.IsEnabled;
            WhenTimePicker.IsEnabled = ! WhenTimePicker.IsEnabled;
            DescriptionRtf.IsEnabled = ! DescriptionRtf.IsEnabled;
            NameEntry.IsEnabled      = ! NameEntry.IsEnabled;
        }
    }

}