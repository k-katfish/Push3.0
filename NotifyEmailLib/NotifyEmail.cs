using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Xml.Linq;

namespace NotifyEmailLib
{
    public class EmailBuilder
    {
        private EmailContact _contact;
        private string _subject;
        private string _tldr;
        private string _body;
        private string _errorsBody;

        public EmailBuilder(string smtpServer, string toAddress, string toName, string subject) 
        {
            _contact = new EmailContact(smtpServer, toAddress, toName);
            _subject = subject;
        }

        public EmailBuilder(EmailContact contact, string subject) { 
            _contact = contact; 
            _subject = subject;
        }

        public void AddLine(string Content) { _body += Content + "<br>"; }
        public void AddTLDR(string Content) { _tldr += Content + "<br>"; }
        public void AddError(string Content) { _body += Content + "<br>"; _errorsBody += Content + "<br>"; }

        public string Subject { get => this._subject; }
        public string Body { get => this._body ;}
        public string TLDR { get => this._tldr; }
        public string Errors { get => this._errorsBody; }

        public EmailContact Contact { get => _contact; }

        public void Send()
        {
            //string Content = "<h1>Report from Push App</h1>" + _body + "<br><br><h2>Summary of Errors reported:</h2>" + _errorsBody;
            //NotifyEmail.SendMail(_smtpServer, _fromAddress, _fromName, _toAddress, _toName, _subject, Content);
            if (!_contact.SMTPServer.Equals("noserver")) { NotifyEmail.SendMail(this); }
        }
    }

    public static class NotifyEmail
    {
        public static void SendMail(string smtpServer, string toAddress, string toName, string subject, string body)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(smtpServer);
                MailAddress from = new MailAddress("PushApp@engr.colostate.edu", "Push App 3");
                MailAddress to = new MailAddress(toAddress, toName);
                MailMessage message = new MailMessage(from, to);

                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;

                message.Body = body;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;

                smtpClient.Send(message);
            } 
            catch (SmtpException ex) { throw new ApplicationException ("Error sending mail: " + ex.Message); }
            catch (Exception ex) { throw ex; }
        }

        public static void SendMail(string smtpServer, string fromAddress, string fromName, string toAddress, string toName, string subject, string body)
        {
            Debug.WriteLine($"Planning to send email via {smtpServer} from {fromAddress} to {toAddress} with subject {subject} and body {body}");
            try
            {
                SmtpClient smtpClient = new SmtpClient(smtpServer);
                MailAddress from = new MailAddress(fromAddress, fromName);
                MailAddress to = new MailAddress(toAddress, toName);
                MailMessage message = new MailMessage(from, to);

                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;

                message.Body = body;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;

                smtpClient.Send(message);
            }
            catch (SmtpException ex) { throw new ApplicationException("Error sending mail: " + ex.Message); }
            catch (Exception ex) { throw ex; }
        }

        public static void SendMail(EmailBuilder email)
        {
            Debug.WriteLine($"Planning to send email via {email.Contact.SMTPServer} from {email.Contact.FromAddress} to {email.Contact.ToAddress} with subject {email.Subject} and body {email.Body}");
            try
            {
                SmtpClient smtpClient = new SmtpClient(email.Contact.SMTPServer);
                MailAddress from = new MailAddress(email.Contact.FromAddress, email.Contact.FromName);
                MailAddress to = new MailAddress(email.Contact.ToAddress, email.Contact.ToName);
                MailMessage message = new MailMessage(from, to);

                message.Subject = email.Subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;

                string Content = "<h1>Report from Push App</h1>" + email.TLDR + "<br><br><h2>Process Output</h2>" + email.Body + "<br><br><h2>Summary of Errors reported:</h2>" + email.Errors;

                message.Body = Content;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;

                smtpClient.Send(message);
            }
            catch (SmtpException ex) { throw new ApplicationException("Error sending mail: " + ex.Message); }
            catch (Exception ex) { throw ex; }
        }
    }
}