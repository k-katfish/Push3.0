//using System.Collections;
//using System.Security.Cryptography.X509Certificates;
//using System.Diagnostics.Metrics;
using Microsoft.Management.Infrastructure;
using System.Runtime.InteropServices;
//using System.IO;
//using System.Management;
//using System.Reflection.Metadata.Ecma335;

namespace MDTlib
{
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
    }
}