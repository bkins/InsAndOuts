using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace InsAndOuts.Utilities
{
    public static class Configuration
    {

        public static bool UseHtmlForEmailBody
        {
            get => Preferences.Get(nameof(UseHtmlForEmailBody)
                                 , false);
            set => Preferences.Set(nameof(UseHtmlForEmailBody)
                                 , value);
        }
        
        public static string EmailToDoctor
        {
            get => Preferences.Get(nameof(EmailToDoctor)
                                 , string.Empty);
            set => Preferences.Set(nameof(EmailToDoctor)
                                 , value);
        }

        public static string BackgroundImage
        {
            get => Preferences.Get(nameof(BackgroundImage)
                                 , string.Empty);
            set => Preferences.Set(nameof(BackgroundImage)
                                 , value);
        }

        public static string ZoeColor
        {
            get => Preferences.Get(nameof(ZoeColor)
                                 , "OriginalPrimary");
            set => Preferences.Set(nameof(ZoeColor)
                                 , value);
        }

    }
    
}
