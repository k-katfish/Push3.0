// librarys written for push
using MDTlib;
//using MDTAppLib;
//using MDTTSLib;
using FixesAndScriptsLib;
using ADHelperLib;
//using InstallSoftwareLib;
using RunTSLib;

// libraries that exist beyond push
using System;
using System.Windows;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;
using ScanHost;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Threading.Tasks;
//using System.Configuration.ConfigurationManager;

namespace Push_3._0_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MDTShare share;
        SecureString password;

        PushSettingsLib.Settings _settings = new();

        public MainWindow()
        {
            this.share = new MDTShare();
            this.share.ShareChanged += Share_Changed;

            InitializeComponent();

            TSListFilter.Items.Add("Applications");
            TSListFilter.Items.Add("Task Sequences");
            TSListFilter.Items.Add("Fixes & Scripts");
            TSListFilter.SelectedIndex = 0;
            SetTSListContent();

            // Get AD OUs for groups location
            OUs ous = ADHelper.GetOUs(ADHelper.GetCurrentDomain());
            foreach (OU ou in ous) { SelectGroup.Items.Add(ou); }

            this.share.Location = _settings.MDTShareLocation;
        }


        private void MainToolBar_Loaded(object sender, RoutedEventArgs e)
        {
#pragma warning disable CS8600, CS8602
            ToolBar toolBar = sender as ToolBar;

            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }

            var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
            if (mainPanelBorder != null)
            {
                mainPanelBorder.Margin = new Thickness(0);
            }
