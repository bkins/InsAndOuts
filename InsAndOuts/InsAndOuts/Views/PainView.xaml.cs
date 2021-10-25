using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using InsAndOuts.Utilities;
using InsAndOuts.ViewModels;
using Syncfusion.SfRangeSlider.XForms;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SelectionChangedEventArgs = Syncfusion.SfPicker.XForms.SelectionChangedEventArgs;

namespace InsAndOuts.Views
{
    [QueryProperty(nameof(ViewMode)
                 , nameof(ViewMode))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PainView : ContentPage, IQueryAttributable
    {
        public  string          ViewMode          { get; set; }
        public  SearchViewModel SearchViewModel   { get; set; }
        public  PainViewModel   ViewModel         { get; set; }
        
        private bool EditMode { get; set; }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            ViewMode = HttpUtility.UrlDecode(query[nameof(ViewMode)]);
            
            if (ViewMode != null 
             && ViewMode.Equals("EDIT"
                              , StringComparison.CurrentCultureIgnoreCase))
            {
                EditMode = true;

                SearchPicker.IsVisible = true;
                SearchViewModel        = new SearchViewModel();

                SearchPicker.ItemsSource = SearchViewModel.SearchablePains;
                SearchPicker.Focus();
            }
            
            SelectToolbarItem.IsEnabled = EditMode;
            DeleteToolbarItem.IsEnabled = EditMode;
            
            ToggleControlsEnabled();

            UpdateViewTitle();
        }

        public PainView()
        {
            InitializeComponent();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            SearchPicker.IsVisible = EditMode;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            ResetData();
        }

        private void ResetData()
        {
            WhenDatePicker.Date             = DateTime.Today;
            WhenTimePicker.Time             = DateTime.Now.TimeOfDay;
            
            SetSaveButtonNotSaved();

            ViewModel = new PainViewModel();

            UpdateViewTitle();
        }
        
        private void UpdateViewTitle()
        {
            Title = $"{ViewMode.ToTitleCase(force: true)} Pain";
        }

        private void SetSaveButtonNotSaved()
        {
            if (NoRecordsChanged())
                return;

            SaveButton.ShowIcon = false;
            SaveButton.Text     = "SAVE";
        }

        private bool NoRecordsChanged()
        {
            return DescriptionHtmlRtEditor.HtmlText == null
                || ViewModel.Pain.DescriptionHtml   == DescriptionHtmlRtEditor.HtmlText
                && ViewModel.Pain.Level             == (int)RangeSlider.Value
                && ViewModel.Pain.When              == GetSelectDateTimeFromPickers();
        }

        private void SetSaveButtonSaved(SfButton button)
        {
            button.ShowIcon = true;
            button.Text     = "SAVED";
        }

        //BENDO: Instead of copying method to each view, move it to a shared location
        private string GetSelectDateTimeFromPickers()
        {
            var dateTimeToSave = $"{WhenDatePicker.Date.ToShortDateString()} {WhenTimePicker.Time.ToString("g", CultureInfo.CreateSpecificCulture("en-US"))}";
            return dateTimeToSave;
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
            ViewModel.Pain.When                = GetSelectDateTimeFromPickers();
            ViewModel.Pain.DescriptionPainText = DescriptionHtmlRtEditor.Text;
            ViewModel.Pain.DescriptionHtml     = DescriptionHtmlRtEditor.HtmlText;

            if (ViewModel.Pain.DescriptionPainText.IsNullEmptyOrWhitespace()
            && ViewModel.Pain.DescriptionHtml.HasValue())
            {
                ViewModel.Pain.DescriptionPainText = DescriptionHtmlRtEditor.Text;
            }

            ViewModel.Save();
            
            if (sender is SfButton button)
            {
                SetSaveButtonSaved(button);
            }
            
            ResetData();
        }
        
        private void SearchPicker_OnOkButtonClicked(object                    sender
                                                  , SelectionChangedEventArgs e)
        {
            ViewModel = new PainViewModel(SearchPicker.SelectedItem.ToString());

            if (ViewModel.Pain == null)
            {
                DisplayAlert("Pain not found"
                           , "Something went wrong while retrieving the pain."
                           , "OK");
                return;
            }
            
            DescriptionHtmlRtEditor.HtmlText = ViewModel.Pain.DescriptionHtml;
            RangeSlider.Value                = ViewModel.Pain.Level;
            WhenDatePicker.Date              = ViewModel.Pain.WhenToDateTime();
            WhenTimePicker.Time              = ViewModel.Pain.WhenToTimeSpan();
            
            ToggleControlsEnabled();

            SearchPicker.IsVisible = false;
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

            SearchPicker.IsVisible = true;
        }

        private async void DeleteToolbarItem_OnClicked(object    sender
                                                   , EventArgs e)
        {
            if (! EditMode
             || ViewModel          == null
             || ViewModel.Pain?.Id == 0)
                return;

            ViewModel.Delete();
            await PageNavigation.NavigateBackwards();
        }
        
        private void ToggleControlsEnabled()
        {
            SaveButton.IsEnabled              = ! SaveButton.IsEnabled;
            WhenDatePicker.IsEnabled          = ! WhenDatePicker.IsEnabled;
            WhenTimePicker.IsEnabled          = ! WhenTimePicker.IsEnabled;
            DescriptionHtmlRtEditor.IsEnabled = ! DescriptionHtmlRtEditor.IsEnabled;
        }
    }
}