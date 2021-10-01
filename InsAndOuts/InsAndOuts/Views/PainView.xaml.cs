using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsAndOuts.Utilities;
using InsAndOuts.ViewModels;
using Syncfusion.SfRangeSlider.XForms;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InsAndOuts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PainView : ContentPage
    {
        public PainViewModel ViewModel { get; set; }

        public PainView()
        {
            InitializeComponent();
            ResetData();

            DescriptionHtmlRtEditor.AlignLeft();

            DescriptionHtmlRtEditor.IsVisible    = Configuration.UseHtmlForEmailBody;
            DescriptionPlainTextEditor.IsVisible = ! DescriptionHtmlRtEditor.IsVisible;

        }
        
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            ResetData();
        }

        private void ResetData()
        {
            DescriptionPlainTextEditor.Text = string.Empty;
            WhenDatePicker.Date             = DateTime.Today;
            WhenTimePicker.Time             = DateTime.Now.TimeOfDay;
            
            SetSaveButtonNotSaved();

            ViewModel = new PainViewModel();

            UpdateViewTitle();
        }
        
        private void UpdateViewTitle()
        {
            //For now this will always be "Add New"
            //However, the idea will be that I will add feature to update the stools and this will be the view I will use.
            //BENDO: On release to user(s) remove the ID for the Title
            var addUpdate = ViewModel.Pain.Id == 0 ?
                                    "Add New" :
                                    "Update";

            Title = $"{addUpdate} Stool ({ViewModel.Pain.Id})";
        }

        private void SetSaveButtonNotSaved()
        {
            SaveButton.ShowIcon      = false;
            SaveButton.Text          = "SAVE";
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
        
        private void WhenDatePicker_OnUnfocused(object         sender
                                              , FocusEventArgs e)
        {
            ViewModel.Pain.When = GetSelectDateTimeFromPickers();
        }

        private void WhenTimePicker_OnUnfocused(object         sender
                                              , FocusEventArgs e)
        {
            ViewModel.Pain.When = GetSelectDateTimeFromPickers();
        }

        private void DescriptionPlainTextEditor_OnUnfocused(object         sender
                                                          , FocusEventArgs e)
        {
            ViewModel.Pain.DescriptionPainText = DescriptionPlainTextEditor.Text;
        }

        private void DescriptionHtmlRtEditor_OnUnfocused(object    sender
                                                       , EventArgs e)
        {
            ViewModel.Pain.DescriptionHtml = DescriptionHtmlRtEditor.HtmlText;
        }
        
        private void DescriptionPlainTextEditor_OnFocused(object         sender
                                                        , FocusEventArgs e)
        {
            SetSaveButtonNotSaved();
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
            //The RichTextEditor doesn't seem to lose focus when going from it to the slider.
            //So the description is never set to the Pain object in the viewmodel
            //Unless you select either the Date or Time pickers
            ViewModel.Pain.DescriptionHtml = DescriptionHtmlRtEditor.HtmlText;
            
            //BENDO: Since I am not implementing silent save in this app, would it make more sense to not have all the assignments in the
            //Unfocused events, and simply assign the values here and then call ViewModel.Save()???
            ViewModel.Save();
            
            UpdateViewTitle();

            //BENDO: Do I need this 'if'?  Seems pointless
            if (sender is SfButton button)
            {
                SetSaveButtonSaved(button);
            }
        }
    }
}