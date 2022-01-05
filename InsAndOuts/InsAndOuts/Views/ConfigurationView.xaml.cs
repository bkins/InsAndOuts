using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Android.Content;
using InsAndOuts.Services;
using InsAndOuts.Utilities;
using InsAndOuts.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Android.App;
using Avails.D_Flat;
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
            var email = new Emailer
                        {
                            EmailFormat = EmailBodyFormat.PlainText
                          , Recipients = new List<string>
                                         {
                                             "<enter your email address here>"
                                         }
                          , SubjectPrefix = "Is&Os&Oohs: "
                        };

            await email.SendEmail("Backup Database"
                                , $"Save this to restore your data to the {AppInfo.Name}"
                                , new List<EmailAttachment>
                                  {
                                      new EmailAttachment(App.FullDatabasePath)
                                  });

            //BENDO: Finish Db3FileActivity.cs in the InsAndOuts.Android project to be able to open a db3 file and
            //restore by copying it over the existing database file, of course first prompting the user to make sure that is ok!
            //This is a temporary solution to the code not working in Android 11.

            //BUG:  Will not work as is.  Either need a new strategy or modify to work:
            //var source = App.FullDatabasePath; //complete filename and path of the DB

            //string[] sourceFiles = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            //CopyDemoDB(App.DatabaseFileName);
            //var destination = CreateDestination();

            //await BackupDataBase(source
            //                   , await destination);

        }

        public void CopyDemoDB(string filename)
        {
            //data/user/0/com.moralcoding.insandouts/files/.local/share
            if (Application.Context.Assets == null) 
                return;

            var sourceFilePath = Path.Combine(App.DatabaseFolder
                                            , filename);

            //This is returning null
            //var sourceFileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(sourceFilePath);

            var sourceFileStream = File.Open(sourceFilePath
                                           , FileMode.Open);

            var destinationPath = Path.Combine(FileSystem.AppDataDirectory, filename); //make file name unique for each call

            var sourceExists      = File.Exists(sourceFilePath);
            var destinationExists = File.Exists(destinationPath);

            try
            {
                if (Application.Context.Resources        != null
                &&  Application.Context.Resources.Assets != null)
                {
                    var test1 = Application.Context.Resources.Assets.OpenFd(sourceFilePath);
                }

                var fileDescriptor = Application.Context.Assets.OpenFd(sourceFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            if (sourceFileStream == null)
                return;

            //File is being written, but I cannot see file from device.
            //Is the file written to a folder the user does not have access to,
            //or is the file not safe to a permanent file?
            using (var binaryReader = new BinaryReader(sourceFileStream))
            {
                using (var binaryWriter = new BinaryWriter(new FileStream(destinationPath
                                                         , FileMode.Create)))
                {
                    var buffer = new byte[2048];
                    int bytesRead;

                    while ((bytesRead = binaryReader.Read(buffer
                                                        , 0
                                                        , buffer.Length))
                         > 0)
                    {
                        binaryWriter.Write(buffer
                                         , 0
                                         , bytesRead);
                    }
                }
            }

        }

        private async Task<string> CreateDestination()
        {
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
                                             , $"IsAndOs-{DateTime.Now.ToLong()}.db3");

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

        private async void EmailLogFileButton_OnClicked(object    sender
                                                      , EventArgs e)
        {
            if ( ! File.Exists(Logger.FullLogPath))
            {
                await DisplayAlert("Log File Not Found"
                                 , "No log file was created.  Perform some action (add, edit or delete), and try again."
                                 , "Ok");
                return;
            }

            var emailer = new Emailer();
            emailer.Recipients.Add("BenHop@GMail.com");
            var attachments = new List<EmailAttachment>
                              {
                                  new EmailAttachment(Logger.FullLogPath)
                              };

            var userSelectedYes = await DisplayAlert("Attach database?"
                                                   , "In addition to the log file, would you like to attach the database file?"
                                                   , "Yes"
                                                   , "No");

            if (userSelectedYes)
            {
                attachments.Add(new EmailAttachment(App.FullDatabasePath));
            }

            try
            {
                await emailer.SendEmail("Log file"
                                      , $"{GetImportantSection()}{GetSystemInfoSection()}{GetLogSection()}"
                                      , attachments);

            }
            catch (Exception exception)
            {
                await DisplayAlert("Error"
                                 , $"Could not sent email because: {Environment.NewLine}{exception.Message}"
                                 , "OK");
            }
            

            userSelectedYes = await DisplayAlert("Delete log file?"
                                               , "Was the log file successfully sent?  If so, would you like to delete log file now?"
                                               , "Yes (recommended)"
                                               , "No");

            if ( ! userSelectedYes)
                return;

            try
            {
                File.Delete(Logger.FullLogPath);
                Logger.Clear();
            }
            catch (Exception exception)
            {
                Logger.WriteLine("Log file failed to delete", Category.Error, exception);
            }
        }

        private static string GetLogSection()
        {
            var section = new StringBuilder();

            section.AppendLine("Contents of log file (log file itself is also attached):");
            section.AppendLine(Logger.CompleteLog);
            section.AppendLine("");

            return section.ToString();
        }

        private static string GetImportantSection()
        {
            var section = new StringBuilder();
            
            section.Append("IMPORTANT: Please review the contents of this email before sending.  ");
            section.Append("Make sure you are comfortable with sharing the information you are providing.  "); 
            section.AppendLine("If you are not, please remove any or all content that you do not want to share with the developer of this application -- this includes the attachments.");
            section.AppendLine("");

            return section.ToString();
        }

        private static string GetSystemInfoSection()
        {
            var sectionText = new StringBuilder();

            sectionText.AppendLine("System Info:");
            sectionText.AppendLine(GetSystemInfo());

            return sectionText.ToString();
        }
        private static string GetSystemInfo()
        {
            var device       = $"Model:         {DeviceInfo.Model}";
            var manufacturer = $"Manufacturer:  {DeviceInfo.Manufacturer}";
            var deviceName   = $"Name:          {DeviceInfo.Name}";
            var version      = $"VersionString: {DeviceInfo.VersionString}";
            var platform     = $"Platform:      {DeviceInfo.Platform}";
            var idiom        = $"Idiom:         {DeviceInfo.Idiom}";
            var deviceType   = $"DeviceType:    {DeviceInfo.DeviceType}";

            var systemInfo = new StringBuilder();
            
            systemInfo.AppendLine(device);
            systemInfo.AppendLine(manufacturer);
            systemInfo.AppendLine(deviceName);
            systemInfo.AppendLine(version);
            systemInfo.AppendLine(platform);
            systemInfo.AppendLine(idiom);
            systemInfo.AppendLine(deviceType);

            return systemInfo.ToString();
        }
    }
}