//using System.Collections;
//using System.Security.Cryptography.X509Certificates;
//using System.Diagnostics.Metrics;
using Microsoft.Management.Infrastructure;
using System.Collections;
using System.ComponentModel;
using System.Dynamic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;
using MDTAppLib;
using MDTTSLib;
using System.Diagnostics.CodeAnalysis;
//using System.IO;
//using System.Management;
//using System.Reflection.Metadata.Ecma335;

namespace MDTlib
{
    public class MDTTaskSequence
    {

    }

    public class ShareHelper
    {
        public event EventHandler ShareChanged;
        public void OnShareChanged()
        {
            EventHandler handler = ShareChanged;
            if (null != handler) handler(this, EventArgs.Empty);
        }

        applications MDTApps;
        tss MDTTaskSequencs;
        string ShareLocation;

        public applications Apps {
            get => this.MDTApps;
            set => this.MDTApps = value;
        }

        public tss TaskSequences
        {
            get => this.MDTTaskSequencs;
            set => this.MDTTaskSequencs = value;
        }
        
        public string Location
        {
            get => this.ShareLocation;
            set => this.ShareLocation = value;
        }

        public void Refresh ()
        {
            this.MDTTaskSequencs = MDTHelper.GetTaskSequencesFromShare(this.Location);
            this.MDTApps = MDTHelper.GetApplicationsFromShare(this.Location);
            OnShareChanged();
        }
    }

    public class DeploymentToolkit
    {
        private string DeploymentRoot;
        public DeploymentToolkit() { }
        public DeploymentToolkit(string DeploymentRoot) => this.DeploymentRoot = DeploymentRoot;
    }

    public static class MDTHelper 
    {
        public static bool TestIfPathIsMDTShare(string DeploymentRoot)
        {
            if (DeploymentRoot == null) { return false; }
            else if (DeploymentRoot.Length == 0) { return false; }
            else if (!Directory.Exists(DeploymentRoot)) { return false; }
            else
            {
                if (DeploymentRoot.StartsWith('\\'))    // starts with '\', so it must be like \\MyServer\MyShare...
                {
                    return TestMDTShare(DeploymentRoot);
                }
                else                                    // starts with a letter, must be a mapped drive. get the mapped place cause we'll need that.
                {
                    return TestMDTShare(ConvertFromMappedDrive(DeploymentRoot));
                }
            }
        }

        public static bool TestMDTShare(string DeploymentRoot)
        {
            //Console.WriteLine("Testing if " + DeploymentRoot + " is an MDT share");
            if (!(Directory.Exists($"{DeploymentRoot}\\Control"))) { return false; }

            string[] TestFiles = { $"{DeploymentRoot}\\Control\\Applications.xml", $"{DeploymentRoot}\\Control\\TaskSequences.xml" };
            foreach (string TestFile in TestFiles)
            {
                //Console.WriteLine("Testing if " + TestFile + " exists...");
                if (!(File.Exists(TestFile)))
                {
                    //Console.WriteLine("It does not");
                    return false;
                }
            }
            return true;
        }

        public static string ConvertFromMappedDrive(string DrivePath)
        {
            /* string Namespace = @"root\cimv2";
             string ClassName = "Win32_LogicalDisk";
             string WMIQuery = $"SELECT * FROM {ClassName} WHERE DeviceID -eq '{DrivePath.Substring(0, 2)}'";
             CimInstance AllDrives = new(ClassName, Namespace);
            */

            // }
            return "";
            //future kyle: yes you can create a cim session with: CimSession my_cim_session = new CimSession("computername");
        }

        public static string ConvertFromMappedDrive(char DriveLetter)
        {
            //ManagementObject 
            return "";
        }

        public static applications GetApplicationsFromShare(string ShareLocation)
        {
            XmlSerializer MDTData = new XmlSerializer(typeof(applications));
            FileStream MDTApplicationsXML = new FileStream($"{ShareLocation}\\Control\\Applications.xml", FileMode.Open);
            applications MDTApplications = MDTData.Deserialize(MDTApplicationsXML) as applications;
            MDTApplicationsXML.Close();

            return MDTApplications;
        }

        public static tss GetTaskSequencesFromShare(string ShareLocation)
        {
            XmlSerializer MDTData = new XmlSerializer(typeof(tss));
            FileStream MDTTSXML = new FileStream($"{ShareLocation}\\Control\\TaskSequences.xml", FileMode.Open);
            tss MDTTS = MDTData.Deserialize(MDTTSXML) as tss;
            MDTTSXML.Close();

            return MDTTS;
        }
    }
}