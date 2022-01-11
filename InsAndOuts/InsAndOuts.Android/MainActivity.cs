using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using ButtonCircle.FormsPlugin.Droid;
using InsAndOuts.Services;
using Xamarin.Essentials;
using Environment = System.Environment;

namespace InsAndOuts.Droid
{
    [Activity
            (
                  Label = "Ins & Outs & Ooohs!"
                , Icon = "@mipmap/icon"
                , Theme = "@style/MainTheme"
                , MainLauncher = true
                , ConfigurationChanges = ConfigChanges.ScreenSize 
                                       | ConfigChanges.Orientation 
                                       | ConfigChanges.UiMode 
                                       | ConfigChanges.ScreenLayout 
                                       | ConfigChanges.SmallestScreenSize 
            )
    ]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private const string ERROR_FILENAME = "Fatal.log";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //https://stackoverflow.com/questions/39503390/global-exception-handling-in-xamarin-cross-platform
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException      += TaskSchedulerOnUnobservedTaskException;  

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            
            Rg.Plugins.Popup.Popup.Init(this);
            
            ButtonCircleRenderer.Init();

            DisplayCrashReport();

            LoadApplication(new App());
        }

        public override void OnBackPressed()
        {
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }

        private void TaskSchedulerOnUnobservedTaskException(object sender
                                                                 , UnobservedTaskExceptionEventArgs e)
        {
            var exception = new Exception("TaskSchedulerOnUnobservedTaskException"
                                        , e.Exception);

            LogUnhandledException(exception);
        }

        private void CurrentDomainOnUnhandledException(object                      sender
                                                            , UnhandledExceptionEventArgs e)
        {
            var exception = new Exception("CurrentDomainOnUnhandledException"
                                        , e.ExceptionObject as Exception);

            LogUnhandledException(exception);
        }

        internal void LogUnhandledException(Exception exception)
        {
            try
            {
                var libraryPath   = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var errorFilePath = Path.Combine(libraryPath, ERROR_FILENAME);
                var errorMessage  = $"Time: {DateTime.Now}\r\nError: Unhandled Exception\r\n{exception}";

                File.WriteAllText(errorFilePath, errorMessage);  
                
                Android.Util.Log.Error("Crash Report", errorMessage);
            }
            catch(Exception e)
            {
                //Let's hope there were no errors while logging.
                BasicAlertNotActionOk("Something very bad happened!"
                                    , $"{e.Message}{Environment.NewLine}{Environment.NewLine}{e.StackTrace}");
            }
        }
        
        [Conditional("DEBUG")]
        private async void DisplayCrashReport()
        {
            
            var          libraryPath   = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var          errorFilePath = Path.Combine(libraryPath, ERROR_FILENAME);

            if (!File.Exists(errorFilePath))
            {
                return; 
            }
            
            var errorText  = await File.ReadAllTextAsync(errorFilePath);

            try
            {
                new AlertDialog.Builder(this).SetPositiveButton("Clear"
                                                              , (sender, args) =>
                                                                {
                                                                    File.Delete(errorFilePath);
                                                                })
                                            ?.SetNegativeButton("Send"
                                                              , async (sender, args) =>
                                                                {
                                                                    try
                                                                    {
                                                                        await SendErrorToEmail(new EmailAttachment(errorFilePath));
                                                                        File.Delete(errorFilePath);
                                                                    }
                                                                    catch (Exception e)
                                                                    {
                                                                        BasicAlertNotActionOk("No Email Client"
                                                                                            , $"{e.Message}{Environment.NewLine}{Environment.NewLine}{e.StackTrace}");
                                                                    }
                                                                })
                                            ?.SetMessage(errorText)
                                            ?.SetTitle("Crash Report")
                                            ?.Show();
            }
            catch (FeatureNotSupportedException e)
            {
                BasicAlertNotActionOk("No Email Client"
                                    , $"{e.Message}{Environment.NewLine}{e.StackTrace}");
            }
            catch (Exception exception)
            {
                BasicAlertNotActionOk(exception.Message
                                    , exception.StackTrace);
                
            }
        }

        private static async Task SendErrorToEmail(FileBase attachment)
        {
                var emailer = new Emailer
                              {
                                  SubjectPrefix = "IsAndOsAndOoohs"
                                , Recipients = new List<string>
                                               {
                                                   "BenHop#GMail.com"
                                               }
                              };

            await emailer.SendEmail("Crash Report"
                                      , "See attachment"
                                      , new List<EmailAttachment>
                                        {
                                            new EmailAttachment(attachment)
                                        });
        }
        
        private void BasicAlertNotActionOk(string title, string message)
        {
            new AlertDialog.Builder(this).SetPositiveButton("OK", (sender, args) => { })
                                        ?.SetMessage(message)
                                        ?.SetTitle(title)
                                        ?.Show();
        }
        public override void OnRequestPermissionsResult(int requestCode
                                                      , string[] permissions
                                                      , [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode
                                              , permissions
                                              , grantResults);

            base.OnRequestPermissionsResult(requestCode,
                                            permissions
                                          , grantResults);
        }
    }
}