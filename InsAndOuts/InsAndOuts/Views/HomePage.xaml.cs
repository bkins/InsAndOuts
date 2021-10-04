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
            var breakfast = new Meal
                            {
                                Name                = "Breakfast"
                              , DescriptionHtml     = "<ul><li>Ham</li><li>Eggs</li><li>Bacon﻿﻿<br></li></ul>"
                              , DescriptionPainText = "Ham\r\nEggs\r\nBacon"
                              , When                = $"{DateTime.Today.ToShortDateString()} 7:02 AM"
                            };

            var lunch = new Meal
                        {
                            Name                = "Lunch"
                          , DescriptionHtml     = "<ul><li>PB&amp;J Sandwich</li><li>Chips</li><li>Sode﻿﻿<br></li></ul>"
                          , DescriptionPainText = "PB&J Sandwich\r\nChips\r\nSode﻿﻿"
                          , When                = $"{DateTime.Today.ToShortDateString()} 12:07 PM"
                        };
            
            var dinner = new Meal
                        {
                            Name                = "Dinner"
                          , DescriptionHtml     = "<ul><li>Steak</li><li>Mash Potatoes</li><li>Salad</li></ul>"
                          , DescriptionPainText = "\r\nSteak\r\nMash Potatoes\r\nSalad"
                          , When                = $"{DateTime.Today.ToShortDateString()} 6:07 PM"
                        };
            
            var yesterdaysBreakfast = new Meal
                                      {
                                          Name                = breakfast.Name
                                        , DescriptionHtml     = breakfast.DescriptionHtml
                                        , DescriptionPainText = breakfast.DescriptionPainText
                                        , When                = $"{DateTime.Today.AddDays(-1).ToShortDateString()} 7:00 AM"
                                      };
            
            var yesterdaysLunch = new Meal
                                      {
                                          Name                = lunch.Name
                                        , DescriptionHtml     = lunch.DescriptionHtml
                                        , DescriptionPainText = lunch.DescriptionPainText
                                        , When                = $"{DateTime.Today.AddDays(-1).ToShortDateString()} 12:00 PM"
                                      };

            var yesterdaysDinner = new Meal
                                   {
                                       Name                = dinner.Name
                                     , DescriptionHtml     = dinner.DescriptionHtml
                                     , DescriptionPainText = dinner.DescriptionPainText
                                     , When                = $"{DateTime.Today.AddDays(-1).ToShortDateString()} 6:00 PM"
                                   };
            
            var dayBeforeYesterdaysBreakfast = new Meal
                                               {
                                                   Name                = breakfast.Name
                                                 , DescriptionHtml     = breakfast.DescriptionHtml
                                                 , DescriptionPainText = breakfast.DescriptionPainText
                                                 , When                = $"{DateTime.Today.AddDays(-2).ToShortDateString()} 8:00 AM"
                                               };
            
            var dayBeforeYesterdaysLunch = new Meal
                                           {
                                               Name                = lunch.Name
                                             , DescriptionHtml     = lunch.DescriptionHtml
                                             , DescriptionPainText = lunch.DescriptionPainText
                                             , When                = $"{DateTime.Today.AddDays(-2).ToShortDateString()} 12:30 PM"
                                           };

            var dayBeforeYesterdaysDinner = new Meal
                                            {
                                                Name                = dinner.Name
                                              , DescriptionHtml     = dinner.DescriptionHtml
                                              , DescriptionPainText = dinner.DescriptionPainText
                                              , When                = $"{DateTime.Today.AddDays(-2).ToShortDateString()} 5:30 PM"
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
    }
}