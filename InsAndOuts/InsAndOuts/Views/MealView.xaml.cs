using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using InsAndOuts.Models;
using InsAndOuts.Utilities;
using InsAndOuts.ViewModels;
using Syncfusion.XForms.Buttons;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InsAndOuts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealView : ContentPage
    {
        public MealsViewModel ViewModel { get; set; }

        public MealView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            ViewModel = new MealsViewModel();
            
            DescriptionRtf.AlignLeft();

            DescriptionRtf.IsVisible    = Configuration.UseHtmlForEmailBody;
            DescriptionEditor.IsVisible = ! DescriptionRtf.IsVisible;

            ResetData();
        }

        private void ResetData()
        {
            NameEntry.Text          = string.Empty;
            DescriptionEditor.Text  = string.Empty;
            DescriptionRtf.Text     = string.Empty;
            DescriptionRtf.HtmlText = string.Empty;
            WhenDatePicker.Date     = DateTime.Today;
            WhenTimePicker.Time     = DateTime.Now.TimeOfDay;
            
            ViewModel              = new MealsViewModel();
        }

        private void NameEntry_OnUnfocused(object         sender
                                         , FocusEventArgs e)
        {
            ViewModel.Meal.Name = NameEntry.Text;
        }
        
        private void DescriptionEditor_OnUnfocused(object         sender
                                                 , FocusEventArgs e)
        {
            ViewModel.Meal.DescriptionPainText = DescriptionEditor.Text;

            if (DescriptionEditor.Text != DescriptionRtf.Text)
            {
                DescriptionRtf.Text     = DescriptionEditor.Text;
                DescriptionRtf.HtmlText = DescriptionEditor.Text.Replace(Environment.NewLine
                                                                       , "<br>");

                ViewModel.Meal.DescriptionPainText = DescriptionEditor.Text;
                ViewModel.Meal.DescriptionHtml     = DescriptionRtf.HtmlText;

            }
        }

        private void DescriptionRtf_OnUnfocused(object    sender
                                              , EventArgs e)
        {
            ViewModel.Meal.DescriptionPainText = DescriptionRtf.Text;
            ViewModel.Meal.DescriptionHtml     = DescriptionRtf.HtmlText;
        }
        private void WhenDatePicker_OnUnfocused(object         sender
                                                  , FocusEventArgs e)
        {
            ViewModel.Meal.When = GetSelectDateTimeFromPickers();
        }

        private void WhenTimePicker_OnUnfocused(object         sender
                                              , FocusEventArgs e)
        {
            ViewModel.Meal.When = GetSelectDateTimeFromPickers();
        }

        private string GetSelectDateTimeFromPickers()
        {
            var dateTimeToSave = $"{WhenDatePicker.Date.ToShortDateString()} {WhenTimePicker.Time.ToString("g", CultureInfo.CreateSpecificCulture("en-US"))}";
            return dateTimeToSave;
        }
        
        private void SaveButton_OnClicked(object    sender
                                        , EventArgs e)
        {
            ViewModel.SaveMeal();

            if (sender is SfButton button)
                SetSaveButtonSaved(button);

            ResetData();
        }

        private void NameEntry_OnFocused(object         sender
                                       , FocusEventArgs e)
        {
            SetSaveButtonNotSaved();
        }

        private void DescriptionEditor_OnFocused(object         sender
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
    }

}