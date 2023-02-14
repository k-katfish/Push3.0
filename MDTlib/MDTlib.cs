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
//using MDTAppLib;
//using MDTTSLib;
//using MDTSettingsLib;
//using FixesAndScriptsLib;
using System.Diagnostics.CodeAnalysis;
//using System.IO;
//using System.Management;
//using System.Reflection.Metadata.Ecma335;

namespace MDTlib
{
    public class MDTShare
    {
        public event EventHandler ShareChanged;
        public void OnShareChanged()
        {
            EventHandler handler = ShareChanged;
            if (null != handler) handler(this, EventArgs.Empty);
        }

        Settings _settings;
        applications _apps;
        tss _tss;
        Fixes _fixes;
        string _location;

        public Settings settings
        {
            get => this._settings;
            set => this._settings = value;
        }

        public string Name
        {
            get { return this._settings.Description; }
            set { this._settings.Description = value; }
        }

        public applications Apps {
            get => this._apps;
            set => this._apps = value;
        }

        public tss TaskSequences
        {
            get => this._tss;
            set => this._tss = value;
        }

        public Fixes fixes
        {
            get => this._fixes;
            set => this._fixes = value;
        }
        
        public string Location
        {
            get => this._location;
            set { this._location = value; Refresh(); }
        }

        public void Refresh ()
        {
#pragma warning disable CS8601
            this._tss = tssHelper.GetTaskSequencesFromShare(this.Location);
            this._apps = applicationsHelper.GetApplicationsFromShare(this.Location);
            this._settings = SettingsHelper.GetSettingsFromShare(this.Location);
            this._fixes = FixesHelper.GetFixes(this.Location);

            /*foreach (var app in this.MDTApps)
            {
                if (app.WorkingDirectory.ToString().StartsWith(@".\")) {
                    app.WorkingDirectory = this.Location + app.WorkingDirectory.Substring(2);
                }
            }*/
#pragma warning restore CS8601
            OnShareChanged();
        }
    }

    /*public class DeploymentToolkit
    {
        private string DeploymentRoot;
        public DeploymentToolkit() { }
        public DeploymentToolkit(string DeploymentRoot) => this.DeploymentRoot = DeploymentRoot;
    }*/

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
            if (!(Directory.Exists($"{DeploymentRoot}\\Control"))) { return false; }

            string[] TestFiles = { $"{DeploymentRoot}\\Control\\Applications.xml", $"{DeploymentRoot}\\Control\\TaskSequences.xml" };
            foreach (string TestFile in TestFiles)
            {
                if (!(File.Exists(TestFile))) { return false; }
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

        public static string GetNameOfShare(string DeploySharePath)
        {
#pragma warning disable CS8600, CS8602
            if (!File.Exists($"{DeploySharePath}\\Control\\Settings.xml")) { return ""; }
            Settings s = SettingsHelper.GetSettingsFromShare(DeploySharePath);
            return s.Description;
#pragma warning restore CS8600, CS8602
        }
    }
}