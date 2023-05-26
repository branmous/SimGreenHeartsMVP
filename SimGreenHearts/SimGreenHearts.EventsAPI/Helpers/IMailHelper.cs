using SimGreenHearts.Shared.Responses;

namespace SimGreenHearts.EventsAPI.Helpers
{
    public interface IMailHelper
    {
        Response<Exception> SendMail(string toName, string toEmail, string subject, string body);

    }
}
