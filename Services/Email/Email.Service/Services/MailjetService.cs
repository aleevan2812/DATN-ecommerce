using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;

namespace Email.Service.Services;

public class MailjetService : IMailjetService
{
    public async Task SendAsync(Entities.Email email)
    {
        MailjetClient client = new MailjetClient("33919eed1a8dbcda417f0b6f9be9fd4b", "9fcab16e9e879cf33a9ce447f40ec651");

        MailjetRequest request = new MailjetRequest
        {
            Resource = Send.Resource,
        }
            .Property(Send.FromEmail, "vananle680@gmail.com")
           .Property(Send.FromName, "Book-store")
           .Property(Send.Subject, email.Subject)
           .Property(Send.HtmlPart, email.HtmlPart)
           .Property(Send.Recipients,
                new JObject {
                {
                "Email",email.ToEmail}
                }
           );

        MailjetResponse response = await client.PostAsync(request);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
            Console.WriteLine(response.GetData());
        }
        else
        {
            Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
            Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
            Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
        }
    }
}