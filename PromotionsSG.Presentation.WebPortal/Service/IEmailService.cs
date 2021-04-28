using System.Collections.Generic;

namespace PromotionsSG.Presentation.WebPortal.Service
{
    public interface IEmailService
    {
        void Send(EmailService.EmailMessage emailMessage);
        List<EmailService.EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}
