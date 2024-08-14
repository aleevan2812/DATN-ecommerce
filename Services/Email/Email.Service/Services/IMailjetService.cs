namespace Email.Service.Services;

public interface IMailjetService
{
    Task SendAsync(Entities.Email email);
}