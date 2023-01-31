using MDTTSLib;
using MDTAppLib;
using Microsoft.Management.Infrastructure;
using System.Management.Automation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Management.Infrastructure.Options;
using System.Management;

namespace InstallSoftwareLib
{
    public static partial class InstallSoftware 
    {
        public static void InstallApp(application App, CimSession Session) 
        {
            string Command = $"pushd {App.WorkingDirectory}&&{App.CommandLine}";
            InvokeCMD(Command, Session);
        }

        public static void RunTS(ts TaskSequence, CimSession Session)
        {
            string Command = $"pushd {/*DeploymentShareLocation*/TaskSequence}&&cscript.exe Scripts\\LiteTouch.wsf /OSDComputerName:%COMPUTERNAME% /TaskSequenceID:{TaskSequence.Id} /SKIPTaskSequence:YES /SKIPComputerName:YES";
            InvokeCMD(Command, Session);
        }

        private static void InvokeCMD(string CommandLine, CimSession Session)
        {
            CommandLine = "cmd /e:on /c " + CommandLine;
            string Namespace = $"\\\\{Session.ComputerName}\\root\\cimv2";
            CimInstance instance = new("Win32_Process", Namespace);

            CimMethodParametersCollection ArgumentList = new CimMethodParametersCollection
            {
                CimMethodParameter.Create("commandLine", CommandLine, CimFlags.In)
            };

            Session.InvokeMethod(instance, "create", ArgumentList);
        }
    }
}

/*
 * 
 *      $ProcessData = Invoke-CimMethod -CimSession $CimSession -ClassName Win32_Process -MethodName create -Arguments @{
 *          commandline="cmd /e:on /c $Command"
 *      }
 * 
 * 
 */