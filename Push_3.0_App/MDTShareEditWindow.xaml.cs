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

namespace Push_3._0_App
{
    /// <summary>
    /// Interaction logic for MDTShareEditWindow.xaml
    /// </summary>
    public partial class MDTShareEditWindow : Window
    {
        ShareHelper share;

        public MDTShareEditWindow(ShareHelper share)
        {
            this.share = share;
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectButton.Content.Equals("Test"))
            {
                if (TestMDTShare(ShareLocationTextBox.Text))
                {
                    // get name of share from Control\Settings.xml... MDTLib?
                    ConnectButton.Content = "Connect";
                }
            }
            else if (ConnectButton.Content.Equals("Connect"))
            {
                /* get available apps from applications.xml on MDT share */
                this.share.Location = ShareLocationTextBox.Text;
                this.share.Refresh();
                this.Close();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ShareNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            {
                this.ShareLocationTextBox.Text = dialog.SelectedPath;
            }
        }
    }
}
