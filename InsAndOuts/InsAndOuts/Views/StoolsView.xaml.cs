using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using InsAndOuts.Utilities;
using InsAndOuts.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InsAndOuts.Views
{
    [QueryProperty(nameof(ViewMode)
                 , nameof(ViewMode))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoolsView : ContentPage, IQueryAttributable
    {   
        public  string          ViewMode          { get; set; }
        public  StoolViewModel  ViewModel         { get; set; }
        private bool            LeftToTakePicture { get; set; }
        public  SearchViewModel SearchViewModel   { get; set; }

        private bool EditMode { get; set; }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            ViewMode = HttpUtility.UrlDecode(query[nameof(ViewMode)]);
            
            if (ViewMode != null 
             && ViewMode.Equals("EDIT"
                              , StringComparison.CurrentCultureIgnoreCase))
            {
                EditMode = true;

                SearchPicker.IsVisible = true;
                SearchViewModel        = new SearchViewModel();

                SearchPicker.ItemsSource = SearchViewModel.SearchableStools;
                SearchPicker.Focus();
            }
        }
        public StoolsView()
        {
            InitializeComponent();
            ResetData();

            DescriptionHtmlRtEditor.AlignLeft();
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
            StoolTypePicker.SelectedItem    = null;
            ImageFromCamera.Source          = new FileImageSource();
            WhenDatePicker.Date             = DateTime.Today;
            WhenTimePicker.Time             = DateTime.Now.TimeOfDay;
            
            SetSaveButtonNotSaved();

            ViewModel = new StoolViewModel();

            UpdateViewTitle();
        }

        private void UpdateViewTitle()
        {
            
            var addUpdate = ViewModel.Stool.Id == 0 ?
                                    "Add New" :
                                    "Update";
            //BENDO: On release to user(s) remove the ID for the Title
            Title = $"{addUpdate} Stool ({ViewModel.Stool.Id})";
        }

        private void SetSaveButtonNotSaved()
        {
            SaveButton.ShowIcon      = false;
            SaveButton.Text          = "SAVE";
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
        
        private void DescriptionHtmlRtEditor_OnFocused(object    sender
                                                     , EventArgs e)
        {
            SetSaveButtonNotSaved();
        }

        private void WhenDatePicker_OnFocused(object         sender
                                            , FocusEventArgs e)
        {
            SetSaveButtonNotSaved();
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
            
            try
            {
                var photo = await GetImage();

                if (await WasImageReturnedFromCamera(photo))
                {
                    return;
                }

                ImageFromCamera.Source = ImageSource.FromStream(() => photo.GetStream());
                
                using (var memory = new MemoryStream()) 
                {
                    var stream = photo.GetStream();
                    await stream.CopyToAsync(memory);
                    ViewModel.Stool.Image = memory.ToArray();
                }
                
                ImageFromCamera.IsVisible = true;
                
                ViewModel.Stool.ImageFileName = $"stool{ViewModel.Stool.Id}.jpg";
                LeftToTakePicture             = false;
            }
            catch (ObjectDisposedException disposedException)
            {
                await DisplayAlert("ObjectDisposedException", disposedException.Message, "OK");
            }
            catch (Exception exception)
            {
                await DisplayAlert("Exception", exception.Message, "OK");
            }
        }
        
        private async Task<bool> WasImageReturnedFromCamera(MediaFile file)
        {
            if (file != null)
                return false;

            await DisplayAlert("No picture found"
                             , "Unable to get photo. Please try again"
                             , "OK");

            return true;

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
            ViewModel.Stool.DescriptionPainText = DescriptionHtmlRtEditor.Text;
            ViewModel.Stool.DescriptionHtml     = DescriptionHtmlRtEditor.HtmlText;
            //StoolType set on Unfocus of the slider control
            //Image and ImageFile set in the tapped event of the camera icon
            ViewModel.Stool.When                = GetSelectDateTimeFromPickers();

            ViewModel.Save();
            
            if (EditMode)
            {
                SearchViewModel = new SearchViewModel();

                SearchPicker.ItemsSource = SearchViewModel.SearchableStools;
            }

            UpdateViewTitle();

            if (sender is SfButton button)
                SetSaveButtonSaved(button);
        }
        
        private void SearchPicker_OnSelectedIndexChanged(object    sender
                                                       , EventArgs e)
        {
            var picker = sender as Picker ?? new Picker();
            
            if (picker.SelectedItem == null)
            {
                return;
            }

            ViewModel = new StoolViewModel(picker.SelectedItem.ToString());

            if (ViewModel.Stool == null)
            {
                DisplayAlert("Stool not found"
                           , "Something went wrong while retrieving the stool."
                           , "OK");
                return;
            }
            
            DescriptionHtmlRtEditor.HtmlText = ViewModel.Stool.DescriptionHtml;

            if (ViewModel.Stool.Image.Length > 0)
            {
                var imageStream = new MemoryStream(ViewModel.Stool.Image);
                ImageFromCamera.Source = ImageSource.FromStream(() => imageStream);
            }
            
            StoolTypePicker.SelectedItem = ViewModel.Stool.StoolType;
            WhenDatePicker.Date          = ViewModel.Stool.WhenToDateTime();
            WhenTimePicker.Time          = ViewModel.Stool.WhenToTimeSpan();
            
            UpdateViewTitle();
        }
    }
}