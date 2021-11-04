using System;
using System.Collections.Generic;
using System.Text;
using InsAndOuts.Utilities.Interfaces;
using Xamarin.Forms;

namespace InsAndOuts.Services
{
    public static class UserInteraction
    {
        public static void Toast(string message)
        {
            DependencyService.Get<IMessage>()
                             .LongAlert(message);
        }
    }
}
