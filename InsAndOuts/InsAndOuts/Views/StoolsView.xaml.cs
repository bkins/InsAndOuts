using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsAndOuts.Utilities;
using InsAndOuts.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InsAndOuts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoolsView : ContentPage, IDisposable
    {
        
        public  StoolViewModel ViewModel         { get; set; }
        private bool           LeftToTakePicture { get; set; }

        public StoolsView()
        {
            InitializeComponent();
            ResetData();

            DescriptionHtmlRtEditor.AlignLeft();

            DescriptionHtmlRtEditor.IsVisible    = Configuration.UseHtmlForEmailBody;
            DescriptionPlainTextEditor.IsVisible = ! DescriptionHtmlRtEditor.IsVisible;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            if (LeftToTakePicture)
            {
                return;
            }

            ResetData();
        }
        private void ResetData()
        {
            DescriptionPlainTextEditor.Text  = string.Empty;
            //DescriptionHtmlRtEditor.Text     = string.Empty;
            //DescriptionHtmlRtEditor.HtmlText = string.Empty;
            StoolTypePicker.SelectedItem = null;
            ImageFromCamera.Source       = new FileImageSource();
            WhenDatePicker.Date          = DateTime.Today;
            WhenTimePicker.Time          = DateTime.Now.TimeOfDay;
            
            ViewModel = new StoolViewModel();

            UpdateViewTitle();
        }

        private void UpdateViewTitle()
        {
            //For now this will always be "Add New"
            //However, the idea will be that I will add feature to update the stools and this will be the view I will use.
            //BENDO: On release to user(s) remove the ID for the Title
            var addUpdate = ViewModel.Stool.Id == 0 ?
                                    "Add New" :
                                    "Update";

            Title = $"{addUpdate} Stool ({ViewModel.Stool.Id})";
        }

        private void SetSaveButtonNotSaved()
        {
            SaveButton.ShowIcon = false;
            SaveButton.Text     = "SAVE";
        }
        
        private void SetSaveButtonSaved(SfButton button)
        {
            button.ShowIcon = true;
            button.Text     = "SAVED";
        }

        private void StoolTypePicker_OnSelectedIndexChanged(object    sender
                                                          , EventArgs e)
        {
            var picker = sender as Picker ?? new Picker();
            
            if (picker.SelectedItem == null)
            {
                return;
            }

            ViewModel.Stool.StoolType = picker.SelectedItem.ToString();
        }

        private void DescriptionPlainTextEditor_OnUnfocused(object         sender
                                                          , FocusEventArgs e)
        {
            ViewModel.Stool.DescriptionPainText = DescriptionPlainTextEditor.Text;
            ViewModel.SaveStool();
        }

        private void DescriptionHtmlRtEditor_OnUnfocused(object    sender
                                                       , EventArgs e)
        {
            ViewModel.Stool.DescriptionHtml = DescriptionHtmlRtEditor.HtmlText;
            ViewModel.SaveStool();
        }

        private void DescriptionHtmlRtEditor_OnFocused(object    sender
                                                     , EventArgs e)
        {
            SetSaveButtonNotSaved();
        }

        private void WhenDatePicker_OnUnfocused(object         sender
                                              , FocusEventArgs e)
        {
            ViewModel.Stool.When = GetSelectDateTimeFromPickers();
            ViewModel.SaveStool();
        }   

        private void WhenDatePicker_OnFocused(object         sender
                                            , FocusEventArgs e)
        {
            SetSaveButtonNotSaved();
        }

        private void WhenTimePicker_OnUnfocused(object         sender
                                              , FocusEventArgs e)
        {
            ViewModel.Stool.When = GetSelectDateTimeFromPickers();
            ViewModel.SaveStool();
        }

        private void WhenTimePicker_OnFocused(object         sender
                                            , FocusEventArgs e)
        {
            SetSaveButtonNotSaved();
        }

        private async void TakePictureImage_Tapped(object    sender
                                           , EventArgs e)
        {
            if (await ReadyToTakePicture())
                return;
            
                //try
                //{
                //    PictureFile = await GetImage();
                //}
                //catch (ObjectDisposedException disposedException)
                //{
                //    Console.WriteLine("Caught: {0}", disposedException.Message);
                //}
                //catch (Exception exception)
                //{
                //    Console.WriteLine(exception);

                //    throw;
                //}
            try
            {
                var photo = await GetImage();
                
                ImageFromCamera.Source = ImageSource.FromStream(() => photo.GetStream());
                
                using (var memory = new MemoryStream()) 
                {
                    var stream = photo.GetStream();
                    await stream.CopyToAsync(memory);
                    ViewModel.Stool.Image = memory.ToArray();
                }
                
                //ImageFromCamera.Source = ImageSource.FromStream(() => GetImage().Result.GetStream());
                //ViewModel.Stool.Image  = File.ReadAllBytes(ImageFromCamera.Source);
                //using (var file = await GetImage())
                //{
                //    ImageFromCamera.Source = ImageSource.FromStream(() => GetImage().Result.GetStream());

                //    //<User takes picture>
                //    if (await WasImageReturnedFromCamera(file))
                //        return;
                    
                //    ViewModel.Stool.Image = File.ReadAllBytes(file.Path);

                //    //using (var fileStream = file.GetStream())
                //    //{
                //        if (ImageFromCamera.Source == null)
                //        {
                //            //Get picture
                //            ImageFromCamera.Source = ImageSource.FromStream(() => file.GetStream());
                //        }
                //    //}
                //}
                
                ImageFromCamera.IsVisible = true;
                
                ViewModel.Stool.ImageFileName = $"stool{ViewModel.Stool.Id}.jpg";
                LeftToTakePicture             = false;
            }
            catch (ObjectDisposedException disposedException)
            {
                await DisplayAlert("ObjectDisposedException", disposedException.Message, "OK");

                //throw;
            }
            catch (Exception exception)
            {
                await DisplayAlert("Exception", exception.Message, "OK");

                //throw;
            }
            //Save
            //ViewModel.SaveStool();
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
            LeftToTakePicture = true;
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

        private string GetSelectDateTimeFromPickers()
        {
            var dateTimeToSave = $"{WhenDatePicker.Date.ToShortDateString()} {WhenTimePicker.Time.ToString("g", CultureInfo.CreateSpecificCulture("en-US"))}";
            return dateTimeToSave;
        }

        private void SaveButton_OnClicked(object    sender
                                        , EventArgs e)
        {
            ViewModel.SaveStool();

            if (sender is SfButton button)
                SetSaveButtonSaved(button);

            //ResetData();
        }

        private void DescriptionPlainTextEditor_OnFocused(object         sender
                                                        , FocusEventArgs e)
        {
            SetSaveButtonNotSaved();
        }

        public void Dispose()
        {
            
        }
    }
}