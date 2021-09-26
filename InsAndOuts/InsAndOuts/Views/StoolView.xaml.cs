using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsAndOuts.Models;
using InsAndOuts.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InsAndOuts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoolView : ContentPage
    {
        public StoolViewModel ViewModel { get; set; }

        public StoolView()
        {
            InitializeComponent();
            ViewModel           = new StoolViewModel();
            WhenDatePicker.Date = DateTime.Now;
            WhenTimePicker.Time = DateTime.Now.TimeOfDay;
        }

        private void DescriptionEditor_OnUnfocused(object         sender
                                                 , FocusEventArgs e)
        {
            ViewModel.Stool.DescriptionPainText = DescriptionEditor.Text;
            ViewModel.SaveStool();
        }

        private void WhenDatePicker_OnUnfocused(object         sender
                                              , FocusEventArgs e)
        {
            ViewModel.Stool.When = GetSelectDateTimeFromPickers();
            ViewModel.SaveStool();
        }

        private void WhenTimePicker_OnUnfocused(object         sender
                                              , FocusEventArgs e)
        {
            ViewModel.Stool.When = GetSelectDateTimeFromPickers();
            ViewModel.SaveStool();
        }
        
        private string GetSelectDateTimeFromPickers()
        {
            var dateTimeToSave = $"{WhenDatePicker.Date.ToShortDateString()} {WhenTimePicker.Time.ToString("g", CultureInfo.CreateSpecificCulture("en-US"))}";
            return dateTimeToSave;
        }
        
        private async void TakePictureImage_Tapped(object    sender
                                           , EventArgs e)
        {
            if (await ReadyToTakePicture())
                return;

            var file = await GetImage();  

            //<User takes picture>
            if (await WasImageReturnedFromCamera(file))
                return;

            //Show location
            await DisplayAlert("File Location", file.Path, "OK");

            //Get picture
            ImageFromCamera.Source = ImageSource.FromStream(() => file.GetStream());  

            //Convert to byte[]
            //Assign it to ViewModel.Stool.Image
            ViewModel.Stool.Image         = File.ReadAllBytes(file.Path);
            ViewModel.Stool.ImageFileName = $"stool{ViewModel.Stool.Id}.jpg";
            file.Dispose();

            //Save
            ViewModel.SaveStool();
        }

        private async Task<bool> WasImageReturnedFromCamera(MediaFile file)
        {
            if (file == null)
            {
                await DisplayAlert("No picture found"
                                 , "Unable to get photo. Please try again"
                                 , "OK");

                return true;
            }

            return false;
        }

        private async Task<MediaFile> GetImage()
        {
            var file = await CrossMedia.Current
                                       .TakePhotoAsync(new StoreCameraMediaOptions
                                                       {
                                                           PhotoSize = PhotoSize.Medium
                                                         , Directory = "Stools"
                                                         , Name      = $"stool{ViewModel.Stool.Id}.jpg"
                                                       });
            
            return file;
        }

        private async Task<bool> ReadyToTakePicture()
        {
            //Ensure a stool has been created first
            if (ViewModel.Stool.Id == 0)
            {
                await DisplayAlert("Define the stool first."
                                 , "Please enter at least a description first."
                                 , "OK");

                return true;
            }

            //Open camera
            if (! CrossMedia.Current.IsCameraAvailable
             || ! CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera"
                                 , "This feature will not work with your device."
                                 , "OK");

                return true;
            }

            return false;
        }

        private void Picker_OnSelectedIndexChanged(object    sender
                                                 , EventArgs e)
        {
            var picker = sender as Picker ?? new Picker();
            ViewModel.Stool.StoolType = picker.SelectedItem.ToString();
        }
        
    }
}