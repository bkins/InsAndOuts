using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using InsAndOuts.Services;
using InsAndOuts.Utilities;
using InsAndOuts.ViewModels;
using Syncfusion.SfRangeSlider.XForms;
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
    public partial class PainView : ContentPage, IQueryAttributable
    {
        public  string          ViewMode          { get; set; }
        public  PainViewModel   ViewModel         { get; set; }
        public  SearchViewModel SearchViewModel   { get; set; }
        
        private bool EditMode { get; set; }
        
        private const string CURRENT_MODEL = "Pain";

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            ViewMode = HttpUtility.UrlDecode(query[nameof(ViewMode)]);
            
            if (ViewMode != null 
             && ViewMode.Equals("EDIT"
                              , StringComparison.CurrentCultureIgnoreCase))
            {
                EditMode               = true;
                SearchPicker.IsVisible = true;

                SearchViewModel = new SearchViewModel();

                SearchPicker.ItemsSource = SearchViewModel.SearchablePains;
                SearchPicker.Focus();

                ToggleControlsVisible();
            }

            SelectToolbarItem.IsEnabled = EditMode;
            DeleteToolbarItem.IsEnabled = EditMode;
            
            UpdateViewTitle();
        }

        public PainView()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if ( SearchViewModel != null
              && ! SearchViewModel.SearchablePains.Any())
            {
                await DisplayAlert("Nothing to edit"
                                 , $"There are no {CURRENT_MODEL}s to edit.  Please add {CURRENT_MODEL}s before attempting to edit them."
                                 , "OK").ConfigureAwait(false);

                await PageNavigation.NavigateBackwards().ConfigureAwait(false);
            }
            else
            {
                ResetData();
                SearchPicker.IsVisible = EditMode;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void ResetData()
        {
            DescriptionHtmlRtEditor.Text = string.Empty;
            RangeSlider.Value            = 0;
            WhenDatePicker.Date          = DateTime.Today;
            WhenTimePicker.Time          = DateTime.Now.TimeOfDay;
            
            SetSaveButtonNotSaved();

            ViewModel = new PainViewModel();

            UpdateViewTitle();
        }
        
        private void UpdateViewTitle()
        {
            Title = $"{ViewMode.ToTitleCase(force: true)} {CURRENT_MODEL}";
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
        
        private void RangeSlider_OnThumbTouchUp(object             sender
                                              , DragThumbEventArgs e)
        {
            RangeSlider.Focus();
            ViewModel.Pain.Level = (int)RangeSlider.Value;
        }
        
        private void DescriptionHtmlRtEditor_OnFocused(object    sender
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

        private void RangeSlider_OnThumbTouchDown(object             sender
                                                , DragThumbEventArgs e)
        {
            SetSaveButtonNotSaved();
        }

        private void SaveButton_OnClicked(object    sender
                                        , EventArgs e)
        {
            SetViewModelDataFromPage();
            ViewModel.Save();

            ResetSearchViewModel();

            ResetData();

            ResetSaveButton(sender);
        }

        private void ResetSaveButton(object sender)
        {
            if (sender is SfButton button)
            {
                SetSaveButtonSaved(button);
            }
        }

        private void ResetSearchViewModel()
        {
            if ( ! EditMode)
                return;

            SearchViewModel = new SearchViewModel();

            SearchPicker.ItemsSource = SearchViewModel.SearchableStools;
        }

        private void SetViewModelDataFromPage()
        {
            ViewModel.Pain.When = DateTimeTimeSpanForSaving(WhenDatePicker.Date
                                                          , WhenTimePicker.Time);

            ViewModel.Pain.DescriptionPainText = DescriptionHtmlRtEditor.Text;
            ViewModel.Pain.DescriptionHtml     = DescriptionHtmlRtEditor.HtmlText;
            
            if (ViewModel.Pain.DescriptionPainText.IsNullEmptyOrWhitespace()
             && ViewModel.Pain.DescriptionHtml.HasValue())
            {
                ViewModel.Pain.DescriptionPainText = DescriptionHtmlRtEditor.Text;
            }
        }

        private void SearchPicker_OnOkButtonClicked(object                    sender
                                                  , SelectionChangedEventArgs e)
        {
            if (FindTextInVewModel(SearchPicker.SelectedItem.ToString()))
                return;
            
            LoadPageFromViewModel();

            ToggleControlsVisible();

            SearchPicker.IsVisible = false;
        }

        private void LoadPageFromViewModel()
        {
            DescriptionHtmlRtEditor.HtmlText = ViewModel.Pain.DescriptionHtml;
            RangeSlider.Value                = ViewModel.Pain.Level;
            WhenDatePicker.Date              = ViewModel.Pain.WhenToDateTime();
            WhenTimePicker.Time              = ViewModel.Pain.WhenToTimeSpan();
        }

        private bool FindTextInVewModel(string searchText)
        {
            ViewModel = new PainViewModel(searchText);

            if (ViewModel.Pain != null)
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

            SearchPicker.IsVisible = true;
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
            SaveButton.IsVisible              = ! SaveButton.IsVisible;
            WhenDatePicker.IsVisible          = ! WhenDatePicker.IsVisible;
            WhenTimePicker.IsVisible          = ! WhenTimePicker.IsVisible;
            DescriptionHtmlRtEditor.IsVisible = ! DescriptionHtmlRtEditor.IsVisible;
            RangeSlider.IsVisible             = ! RangeSlider.IsVisible;
        }
    }
}