﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using InsAndOuts.Models;
using InsAndOuts.ViewModels;
using Plugin.Media.Abstractions;
using Syncfusion.SfChart.XForms;
using Syncfusion.XForms.Buttons;
using Xamarin.Essentials;
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

        public ReportPainStoolViewModel ViewModel { get; set; }
        
        public ReportsView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel = new ReportPainStoolViewModel();
            
            SearchPicker.ItemsSource = ViewModel.AvailableDateStrings;
            PieChart.BindingContext  = ViewModel;

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
            PainsByHourGroupRadioButton.IsChecked     = false;
            
            SearchPicker.IsVisible           = true;
            PainAndStoolChart.IsVisible      = true;
            PieChart.IsVisible               = false;
            StoolsByDayOfWeekChart.IsVisible = false;
            ByHourGroupChart.IsVisible       = false;

            //var viewModel = new ReportPainStoolViewModel();
            
            PainAndStoolChart.BindingContext = ViewModel;

            Title = PainAndStoolRadioButton.Text;

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
            PainsByHourGroupRadioButton.IsChecked     = false;

            PieChart.IsVisible               = true;
            SearchPicker.IsVisible           = false;
            PainAndStoolChart.IsVisible      = false;
            StoolsByDayOfWeekChart.IsVisible = false;
            ByHourGroupChart.IsVisible       = false;

            //var viewModel = new ReportPainStoolViewModel();

            PieSeries.ItemsSource = ViewModel.PainPercentages;
            ViewModel.ChartTitle  = PAIN_PIE_CHART_TITLE;
            Title                 = PAIN_PIE_CHART_TITLE;
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
            PainsByHourGroupRadioButton.IsChecked     = false;

            PieChart.IsVisible               = true;
            SearchPicker.IsVisible           = false;
            PainAndStoolChart.IsVisible      = false;
            StoolsByDayOfWeekChart.IsVisible = false;
            ByHourGroupChart.IsVisible       = false;

            //var viewModel = new ReportPainStoolViewModel();

            PieSeries.ItemsSource = ViewModel.StoolTypePercentages;
            ViewModel.ChartTitle  = STOOL_TYPE_PIE_CHART_TITLE;
            Title                 = STOOL_TYPE_PIE_CHART_TITLE;
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
            PainsByHourGroupRadioButton.IsChecked     = false;

            PieChart.IsVisible               = false;
            SearchPicker.IsVisible           = false;
            PainAndStoolChart.IsVisible      = false;
            StoolsByDayOfWeekChart.IsVisible = true;
            ByHourGroupChart.IsVisible       = false;

            Title = StoolsByDayOfWeekRadioButton.Text;
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
            
            //var viewModel = new ReportPainStoolViewModel();

            CountByHourGroupBarSeries.ItemsSource     = ViewModel.NumberOfStoolsByHourGroup;
            AverageByHourGroupBarSeries.ItemsSource   = ViewModel.AverageStoolTypesByHourGroup;

            PainAndStoolRadioButton.IsChecked         = false;
            PainPercentagesRadioButton.IsChecked      = false;
            StoolTypePercentagesRadioButton.IsChecked = false;
            StoolsByDayOfWeekRadioButton.IsChecked    = false;
            StoolsByHourGroupRadioButton.IsChecked    = true;
            PainsByHourGroupRadioButton.IsChecked     = false;

            PieChart.IsVisible               = false;
            SearchPicker.IsVisible           = false;
            PainAndStoolChart.IsVisible      = false;
            StoolsByDayOfWeekChart.IsVisible = false;
            ByHourGroupChart.IsVisible       = true;

            Title = StoolsByHourGroupRadioButton.Text;
        }
        
        private void PainsByHourGroupRadioButton_OnStateChanged(object                sender
                                                              , StateChangedEventArgs e)
        {
            var isChecked = e.IsChecked.HasValue 
                         && e.IsChecked.Value;

            if ( ! isChecked)
            {
                return;
            }
            
            //var viewModel = new ReportPainStoolViewModel();

            CountByHourGroupBarSeries.ItemsSource     = ViewModel.NumberOfPainsByHourGroup;
            AverageByHourGroupBarSeries.ItemsSource   = ViewModel.AveragePainsByHourGroup;

            PainAndStoolRadioButton.IsChecked         = false;
            PainPercentagesRadioButton.IsChecked      = false;
            StoolTypePercentagesRadioButton.IsChecked = false;
            StoolsByDayOfWeekRadioButton.IsChecked    = false;
            StoolsByHourGroupRadioButton.IsChecked    = false;
            PainsByHourGroupRadioButton.IsChecked     = true;

            PieChart.IsVisible                        = false;
            SearchPicker.IsVisible                    = false;
            PainAndStoolChart.IsVisible               = false;
            StoolsByDayOfWeekChart.IsVisible          = false;
            ByHourGroupChart.IsVisible                = true;

            Title = PainsByHourGroupRadioButton.Text;
        }

        private async void ShareToolbarItem_OnClicked(object    sender
                                                    , EventArgs e)
        {
            var screenShot = await Screenshot.CaptureAsync();
            var stream     = await screenShot.OpenReadAsync();
            var fileName   = $"{Title}.png";
            var file       = Path.Combine(FileSystem.CacheDirectory, fileName);

            using (var fileStream = File.Open(file, FileMode.OpenOrCreate))
            {
                await stream.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }

            await ShareFile(fileName
                          , file);
        }

        private async Task ShareFile(string filename, string filepath) 
        {
            await Share.RequestAsync(new ShareFileRequest()
                                     {
                                         Title = filename,
                                         File  = new ShareFile(filepath)
                                     });
        }

    }
    
}