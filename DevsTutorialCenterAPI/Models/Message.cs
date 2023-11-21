namespace DevsTutorialCenterAPI.Models;

public class Message
{
    public Message(string subject, IList<string> to, string body)
    {
        Subject = subject;
        To = to;
        Body = body;
        
    }
    public string Subject { get; set; }
    public IList<string> To { get; set; } = new List<string>();
    public string Body { get; set; }
}
