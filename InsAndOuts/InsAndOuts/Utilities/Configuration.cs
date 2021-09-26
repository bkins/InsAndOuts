using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

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

    }
}
