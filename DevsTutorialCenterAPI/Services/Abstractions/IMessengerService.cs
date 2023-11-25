using DevsTutorialCenterAPI.Models;

namespace DevsTutorialCenterAPI.Services.Abstractions;

public interface IMessengerService
{
    Task<bool> Send(Message message, string attachment = "");
}