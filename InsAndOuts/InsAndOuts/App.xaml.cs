
using System;
using InsAndOuts.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InsAndOuts
{
    public partial class App : Application
    {
        private static Database _database;
        private static readonly string Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                                                                   , "IsAndOs.db3");
        
        public static Database Database => _database = _database ?? new Database(Path);

        public App()
        {
            //BENDO:  Look at https://github.com/XamSome/awesome-xamarin for some tools and ideas to consider.

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTA0MDI3QDMxMzkyZTMyMmUzMFZXUzZFUWJuRnpLWm9IRFVXN0FKMUswMEdNUVd2WG15dE4yU3lQQmRWeE09");
            
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
