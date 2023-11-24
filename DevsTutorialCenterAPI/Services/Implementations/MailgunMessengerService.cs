// Import necessary namespaces
using Microsoft.AspNetCore.Mvc;
using RestSharp.Authenticators;
using RestSharp;
using System.Text;
using DevsTutorialCenterAPI.Models;

namespace DevsTutorialCenterAPI.Services.Implementations
{


    public class MailgunMessengerService 
    {
        private readonly RestClient _mailgunClient;
        private readonly Dictionary<string, string> _config;
        public MailgunMessengerService(IConfiguration config)
        {
            _config = config.GetRequiredSection("Mailgun").GetChildren()
           .ToDictionary(child => child.Key, child => child.Value);

           var apiKey = _config["ApiKey"];
           var domain = _config["Domain"];

           
            _mailgunClient = new RestClient($"https://api.mailgun.net/v3/{domain}");
            _mailgunClient.AddDefaultHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{apiKey}")));
        }

        public RestResponse Send(Message message)
        {
            string from = _config["SenderEmail"];
            var request = new RestRequest("messages", Method.Post);
            request.AddParameter("from", from);
            request.AddParameter("to", message.To.FirstOrDefault() );
            request.AddParameter("subject", message.Subject);
            request.AddParameter("text", message.Body);

            return _mailgunClient.Execute(request);
        }

    }

}


