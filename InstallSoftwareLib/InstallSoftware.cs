using MDTAppLib;
using MDTTSLib;
using Microsoft.Management.Infrastructure;
using System.Security;

namespace InstallSoftwareLib
{
    public static partial class InstallSoftware
    {
        public static void InstallApp(applications apps, string[] ComputerName)
        { foreach (application app in apps) { InstallApp(app, ComputerName); }}

        public static void InstallApp(applications apps, string ComputerName)
        { foreach (application app in apps) { InstallApp(app, ComputerName); }}

        public static void InstallApp(application App, string[] ComputerName)
        { foreach (string Computer in ComputerName) { InstallApp(App, Computer); }}

        public static void InstallApp(application App, string ComputerName)
        {
            CimSession cs = CimSession.Create("ComputerName");
            InstallApp(App, cs); 
        }

        /* public static void InstallApp(application App, CimSession Session) { } -- in InstallSoftwareLib.cs */

        public static void RunTS(ts TaskSequence, string[] ComputerName)
        { foreach (string Computer in ComputerName) { RunTS(TaskSequence, Computer); }}

        public static void RunTS(ts TaskSequence, string ComputerName)
        {
            CimSession cs = CimSession.Create(ComputerName);
            RunTS(TaskSequence, cs);
        }

        /* public static void RunTS(ts TaskSequence, CimSession Session) { } -- in InstallSoftwareLib.cs */
    }
}