using RealState.Core.Application.Dtos.Email;
using RealState.Core.Domain.Settings;

namespace RealState.Core.Application.Interfaces.Services    
{
    public interface IEmailServices
    {
        public MailSettings MailSettings { get; }
        Task SendAsync(EmailRequest request);
    }
}