using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using NotifyEmailLib;

namespace PushSettingsLib
{
    public class Settings
    {
        private string _MDTShareLocation;
        //private EmailContact _contact;
        private string _SMTPServer;
        private string _FromAddress;
        private string _ToAddress;
        private string _FromName;
        private string _ToName;

        public Settings() 
        {
            _MDTShareLocation = ReadSetting("MDTShareLocation");
            _SMTPServer = ReadSetting("SMTPServer");
            _FromAddress = ReadSetting("FromAddress");
            _ToAddress = ReadSetting("ToAddress");
            _FromName = ReadSetting("FromName");
            _ToName = ReadSetting("ToName");
        }

        public string MDTShareLocation { get => this._MDTShareLocation; set { this._MDTShareLocation = value; SetSetting("MDTShareLocation", value); }}
        public string SMTPServer { get => this._SMTPServer; set { this._SMTPServer = value; SetSetting("SMTPServer", value); }}
        public string FromAddress { get => this._FromAddress; set { this._FromAddress = value; SetSetting("FromAddress", value); } }
        public string ToAddress { get => this._ToAddress; set { this._ToAddress = value; SetSetting("ToAddress", value); } }
        public string FromName { get => this._FromName; set { this._FromName = value; SetSetting("FromName", value); } }
        public string ToName { get => this._ToName; set { this._ToName = value; SetSetting("ToName", value); } }

        static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                return result;
            } catch (ConfigurationErrorsException) { Debug.WriteLine("Error reading configuration."); }
            return "Error";
        }

        static void SetSetting(string key, string value)
        {
            try
            {
                var ConfigFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
                var settings = ConfigFile.AppSettings.Settings;
                if (settings[key] == null) { settings.Add(key, value); }
                else { settings[key].Value = value; }
                ConfigFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(ConfigFile.AppSettings.SectionInformation.Name);
            }
            catch { Debug.WriteLine("Error writing configuration."); }
        }
    }
}