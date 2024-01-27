using Arts_be.Helpter;

namespace Arts_be.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(Mailrequest mailrequest);
    }
}
