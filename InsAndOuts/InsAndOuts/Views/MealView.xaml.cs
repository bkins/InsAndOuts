using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Avails.D_Flat;
using InsAndOuts.Services;
using InsAndOuts.Utilities;
using InsAndOuts.ViewModels;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static InsAndOuts.Utilities.DateTimeFormatter;
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
        
        private const string CURRENT_MODEL = "Meal";

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            ViewMode  = HttpUtility.UrlDecode(query[nameof(ViewMode)]);
            
            if (ViewMode != null 
             && ViewMode.Equals("EDIT"
                              , StringComparison.CurrentCultureIgnoreCase))
            {
                EditMode               = true;
                SearchPicker.IsVisible = true;

                SearchViewModel        = new SearchViewModel();

                SearchPicker.ItemsSource = SearchViewModel.SearchableMeals;
                SearchPicker.Focus();

                ToggleControlsVisible();

            }

            SelectToolbarItem.IsEnabled = EditMode;
            DeleteToolbarItem.IsEnabled = EditMode;
            
            UpdateViewTitle();
        }

        public MealView()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            
            if ( SearchViewModel != null
            && ! SearchViewModel.SearchableMeals.Any())
            {
                await DisplayAlert("Nothing to edit"
                                 , $"There are no {CURRENT_MODEL}s to edit.  Please add {CURRENT_MODEL}s before attempting to edit them."
                                 , "OK");

                await PageNavigation.NavigateBackwards();
            }
            else
            {
                ResetData();

                SearchPicker.IsVisible = EditMode;

                ViewModel = new MealsViewModel();
            
                DescriptionRtf.AlignLeft();
            }
        }
        
        private void ResetData()
        {
            NameEntry.Text          = string.Empty;
            DescriptionRtf.Text     = string.Empty;
            DescriptionRtf.HtmlText = string.Empty;
            WhenDatePicker.Date     = DateTime.Today;
            WhenTimePicker.Time     = DateTime.Now.TimeOfDay;
            
            ViewModel = new MealsViewModel();

            UpdateViewTitle();
        }
        
        private void UpdateViewTitle()
        {
            Title = $"{ViewMode.ToTitleCase(force: true)} {CURRENT_MODEL}";
        }

        private string GetSelectDateTimeFromPickers()
        {
            var dateTimeToSave = $"{WhenDatePicker.Date.ToShortDateString()} {WhenTimePicker.Time.ToString("g", CultureInfo.CreateSpecificCulture("en-US"))}";
            return dateTimeToSave;
        }
        
        private async void SaveButton_OnClicked(object    sender
                                        , EventArgs e)
        {
            try
            {
                if (NameInvalid())
                    return;

                SetViewModelDataFromPage();
                ViewModel.Save();

                ResetSearchViewModel();

                ResetData();

                ResetSaveButton(sender);
            }
            catch (Exception exception)
            {
                await DisplayAlert("Error"
                                 , exception.Message
                                 , "OK");

                Logger.WriteLine($"Error occurred in {nameof(SaveButton_OnClicked)}"
                               , Category.Error
                               , exception);
            }
            
        }

        private void SetViewModelDataFromPage()
        {
            ViewModel.Meal.Name                = NameEntry.Text;
            ViewModel.Meal.DescriptionPlainText = DescriptionRtf.Text;
            ViewModel.Meal.DescriptionHtml     = DescriptionRtf.HtmlText;

            ViewModel.Meal.When = DateTimeTimeSpanForSaving(WhenDatePicker.Date
                                                          , WhenTimePicker.Time);
        }

        private void ResetSaveButton(object sender)
        {
            if (sender is SfButton button)
            {
                SetSaveButtonSaved(button);
            }
        }

        private bool NameInvalid()
        {
            if (NameEntry.Text.Length==0)
            {
                DisplayAlert("Must provide a name"
                           , $"Please give your {CURRENT_MODEL} a name (e.g. Breakfast, Lunch, Dinner, or Snack)."
                           , "OK");

                return true;
            }
            if (NameEntry.Text.Contains("-"))
            {
                DisplayAlert("Invalid character"
                           , $"You cannot have hyphens ( - ) in the name of the {CURRENT_MODEL}.{Environment.NewLine}{CURRENT_MODEL} not save."
                           , "OK");

                return true;
            }

            return false;
        }

        private void ResetSearchViewModel()
        {
            if (EditMode)
            {
                SearchViewModel = new SearchViewModel();

                SearchPicker.ItemsSource = SearchViewModel.SearchableMeals;
            }
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
        
        private void SearchPicker_OnOkButtonClicked(object                    sender
                                                  , SelectionChangedEventArgs e)
        {
            if (FindTextInVewModel(SearchPicker.SelectedItem.ToString()))
                return;

            LoadPageFromViewModel();

            ToggleControlsVisible();

            SearchPicker.IsVisible   = false;
        }

        private void LoadPageFromViewModel()
        {
            NameEntry.Text          = ViewModel.Meal.Name;
            DescriptionRtf.HtmlText = ViewModel.Meal.DescriptionHtml;
            WhenDatePicker.Date     = ViewModel.Meal.WhenToDateTime();
            WhenTimePicker.Time     = ViewModel.Meal.WhenToTimeSpan();
        }

        private bool FindTextInVewModel(string searchText)
        {
            ViewModel = new MealsViewModel(searchText);

            if (ViewModel.Meal != null)
                return false;

            DisplayAlert($"{CURRENT_MODEL} not found"
                       , $"Something went wrong while retrieving the {CURRENT_MODEL}."
                       , "OK");

            return true;

        }

        private async void SearchPicker_OnCancelButtonClicked(object                    sender
                                                      , SelectionChangedEventArgs e)
        {
            await PageNavigation.NavigateBackwards();
        }

        private void SelectToolbarItem_OnClicked(object    sender
                                                   , EventArgs e)
        {
            ToggleControlsVisible();

            SearchPicker.IsVisible   = true;
        }
        
        private async void DeleteToolbarItem_OnClicked(object    sender
                                                     , EventArgs e)
        {
            ViewModel?.Delete();
            UserInteraction.Toast($"{CURRENT_MODEL} Deleted");
            await PageNavigation.NavigateBackwards();
        }

        private void ToggleControlsVisible()
        {
            SaveButton.IsVisible     = ! SaveButton.IsVisible;
            WhenDatePicker.IsVisible = ! WhenDatePicker.IsVisible;
            WhenTimePicker.IsVisible = ! WhenTimePicker.IsVisible;
            DescriptionRtf.IsVisible = ! DescriptionRtf.IsVisible;
            NameEntry.IsVisible      = ! NameEntry.IsVisible;
        }
    }

}