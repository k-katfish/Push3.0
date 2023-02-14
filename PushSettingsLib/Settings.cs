using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using NotifyEmailLib;

namespace PushSettingsLib
{
    public class Settings
    {
        public event EventHandler SettingsChanged;
        public void OnSettingsChanged()
        {
            EventHandler handler = SettingsChanged;
            if (null != handler) handler(this, EventArgs.Empty);
        }

        public Settings() 
        {
            if (ReadSetting("DoEmail").Equals("Not Found"))
            {   SetSetting("DoEmail", "false"); }
        }

        public string MDTShareLocation { get => ReadSetting("MDTShareLocation"); set { SetSetting("MDTShareLocation", value); OnSettingsChanged(); }}
        public string SMTPServer { get => ReadSetting("SMTPServer"); set { SetSetting("SMTPServer", value); OnSettingsChanged(); }}
        public string FromAddress { get => ReadSetting("FromAddress"); set => SetSetting("FromAddress", value); }
        public string ToAddress { get => ReadSetting("ToAddress"); set => SetSetting("ToAddress", value); }
        public string FromName { get => ReadSetting("FromName"); set => SetSetting("FromName", value); }
        public string ToName { get => ReadSetting("ToName"); set => SetSetting("ToName", value); }
        public bool   DoEmail { get => Boolean.Parse(ReadSetting("DoEmail")); set => SetSetting("DoEmail", value.ToString()); }
        public EmailContact emailContact
        { get { return new EmailContact(SMTPServer, FromAddress, FromName, ToAddress, ToName); }}

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
                Configuration ConfigFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                KeyValueConfigurationCollection settings = ConfigFile.AppSettings.Settings;
                if (settings[key] == null) { settings.Add(key, value); }
                else { settings[key].Value = value; }
                ConfigFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(ConfigFile.AppSettings.SectionInformation.Name);
            }
            catch { Debug.WriteLine("Error writing configuration."); }
        }
    }
}