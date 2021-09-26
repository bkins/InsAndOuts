using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using InsAndOuts.Utilities;
using InsAndOuts.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InsAndOuts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigurationView : ContentPage, INotifyPropertyChanged
    {
        private ConfigurationViewModel ViewModel { get; set; }

        public bool UseHtmlForEmailBody
        {
            get => Configuration.UseHtmlForEmailBody;
            set
            {
                if (UseHtmlForEmailBody == value)
                {
                    return;
                }

                Configuration.UseHtmlForEmailBody = value;

                OnPropertyChanged(nameof(UseHtmlForEmailBody));
            }
        }

        public string EmailToDoctor
        {
            get => Configuration.EmailToDoctor;
            set
            {
                if (EmailToDoctor == value)
                {
                    return;
                }

                Configuration.EmailToDoctor = value;

                OnPropertyChanged(nameof(EmailToDoctor));
            }
        }
        
        public new event PropertyChangedEventHandler PropertyChanged;

        public ConfigurationView()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
            ViewModel = new ConfigurationViewModel();

            //EmailToDoctor            = "test";
            EmailToDoctorEditor.Text = EmailToDoctor;
            UseHtmlSwitch.IsToggled  = UseHtmlForEmailBody;

        }

        
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            handler?.Invoke(this
                          , new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T                     backingField
                                 , T                         value
                                 , [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField
                                                 , value))
            {
                return;
            }

            backingField = value;

            OnPropertyChanged(propertyName);
        }

        private void EmailToDoctorEditor_OnUnfocused(object         sender
                                                   , FocusEventArgs e)
        {
            if (EmailToDoctor != EmailToDoctorEditor.Text)
            {
                EmailToDoctor = EmailToDoctorEditor.Text;
            }
        }
        
        private void UseHtmlSwitch_OnToggled(object           sender
                                           , ToggledEventArgs e)
        {
            UseHtmlForEmailBody = UseHtmlSwitch.IsToggled;
        }

        private void ClearSettings_OnClicked(object    sender
                                           , EventArgs e)
        {
            EmailToDoctorEditor.Text = string.Empty;
            Preferences.Clear();
        }

        private async void ClearData_OnClicked(object    sender
                                       , EventArgs e)
        {
            var clearData = await DisplayAlert("Delete All Data?"
                                             , "This will delete ALL data and cannot be undone.\r\nAre you sure you would like to continue?"
                                             , "Yes"
                                             , "No");

            if (clearData)
            {
                ViewModel.ClearData();
            }
        }
        
        private async void DoneButton_OnClicked(object    sender
                                              , EventArgs e)
        {
            await PageNavigation.NavigateTo(nameof(HomePage));
        }

        private void AdvancedSwitch_OnToggled(object           sender
                                            , ToggledEventArgs e)
        {
            MiddleStackLayout.IsVisible = AdvancedSwitch.IsToggled;
        }
    }
}