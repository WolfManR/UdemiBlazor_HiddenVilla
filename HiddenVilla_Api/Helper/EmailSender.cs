using Mailjet.Client;
using Mailjet.Client.Resources;

using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla_Api.Helper
{
    public class EmailSender : IEmailSender
    {
        private readonly MailJetSettings mailJetOptions;

        public EmailSender(IOptions<MailJetSettings> mailJetOptions)
        {
            this.mailJetOptions = mailJetOptions.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailjetClient client = new MailjetClient(mailJetOptions.PublicKey, mailJetOptions.PrivateKey);
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
               .Property(
                Send.Messages,
                new JArray {
                     new JObject {
                        { "From", new JObject {
                            {"Email", mailJetOptions.Email},
                            {"Name", "Me"}
                        }},
                        {"To", new JArray {
                            new JObject {
                                {"Email", email},
                                {"Name", "You"}
                            }
                        }},
                        {"Subject", subject},
                        {"HTMLPart", htmlMessage}
                     }
                });
            MailjetResponse response = await client.PostAsync(request);
        }
    }
}
