using System.Net.Mail;
using System.Net;

using DevsTutorialCenterAPI.Models;
using DevsTutorialCenterAPI.Services.Abstractions;


namespace DevsTutorialCenterAPI.Services.Implementations;

public class MessengerService : IMessengerService
{
    private readonly Dictionary<string, string> _config;
    public MessengerService(IConfiguration config)
    {
        _config = config.GetRequiredSection("EmailSettings").GetChildren()
            .ToDictionary( child => child.Key, child => child.Value);
    }

    public string Send(Message message, string AttachmentPath = "")
    {
        try
        {
            
            string GmailAccount = _config["SenderEmail"];
            string GmailPassword = _config["AppPassword"];
            IList<string> ToEmails = message.To;

            MailMessage appMail = new();

            foreach (string toEmail in ToEmails)
            {
                appMail.To.Add(toEmail);
            }
            appMail.From = new MailAddress(GmailAccount);
            appMail.Sender = new MailAddress(GmailAccount);
            appMail.Subject = message.Subject;
            appMail.Body = message.Body;
            appMail.IsBodyHtml = true;

            if (!string.IsNullOrEmpty(AttachmentPath))
            {
                Attachment attachment = new(AttachmentPath);
                appMail.Attachments.Add(attachment);
                appMail.Priority = MailPriority.High;
            }

            SmtpClient smtpClient = new( _config["Host"], int.Parse(_config["Port"]) );
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(GmailAccount, GmailPassword);
            smtpClient.Send(appMail);

            return "";
        }
        catch (Exception e)
        {
            return "error: " + e.Message;
        }
    }



}
