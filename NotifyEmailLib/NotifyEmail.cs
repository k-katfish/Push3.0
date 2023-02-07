using System.Net.Mail;

namespace NotifyEmailLib
{
    public class EmailBuilder
    {
        private string _smtpServer;
        private string _fromAddress;
        private string _fromName;
        private string _toAddress;
        private string _toName;
        private string _subject;
        private string _body;
        private string _errorsBody;

        public EmailBuilder(string smtpServer, string toAddress, string toName, string subject) 
        {
            _smtpServer = smtpServer;
            _fromAddress = "PushApp@engr.colostate.edu";
            _fromName = "Push App 3";
            _toAddress = toAddress;
            _toName = toName;
            _subject = subject;
        }

        public void AddLine(string Content) { _body += Content + "<br>"; }

        public void AddError(string Content) { _body += Content + "<br>"; _errorsBody += Content + "<br>"; }

        public void Send()
        {
            string Content = "<h1>Report from Push App</h1>" + _body + "<br><br><h2>Summary of Errors reported:</h2>" + _errorsBody;
            NotifyEmail.SendMail(_smtpServer, _fromAddress, _fromName, _toAddress, _toName, _subject, Content);
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
    }
}