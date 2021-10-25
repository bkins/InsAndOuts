using System;
using System.Collections.Generic;
using System.Linq;
using InsAndOuts.Data;
using InsAndOuts.Models;
using InsAndOuts.Services;
using InsAndOuts.ViewModels;
using Xunit;
using Xunit.Abstractions;

namespace InsAndOutsTest
{
    public class Units
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private Database _database;

        private readonly string _path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                                                             , "IsAndOs_Tests.db3");
        
        public Database Database => _database = _database ?? new Database(_path);

        public DemoData TestData { get; set; }
        
        public IEnumerable<Pain> AllPains { get; set; }

        public IEnumerable<Stool> AllStools { get; set; }

        public Units(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ReportPainStoolViewModel_AverageStoolTypesByHourGroup()
        {
            LoadTestData();
            DisplayAddedTestData();

            var expectedStoolTypeAveragesByDay = DisplayCalculatedExpectedResults();

            var viewModel = new ReportPainStoolViewModel(useTestData: true, Database);
            
            var listOfPains             = viewModel.Pains;
            var painsByDay              = viewModel.NumberOfPainsByDayOfWeek;
            var painsByHourGroup        = viewModel.NumberOfPainsByHourGroup;
            var averagePainsByHourGroup = viewModel.AveragePainsByHourGroup;
            var averagePainsByDay       = viewModel.AveragePainsByDayOfWeek;

            var listOfStools                 = viewModel.StoolTypes;
            var stoolsByHourGroup            = viewModel.NumberOfStoolsByHourGroup;
            var stoolsByDay                  = viewModel.NumberOfStoolByDayOfWeek;
            var averageStoolTypesByHourGroup = viewModel.AverageStoolTypesByHourGroup;
            var averageStoolTypesByDay       = viewModel.AverageStoolTypesByDayOfWeek;

        }

        #region Helper Methods

        private void LoadTestData()
        {
            Database.RefreshDatabase();

            TestData = new DemoData(Database);
            
            TestData.AddCompleteSetOfData(day1: DateTime.Today.AddDays(1)
                                        , day2: DateTime.Today.AddDays(2)
                                        , day3: DateTime.Today.AddDays(3)
                                        , minutesToAdd: 5
                                        , hoursToAdd:   2
                                        , painLevel:    2
                                        , stoolType:    1);
            
            TestData.AddCompleteSetOfData(day1: DateTime.Today.AddDays(4)
                                        , day2: DateTime.Today.AddDays(5)
                                        , day3: DateTime.Today.AddDays(6)
                                        , minutesToAdd: 35
                                        , hoursToAdd:   6
                                        , painLevel:    4
                                        , stoolType:    3);

            TestData.AddCompleteSetOfData(day1: DateTime.Today.AddDays(1)
                                        , day2: DateTime.Today.AddDays(2)
                                        , day3: DateTime.Today.AddDays(3)
                                        , minutesToAdd: 0
                                        , hoursToAdd:   10
                                        , painLevel:    6
                                        , stoolType:    5);

            TestData.AddCompleteSetOfData(day1: DateTime.Today.AddDays(4)
                                        , day2: DateTime.Today.AddDays(5)
                                        , day3: DateTime.Today.AddDays(6)
                                        , minutesToAdd: 0
                                        , hoursToAdd:   10
                                        , painLevel:    8
                                        , stoolType:    7);
            
            AllStools = Database.GetAllStools();
            AllPains  = Database.GetAllPain();
        }

        private void DisplayAddedTestData()
        {
            _testOutputHelper.WriteLine("Stool Type | When                | Hour | Day");
            _testOutputHelper.WriteLine("-----------+---------------------+------+--------------");

            foreach (var stool in AllStools)
            {
                var column1 = stool.StoolTypeNumber.ToString()
                                   .PadRight(10, ' ');

                var column2 = stool.When.PadRight(19, ' ');

                var column3 = $"{stool.WhenDateTime:HH}  ";

                var column4 = stool.WhenToDateTime()
                                   .DayOfWeek;

                _testOutputHelper.WriteLine($"{column1} | {column2} | {column3} | {column4}");
            }

            _testOutputHelper.WriteLine("");
            _testOutputHelper.WriteLine("Pain Level | When                | Hour | Day");
            _testOutputHelper.WriteLine("-----------+---------------------+------+--------------");

            foreach (var pain in AllPains)
            {
                var column1 = pain.Level.ToString()
                                  .PadLeft(2, '0')
                                  .PadRight(10, ' ');

                var column2 = pain.When.PadRight(19, ' ');

                var column3 = $"{pain.WhenDateTime:HH}  ";

                var column4 = pain.WhenToDateTime()
                                  .DayOfWeek;

                _testOutputHelper.WriteLine($"{column1} | {column2} | {column3} | {column4}");
            }
        }

        private Dictionary<string, List<ChartItem>> DisplayCalculatedExpectedResults()
        {
            var stoolTypeAveragesByDay = CalculateStoolTypeAveragesByDay();

            var painLevelAveragesByDay = CalculatePainLevelAveragesByDay();

            DisplayDictionaryKeyValues(stoolTypeAveragesByDay, "Averages of Stool Types by Day of Week:");

            var painStoolAveragesDictionary = new Dictionary<string, List<ChartItem>>
                                              {
                                                  { "PainAveragesByDay", painLevelAveragesByDay }
                                                 ,
                                                  { "StoolAveragesByDay", stoolTypeAveragesByDay }
                                              };

            return painStoolAveragesDictionary;
        }

        private List<ChartItem> CalculatePainLevelAveragesByDay()
        {
            var painLevelCountByDay = new List<ChartItem>(); //new Dictionary<string, int>();
            var painLevelSumsByDay  = new List<ChartItem>(); //new Dictionary<string, int>();

            foreach (var pain in AllPains)
            {
                UpdateDayOfWeekListsValues(pain.WhenToDateTime()
                                               .DayOfWeek
                                         , pain.Level
                                         , painLevelSumsByDay
                                         , painLevelCountByDay);
            }

            DisplayDictionaryKeyValues(painLevelCountByDay
                                     , "Count of Stool Types by Day of Week:");

            DisplayDictionaryKeyValues(painLevelSumsByDay
                                     , "Sum of Stool Types by Day of Week:");

            return painLevelCountByDay.Select(chartItem => 
                                              new ChartItem
                                              {
                                                  Label                = chartItem.Label
                                                , LabelUnderlyingValue = chartItem.LabelUnderlyingValue
                                                , Value = ((double)painLevelSumsByDay.FirstOrDefault(fields => fields.Label == chartItem.Label)
                                                                                     .Value)
                                                        / ((double)painLevelCountByDay.FirstOrDefault(fields => fields.Label == chartItem.Label)
                                                                                       .Value)
                                              })
                                       .ToList();
        }

        private List<ChartItem> CalculateStoolTypeAveragesByDay()
        {
            var stoolTypeCountsByDay = new List<ChartItem>(); //new Dictionary<string, int>();
            var stoolTypeSumsByDay   = new List<ChartItem>(); //new Dictionary<string, int>();

            foreach (var stool in AllStools)
            {
                UpdateDayOfWeekListsValues(stool.WhenToDateTime()
                                       .DayOfWeek
                                , stool.StoolTypeNumber
                                , stoolTypeSumsByDay
                                , stoolTypeCountsByDay);
            }

            DisplayDictionaryKeyValues(stoolTypeCountsByDay
                                     , "Count of Stool Types by Day of Week:");

            DisplayDictionaryKeyValues(stoolTypeSumsByDay
                                     , "Sum of Stool Types by Day of Week:");

            return stoolTypeCountsByDay.Select(chartItem => 
                                               new ChartItem
                                               {
                                                   Label                = chartItem.Label
                                                 , LabelUnderlyingValue = chartItem.LabelUnderlyingValue
                                                 , Value = ((double)stoolTypeSumsByDay.FirstOrDefault(fields => fields.Label == chartItem.Label)
                                                                                      .Value)
                                                         / ((double)stoolTypeCountsByDay.FirstOrDefault(fields => fields.Label == chartItem.Label)
                                                                                        .Value)
                                               })
                                       .ToList();
        }

        private static void UpdateDayOfWeekListsValues(DayOfWeek       dayOfWeek
                                                     , int             value
                                                     , List<ChartItem> sumsByDay
                                                     , List<ChartItem> countsByDay)
        {

            if (sumsByDay.All(fields => fields.Label != dayOfWeek.ToString()))
            {
                sumsByDay.Add(new ChartItem
                              {
                                  Label                = dayOfWeek.ToString()
                                , LabelUnderlyingValue = (int)dayOfWeek
                                , Value                = value
                              });

                countsByDay.Add(new ChartItem
                                {
                                    Label                = dayOfWeek.ToString()
                                  , LabelUnderlyingValue = (int)dayOfWeek
                                  , Value                = 1
                                });
            }
            else
            {
                sumsByDay.FirstOrDefault(fields => fields != null 
                                                && fields.Label == dayOfWeek.ToString())
                                  .Value += value;

                countsByDay.FirstOrDefault(fields => fields != null 
                                                  && fields.Label == dayOfWeek.ToString())
                                    .Value++;
            }
        }

        private void DisplayDictionaryKeyValues(List<ChartItem> stoolTypeSumsByDay, string title)
        {
            _testOutputHelper.WriteLine("");
            _testOutputHelper.WriteLine(title);
            _testOutputHelper.WriteLine("-".PadRight(title.Length, '-'));

            foreach (var item in stoolTypeSumsByDay)
            {
                _testOutputHelper.WriteLine($"{item.Label.PadRight(11, ' ')}: {item.Value}");
            }
        }

        #endregion
        
    }
}
