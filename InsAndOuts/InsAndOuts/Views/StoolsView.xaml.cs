using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using InsAndOuts.Services;
using InsAndOuts.Utilities;
using InsAndOuts.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static InsAndOuts.Utilities.DateTimeFormatter;
using SelectionChangedEventArgs = Syncfusion.SfPicker.XForms.SelectionChangedEventArgs;

namespace InsAndOuts.Views
{
    [QueryProperty(nameof(ViewMode)
                 , nameof(ViewMode))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoolsView : ContentPage, IQueryAttributable
    {   
        public  string          ViewMode        { get; set; }
        public  StoolViewModel  ViewModel       { get; set; }
        private bool            LeftToGetData   { get; set; }
        public  SearchViewModel SearchViewModel { get; set; }

        private bool EditMode { get; set; }
        
        private const string CURRENT_MODEL             = "Stool";
        private const string INITIAL_STOOL_PICKER_TEXT = "Select Bristol Stool Type:";

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            ViewMode = HttpUtility.UrlDecode(query[nameof(ViewMode)]);
            
            if (ViewMode != null 
             && ViewMode.Equals("EDIT"
                              , StringComparison.CurrentCultureIgnoreCase))
            {
                EditMode               = true;
                SearchPicker.IsVisible = true;

                SearchViewModel        = new SearchViewModel();

                SearchPicker.ItemsSource = SearchViewModel.SearchableStools;
                SearchPicker.Focus();

                ToggleControlsVisible();
            }
            
            SelectToolbarItem.IsEnabled = EditMode;
            DeleteToolbarItem.IsEnabled = EditMode;
            
            UpdateViewTitle();
        }

        public StoolsView()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if ( SearchViewModel    != null
            && ! SearchViewModel.SearchableStools.Any())
            {
                await DisplayAlert("Nothing to edit"
                                 , $"There are no {CURRENT_MODEL}s to edit.  Please add {CURRENT_MODEL}s before attempting to edit them."
                                 , "OK").ConfigureAwait(false);

                await PageNavigation.NavigateBackwards().ConfigureAwait(false);
            }
            else
            {
                ResetData();
            
                SearchPicker.IsVisible = EditMode;

                if (PageCommunication.Instance.IntegerValue == 0)
                    return;

                DescriptionHtmlRtEditor.Text = PageCommunication.Instance.CachedStringValue;
                SelectedStoolTypeLabel.Text  = $"Type {PageCommunication.Instance.IntegerValue}: {PageCommunication.Instance.StringValue}";
                PageCommunication.Instance.Clear();
            }
            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            if ( ! LeftToGetData)
            {
                PageCommunication.Instance.Clear();
            }
        }

        private void ResetData()
        {
            DescriptionHtmlRtEditor.Text = PageCommunication.Instance.CachedStringValue;
            
            SelectedStoolTypeLabel.Text  = INITIAL_STOOL_PICKER_TEXT;
            ImageFromCamera.Source       = new FileImageSource();
            WhenDatePicker.Date          = DateTime.Today;
            WhenTimePicker.Time          = DateTime.Now.TimeOfDay;
            
            SetSaveButtonNotSaved();

            ViewModel = new StoolViewModel();

            UpdateViewTitle();
        }

