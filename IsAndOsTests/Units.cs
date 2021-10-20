using System;
using System.Collections.Generic;
using InsAndOuts.Data;
using InsAndOuts.Services;
using InsAndOuts.ViewModels;
using Xamarin.Essentials;
using Xunit;

namespace IsAndOsTests
{
    public class Units
    {
        private static Database _database;
        private static readonly string Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                                                                   , "IsAndOs_Tests.db3");
        
        public static Database Database => _database = _database ?? new Database(Path);

        [Fact]
        public void ReportPainStoolViewModel_AverageStoolTypesByHourGroup()
        {
            var viewModel = new ReportPainStoolViewModel
                            {
                                DataAccessLayer = new DataAccess(Database)
                            };
            //Load test data (see LoadTestData in Configuration.xaml.cs)

        }
    }
}
