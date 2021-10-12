using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsAndOuts.Data;
using InsAndOuts.Models;
using InsAndOuts.Services;
using InsAndOuts.Utilities;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InsAndOuts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BackgroundImageSource = Utilities.Configuration.BackgroundImage;

        } 

        private async void TestEmail_OnClicked(object    sender
                                             , EventArgs e)
        {
            var emailClient = new Emailer
                              {
                                  SubjectPrefix = "Test Email: "
                                , EmailFormat   = EmailBodyFormat.PlainText
                                , Recipients    = new List<string>{"benhop@gmail.com"}
                              };

            await emailClient.SendEmail($"{nameof(HomePage)}.{nameof(TestEmail_OnClicked)}"
                                , "Email sent from IsAndOsTests project.");
        }

        private async void Configuration_OnClicked(object    sender
                                           , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(ConfigurationView));
        }
        
        private async void ReportButton_OnClicked(object    sender
                                          , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(DailyReportView));
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

            //AddTestMeals(today
            //           , yesterday
            //           , dayBeforeYesterday);

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
                                  When = $"{today} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                              };
            var yesterdaysStool = new Stool(randomNumber.Next(1, 7))
                                  {
                                      When = $"{yesterday} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                                  };
            var dayBeforeYesterdaysStool = new Stool(randomNumber.Next(1, 7))
                                           {
                                               When = $"{dayBeforeYesterday} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
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
                                  Level = randomNumber.Next(10)
                                , When  = $"{today} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                              };

            var todaysPain2 = new Pain
                              {
                                  Level = randomNumber.Next(10)
                                , When  = $"{today} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                              };

            var todaysPain3 = new Pain
                              {
                                  Level = randomNumber.Next(10)
                                , When  = $"{today} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                              };

            var yesterdaysPain1 = new Pain
                                  {
                                      Level = randomNumber.Next(10)
                                    , When  = $"{yesterday} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                                  };

            var yesterdaysPain2 = new Pain
                                  {
                                      Level = randomNumber.Next(10)
                                    , When  = $"{yesterday} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                                  };

            var yesterdaysPain3 = new Pain
                                  {
                                      Level = randomNumber.Next(10)
                                    , When  = $"{yesterday} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                                  };

            var dayBeforeYesterdaysPain1 = new Pain
                                           {
                                               Level = randomNumber.Next(10)
                                             , When  = $"{dayBeforeYesterday} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                                           };

            var dayBeforeYesterdaysPain2 = new Pain
                                           {
                                               Level = randomNumber.Next(10)
                                             , When  = $"{dayBeforeYesterday} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
                                           };

            var dayBeforeYesterdaysPain3 = new Pain
                                           {
                                               Level = randomNumber.Next(10)
                                             , When  = $"{dayBeforeYesterday} {DateTime.Now.AddHours(randomNumber.Next(0, 24)).ToShortTimeString()}"
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

        private async void BrowseDataButton_OnClicked(object    sender
                                                    , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(ManageDataView));
        }

        private async void AboutButton_OnClicked(object    sender
                                         , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(AboutView));
        }

        private async void TendsAnalysisReportsButton_OnClicked(object    sender
                                                              , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(ReportsView));
        }
    }
}