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

namespace Push_3._0_App
{
    public class SettingsUpdatedEventArgs 
    {
        private PushSettingsLib.Settings _updated;
        public SettingsUpdatedEventArgs(PushSettingsLib.Settings updated) { _updated = updated; }
        public PushSettingsLib.Settings Updated { get => _updated; }
    }

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class PushSettings : Window
    {
        public delegate void SettingsUpdatedEventHandler(object sender, SettingsUpdatedEventArgs e);
        public event SettingsUpdatedEventHandler SettingsUpdated;

        PushSettingsLib.Settings _settings = new PushSettingsLib.Settings();

        public PushSettings()
        {
            InitializeComponent();
            ShareLocationTextBox.Text = _settings.MDTShareLocation;
            UsernameTextBox.Text = Environment.UserDomainName + "\\" + Environment.UserName;
        }

        private void ShareLocationTextBox_TextChanged(object sender, EventArgs e)
        {
            ConnectButton.IsEnabled = true;
            ConnectButton.Content = "Test Share";
            ShareNameTextBox.Text = "";
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
            if (ConnectButton.Content.Equals("Test"))
            {
                if (MDTHelper.TestMDTShare(ShareLocationTextBox.Text))
                {
                    ShareNameTextBox.Text = MDTHelper.GetNameOfShare(ShareLocationTextBox.Text);
                    // TODO: get name of share from Control\Settings.xml... MDTlib?
                    ConnectButton.Content = "Connect";
                }
            }
            else if (ConnectButton.Content.Equals("Connect"))
            {
                this._settings.MDTShareLocation = ShareLocationTextBox.Text;
                //this.share.Refresh();
                SettingsUpdated?.Invoke(this, new SettingsUpdatedEventArgs(_settings));
                this.Close();
            }
        }
    }
}
