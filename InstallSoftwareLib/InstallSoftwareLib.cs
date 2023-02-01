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
using System.Security;
using Microsoft.VisualBasic;
using System.Net;

namespace InstallSoftwareLib
{
    public static partial class InstallSoftware 
    {
        public static void InstallApp(application App, CimSession Session) 
        {
            string Command = $"pushd {App.WorkingDirectory}&&{App.CommandLine}";
            //InvokeCMD(Command, Session);
        }

        public static void InstallApp(SecureString password, application app, string ComputerName)
        {
            //string Command = $"pushd {app.WorkingDirectory}&&{app.CommandLine}";
            string Command = $"msg kkatfish Hello, world";
            CimSession session = EnableWSManCredSSP(password, ComputerName);
            InvokeCMD(Command, session, ComputerName);
        }

        public static void RunTS(ts TaskSequence, CimSession Session)
        {
            string Command = $"pushd {/*DeploymentShareLocation*/TaskSequence}&&cscript.exe Scripts\\LiteTouch.wsf /OSDComputerName:%COMPUTERNAME% /TaskSequenceID:{TaskSequence.Id} /SKIPTaskSequence:YES /SKIPComputerName:YES";
            //InvokeCMD(Command, Session);
        }

        public static void RunTS(SecureString password, ts TaskSequence, string ShareLocation, string ComputerName)
        {
            //string Command = $"pushd {ShareLocation}\\Scripts&&cscript.exe Scripts\\LiteTouch.wsf /OSDComputerName:%COMPUTERNAME% /TaskSequenceID:{TaskSequence.Id} /SKIPTaskSequence:YES /SKIPComputerName:YES";
            string Command = $"pushd {ShareLocation}\\Scripts&&msg kkatfish Hello, world!";
            CimSession session = EnableWSManCredSSP(password, ComputerName);
            InvokeCMD(Command, session, ComputerName);
        }

        public static void InvokeCMD(string CommandLine, CimSession Session, string ComputerName)
        {
            //CimCredential credential = GetCredential();

            //CimSession EnabledSession = EnableWSManCredSSP(credential, Session.ComputerName);

            CommandLine = "cmd /e:on /c " + CommandLine;
            string Namespace = @"\\" + $"{ComputerName}" + @"\root\cimv2";
            CimInstance instance = new("Win32_Process", Namespace);

            CimMethodParametersCollection ArgumentList = new CimMethodParametersCollection
            {
                CimMethodParameter.Create("commandLine", CommandLine, CimFlags.In)
            };

            try { Session.InvokeMethodAsync(instance, "create", ArgumentList); }
            catch (Exception e) { throw new Exception($"Failed to act on '{ComputerName}'", e); }

            DisableWSManCredSSP(ComputerName);
        }

        /*private static CimSession CreateCredSSPCimSession(SecureString password, string ComputerName)
        {
            SecureString PassCreds = new NetworkCredential(Environment.UserName, password, Environment.UserDomainName).SecurePassword;
            CimCredential credential = new CimCredential(PasswordAuthenticationMechanism.CredSsp, Environment.UserDomainName, Environment.UserName, PassCreds);
            return EnableWSManCredSSP(credential, ComputerName);
        }*/

