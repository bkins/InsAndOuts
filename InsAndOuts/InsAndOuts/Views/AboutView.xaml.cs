using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsAndOuts.Utilities;
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
        }

        private void Email_Tapped(object    sender
                                , EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ForZoeButton_OnClicked(object    sender
                                          , EventArgs e)
        {
            Configuration.BackgroundImage = Configuration.BackgroundImage.IsNullEmptyOrWhitespace() ?
                                                    "HappyMushrooms.jpg" :
                                                    string.Empty;
            
            Configuration.ZoeColor = Configuration.ZoeColor == nameof(Configuration.ZoeColor) ?
                                             "OriginalPrimary" :
                                             nameof(Configuration.ZoeColor);
            
            Application.Current.Resources["Primary"] = Application.Current.Resources[Configuration.ZoeColor];
        }
    }
}