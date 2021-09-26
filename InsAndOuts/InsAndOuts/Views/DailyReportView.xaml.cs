using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InsAndOuts.Models;
using InsAndOuts.Services;
using InsAndOuts.Utilities;
using InsAndOuts.ViewModels;
using Plugin.Media.Abstractions;
using Syncfusion.XForms.RichTextEditor;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SelectionChangedEventArgs = Syncfusion.SfPicker.XForms.SelectionChangedEventArgs;

namespace InsAndOuts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DailyReportView : ContentPage
    {
        private static string   EmailToDoctor => Configuration.EmailToDoctor;
        private static DateTime DateToReport  { get; set; }

        public  DailyReportViewModel ViewModel     { get; set; }

        public  ICommand ShowReportDatePickerCommand => new Command(ReportDatePickedLabel_Tapped);

        public DailyReportView()
        {
            InitializeComponent();

            DateToReport = DateTime.Today;
            GenerateReport();
        }

        private void GenerateReport()
        {
            ViewModel                    = new DailyReportViewModel(DateToReport);
            ReportPlainText.Text         = ViewModel.ToPainText();
            ReportHtml.HtmlText          = ViewModel.ToHtml();
            SendTo.Text                  = EmailToDoctor;

            var datesWIthData = new ObservableCollection<string>(ViewModel.DatesWithData);

            if ( ! datesWIthData.Any())
            {
                datesWIthData.Add("No date to select.");
                ReportDatePicker.OKButtonTextColor = Color.Gray;
            }

            ReportDatePicker.ItemsSource = datesWIthData;
        }

        private async void SendReport_OnClicked(object    sender
                                        , EventArgs e)
        {
            try
            {
                SendEmail();
            }
            catch (FeatureNotSupportedException exception)
            {
                await DisplayAlert("Feature Not Supported"
                                 , $"{exception.Message}{Environment.NewLine}(Your device is not setup to send emails.)"
                                 , "OK");
            }
            
        }

        private async void SendEmail()
        {
            var stoolsWithImages = ViewModel.Stools
                                            .Where(field => field.Image.Any())
                                            .ToList();

            var listOfAttachments = GetListOfEmailAttachments(stoolsWithImages);
            
            EmailBodyFormat emailFormat;
            string          body;

            if (HtmlSwitch.IsToggled 
             && Device.RuntimePlatform != Device.UWP)
            {
                emailFormat = EmailBodyFormat.Html;
                body        = ViewModel.ToHtml();
            }
            else
            {
                emailFormat = EmailBodyFormat.PlainText;
                body        = ViewModel.ToPainText();
            }

            var emailer = new Emailer
                          {
                              EmailFormat = emailFormat
                            , Recipients = Configuration.EmailToDoctor.Split(';')
                                                        .ToList()
                            , SubjectPrefix = string.Empty
                          };

            await emailer.SendEmail($"Ins & Outs Report for {DateToReport.Date.ToShortDateString()}"
                                  , body
                                  , listOfAttachments);
        }

        private static List<EmailAttachment> GetListOfEmailAttachments(List<Stool> stoolsWithImages)
        {
            var listOfAttachments = new List<EmailAttachment>();
            
            foreach (var stoolWithImage in stoolsWithImages)
            {
                System.IO.File.WriteAllBytes($@".\attachments\{stoolWithImage.ImageFileName}"
                                           , stoolWithImage.Image);

                var attachment = new EmailAttachment($@".\attachments\{stoolWithImage.ImageFileName}"
                                                   , "image/jpg");

                listOfAttachments.Add(attachment);
            }

            return listOfAttachments;
        }

        private void HtmlSwitch_OnToggled(object           sender
                                        , ToggledEventArgs e)
        {
            ReportHtml.IsVisible      = HtmlSwitch.IsToggled;
            ReportPlainText.IsVisible = ! ReportHtml.IsVisible;
        }

       
        private void ReportDatePicker_OnOkButtonClicked(object                    sender
                                                      , SelectionChangedEventArgs e)
        {
            //I couldn't figure out to disable the OK, button.  So call cancel event and bail out.
            if ( ! ViewModel.DatesWithData.Any())
            {
                ReportDatePicker_OnCancelButtonClicked(sender, e);
                return;
            }

            ReportDatePickedLabel.Text = ReportDatePicker.SelectedItem.ToString();
            
            DateToReport               = DateTime.Parse(ReportDatePickedLabel.Text);

            GenerateReport();
            ShowReportDatePicker(false);
        }

        private void ReportDatePicker_OnCancelButtonClicked(object                    sender
                                                          , SelectionChangedEventArgs e)
        {
            ReportDatePickedLabel.Text = "Pick a date...";

            ShowReportDatePicker(false);
        }

        private void ShowReportDatePicker(bool show)
        {
            ReportDatePicker.IsOpen         = show;
            //ReportDatePicker.IsVisible      = show;
            //ReportDatePickedLabel.IsVisible = ! show;
        }
        
        private void ReportDatePickedLabel_Tapped()
        {
            ShowReportDatePicker(true);
        }

    }
}