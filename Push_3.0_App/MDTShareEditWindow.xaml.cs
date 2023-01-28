using MDTAppLib;
using MDTlib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;
using static MDTlib.MDTHelper;
using System.Windows.Forms;
using Path = System.IO.Path;
using MDTSettingsLib;

namespace Push_3._0_App
{
    /// <summary>
    /// Interaction logic for MDTShareEditWindow.xaml
    /// </summary>
    public partial class MDTShareEditWindow : Window
    {
        MDTShare share;

        public MDTShareEditWindow(MDTShare share)
        {
            this.share = share;
            InitializeComponent();
            if (this.share.Location != null) { ShareLocationTextBox.Text = this.share.Location; }
            if (this.share.settings != null) { ShareNameTextBox.Text = this.share.Name; }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectButton.Content.Equals("Test"))
            {
                if (TestMDTShare(ShareLocationTextBox.Text))
                {
                    ShareNameTextBox.Text = MDTHelper.GetNameOfShare(ShareLocationTextBox.Text);
                    // TODO: get name of share from Control\Settings.xml... MDTlib?
                    ConnectButton.Content = "Connect";
                }
            }
            else if (ConnectButton.Content.Equals("Connect"))
            {
                this.share.Location = ShareLocationTextBox.Text;
                this.share.Refresh();
                this.Close();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) { /* Don't delete this method? */ }

        private void ShareNameTextBox_TextChanged(object sender, TextChangedEventArgs e) { /* I think this has to stay here for some reason? */ }

        private void CancelButton_Click(object sender, RoutedEventArgs e) { this.Close(); }

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
            {
                this.ShareLocationTextBox.Text = dialog.SelectedPath;
            }
        }
    }
}
