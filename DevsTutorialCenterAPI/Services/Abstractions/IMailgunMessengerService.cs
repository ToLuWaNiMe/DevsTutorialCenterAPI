using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models;
using RestSharp;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    public interface IMailgunMessengerService
    {
        public RestResponse Send(Message message);


    }
}
