using System;
using System.Collections.Generic;
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

            VersionNumberSpan.Text = $" version: {VersionTracking.CurrentVersion}";
            BuildNumberSpan.Text   = $" ({VersionTracking.CurrentBuild})";
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