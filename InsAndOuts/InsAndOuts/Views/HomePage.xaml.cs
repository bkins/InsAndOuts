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

        private async void MealsButton_OnClicked(object    sender
                                         , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(MealView));
        }

        private async void PainButton_OnClicked(object    sender
                                        , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(PainView));
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

        private async void StoolButton_OnClicked(object    sender
                                         , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(StoolsView));
        }

        private async void ReportButton_OnClicked(object    sender
                                          , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(DailyReportView));
        }

        private void LoadTestData_OnClicked(object    sender
                                          , EventArgs e)
        {
            AddMeals();
            AddPains();
            AddStools();
        }

        private void AddMeals()
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
        private void AddPains()
        {
            
            var todaysPain1 = new Pain
                              {
                                  DescriptionPainText = "It hurts"
                                , DescriptionHtml     = "It hurts"
                                , Level               = 5
                                , When                = $"{DateTime.Today.ToShortDateString()} 8:02 AM"
                              };
            
            var todaysPain2 = new Pain
                              {
                                  DescriptionPainText = "It hurts"
                                , DescriptionHtml     = "It hurts"
                                , Level               = 5
                                , When                = $"{DateTime.Today.ToShortDateString()} 1:02 PM"
                              };

            var todaysPain3 = new Pain
                              {
                                  DescriptionPainText = "It hurts"
                                , DescriptionHtml     = "It hurts"
                                , Level               = 5
                                , When                = $"{DateTime.Today.ToShortDateString()} 8:02 PM"
                              };
            
            var yesterdaysPain1 = new Pain
                                  {
                                      DescriptionPainText = "It hurts"
                                    , DescriptionHtml     = "It hurts"
                                    , Level               = 5
                                    , When                = $"{DateTime.Today.AddDays(-1).ToShortDateString()} 8:03 AM"
                                  };
            
            var yesterdaysPain2 = new Pain
                                  {
                                      DescriptionPainText = "It hurts"
                                    , DescriptionHtml     = "It hurts"
                                    , Level               = 5
                                    , When                = $"{DateTime.Today.AddDays(-1).ToShortDateString()} 1:03 PM"
                                  };

            var yesterdaysPain3 = new Pain
                                  {
                                      DescriptionPainText = "It hurts"
                                    , DescriptionHtml     = "It hurts"
                                    , Level               = 5
                                    , When                = $"{DateTime.Today.AddDays(-1).ToShortDateString()} 8:03 PM"
                                  };
            
            var dayBeforeYesterdaysPain1 = new Pain
                                           {
                                               DescriptionPainText = "It hurts"
                                             , DescriptionHtml     = "It hurts"
                                             , Level               = 5
                                             , When                = $"{DateTime.Today.AddDays(-2).ToShortDateString()} 8:04 AM"
                                           };
            
            var dayBeforeYesterdaysPain2 = new Pain
                                           {
                                               DescriptionPainText = "It hurts"
                                             , DescriptionHtml     = "It hurts"
                                             , Level               = 5
                                             , When                = $"{DateTime.Today.AddDays(-2).ToShortDateString()} 1:04 PM"
                                           };

            var dayBeforeYesterdaysPain3 = new Pain
                                           {
                                               DescriptionPainText = "It hurts"
                                             , DescriptionHtml     = "It hurts"
                                             , Level               = 5
                                             , When                = $"{DateTime.Today.AddDays(-2).ToShortDateString()} 8:04 PM"
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
        private void AddStools()
        {
            /*
             * <x:String>Type 1: Separate hard lumps, like nuts (difficult to pass and can be black)</x:String>
               <x:String>Type 2: Sausage-shaped, but lumpy</x:String>
               <x:String>Type 3: Like a sausage but with cracks on its surface (can be black)</x:String>
               <x:String>Type 4: Like a sausage or snake, smooth and soft (average stool)</x:String>
               <x:String>Type 5: Soft blobs with clear cut edges</x:String>
               <x:String>Type 6: Fluffy pieces with ragged edges, a mushy stool (diarrhea)</x:String>
               <x:String>Type 7: Watery, no solid pieces, entirely liquid (diarrhea)</x:String>
             */
            var todaysStool1 = new Stool
                               {
                                   DescriptionPainText = "It was brown"
                                 , DescriptionHtml     = "It was brown"
                                 , StoolType           = "Type 1: Separate hard lumps, like nuts (difficult to pass and can be black)"
                                 , When                = $"{DateTime.Today.ToShortDateString()} 9:02 AM"
                               };

            var todaysStool2 = new Stool
                               {
                                   DescriptionPainText = "It was brown"
                                 , DescriptionHtml     = "It was brown"
                                 , StoolType           = "Type 2: Sausage-shaped, but lumpy"
                                 , When                = $"{DateTime.Today.ToShortDateString()} 2:02 PM"
                               };

            var todaysStool3 = new Stool
                               {
                                   DescriptionPainText = "It was brown"
                                 , DescriptionHtml     = "It was brown"
                                 , StoolType           = "Type 3: Like a sausage but with cracks on its surface (can be black)"
                                 , When                = $"{DateTime.Today.ToShortDateString()} 9:02 PM"
                               };

            var yesterdaysStool1 = new Stool
                                   {
                                       DescriptionPainText = "It was brown"
                                     , DescriptionHtml     = "It was brown"
                                     , StoolType           = "Type 4: Like a sausage or snake, smooth and soft (average stool)"
                                     , When                = $"{DateTime.Today.ToShortDateString()} 9:03 AM"
                                   };

            var yesterdaysStool2 = new Stool
                                   {
                                       DescriptionPainText = "It was brown"
                                     , DescriptionHtml     = "It was brown"
                                     , StoolType           = "Type 5: Soft blobs with clear cut edges"
                                     , When                = $"{DateTime.Today.ToShortDateString()} 2:03 PM"
                                   };

            var yesterdaysStool3 = new Stool
                                   {
                                       DescriptionPainText = "It was brown"
                                     , DescriptionHtml     = "It was brown"
                                     , StoolType           = "Type 6: Fluffy pieces with ragged edges, a mushy stool (diarrhea)"
                                     , When                = $"{DateTime.Today.ToShortDateString()} 9:03 PM"
                                   };
            
            var dayBeforeYesterdaysStool1 = new Stool
                                            {
                                                DescriptionPainText = "It was brown"
                                              , DescriptionHtml     = "It was brown"
                                              , StoolType           = "Type 7: Watery, no solid pieces, entirely liquid (diarrhea)"
                                              , When                = $"{DateTime.Today.ToShortDateString()} 9:04 AM"
                                            };

            var dayBeforeYesterdaysStool2 = new Stool
                                            {
                                                DescriptionPainText = "It was brown"
                                              , DescriptionHtml     = "It was brown"
                                              , StoolType           = "Type 1: Separate hard lumps, like nuts (difficult to pass and can be black)"
                                              , When                = $"{DateTime.Today.ToShortDateString()} 2:04 PM"
                                            };

            var dayBeforeYesterdaysStool3 = new Stool
                                            {
                                                DescriptionPainText = "It was brown"
                                              , DescriptionHtml     = "It was brown"
                                              , StoolType           = "Type 2: Sausage-shaped, but lumpy"
                                              , When                = $"{DateTime.Today.ToShortDateString()} 9:04 PM"
                                            };

            App.Database.AddStoolWithType(todaysStool1);
            App.Database.AddStoolWithType(todaysStool2);
            App.Database.AddStoolWithType(todaysStool3);

            App.Database.AddStoolWithType(yesterdaysStool1);
            App.Database.AddStoolWithType(yesterdaysStool2);
            App.Database.AddStoolWithType(yesterdaysStool3);
            
            App.Database.AddStoolWithType(dayBeforeYesterdaysStool1);
            App.Database.AddStoolWithType(dayBeforeYesterdaysStool2);
            App.Database.AddStoolWithType(dayBeforeYesterdaysStool3);

        }
    }
}