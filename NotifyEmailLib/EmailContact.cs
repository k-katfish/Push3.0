using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotifyEmailLib
{
    public class EmailContact
    {
        private string _smtpServer;
        private string _fromAddress;
        private string _fromName;
        private string _toAddress;
        private string _toName;

        public EmailContact(string smtpServer, string fromAddresss, string fromName, string toAddress, string toName)
        {
            _smtpServer = smtpServer;
            _fromAddress = fromAddresss;
            _fromName = fromName;
            _toAddress = toAddress;
            _toName = toName;
        }

        public EmailContact(string smtpServer, string toAddress, string toName)
        {
            _smtpServer = smtpServer;
            _fromAddress = "PushApp@" + toAddress.Substring(toAddress.IndexOf('@'));
            _fromName = "Push App v3";
            _toAddress = toAddress;
            _toName = toName;
        }

        public string SMTPServer { get => _smtpServer; set => _smtpServer = value; }
        public string FromAddress { get => _fromAddress; set => _fromAddress = value; }
        public string ToAddress { get => _toAddress; set => _toAddress = value; }
        public string ToName { get => _toName; set => _toName = value; }
        public string FromName { get => _fromName; set => _fromName = value; }

        public override string ToString()
        { return $"From: \"{_fromName}\" <{_fromAddress}>\r\nTo: \"{_toName}\" <{_toAddress}>"; }
    }
}
