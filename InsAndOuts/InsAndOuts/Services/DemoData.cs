using System;
using InsAndOuts.Data;
using InsAndOuts.Models;

namespace InsAndOuts.Services
{
    public class DemoData
    {
        private Database Database { get; set; }
        
        public DemoData(Database database)
        {
            Database = database;
        }

        public void AddCompleteSetOfData(DateTime day1
                                       , DateTime day2
                                       , DateTime day3
                                       , int      minutesToAdd
                                       , int      hoursToAdd
                                       , int      painLevel
                                       , int      stoolType)
        {
            AddTestMeals(minutesToAdd
                       , day1.ToShortDateString()
                       , day2.ToShortDateString()
                       , day3.ToShortDateString());

            AddPains(painLevel
                   , hoursToAdd
                   , day1.ToShortDateString()
                   , day2.ToShortDateString()
                   , day3.ToShortDateString());

            AddStools(stoolType
                    , hoursToAdd
                    , day1.ToShortDateString()
                    , day2.ToShortDateString()
                    , day3.ToShortDateString());
        }

        public void AddCompleteSetOfDataRandomized()
        {
            var randomNumber = new Random();

            var day1 = DateTime.Today.AddDays(randomNumber.Next(0, 7)).ToShortDateString();

            var day2 = DateTime.Today.AddDays(randomNumber.Next(0, 7))
                               .ToShortDateString();

            var day3 = DateTime.Today.AddDays(randomNumber.Next(0, 7))
                               .ToShortDateString();

            AddTestMeals(randomNumber
                       , day1
                       , day2
                       , day3);

            AddPains(randomNumber
                       , day1
                       , day2
                       , day3);

            AddStools(randomNumber
                        , day1
                        , day2
                        , day3);
        }

        public void AddStool(int stoolType
                           , int hoursToAdd
                           , string date)
        {
            var stool = new Stool(stoolType)
                        {
                            DescriptionHtml     = "See image"
                          , DescriptionPainText = "See image"
                          , When                = $"{date} {DateTime.Now.AddHours(hoursToAdd).ToShortTimeString()}"
                        };

            Database.AddStool(stool);
        }

        public void AddPain(int    painLevel
                          , int    hoursToAdd
                          , string date)
        {
            var pain = new Pain
                       {
                           Level               = painLevel
                         , DescriptionHtml     = "Ouch"
                         , DescriptionPainText = "Ouch"
                         , When                = $"{date} {DateTime.Now.AddHours(hoursToAdd).ToShortTimeString()}"
                       };

            Database.AddPain(pain);
        }

        public void AddMeal(string minutesPastTheHour
                          , string hourOfMeal
                          , string date)
        {
            var meal = new Meal
                       {
                           Name                = "Breakfast"
                         , DescriptionHtml     = "<ul><li>Ham</li><li>Eggs</li><li>Bacon﻿﻿<br></li></ul>"
                         , DescriptionPainText = "Ham\r\nEggs\r\nBacon"
                         , When                = $"{date} {hourOfMeal}:{minutesPastTheHour} AM"
                       };

            Database.AddMeal(meal);
        }
        
        public void AddStools(int stoolType
                            , int hoursToAdd
                            , string today
                            , string yesterday
                            , string dayBeforeYesterday)
        {

            AddStool(stoolType
                   , hoursToAdd
                   , today);

            AddStool(stoolType
                   , hoursToAdd
                   , yesterday);
            
            AddStool(stoolType
                   , hoursToAdd
                   , dayBeforeYesterday);
        }

        public void AddStools(Random randomNumber
                            , string today
                            , string yesterday
                            , string dayBeforeYesterday)
        {

            AddStool(randomNumber.Next(1, 7)
                       , randomNumber.Next(0, 24)
                       , today);

            AddStool(randomNumber.Next(1, 7)
                       , randomNumber.Next(0, 24)
                       , yesterday);
            
            AddStool(randomNumber.Next(1, 7)
                       , randomNumber.Next(0, 24)
                       , dayBeforeYesterday);
        }
        
