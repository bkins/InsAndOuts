using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using InsAndOuts.Services;
using InsAndOuts.Utilities;
using InsAndOuts.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Android.App;
using Application = Android.App.Application;

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

        private async void LoadTestData_OnClicked(object    sender
                                          , EventArgs e)
        {
            var addDemoData = await DisplayAlert("Add Demo Data?"
                                               , $"This will add data to any currently entered data.{Environment.NewLine} It is not recommended to perform this action if you have already added on your own data.{Environment.NewLine}Would like to add this demo data?"
                                               , "Yes"
                                               , "No");
            if (addDemoData)
            {
                var demoData = new DemoData(App.Database);
                demoData.AddCompleteSetOfDataRandomized();
            }
        }

        private async void BackupDatabaseButton_OnClicked(object    sender
                                                  , EventArgs e)
        {
            var source = App.Path; //complete filename and path of the DB
            
            string[] sourceFiles = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

            var destination = CreateDestination();

            await BackupDataBase(source
                               , await destination);

        }

        private async Task<string> CreateDestination()
        {
            var year = DateTime.Today.Year;

            var month = DateTime.Today.Month
                                .ToString()
                                .PadLeft(2, '0');

            var day = DateTime.Today.Day
                              .ToString()
                              .PadLeft(2, '0');

            var hour = DateTime.Now.Hour.ToString()
                               .PadLeft(2, '0');

            var minute = DateTime.Now.Minute.ToString()
                                 .PadLeft(2, '0');

            var second = DateTime.Now.Second.ToString()
                                 .PadLeft(2, '0');

            var externalDirectory = Application.Context
                                               .GetExternalFilesDir(null)
                                               ?.AbsolutePath;

            var fileDirs = new StringBuilder($"GetExternalFilesDirs{Environment.NewLine}");

            for (var i = 0; i < Application.Context.GetExternalFilesDirs(null)?.Length-1; i++)
            {
                fileDirs.AppendLine(Application.Context.GetExternalFilesDirs(null)?[i]
                                                       .AbsolutePath);
            }

            fileDirs.AppendLine("GetExternalMediaDirs");

            for (int i = 0; i < Application.Context.GetExternalMediaDirs()?.Length-1; i++)
            {
                fileDirs.AppendLine(Application.Context.GetExternalMediaDirs()?[i]
                                               .AbsolutePath);
            }

            var packageName = AppInfo.PackageName;

            fileDirs.AppendLine($"DataDir: {Application.Context.DataDir?.AbsolutePath}");
            fileDirs.AppendLine($"FilesDir: {Application.Context.FilesDir?.AbsolutePath}");
            
            //fileDirs.AppendLine($"{Environment.DirectoryDownloads} ");
            fileDirs.AppendLine($"ApplicationData: {Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}");
            fileDirs.AppendLine($"CommonPictures: {Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures)}");
            fileDirs.AppendLine($"MyDocuments: {Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}");
            fileDirs.AppendLine($"FileSystem.AppDataDirectory: {FileSystem.AppDataDirectory}");
            fileDirs.AppendLine($"AppInfo.PackageName: {AppInfo.PackageName}");

            if (externalDirectory ==null)
            {
                await DisplayAlert("Path not found"
                                 , "Database cannot be backed up."
                                 , "OK").ConfigureAwait(false);

                return null;
            }

            var dataDir           = string.Empty;

            if (Application.Context?.DataDir?.AbsolutePath != null)
            {
                dataDir = Application.Context?.DataDir?.AbsolutePath.Replace(AppInfo.PackageName, string.Empty);
            }
            var destinationFolder = Path.Combine(dataDir, "BackupDbs");
            
            try
            {
                string[] destinationFiles = Directory.GetFiles(destinationFolder);
                Directory.CreateDirectory(destinationFolder);

                var destination = Path.Combine(destinationFolder
                                             , $"IsAndOs-{year}{month}{day}{hour}{minute}{second}.db3");

                return destination;
            }
            catch (Exception e)
            {
                await DisplayAlert("Could not create folder"
                                 , $"{e.Message}"
                                 , "OK");
            }

            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        private async Task BackupDataBase(string source
                                        , string destination)
        {
            try
            {
                File.Copy(source
                        , destination
                        , overwrite: true);

                await DisplayAlert("Backup Successfull!"
                                 , $"The database was sucessfully backed up to: {destination}"
                                 , "OK");
            }
            catch (Exception exception)
            {
                await DisplayAlert("Could not back up database!"
                                 , $"{exception.Message}"
                                 , "OK");
            }
        }
    }
}