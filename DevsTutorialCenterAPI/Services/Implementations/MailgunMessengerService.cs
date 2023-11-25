using System.Net.Http.Headers;
using System.Text;
using DevsTutorialCenterAPI.Models;
using DevsTutorialCenterAPI.Services.Abstractions;

namespace DevsTutorialCenterAPI.Services.Implementations;

public class MailgunMessengerService: IMessengerService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _apiKey;
    private readonly string _domain;

    public MailgunMessengerService(IConfiguration config, IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _apiKey = config["Mailgun:ApiKey"];
        _domain = config["Mailgun:Domain"];
    }

    public async Task<bool> Send(Message message, string attachment = "")
    {
        var credentials = $"api:{_apiKey}";
        var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials));
        
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri("https://api.mailgun.net");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
        var formData = new MultipartFormDataContent
        {
            { new StringContent("Devs Tutorial Center <info@devstutorialcenter.com>"), "from" },
            { new StringContent(message.Subject), "subject" },
            { new StringContent(message.Body), "text" }
        };

        foreach (var recipient in message.To)
        {
            formData.Add(new StringContent(recipient), "to");
        }
        
        var response = await client.PostAsync($"/v3/{_domain}/messages", formData);

        return response.IsSuccessStatusCode;
    }
}