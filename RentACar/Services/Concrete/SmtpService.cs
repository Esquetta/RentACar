using RentACar.Entities.Concrete;
using RentACar.UI.Services.Abstract;
using System;
using System.Net;
using System.Net.Mail;

namespace RentACar.UI.Services.Concrete
{
    public class SmtpService : ISmtpService
    {
        private SmtpClient smtpClient;

        public SmtpService()
        {
            smtpClient = new SmtpClient("smtp.office365.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryFormat = SmtpDeliveryFormat.International;
            smtpClient.Credentials = new NetworkCredential("OtoFurki@hotmail.com", "Dolar11Tl!");

        }
        public void AccountConfirmMail(string MailTo, string UserName, string ConfirmCode)
        {
            MailAddress Sender = new MailAddress("OtoFurki@hotmail.com", "Car Rent");
            MailAddress getter = new MailAddress(MailTo);
            MailMessage mailMessage = new MailMessage(Sender, getter);
            mailMessage.Subject = "Account Activate Code";
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = $"Hi! {UserName} here your activation code {Environment.NewLine} https://localhost:44327{ConfirmCode}";
            smtpClient.Send(mailMessage);
        }

        public void PasswordChangedInfo(string MailTo, string UserName)
        {
            MailAddress Sender = new MailAddress("OtoFurki@hotmail.com", "Car Rent");
            MailAddress getter = new MailAddress(MailTo);
            MailMessage mailMessage = new MailMessage(Sender, getter);
            mailMessage.Subject = "Password Changed";
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = $"Hi! {UserName} your account password changed,<br> \n  if this was not you ,please immediately secure your account.Contat with us.";
            smtpClient.Send(mailMessage);
        }

        public void PaymentReceipit(string email, string username, Car car, DateTime taken, DateTime returnDate)
        {
            MailAddress Sender = new MailAddress("OtoFurki@hotmail.com", "Car Rent");
            MailAddress getter = new MailAddress(email);
            MailMessage mailMessage = new MailMessage(Sender, getter);
            mailMessage.Subject = "Your payment receipit";
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.Body = $"hi {username}  Here your payment receipit {Environment.NewLine} {car.Brand.BrandName} {car.Model} {car.Price}  Given Date:{taken}, Taken Date:{returnDate} ";
            smtpClient.Send(mailMessage);
        }

        public void ResetPasswordMail(string MailTo, string UserName, string PasswordResetUrl)
        {
            MailAddress Sender = new MailAddress("OtoFurki@hotmail.com", "Car Rent");
            MailAddress Getter = new MailAddress(MailTo);
            MailMessage mailMessage = new MailMessage(Sender, Getter);
            mailMessage.Subject = "Password Reset";
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.Body = $"Hi {UserName} here your password reset code, {Environment.NewLine} https://localhost:44327{PasswordResetUrl}";
            mailMessage.IsBodyHtml = true;
            smtpClient.Send(mailMessage);
        }

        public void TwoFactorCode(string MailTo, string UserName, string TwoFactorCode)
        {
            MailAddress Sender = new MailAddress("OtoFurki@hotmail.com", "Car Rent");
            MailAddress Getter = new MailAddress(MailTo);
            MailMessage mailMessage = new MailMessage(Sender, Getter);
            mailMessage.Subject = "Password Reset";
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.Body = $"Hi {UserName} here your two factor authentication  code, {Environment.NewLine} " + TwoFactorCode;
            mailMessage.IsBodyHtml = true;
            smtpClient.Send(mailMessage);
        }

    }
}
