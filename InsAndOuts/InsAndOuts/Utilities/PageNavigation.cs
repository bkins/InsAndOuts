using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InsAndOuts.Utilities
{
    public static class PageNavigation
    {
        public static async Task NavigateTo(string nameOfPage)
        {
            try
            {
                Navigate($"{nameOfPage}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                throw;
            }

            //await 
        }

        public static async Task NavigateTo(string nameOfPage
                                          , string nameOfParameter1
                                          , string valueOfParameter1)
        {
                await Navigate($"{nameOfPage}?{nameOfParameter1}={valueOfParameter1}");
            
        }

        public static async Task NavigateTo(string nameOfPage
                                          , string nameOfParameter1
                                          , string valueOfParameter1
                                          , string nameOfParameter2
                                          , string valueOfParameter2)
        {
            await Navigate($"{nameOfPage}?{nameOfParameter1}={valueOfParameter1}&{nameOfParameter2}={valueOfParameter2}");
        }

        private static async Task Navigate(string path)
        {
            try
            {
                Shell.Current.GoToAsync(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                throw;
            }
            
        }

        public static async Task NavigateBackwards()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
