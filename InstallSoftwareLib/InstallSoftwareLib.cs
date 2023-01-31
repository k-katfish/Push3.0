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
            //            Session.InvokeMethod(CimInstance.Win32_Process, "Create", CimMethodParametersCollection())
            //Session.InvokeMethodAsync
            //CimMethodParametersCollection cmpc = new CimMethodParametersCollection();
            //cmpc.Add(CimMethodParameter.Create("MethodName", "Create", CimFlags.Any));
            string Command = $"pushd {App.WorkingDirectory}&&{App.CommandLine}";
            InvokeCMD(Command, Session);
        }

        public static void RunTS(ts TaskSequence, CimSession Session)
        {
            string Command = $"pushd {/*DeploymentShareLocation*/TaskSequence}&&cscript.exe Scripts\\LiteTouch.wsf /OSDComputerName:%COMPUTERNAME% /TaskSequenceID:$TaskSequenceID /SKIPTaskSequence:YES /SKIPComputerName:YES";
            InvokeCMD(Command, Session);
        }

        private static void InvokeCMD(string CommandLine, CimSession Session)
        {
            /*using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                string Command = $"Invoke-CimMethod -Session {Session} -ClassName Win32_Process -MethodName create -ArgumentList @{{ commandLine = cmd.exe /e:on /c {CommandLine}}}";
                PowerShellInstance.AddCommand(Command);
            }*/

            var process = new[] { $"cmd.exe /e:on /c {CommandLine}" };
            var connection = new ConnectionOptions();
            connection.Impersonation = ImpersonationLevel.Delegate;
            //connection.Username = "";
            //connection.Password = [somesecurestringhere];
            var wmiScope = new ManagementScope($"\\\\{Session.ComputerName}\\root\\cimv2", connection);
            var wmiProcess = new ManagementClass(wmiScope, new ManagementPath("Win32_Process"), new ObjectGetOptions());
            wmiProcess.InvokeMethod("Create", process);


            /*
            const string hvNamespace = @"root\cimv2";

            var sessionOptions = new DComSessionOptions
            { Timeout = TimeSpan.FromSeconds(30) };

            //var cimSession = CimSession.Create("localhost", sessionOptions);

            // Get an instance of the VM to snapshot
            var vm = Session.QueryInstances(hvNamespace, "WQL", $"SELECT * FROM Win32_Process").First();

            // Get the instance of Msvm_VirtualSystemSnapshotService. There is only one.
            // var vmSnapshotService = Session.EnumerateInstances(hvNamespace, "Msvm_VirtualSystemSnapshotService").First();

            var ProcessCreateService = Session.EnumerateInstances(hvNamespace, "Win32_Process").First();

            // Set the snapshot parameters by creating a Msvm_VirtualSystemSnapshotSettingData
            var snapshotSettings = new CimInstance("Msvm_VirtualSystemSnapshotSettingData");
            snapshotSettings.CimInstanceProperties.Add(CimProperty.Create("ConsistencyLevel", 1, CimType.UInt8, CimFlags.ReadOnly));
            snapshotSettings.CimInstanceProperties.Add(CimProperty.Create("IgnoreNonSnapshottableDisks", true, CimFlags.ReadOnly));


            // set the cmd/process parameters by creating a Win32_Process
            var ProcessSetup = new CimInstance("Win32_Process");
            ProcessSetup.CimInstanceProperties.Add(CimProperty.Create("??", "??", CimType.String, CimFlags.ReadOnly));



            // Put all of these things into a CimMethodParametersCollection.
            // Note; no need to specify the "Out" parameters. They will be returned by the call to InvokeMethod.
            var methodParameters = new CimMethodParametersCollection
            {
                /*CimMethodParameter.Create("AffectedSystem", vm, CimType.Reference, CimFlags.In),
                CimMethodParameter.Create("SnapshotSettings", snapshotSettings.ToString(), CimType.String, CimFlags.In),
                CimMethodParameter.Create("SnapshotType", 2, CimType.UInt16, CimFlags.In), * /
                CimMethodParameter.Create("commandLine", CommandLine, CimType.String, CimFlags.In)
                /* I think this is how it works ?????? * /
            };

            Session.InvokeMethod(hvNamespace, ProcessCreateService, "Create", methodParameters);

            Console.WriteLine($"Snapshot created!");*/
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