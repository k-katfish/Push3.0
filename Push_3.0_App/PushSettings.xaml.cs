using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

using PushSettingsLib;
using MDTlib;
using System.Threading;

namespace Push
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class PushSettings : Window
    {
        PushSettingsLib.Settings _settings = new PushSettingsLib.Settings();

        public PushSettings()
        {
            InitializeComponent();
            ShareLocationTextBox.Text = _settings.MDTShareLocation;
            FromAddressTextBox.Text = _settings.FromAddress;
            ToAddressTextBox.Text = _settings.ToAddress;
            FromNameTextBox.Text = _settings.FromName;
            ToNameTextBox.Text = _settings.ToName;
            SMTPServerTextBox.Text = _settings.SMTPServer;
            if (_settings.DoEmail) { EmailMe.IsChecked = true; }
            UsernameTextBox.Text = Environment.UserDomainName + "\\" + Environment.UserName;
        }

        /*private void ShareLocationTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!ShareLocationTextBox.Text.Equals(_settings.MDTShareLocation))
            {
                ConnectButton.IsEnabled = true;
                ConnectButton.Content = "Test Share";
                ShareNameTextBox.Text = "";
            }
        }*/

        private void EmailMe_CheckedChanged(object sender, EventArgs e)
        {
            if (EmailMe.IsChecked == true)
            {
                _settings.DoEmail = true;
                FromAddressTextBox.IsEnabled= true;
                ToAddressTextBox.IsEnabled  = true;
                FromNameTextBox.IsEnabled   = true;
                ToNameTextBox.IsEnabled     = true;
                SMTPServerTextBox.IsEnabled = true;
                SetEmailPrefsButton.IsEnabled = true;
            } else
            {
                _settings.DoEmail = false;
                FromAddressTextBox.IsEnabled = false;
                ToAddressTextBox.IsEnabled   = false;
                FromNameTextBox.IsEnabled    = false;
                ToNameTextBox.IsEnabled      = false;
                SMTPServerTextBox.IsEnabled  = false;
                SetEmailPrefsButton.IsEnabled = false;
            }
        }

        private void SetEmailPrefsButton_Click(object sender, EventArgs e)
        {
            _settings.FromAddress = FromAddressTextBox.Text;
            _settings.FromName    = FromNameTextBox.Text;
            _settings.ToAddress   = ToAddressTextBox.Text;
            _settings.ToName      = ToNameTextBox.Text;
            _settings.SMTPServer  = SMTPServerTextBox.Text;
        }

        private void BrowseToMDTShareButton_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = "Select Deployment Root",
                UseDescriptionForTitle = true,
                SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Path.DirectorySeparatorChar,
                ShowNewFolderButton = true
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { this.ShareLocationTextBox.Text = dialog.SelectedPath; }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectButton.Content.Equals("Test Share"))
            {
                if (MDTHelper.TestMDTShare(ShareLocationTextBox.Text))
                {
                    ShareNameTextBox.Text = MDTHelper.GetNameOfShare(ShareLocationTextBox.Text);
                    ConnectButton.Content = "Connect";
                }
            }
            else if (ConnectButton.Content.Equals("Connect"))
            {
                this._settings.MDTShareLocation = ShareLocationTextBox.Text;
            }
        }
    }
}