#pragma warning restore CS8600, CS8602
        }

        private void SetTSListContent()
        {
            TSList.Items.Clear();
            if (TSListFilter.SelectedItem.Equals("Applications") && this.share.Apps != null)
            {
                foreach (application app in this.share.Apps)
                {
                    if (ShowHidden.IsChecked == false && /*(app.hide.Equals("false") ||*/ app.enable.Equals("false")) { continue; }
                    if (app.Name.ToLower().Contains(SearchTS.Text.ToLower()))
                    { TSList.Items.Add(app); }
                }
            } 
            
            else if (TSListFilter.SelectedItem.Equals("Task Sequences") && this.share.TaskSequences != null)
            {
                foreach (ts TS in this.share.TaskSequences)
                {
                    if (ShowHidden.IsChecked == false && (TS.Enabled.Equals("false"))) { continue; }
                    if (TS.Name.ToLower().Contains(SearchTS.Text.ToLower()))
                    { TSList.Items.Add(TS); }
                }
            }
            
            else if (TSListFilter.SelectedItem.Equals("Fixes & Scripts") && this.share.TaskSequences != null)
            {
                foreach (Fix fix in this.share.fixes)
                {
                    //if (ShowHidden.IsChecked == false && (fix.hide.Equals("false") || fix.enable.Equals("false"))) { continue; }
                    if (fix.Name.ToLower().Contains(SearchTS.Text.ToLower()))
                    { TSList.Items.Add(fix); }
                }
            }
        }

        private void Share_Changed(object? sender, EventArgs e)
        { SetTSListContent(); }

        //private void SettingsUpdated(object? sender, SettingsUpdatedEventArgs e)
        //{
        //    this.share.Location = e.Updated.MDTShareLocation;
        //    this.share.Refresh();
       // }

        private void SetCredential_Click(object? sender, EventArgs e)
        {
            GetPW pW= new GetPW();
            pW.ShowDialog();
            this.password = pW.Password;
        }

        /*private void ConnectMDTShare_Click(object sender, RoutedEventArgs e)
        {
            MDTShareEditWindow sew = new MDTShareEditWindow(this.share);
            sew.Show();
        }*/

        //private void ConnectAD_Click(object sender, RoutedEventArgs e)
        //{
        //OUs ous = ADHelper.GetOUs(ADHelper.GetCurrentDomain());
        //    foreach (OU ou in ous)
        //    {
        //        SelectGroup.Items.Add(ou);
        //    }
        //}

        private void SelectGroup_SelectionChanged(object? sender, EventArgs e)
        {
            ComputerList.Items.Clear();
#pragma warning disable CS8602
            foreach (Computer computer in SelectGroup.SelectedItem as OU)
            {
                ComputerList.Items.Add(computer);
            }
#pragma warning restore CS8602
        }

        //private void QuickConfig_Click(object? sender, RoutedEventArgs e)
        //{
            //ConnectMDTShare_Click(sender, e);
            //SetCredential_Click(sender, e);
            //ConnectAD_Click(sender, e);
        //}

        private void UpdatePushSettings_Click(object? sender, EventArgs e)
        {
            PushSettings settingsWindow = new PushSettings();
            settingsWindow.ShowDialog();
            SetTSListContent();
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

        private void StartRemoteDesktop_Click(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "C:\\Windows\\System32\\mstsc.exe";
            process.StartInfo.Arguments = $"/v:{Computer_Name.Text}";
            process.Start();
        }

        private void Computer_Name_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // TODO: do the 'see if computer is online' thing, it should be easy in c#
        }

        private void ScanComputer_Click(object sender, RoutedEventArgs e)
        {
            ScanHostWindow sh = new ScanHostWindow(Computer_Name.Text);
            sh.Show();
        }

        private void InstallOnSingleMachine_Click(object sender, RoutedEventArgs e)
        {
            if (this.password == null)
            {
                GetPW pW = new GetPW();
                pW.ShowDialog();
                this.password = pW.Password;
            }

            if (TSListFilter.SelectedItem.Equals("Applications"))
            {
                // install
                application app = (application)TSList.SelectedItem;
                if (_settings.DoEmail)
                { InstallSoftware.InstallAppEmailResult(password, app, Computer_Name.Text, Environment.UserName, Environment.UserDomainName, _settings.emailContact); } 
                else 
                { InstallSoftware.InstallApp(password, app, Computer_Name.Text, Environment.UserName, Environment.UserDomainName); }
            } 
            else if (TSListFilter.SelectedItem.Equals("Task Sequences"))
            {
                ts SelectedTaskSequence = (ts)TSList.SelectedItem;

                if (_settings.DoEmail)
                { InstallSoftware.RunTSEmailResult(password, SelectedTaskSequence, share.Location, Computer_Name.Text, Environment.UserName, Environment.UserDomainName, _settings.emailContact);} 
                else
                { InstallSoftware.RunTS(password, SelectedTaskSequence, share.Location, Computer_Name.Text, Environment.UserName, Environment.UserDomainName); }
            }
        }

        private void InstallOnMultipleMachines_Click(object sender, RoutedEventArgs e)
        {
            if (this.password == null)
            {
                GetPW pW = new GetPW();
                pW.ShowDialog();
                this.password = pW.Password;
            }

            if (TSListFilter.SelectedItem.Equals("Applications"))
            {
                // install
                //applications apps = (applications)TSList.SelectedItems;
                //string[] ComputerNames = (string[])ComputerList.SelectedItems;
                //InstallSoftware.InstallApp(apps, ComputerNames);
                foreach (application app in TSList.SelectedItems)
                {
                    foreach (Computer ComputerName in ComputerList.SelectedItems)
                    {
                        if (_settings.DoEmail)
                        {
                            Task.Run(() => InstallSoftware.InstallAppEmailResult(password, app, ComputerName.ToString(), Environment.UserName, Environment.UserDomainName, _settings.emailContact));
                        } else
                        {
                            Task.Run(() => InstallSoftware.InstallApp(password, app, ComputerName.ToString(), Environment.UserName, Environment.UserDomainName));
                        }
                    }
                }
            }
            else if (TSListFilter.SelectedItem.Equals("Task Sequences"))
            {
                // runts
                ts SelectedTaskSequence = (ts)TSList.SelectedItem;
                //string[] ComputerNames = (string[])ComputerList.SelectedItems;
                foreach (Computer computerName in ComputerList.SelectedItems) {
                    if (_settings.DoEmail)
                    { Task.Run(() => InstallSoftware.RunTSEmailResult(password, SelectedTaskSequence, share.Location, computerName.ToString(), Environment.UserName, Environment.UserDomainName, _settings.emailContact)); }
                    else
                    { Task.Run(() => InstallSoftware.RunTS(password, SelectedTaskSequence, share.Location, computerName.ToString(), Environment.UserName, Environment.UserDomainName)); }
                }
            }
        }

        private void SearchTS_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        { SetTSListContent(); }
    }
}
