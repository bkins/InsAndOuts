using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Environment = System.Environment;

namespace InsAndOuts.Droid
{
    [Activity(Label = "InsAndOuts", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
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
             
            DisplayCrashReport();

            LoadApplication(new App());
        }
        private static void TaskSchedulerOnUnobservedTaskException(object sender
                                                                 , UnobservedTaskExceptionEventArgs e)
        {
            var exception = new Exception("TaskSchedulerOnUnobservedTaskException"
                                        , e.Exception);

            LogUnhandledException(exception);
        }

        private static void CurrentDomainOnUnhandledException(object                      sender
                                                            , UnhandledExceptionEventArgs e)
        {
            var exception = new Exception("CurrentDomainOnUnhandledException"
                                        , e.ExceptionObject as Exception);

            LogUnhandledException(exception);
        }

        internal static void LogUnhandledException(Exception exception)
        {
            try
            {
                var libraryPath   = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var errorFilePath = Path.Combine(libraryPath, ERROR_FILENAME);
                var errorMessage  = $"Time: {DateTime.Now}\r\nError: Unhandled Exception\r\n{exception}";

                File.WriteAllText(errorFilePath, errorMessage);  
                
                Android.Util.Log.Error("Crash Report", errorMessage);
            }
            catch
            {
                //Let's hope there were no errors while logging.
            }
        }
        
        [Conditional("DEBUG")]
        private void DisplayCrashReport()
        {
            
            var          libraryPath   = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var          errorFilePath = Path.Combine(libraryPath, ERROR_FILENAME);

            if (!File.Exists(errorFilePath))
            {
                return; 
            }

            var errorText = File.ReadAllText(errorFilePath);
            new AlertDialog.Builder(this)
                           .SetPositiveButton("Clear", (sender, args) =>
                                                       {
                                                           File.Delete(errorFilePath);
                                                       })
                          ?.SetNegativeButton("Close", (sender, args) =>
                                                       {
                                                           // Nothing to do if Close was pressed.
                                                       })
                          ?.SetMessage(errorText)
                          ?.SetTitle("Crash Report")
                          ?.Show();
        } 
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}