        private void UpdateViewTitle()
        {
            Title = $"{ViewMode.ToTitleCase(force: true)} {CURRENT_MODEL}";
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
            //BENDO: Refactor this method
            TakePictureImage.IsEnabled = false;

            if ( ! await ReadyToTakePicture())
            {
                TakePictureImage.IsEnabled = true;
                return;
            }

            try
            {
                var photo = await GetImage();

                if (await WasImageReturnedFromCamera(photo))
                {
                    TakePictureImage.IsEnabled = true;

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

                ViewModel.Stool.ImageFileName = $"{CURRENT_MODEL}{ViewModel.Stool.Id}.jpg";
                LeftToGetData                 = false;
            }
            catch (ObjectDisposedException disposedException)
            {
                await DisplayAlert("ObjectDisposedException"
                                 , disposedException.Message
                                 , "OK");
            }
            catch (Exception exception)
            {
                await DisplayAlert("Exception"
                                 , exception.Message
                                 , "OK");
            }
            finally
            {
                TakePictureImage.IsEnabled = true;
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
            LeftToGetData = true;
            var file = await CrossMedia.Current
                                       .TakePhotoAsync(new StoreCameraMediaOptions
                                                       {
                                                           PhotoSize = PhotoSize.Medium
                                                         , Directory = $"{CURRENT_MODEL}s"
                                                         , Name      = $"{CURRENT_MODEL}{ViewModel.Stool.Id}.jpg"
                                                       });
            
            return file;
        }

        private async Task<bool> ReadyToTakePicture()
        {
            if (SelectedStoolTypeLabel.Text == INITIAL_STOOL_PICKER_TEXT)
            {
                await DisplayAlert($"Select a {CURRENT_MODEL} type first."
                                 , $"Please select a {CURRENT_MODEL} type before taking a picture."
                                 , "OK");

                return false;
            }

            //Open camera
            if (! CrossMedia.Current.IsCameraAvailable
             || ! CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera"
                                 , "This feature will not work with your device."
                                 , "OK");

                return false;
            }

            return true;
        }
        
        private void SaveButton_OnClicked(object    sender
                                        , EventArgs e)
        {
            SaveButton.IsEnabled = false;

            SetViewModelDataFromPage();
            ViewModel.Save();
            
            ResetSearchViewModel();
            
            ResetData();

            ResetSaveButton(sender);

            SaveButton.IsEnabled = true;
        }

        private void SetViewModelDataFromPage()
        {
            ViewModel.Stool.DescriptionPainText = DescriptionHtmlRtEditor.Text;
            ViewModel.Stool.DescriptionHtml     = DescriptionHtmlRtEditor.HtmlText;
            ViewModel.Stool.StoolType           = SelectedStoolTypeLabel.Text;

            //StoolType set on Unfocus of the slider control
            //Image and ImageFile set in the tapped event of the camera icon
            ViewModel.Stool.When = DateTimeTimeSpanForSaving(WhenDatePicker.Date
                                                           , WhenTimePicker.Time);
        }

        private void ResetSaveButton(object sender)
        {
            if (sender is SfButton button)
            {
                SetSaveButtonSaved(button);
            }
        }

        private void ResetSearchViewModel()
        {
            if ( ! EditMode)
                return;

            SearchViewModel = new SearchViewModel();

            SearchPicker.ItemsSource = SearchViewModel.SearchableStools;
        }

        private void SearchPicker_OnOkButtonClicked(object                    sender
                                                  , SelectionChangedEventArgs e)
        {
            if (FindTextInVewModel(SearchPicker.SelectedItem.ToString()))
                return;

            LoadPageFromViewModel();

            ToggleControlsVisible();

            SearchPicker.IsVisible = false;
        }
        
        private async void SearchPicker_OnCancelButtonClicked(object                    sender
                                                            , SelectionChangedEventArgs e)
        {
            await PageNavigation.NavigateBackwards();
        }

        private bool FindTextInVewModel(string searchText)
        {
            ViewModel = new StoolViewModel(searchText);

            if (ViewModel.Stool != null)
                return false;

            DisplayAlert($"{CURRENT_MODEL} not found"
                       , $"Something went wrong while retrieving the {CURRENT_MODEL}."
                       , "OK");

            return true;

        }

        private void LoadPageFromViewModel()
        {
            if (ViewModel.Stool.Image.Length > 0)
            {
                var imageStream = new MemoryStream(ViewModel.Stool.Image);
                //ImageFromCamera.Source = ImageSource.FromStream(() => imageStream);
                ImageFromCamera.Source = ImageSource.FromStream(() => new MemoryStream(ViewModel.Stool.Image));
            }

            DescriptionHtmlRtEditor.HtmlText = ViewModel.Stool.DescriptionHtml;
            SelectedStoolTypeLabel.Text      = ViewModel.Stool.StoolType;
            WhenDatePicker.Date              = ViewModel.Stool.WhenToDateTime();
            WhenTimePicker.Time              = ViewModel.Stool.WhenToTimeSpan();
        }

        private void SelectToolbarItem_OnClicked(object    sender
                                               , EventArgs e)
        {
            SelectToolbarItem.IsEnabled = false;
            ToggleControlsVisible();

            SearchPicker.IsVisible = true;

            SelectToolbarItem.IsEnabled = true;
        }

        private async void DeleteToolbarItem_OnClicked(object    sender
                                               , EventArgs e)
        {
            DeleteToolbarItem.IsEnabled = false;
            ViewModel?.Delete();
            UserInteraction.Toast($"{CURRENT_MODEL} Deleted");
            await PageNavigation.NavigateBackwards();

            DeleteToolbarItem.IsEnabled = true;
        }
        
        private void ToggleControlsVisible()
        {
            SaveButton.IsVisible              = ! SaveButton.IsVisible;
            WhenDatePicker.IsVisible          = ! WhenDatePicker.IsVisible;
            WhenTimePicker.IsVisible          = ! WhenTimePicker.IsVisible;
            DescriptionHtmlRtEditor.IsVisible = ! DescriptionHtmlRtEditor.IsVisible;
            SelectedStoolTypeLabel.IsVisible  = ! SelectedStoolTypeLabel.IsVisible;
            TakePictureImage.IsVisible        = ! TakePictureImage.IsVisible;
        }

        private async void SelectedStoolTypeLabel_Tapped(object    sender
                                                       , EventArgs e)
        {
            SelectedStoolTypeLabel.IsEnabled             = false;
            LeftToGetData                                = true;
            PageCommunication.Instance.CachedStringValue = DescriptionHtmlRtEditor.HtmlText;

            await PageNavigation.NavigateTo(nameof(PopUpPickerView));

            SelectedStoolTypeLabel.IsEnabled = true;
        }
    }
}