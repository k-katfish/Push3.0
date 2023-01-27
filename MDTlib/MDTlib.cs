using System.Collections;
using System.Diagnostics.Metrics;
//using Microsoft.Management.Infrastructure;
using System.IO;
using System.Management;
using System.Reflection.Metadata.Ecma335;

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
        public static bool TestMDTShare(string DeploymentRoot)
        {
            ArrayList TestFiles = new ArrayList { $"{DeploymentRoot}\\Control", $"{DeploymentRoot}\\Control\\Applications.xml", $"{DeploymentRoot}\\Control\\TaskSequences.xml" };
            foreach (string TestFile in TestFiles)
            {
                if (!(File.Exists(TestFile)))
                {
                    return false;
                }
            }
            return true;
        }

        public static string ConvertMappedDrive(string DriveLetter)
        {
            //ManagementObject mo = new ManagementObject();
            //mo.Path = new ManagementPath( String.Format( "Win32_LogicalDisk='{0}'", DriveLetter) );
            string Drive = DriveLetter.Substring(0, 1).ToLower();
            string FullPath = ConvertMappedDrive(Drive);
            return FullPath;
        }

        public static string ConvertMappedDrive(char DriveLetter)
        {
            ManagementObject 
            return "";
        }
    }
}