using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using InsAndOuts.Models;
using InsAndOuts.Utilities;
using InsAndOuts.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InsAndOuts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigurationView : ContentPage, INotifyPropertyChanged
    {
        private ConfigurationViewModel ViewModel { get; set; }

        public bool UseHtmlForEmailBody
        {
            get => Configuration.UseHtmlForEmailBody;
            set
            {
                if (UseHtmlForEmailBody == value)
                {
                    return;
                }

                Configuration.UseHtmlForEmailBody = value;

                OnPropertyChanged(nameof(UseHtmlForEmailBody));
            }
        }

        public string EmailToDoctor
        {
            get => Configuration.EmailToDoctor;
            set
            {
                if (EmailToDoctor == value)
                {
                    return;
                }

                Configuration.EmailToDoctor = value;

                OnPropertyChanged(nameof(EmailToDoctor));
            }
        }
        
        public new event PropertyChangedEventHandler PropertyChanged;

        public ConfigurationView()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
            ViewModel = new ConfigurationViewModel();

            //EmailToDoctor            = "test";
            EmailToDoctorEditor.Text = EmailToDoctor;
            UseHtmlSwitch.IsToggled  = UseHtmlForEmailBody;

        }

        
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            handler?.Invoke(this
                          , new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T                     backingField
                                 , T                         value
                                 , [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField
                                                 , value))
            {
                return;
            }

            backingField = value;

            OnPropertyChanged(propertyName);
        }

        private void EmailToDoctorEditor_OnUnfocused(object         sender
                                                   , FocusEventArgs e)
        {
            if (EmailToDoctor != EmailToDoctorEditor.Text)
            {
                EmailToDoctor = EmailToDoctorEditor.Text;
            }
        }
        
        private void UseHtmlSwitch_OnToggled(object           sender
                                           , ToggledEventArgs e)
        {
            UseHtmlForEmailBody = UseHtmlSwitch.IsToggled;
        }

        private void ClearSettings_OnClicked(object    sender
                                           , EventArgs e)
        {
            EmailToDoctorEditor.Text = string.Empty;
            Preferences.Clear();
        }

        private async void ClearData_OnClicked(object    sender
                                       , EventArgs e)
        {
            var clearData = await DisplayAlert("Delete All Data?"
                                             , "This will delete ALL data and cannot be undone.\r\nAre you sure you would like to continue?"
                                             , "Yes"
                                             , "No");

            if (clearData)
            {
                ViewModel.ClearData();
            }
        }
        
        private async void DoneButton_OnClicked(object    sender
                                              , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(HomePage));
        }

        private void AdvancedSwitch_OnToggled(object           sender
                                            , ToggledEventArgs e)
        {
            MiddleStackLayout.IsVisible = AdvancedSwitch.IsToggled;
        }

        private void LoadTestData_OnClicked(object    sender
                                          , EventArgs e)
        {
            var randomNumber = new Random();

            var day1 = DateTime.Today.AddDays(randomNumber.Next(0, 7)).ToShortDateString();

            var day2 = DateTime.Today.AddDays(randomNumber.Next(0, 7))
                               .ToShortDateString();

            var day3 = DateTime.Today.AddDays(randomNumber.Next(0, 7))
                               .ToShortDateString();

            AddTestMeals(day1
                       , day2
                       , day3);

            AddTestPains(randomNumber
                       , day1
                       , day2
                       , day3);

            AddTestStools(randomNumber
                        , day1
                        , day2
                        , day3);
        }

        private void AddTestStools(Random randomNumber
                                     , string today
                                     , string yesterday
                                     , string dayBeforeYesterday)
        {

            var todaysStool = new Stool(randomNumber.Next(1, 7))
                              {
                                  DescriptionHtml     = "See image"
                                , DescriptionPainText = "See image"
                                , When                = $"{today} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                              };

            var yesterdaysStool = new Stool(randomNumber.Next(1, 7))
                                  {
                                      DescriptionHtml     = "See image"
                                    , DescriptionPainText = "See image"
                                    , When                = $"{yesterday} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                                  };

            var dayBeforeYesterdaysStool = new Stool(randomNumber.Next(1, 7))
                                           {
                                               DescriptionHtml     = "See image"
                                             , DescriptionPainText = "See image"
                                             , When                = $"{dayBeforeYesterday} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                                           };

            App.Database.AddStool(todaysStool);
            App.Database.AddStool(yesterdaysStool);
            App.Database.AddStool(dayBeforeYesterdaysStool);
        }

        private static void AddTestPains(Random randomNumber
                                       , string today
                                       , string yesterday
                                       , string dayBeforeYesterday)
        {
            var todaysPain1 = new Pain
                              {
                                  Level               = randomNumber.Next(10)
                                , DescriptionHtml     = "Ouch"
                                , DescriptionPainText = "Ouch"
                                , When                = $"{today} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                              };

            var todaysPain2 = new Pain
                              {
                                  Level               = randomNumber.Next(10)
                                , DescriptionHtml     = "Dang!"
                                , DescriptionPainText = "Dang!"
                                , When                = $"{today} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                              };

            var todaysPain3 = new Pain
                              {
                                  Level               = randomNumber.Next(10)
                                , DescriptionHtml     = "It hurt"
                                , DescriptionPainText = "It hurt"
                                , When                = $"{today} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                              };

            var yesterdaysPain1 = new Pain
                                  {
                                      Level               = randomNumber.Next(10)
                                    , DescriptionHtml     = "Ouch"
                                    , DescriptionPainText = "Ouch"
                                    , When                = $"{yesterday} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                                  };

            var yesterdaysPain2 = new Pain
                                  {
                                      Level               = randomNumber.Next(10)
                                    , DescriptionHtml     = "Dang!"
                                    , DescriptionPainText = "Dang!"
                                    , When                = $"{yesterday} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                                  };

            var yesterdaysPain3 = new Pain
                                  {
                                      Level               = randomNumber.Next(10)
                                    , DescriptionHtml     = "It hurt"
                                    , DescriptionPainText = "It hurt"
                                    , When                = $"{yesterday} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                                  };

            var dayBeforeYesterdaysPain1 = new Pain
                                           {
                                               Level               = randomNumber.Next(10)
                                             , DescriptionHtml     = "Ouch"
                                             , DescriptionPainText = "Ouch"
                                             , When                = $"{dayBeforeYesterday} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                                           };

            var dayBeforeYesterdaysPain2 = new Pain
                                           {
                                               Level               = randomNumber.Next(10)
                                             , DescriptionHtml     = "Dang!"
                                             , DescriptionPainText = "Dang!"
                                             , When                = $"{dayBeforeYesterday} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                                           };

            var dayBeforeYesterdaysPain3 = new Pain
                                           {
                                               Level               = randomNumber.Next(10)
                                             , DescriptionHtml     = "It hurt"
                                             , DescriptionPainText = "It hurt"
                                             , When                = $"{dayBeforeYesterday} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                                           };

            App.Database.AddPain(todaysPain1);
            App.Database.AddPain(todaysPain2);
            App.Database.AddPain(todaysPain3);

            App.Database.AddPain(yesterdaysPain1);
            App.Database.AddPain(yesterdaysPain2);
            App.Database.AddPain(yesterdaysPain3);

            App.Database.AddPain(dayBeforeYesterdaysPain1);
            App.Database.AddPain(dayBeforeYesterdaysPain2);
            App.Database.AddPain(dayBeforeYesterdaysPain3);
        }

        private static void AddTestMeals(string today
                                       , string yesterday
                                       , string dayBeforeYesterday)
        {
            var breakfast = new Meal
                            {
                                Name                = "Breakfast"
                              , DescriptionHtml     = "<ul><li>Ham</li><li>Eggs</li><li>Bacon﻿﻿<br></li></ul>"
                              , DescriptionPainText = "Ham\r\nEggs\r\nBacon"
                              , When                = $"{today} 7:02 AM"
                            };

            var lunch = new Meal
                        {
                            Name                = "Lunch"
                          , DescriptionHtml     = "<ul><li>PB&amp;J Sandwich</li><li>Chips</li><li>Sode﻿﻿<br></li></ul>"
                          , DescriptionPainText = "PB&J Sandwich\r\nChips\r\nSode﻿﻿"
                          , When                = $"{today} 12:07 PM"
                        };

            var dinner = new Meal
                         {
                             Name                = "Dinner"
                           , DescriptionHtml     = "<ul><li>Steak</li><li>Mash Potatoes</li><li>Salad</li></ul>"
                           , DescriptionPainText = "\r\nSteak\r\nMash Potatoes\r\nSalad"
                           , When                = $"{today} 6:07 PM"
                         };

            var yesterdaysBreakfast = new Meal
                                      {
                                          Name                = breakfast.Name
                                        , DescriptionHtml     = breakfast.DescriptionHtml
                                        , DescriptionPainText = breakfast.DescriptionPainText
                                        , When                = $"{yesterday} 7:00 AM"
                                      };

            var yesterdaysLunch = new Meal
                                  {
                                      Name                = lunch.Name
                                    , DescriptionHtml     = lunch.DescriptionHtml
                                    , DescriptionPainText = lunch.DescriptionPainText
                                    , When                = $"{yesterday} 12:00 PM"
                                  };

            var yesterdaysDinner = new Meal
                                   {
                                       Name                = dinner.Name
                                     , DescriptionHtml     = dinner.DescriptionHtml
                                     , DescriptionPainText = dinner.DescriptionPainText
                                     , When                = $"{yesterday} 6:00 PM"
                                   };

            var dayBeforeYesterdaysBreakfast = new Meal
                                               {
                                                   Name                = breakfast.Name
                                                 , DescriptionHtml     = breakfast.DescriptionHtml
                                                 , DescriptionPainText = breakfast.DescriptionPainText
                                                 , When                = $"{dayBeforeYesterday} 8:00 AM"
                                               };

            var dayBeforeYesterdaysLunch = new Meal
                                           {
                                               Name                = lunch.Name
                                             , DescriptionHtml     = lunch.DescriptionHtml
                                             , DescriptionPainText = lunch.DescriptionPainText
                                             , When                = $"{dayBeforeYesterday} 12:30 PM"
                                           };

            var dayBeforeYesterdaysDinner = new Meal
                                            {
                                                Name                = dinner.Name
                                              , DescriptionHtml     = dinner.DescriptionHtml
                                              , DescriptionPainText = dinner.DescriptionPainText
                                              , When                = $"{dayBeforeYesterday} 5:30 PM"
                                            };

            App.Database.AddMeal(breakfast);
            App.Database.AddMeal(lunch);
            App.Database.AddMeal(dinner);

            App.Database.AddMeal(yesterdaysBreakfast);
            App.Database.AddMeal(yesterdaysLunch);
            App.Database.AddMeal(yesterdaysDinner);

            App.Database.AddMeal(dayBeforeYesterdaysBreakfast);
            App.Database.AddMeal(dayBeforeYesterdaysLunch);
            App.Database.AddMeal(dayBeforeYesterdaysDinner);
        }

    }
}