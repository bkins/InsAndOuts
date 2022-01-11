using System;
using System.Collections.Generic;
using Avails.D_Flat;
using InsAndOuts.Services;
using InsAndOuts.Utilities;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InsAndOuts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutView : ContentPage
    {
        //BENDO: See link below for a way to get the version/build number from Android and iOS:
        //https://www.c-sharpcorner.com/article/how-to-get-app-version-and-build-number-in-xamarin-forms/

        public AboutView()
        {
            InitializeComponent();
            /*
             * Semantic Versioning 2.0.0
             *      MAJOR version when you make incompatible API changes,
             *      MINOR version when you add functionality in a backwards compatible manner, and
             *      PATCH version when you make backwards compatible bug fixes.
             *  
             * Which I will modify as follows:
             *      MAJOR (Increment VersionTracking.CurrentBuild) 
             *          Any significant change to the look and/or how the app works
             *      MINOR (Increment the first number (left of the dot) of VersionTracking.CurrentVersion)
             *          Added functionality (when a significant number of features are added, consider this a MAJOR change)
             *      PATCH (Increment the second number (right of the dot) of VersionTracking.CurrentVersion)
             *          Bug fixes
             *          
             *  These values are updated in Android Manifest file (AndroidManifest.xml)
             *      versionCode = VersionTracking.CurrentBuild   (int)      >= 1
             *      versionName = VersionTracking.CurrentVersion (string)   >= 0.0 (in decimal format: an int followed by a dot ('.') then another int.)
             *          (Optionally, appended to the CurrentVersion, in parentheses, there can be a named version)
             *      
             *      Examples:
             *          Very first version  : 1.0.0
             *          First bug fix       : 1.1.1
             *          Then feature added  : 1.2.1
             *          Major UI revision   : 2.0.0 (beta)  <-- Notice the optional name of the version
             *          Post beta version   : 2.1.1         <-- After fixing a bug and making a small change based on user feedback.
             *          
             */
            VersionNumberSpan.Text = $" version: {VersionTracking.CurrentBuild}.{VersionTracking.CurrentVersion}";
            //BuildNumberSpan.Text   = $" ({VersionTracking.CurrentBuild})";
        }

        private void Email_Tapped(object    sender
                                , EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ForZoeButton_OnClicked(object    sender
                                          , EventArgs e)
        {
            //HappyMushrooms image found here: https://drewbrophyart.com/products/happy-mushroom?variant=39880460075166
            Configuration.BackgroundImage = Configuration.BackgroundImage.IsNullEmptyOrWhitespace() ?
                                                    "HappyMushrooms.jpg" :
                                                    string.Empty;
            
            Configuration.ZoeColor = Configuration.ZoeColor == nameof(Configuration.ZoeColor) ?
                                             "OriginalPrimary" :
                                             nameof(Configuration.ZoeColor);
            
            Application.Current.Resources["Primary"] = Application.Current.Resources[Configuration.ZoeColor];
        }

        private async void Credits_OnClicked(object    sender
                                           , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(Credits));
        }

        private async void CreatorButton_OnClicked(object    sender
                                                 , EventArgs e)
        {
            var emailer = new Emailer();

            emailer.Recipients = new List<string>
                                 {
                                     "benhop@gmail.com"
                                 };

            emailer.SubjectPrefix = $"Question about {AppNameSpan.Text}";

            await emailer.SendEmail(string.Empty
                                  , string.Empty);
        }

        private async void AssociationWithButton_OnClicked(object    sender
                                                        , EventArgs e)
        {
            try
            {
                //await Launcher.OpenAsync(url)
                await Browser.OpenAsync("http://www.MoralCoding.org"
                                      , BrowserLaunchMode.SystemPreferred);
            }
            catch(Exception ex)
            {
                await DisplayAlert("Problem loading web browser."
                                 , $"Problem: {ex.Message}{Environment.NewLine}Is a browser installed on your device?"
                                 , "OK");
            }
        }
    }
}