        public void AddPains(int painLevel
                           , int hoursToAdd
                           , string today
                           , string yesterday
                           , string dayBeforeYesterday)
        {
            AddPain(painLevel
                  , hoursToAdd
                  , today);

            AddPain(painLevel
                  , hoursToAdd
                  , yesterday);

            AddPain(painLevel
                  , hoursToAdd
                  , dayBeforeYesterday);
        }

        public void AddPains(Random randomNumber
                                      , string today
                                      , string yesterday
                                      , string dayBeforeYesterday)
        {
            AddPain(randomNumber.Next(10)
                  , randomNumber.Next(0, 24)
                  , today);

            AddPain(randomNumber.Next(10)
                  , randomNumber.Next(0, 24)
                  , yesterday);

            AddPain(randomNumber.Next(10)
                  , randomNumber.Next(0, 24)
                  , dayBeforeYesterday);
        }

        public void AddTestMeals(int minutesToAdd
                               , string today
                               , string yesterday
                               , string dayBeforeYesterday)
        {
            //Breakfast today
            AddMeal(minutesToAdd.ToString()
                                .PadLeft(2, '0')
                  , "07"
                  , today);
            
            //Lunch today
            AddMeal(minutesToAdd.ToString()
                                .PadLeft(2, '0')
                  , "12"
                  , today);
            
            //Dinner today
            AddMeal(minutesToAdd.ToString()
                                .PadLeft(2, '0')
                  , "17"
                  , today);
            
            //Breakfast yesterday
            AddMeal(minutesToAdd.ToString()
                                .PadLeft(2, '0')
                  , "07"
                  , yesterday);
            
            //Lunch yesterday
            AddMeal(minutesToAdd.ToString()
                                .PadLeft(2, '0')
                  , "12"
                  , yesterday);
            
            //Dinner yesterday
            AddMeal(minutesToAdd.ToString()
                                .PadLeft(2, '0')
                  , "17"
                  , yesterday);

            //Breakfast dayBeforeYesterday
            AddMeal(minutesToAdd.ToString()
                                .PadLeft(2, '0')
                  , "07"
                  , dayBeforeYesterday);
            
            //Lunch dayBeforeYesterday
            AddMeal(minutesToAdd.ToString()
                                .PadLeft(2, '0')
                  , "12"
                  , dayBeforeYesterday);
            
            //Dinner dayBeforeYesterday
            AddMeal(minutesToAdd.ToString()
                                .PadLeft(2, '0')
                  , "17"
                  , dayBeforeYesterday);
        }
        
        public void AddTestMeals(Random randomNumber
                               , string today
                               , string yesterday
                               , string dayBeforeYesterday)
        {
            //Breakfast today
            AddMeal(randomNumber.Next(0, 60)
                                .ToString()
                                .PadLeft(2, '0')
                  , "07"
                  , today);
            
            //Lunch today
            AddMeal(randomNumber.Next(0, 60)
                                .ToString()
                                .PadLeft(2, '0')
                  , "12"
                  , today);
            
            //Dinner today
            AddMeal(randomNumber.Next(0, 60)
                                .ToString()
                                .PadLeft(2, '0')
                  , "17"
                  , today);
            
            //Breakfast yesterday
            AddMeal(randomNumber.Next(0, 60)
                                .ToString()
                                .PadLeft(2, '0')
                  , "07"
                  , yesterday);
            
            //Lunch yesterday
            AddMeal(randomNumber.Next(0, 60)
                                .ToString()
                                .PadLeft(2, '0')
                  , "12"
                  , yesterday);
            
            //Dinner yesterday
            AddMeal(randomNumber.Next(0, 60)
                                .ToString()
                                .PadLeft(2, '0')
                  , "17"
                  , yesterday);

            //Breakfast dayBeforeYesterday
            AddMeal(randomNumber.Next(0, 60)
                                .ToString()
                                .PadLeft(2, '0')
                  , "07"
                  , dayBeforeYesterday);
            
            //Lunch dayBeforeYesterday
            AddMeal(randomNumber.Next(0, 60)
                                .ToString()
                                .PadLeft(2, '0')
                  , "12"
                  , dayBeforeYesterday);
            
            //Dinner dayBeforeYesterday
            AddMeal(randomNumber.Next(0, 60)
                                .ToString()
                                .PadLeft(2, '0')
                  , "17"
                  , dayBeforeYesterday);
        }
    
    }
}
