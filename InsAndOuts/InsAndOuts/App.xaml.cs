
using System;
using InsAndOuts.Data;
using InsAndOuts.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InsAndOuts
{
    public partial class App : Application
    {
        private static Database _database;
        public static  string   DatabaseFolder   => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static  string   DatabaseFileName => "IsAndOs.db3";

        public static readonly string FullDatabasePath = System.IO.Path.Combine(DatabaseFolder
                                                                              , DatabaseFileName);
        
        public static Database Database => _database = _database ?? new Database(FullDatabasePath);
        
        public App()
        {
            //BENDO:  Look at https://github.com/XamSome/awesome-xamarin for some tools and ideas to consider.
            //NTYxNzQyQDMxMzkyZTMyMmUzMGVCcDVrc0NXVDBqKytMRUlwTnZ6ZE44bjhld3lMb1ZpS1F2OHc4ZXJNc3M9 <-- Xamarin 19.2.0.62
            //NTU5NzkyQDMxMzkyZTM0MmUzMEpqZmdrQmtSd2FWVmxZczRmVFVZZFNpZDZsQ3U3a3JrZFRlK3g5eUgyU0U9 <-- Xamarin 19.4.0.41
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTYxNzQyQDMxMzkyZTMyMmUzMGVCcDVrc0NXVDBqKytMRUlwTnZ6ZE44bjhld3lMb1ZpS1F2OHc4ZXJNc3M9");
            
            InitializeComponent();
            
            Xamarin.Essentials.VersionTracking.Track();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
