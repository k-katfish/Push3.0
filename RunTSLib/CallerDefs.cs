using System.Security;
//using MDTAppLib;
//using MDTTSLib;
using MDTlib;
using NotifyEmailLib;
using System.Diagnostics;
using Microsoft.Management.Infrastructure;
using Microsoft.Management.Infrastructure.Options;
using System.Net;

namespace RunTSLib
{
    public static partial class InstallSoftware
    {
        public static void InstallApp(SecureString password, application app, string ComputerName, string Username, string UserDomain)
        {
            Debug.WriteLine("InstallApp called with a password, app: " + app.Name + " and computer: " + ComputerName);
            string CommandLine = "cmd /e:on /c " + $"pushd {app.WorkingDirectory}&&{app.CommandLine}";
            EmailBuilder email = new("noserver", "nobody", "nobody", "nothing");
            InvokeCMDWait(password, ComputerName, CommandLine, Username, UserDomain, email);
        }

        public static void RunTS(SecureString password, ts TS, string ShareLocation, string ComputerName, string Username, string UserDomain)
        {
            Debug.WriteLine("InstallApp called with a password, ts: " + TS.Name + " and computer: " + ComputerName);
            string CommandLine = "cmd /e:on /c " + $"pushd {ShareLocation}\\&&C:\\Windows\\System32\\cscript.exe Scripts\\LiteTouch.wsf /OSDCompterName:%COMPUTERNAME% /TaskSequenceID:{TS.ID} /SKIPTaskSequence:YES /SKIPComputerName:YES";
            EmailBuilder email = new EmailBuilder("noserver", "nobody", "nobody", "nothing");
            InvokeCMDWait(password, ComputerName, CommandLine, Username, UserDomain, email);
        }

        public static void InstallAppEmailResult(SecureString password, application app, string ComputerName, string UserName, string UserDomain, EmailContact contact)
        {
            string CommandLine = "cmd /e:on /c " + $"pushd {app.WorkingDirectory}\\&&{app.CommandLine}";
            EmailBuilder email = new EmailBuilder(contact, $"Runing {app.Name} on {ComputerName} via Push");
            email.AddTLDR($"Attempting to install application {app.Name} on {ComputerName}...");
            InvokeCMDWait(password, ComputerName, CommandLine, UserName, UserDomain, email);
            email.Send();
        }

        public static void RunTSEmailResult(SecureString password, ts TS, string ShareLocation, string ComputerName, string UserName, string UserDomain, EmailContact contact)
        {
            string CommandLine = "cmd /e:on /c " + $"pushd {ShareLocation}\\&&C:\\Windows\\System32\\cscript.exe Scripts\\LiteTouch.wsf /OSDComputerName:%COMPUTERNAME% /TaskSequenceID:{TS.ID} /SKIPTaskSequence:YES /SKIPComputerName:YES";
            EmailBuilder email = new EmailBuilder(contact, $"Runing {TS.Name} on {ComputerName} via Push");
            email.AddTLDR($"Attempting to run Task Sequence {TS.Name} on {ComputerName}...");
            InvokeCMDWait(password, ComputerName, CommandLine, UserName, UserDomain, email);
            email.Send();
        }
    }
}