        public static CimSession EnableWSManCredSSP(SecureString password, string ComputerName)
        {
            string EnableCSSPClientPScmd = $"powershell.exe /c Enable-WSManCredSSP -Role Client -DelegateComputer {ComputerName} -Force";
            CimSession localenablecsspsession = CimSession.Create("localhost");
            CimInstance localwinprocessinstance = new("Win32_Process", "root\\cimv2");
            CimMethodParametersCollection EnableClientCSSPmethodparameters = new CimMethodParametersCollection { CimMethodParameter.Create("commandLine", EnableCSSPClientPScmd, CimFlags.In) };
            localenablecsspsession.InvokeMethodAsync(localwinprocessinstance, "create", EnableClientCSSPmethodparameters);

            string EnableCSSPServerPScmd = "powershell.exe /c Enable-WSManCredSSP -Role Server -Force";
            CimSession remoteenablecsspsession = CimSession.Create(ComputerName);
            CimInstance remotewinprocessinstance = new("Win32_Process", $"\\\\{ComputerName}\\root\\cimv2");
            CimMethodParametersCollection EnableServerCSSPmethodparameters = new CimMethodParametersCollection { CimMethodParameter.Create("commandLine", EnableCSSPServerPScmd, CimFlags.In) };
            remoteenablecsspsession.InvokeMethodAsync(remotewinprocessinstance, "create", EnableServerCSSPmethodparameters);

            SecureString PassCreds = new NetworkCredential(Environment.UserName, password, Environment.UserDomainName).SecurePassword;
            CimCredential credential = new CimCredential(PasswordAuthenticationMechanism.CredSsp, Environment.UserDomainName, Environment.UserName, PassCreds);

            WSManSessionOptions options = new WSManSessionOptions();
            options.AddDestinationCredentials(credential);
            CimSession CredSSPSession = CimSession.Create(ComputerName, options);

            return CredSSPSession;
        }

        public static CimSession EnableTestWMPDFkjsd(CimCredential cimCredential, string ComputerName)
        {
            string EnableCSSPClientPScmd = $"powershell.exe /c Enable-WSManCredSSP -Role Client -DelegateComputer {ComputerName} -Force";
            CimSession localenablecsspsession = CimSession.Create("localhost");
            CimInstance localwinprocessinstance = new("Win32_Process", "root\\cimv2");
            CimMethodParametersCollection EnableClientCSSPmethodparameters = new CimMethodParametersCollection { CimMethodParameter.Create("commandLine", EnableCSSPClientPScmd, CimFlags.In) };
            localenablecsspsession.InvokeMethodAsync(localwinprocessinstance, "create", EnableClientCSSPmethodparameters);

            string EnableCSSPServerPScmd = "powershell.exe /c Enable-WSManCredSSP -Role Server -Force";
            CimSession remoteenablecsspsession = CimSession.Create(ComputerName);
            CimInstance remotewinprocessinstance = new("Win32_Process", $"\\\\{ComputerName}\\root\\cimv2");
            CimMethodParametersCollection EnableServerCSSPmethodparameters = new CimMethodParametersCollection { CimMethodParameter.Create("commandLine", EnableCSSPServerPScmd, CimFlags.In) };
            remoteenablecsspsession.InvokeMethodAsync(remotewinprocessinstance, "create", EnableServerCSSPmethodparameters);

            WSManSessionOptions options = new WSManSessionOptions();
            options.AddDestinationCredentials(cimCredential);
            CimSession CredSSPSession = CimSession.Create(ComputerName, options);

            return CredSSPSession;
        }

        private static void DisableWSManCredSSP(string ComputerName)
        {
            string DisableCSSPClientPScmd = $"powershell.exe /c Disable-WSManCredSSP -Role Client -Force";
            CimSession localenablecsspsession = CimSession.Create("localhost");
            CimInstance localwinprocessinstance = new("Win32_Process", "root\\cimv2");
            CimMethodParametersCollection DisableClientCSSPmethodparameters = new CimMethodParametersCollection { CimMethodParameter.Create("commandLine", DisableCSSPClientPScmd, CimFlags.In) };
            localenablecsspsession.InvokeMethodAsync(localwinprocessinstance, "create", DisableClientCSSPmethodparameters);

            string DisableCSSPServerPScmd = "powershell.exe /c Disable-WSManCredSSP -Role Server -Force";
            CimSession remoteenablecsspsession = CimSession.Create(ComputerName);
            CimInstance remotewinprocessinstance = new("Win32_Process", $"\\\\{ComputerName}\\root\\cimv2");
            CimMethodParametersCollection DisableServerCSSPmethodparameters = new CimMethodParametersCollection { CimMethodParameter.Create("commandLine", DisableCSSPServerPScmd, CimFlags.In) };
            remoteenablecsspsession.InvokeMethodAsync(remotewinprocessinstance, "create", DisableServerCSSPmethodparameters);
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