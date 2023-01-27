using MDTlib;
using System;
using System.Windows;
using MDTAppLib;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConnectMDTShare_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Reading data from MDT Share...");

            if (MDTHelper.TestIfPathIsMDTShare("\\\\labs-mdt\\labs-mdt$"))

            TSList.Items.Clear();

            applications apps = new applications();

            /* get available apps from applications.xml on MDT share */

            XmlSerializer MDTData = new XmlSerializer(typeof(applications));
            FileStream MDTApplicationsXML = new FileStream("\\\\labs-mdt\\labs-mdt$\\Control\\Applications.xml", FileMode.Open);
            applications MDTApplications = MDTData.Deserialize(MDTApplicationsXML) as applications;
            MDTApplicationsXML.Close();

            foreach (application app in MDTApplications)
            {
                TSList.Items.Add(app.Name);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "C:\\Program Files\\Mozilla Firefox\\firefox.exe";
            process.StartInfo.Arguments = "http://make-everything-ok.com/";
            process.StartInfo.WindowStyle= ProcessWindowStyle.Hidden;
            process.Start();
        }
    }
}
