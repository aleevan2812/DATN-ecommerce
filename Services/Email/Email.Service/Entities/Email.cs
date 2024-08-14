namespace Email.Service.Entities;

public class Email
{
    public string ToEmail { get; set; }
    public string ToName { get; set; }
    public string Subject { get; set; }
    public string HtmlPart { get; set; }
}