using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace InsAndOuts.Services
{
    public class Emailer
    {
        public string          SubjectPrefix { get; set; }
        public EmailBodyFormat EmailFormat   { get; set; }
        public List<string>    Recipients    { get; set; }

        public async Task SendEmail(string subject, string body, List<EmailAttachment> attachments = null)
        {
            try
            {
                var message = new EmailMessage
                              {
                                  Subject    = $"{SubjectPrefix}{subject}"
                                , Body       = body
                                , To         = Recipients
                                , BodyFormat = EmailFormat
                              };

                if (attachments != null
                && attachments.Any())
                {
                    message.Attachments = attachments;
                }

                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException featureNotSupportedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
