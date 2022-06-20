using RentACar.Entities.Concrete;
using System;

namespace RentACar.UI.Services.Abstract
{
    public interface ISmtpService
    {
        void AccountConfirmMail(string MailTo, string UserName, string ConfirmCode);
        void ResetPasswordMail(string MailTo, string UserName, string PasswordResetUrl);
        void PasswordChangedInfo(string MailTo, string UserName);
        void TwoFactorCode(string MailTo, string UserName, string TwoFactorCode);
        void PaymentReceipit(string email, string username, Car car, DateTime taken, DateTime returnDate);
    }
}
