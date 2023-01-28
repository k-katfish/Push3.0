using MDTlib;
using System;
using System.Windows;
using MDTAppLib;
using MDTTSLib;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Push_3._0_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ShareHelper share;

        public MainWindow()
        {
            this.share = new ShareHelper();
            this.share.ShareChanged += Share_Changed;

            InitializeComponent();

            TSListFilter.Items.Add("Applications");
            TSListFilter.Items.Add("Task Sequences");
            TSListFilter.SelectedIndex = 0;
        }

        private void SetTSListContent()
        {
            TSList.Items.Clear();
            if (TSListFilter.SelectedItem.Equals("Applications") && this.share.Apps != null)
            {
                foreach (application app in this.share.Apps)
                { TSList.Items.Add(app.Name); }
            } else if (TSListFilter.SelectedItem.Equals("Task Sequences") && this.share.TaskSequences != null)
            {
                foreach (ts TS in this.share.TaskSequences)
                { TSList.Items.Add(TS.Name); }
            }
        }

        private void Share_Changed(object? sender, EventArgs e)
        {
            SetTSListContent();
        }

        private void ConnectMDTShare_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Reading data from MDT Share...");
            MDTShareEditWindow sew = new MDTShareEditWindow(this.share);
            sew.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "C:\\Program Files\\Mozilla Firefox\\firefox.exe";
            process.StartInfo.Arguments = "http://make-everything-ok.com/";
            process.StartInfo.WindowStyle= ProcessWindowStyle.Hidden;
            process.Start();
        }

        private void TSListFilter_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) { SetTSListContent(); }
    }
}
