using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsAndOuts.Models;
using InsAndOuts.ViewModels;
using Syncfusion.SfChart.XForms;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pain = InsAndOuts.Models.Pain;

namespace InsAndOuts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportsView : ContentPage
    {
        private const string PAIN_PIE_CHART_TITLE            = "Pain Level Percentages";
        private const string STOOL_TYPE_PIE_CHART_TITLE      = "Stool Type Percentages";
        private const string NUMBER_OF_STOOLS_BY_DAY_OF_WEEK = "Number of Stools by Day Of Week";

        public ReportsView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = new ReportPainStoolViewModel();
            
            SearchPicker.ItemsSource = viewModel.AvailableDateStrings;
            PieChart.BindingContext  = viewModel;

            PainAndStoolRadioButton.IsChecked = true;
        }

        private void SearchPicker_OnSelectedIndexChanged(object    sender
                                                       , EventArgs e)
        {
            var picker    = sender as Picker ?? new Picker();

            var viewModel = new ReportPainStoolViewModel(picker.SelectedItem.ToString());

            PainAndStoolChart.BindingContext = viewModel;
        }

        private void PainAndStoolRadioButton_OnStateChanged(object                sender
                                                          , StateChangedEventArgs e)
        {
            var isChecked = e.IsChecked.HasValue 
                         && e.IsChecked.Value;

            if ( ! isChecked)
            {
                return;
            }

            PainAndStoolRadioButton.IsChecked         = true;
            PainPercentagesRadioButton.IsChecked      = false;
            StoolTypePercentagesRadioButton.IsChecked = false;
            StoolsByDayOfWeekRadioButton.IsChecked    = false;
            StoolsByHourGroupRadioButton.IsChecked    = false;
            
            SearchPicker.IsVisible           = true;
            PainAndStoolChart.IsVisible      = true;
            PieChart.IsVisible               = false;
            StoolsByDayOfWeekChart.IsVisible = false;
            StoolsByHourGroupChart.IsVisible = false;

            var viewModel = new ReportPainStoolViewModel();
            
            PainAndStoolChart.BindingContext = viewModel;
            
        }

        private void PainPercentagesRadioButton_OnStateChanged(object                sender
                                                             , StateChangedEventArgs e)
        {
            var isChecked = e.IsChecked.HasValue 
                         && e.IsChecked.Value;

            if ( ! isChecked)
            {
                return;
            }
            
            PainAndStoolRadioButton.IsChecked         = false;
            PainPercentagesRadioButton.IsChecked      = true;
            StoolTypePercentagesRadioButton.IsChecked = false;
            StoolsByDayOfWeekRadioButton.IsChecked    = false;
            StoolsByHourGroupRadioButton.IsChecked    = false;
            

            PieChart.IsVisible               = true;
            SearchPicker.IsVisible           = false;
            PainAndStoolChart.IsVisible      = false;
            StoolsByDayOfWeekChart.IsVisible = false;
            StoolsByHourGroupChart.IsVisible = false;

            var viewModel = new ReportPainStoolViewModel();

            PieSeries.ItemsSource   = viewModel.PainPercentages;
            viewModel.ChartTitle = PAIN_PIE_CHART_TITLE;
        }

        private void StoolTypePercentagesRadioButton_OnStateChanged(object                sender
                                                                  , StateChangedEventArgs e)
        {
            var isChecked = e.IsChecked.HasValue 
                         && e.IsChecked.Value;

            if ( ! isChecked)
            {
                return;
            }
            
            PainAndStoolRadioButton.IsChecked         = false;
            PainPercentagesRadioButton.IsChecked      = false;
            StoolTypePercentagesRadioButton.IsChecked = true;
            StoolsByDayOfWeekRadioButton.IsChecked    = false;
            StoolsByHourGroupRadioButton.IsChecked    = false;

            PieChart.IsVisible               = true;
            SearchPicker.IsVisible           = false;
            PainAndStoolChart.IsVisible      = false;
            StoolsByDayOfWeekChart.IsVisible = false;
            StoolsByHourGroupChart.IsVisible = false;

            var viewModel = new ReportPainStoolViewModel();

            PieSeries.ItemsSource   = viewModel.StoolTypePercentages;
            viewModel.ChartTitle = STOOL_TYPE_PIE_CHART_TITLE;
        }

        private void StoolsByDayOfWeekRadioButton_OnStateChanged(object                sender
                                                               , StateChangedEventArgs e)
        {
            var isChecked = e.IsChecked.HasValue 
                         && e.IsChecked.Value;

            if ( ! isChecked)
            {
                return;
            }
            
            PainAndStoolRadioButton.IsChecked         = false;
            PainPercentagesRadioButton.IsChecked      = false;
            StoolTypePercentagesRadioButton.IsChecked = false;
            StoolsByDayOfWeekRadioButton.IsChecked    = true;
            StoolsByHourGroupRadioButton.IsChecked    = false;

            PieChart.IsVisible               = false;
            SearchPicker.IsVisible           = false;
            PainAndStoolChart.IsVisible      = false;
            StoolsByDayOfWeekChart.IsVisible = true;
            StoolsByHourGroupChart.IsVisible = false;
        }

        private void StoolsByHourGroupRadioButton_OnStateChanged(object                sender
                                                               , StateChangedEventArgs e)
        {
            var isChecked = e.IsChecked.HasValue 
                         && e.IsChecked.Value;

            if ( ! isChecked)
            {
                return;
            }
            
            PainAndStoolRadioButton.IsChecked         = false;
            PainPercentagesRadioButton.IsChecked      = false;
            StoolTypePercentagesRadioButton.IsChecked = false;
            StoolsByDayOfWeekRadioButton.IsChecked    = false;
            StoolsByHourGroupRadioButton.IsChecked    = true;

            PieChart.IsVisible               = false;
            SearchPicker.IsVisible           = false;
            PainAndStoolChart.IsVisible      = false;
            StoolsByDayOfWeekChart.IsVisible = false;
            StoolsByHourGroupChart.IsVisible = true;
        }
    }
    
}