using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsAndOuts.ViewModels;
using Syncfusion.SfRangeSlider.XForms;
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
            ViewModel = new PainViewModel();
        }

        private void DescriptionEditor_OnUnfocused(object         sender
                                                 , FocusEventArgs e)
        {
            ViewModel.Pain.DescriptionPainText = DescriptionEditor.Text;
            ViewModel.SavePain();
        }

        private void WhenDatePicker_OnUnfocused(object         sender
                                              , FocusEventArgs e)
        {
            ViewModel.Pain.When = GetSelectDateTimeFromPickers();
            ViewModel.SavePain();
        }

        private void WhenTimePicker_OnUnfocused(object         sender
                                              , FocusEventArgs e)
        {
            ViewModel.Pain.When = GetSelectDateTimeFromPickers();
            ViewModel.SavePain();
        }

        private void PainLevelPicker_OnSelectedIndexChanged(object    sender
                                                          , EventArgs e)
        {
            var picker = sender as Picker ?? new Picker();
            ViewModel.Pain.Level = int.Parse(picker.SelectedItem.ToString());
            ViewModel.SavePain();
        }
        
        //BENDO: Instead of copying method to each view, move it to a shared location
        private string GetSelectDateTimeFromPickers()
        {
            var dateTimeToSave = $"{WhenDatePicker.Date.ToShortDateString()} {WhenTimePicker.Time.ToString("g", CultureInfo.CreateSpecificCulture("en-US"))}";
            return dateTimeToSave;
        }

        private void RangeSlider_OnValueChanging(object         sender
                                               , ValueEventArgs e)
        {
            //var painLevel = e.Value;
            //ViewModel.Pain.Level = (int)e.Value;
            //ViewModel.SavePain();
        }

        private void RangeSlider_OnPropertyChanged(object                   sender
                                                 , PropertyChangedEventArgs e)
        {
            //var evalue = e.PropertyName;

            //if (e.PropertyName == "Value")
            //{
            
            //}
        }

        private void RangeSlider_OnUnfocused(object         sender
                                           , FocusEventArgs e)
        {
            //ViewModel.Pain.Level = (int)RangeSlider.Value;
            //ViewModel.SavePain();
        }
    }
}