using DevsTutorialCenterAPI.Models;

namespace DevsTutorialCenterAPI.Services.Abstractions;

public interface IMessengerService
{
    string Send(Message message, string Attachment = "");
}