using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using InsAndOuts.Models;
using InsAndOuts.Services;
using InsAndOuts.Utilities;
using InsAndOuts.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SelectionChangedEventArgs = Syncfusion.SfPicker.XForms.SelectionChangedEventArgs;

namespace InsAndOuts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DailyReportView : ContentPage
    {
        private static string   EmailToDoctor        => Configuration.EmailToDoctor;
        private static DateTime DateToReport         { get; set; }
        public static  string   DefaultSfPickerValue { get; set; }

        public  DailyReportViewModel ViewModel     { get; set; }

        public  ICommand ShowReportDatePickerCommand => new Command(ReportDatePickedLabel_Tapped);

        public DailyReportView()
        {
            InitializeComponent();

            DateToReport         = DateTime.Today;

            ToggleHtmlAndPainTextReport(Configuration.UseHtmlForEmailBody);

            GenerateReport();
        }

        private void GenerateReport()
        {
            ViewModel                  = new DailyReportViewModel(DateToReport);
            ReportPlainText.Text       = ViewModel.ToPainText();
            ReportHtml.HtmlText        = ViewModel.ToHtml();
            SendTo.Text                = EmailToDoctor;
            ReportDatePickedLabel.Text = DateToReport.ToShortDateString();

            var datesWIthData = new ObservableCollection<string>(ViewModel.DatesWithData);

            if ( ! datesWIthData.Any())
            {
                datesWIthData.Add("No date to select.");
                ReportDatePicker.OKButtonTextColor = Color.Gray;
            }

            ReportDatePicker.ItemsSource = datesWIthData;
            ReportDatePicker.SelectedItem = DateToReport.ToShortDateString();
        }

        private  void SendReport_OnClicked(object    sender
                                        , EventArgs e)
        {
            SendEmail();
        }

        private async void SendEmail()
        {
            var stoolsWithImages = ViewModel.Stools
                                            .Where(field => field.Image.Any())
                                            .ToList();

            var listOfAttachments = GetListOfEmailAttachments(stoolsWithImages);

            //if (HtmlSwitch.IsToggled 
            // && Device.RuntimePlatform != Device.UWP)
            //{

            //    emailFormat = EmailBodyFormat.Html;
            //    body        = ViewModel.ToHtml();
            //}
            //else
            //{
            //    emailFormat = EmailBodyFormat.PlainText;
            //    body        = ViewModel.ToPainText();
            //}
            
            var emailFormat = EmailBodyFormat.PlainText;
            
            var body = Configuration.UseHtmlForEmailBody ?
                               ViewModel.ToHtml() :
                               ViewModel.ToPainText();
    
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
        
        private List<EmailAttachment> GetListOfEmailAttachments(List<Stool> stoolsWithImages)
        {
            var listOfAttachments = new List<EmailAttachment>();

            try
            {
                foreach (var stoolWithImage in stoolsWithImages)
                {
                    var attachmentFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                                                        , stoolWithImage.ImageFileName);

                    File.WriteAllBytes(attachmentFilePath
                                     , stoolWithImage.Image);
                    
                    var attachment = new EmailAttachment(attachmentFilePath
                                                       , "image/jpg");

                    listOfAttachments.Add(attachment);
                }
            }
            catch (Exception e)
            {
                DisplayAlert("Error!"
                           , $"{e.Message}{Environment.NewLine}{e.StackTrace}", "OK");

                //BENDO: implement error logging and reporting of errors
                //Log the error
                //Ask user if they would like to email the error report (Stack Trace) to Developer
                //Consider implementing a page for managing error logs/reports
            }

            return listOfAttachments;
        }

        private void HtmlSwitch_OnToggled(object           sender
                                        , ToggledEventArgs e)
        {
            ToggleHtmlAndPainTextReport(HtmlSwitch.IsToggled);
        }

        private void ToggleHtmlAndPainTextReport(bool showHtml)
        {
            ReportHtml.IsVisible      = showHtml;
            ReportPlainText.IsVisible = ! showHtml;

            Configuration.UseHtmlForEmailBody = HtmlSwitch.IsToggled;
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