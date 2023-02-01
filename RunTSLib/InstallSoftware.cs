using System.Security;
using MDTAppLib;
using MDTTSLib;
using System.Diagnostics;
using Microsoft.Management.Infrastructure;
using Microsoft.Management.Infrastructure.Options;
using System.Net;

namespace RunTSLib
{
    public static class InstallSoftware
    {
        public static void InstallApp(SecureString password, application app, string ComputerName, string Username, string UserDomain)
        {
            Debug.WriteLine("InstallApp called with a password, app: " + app.Name + " and computer: " + ComputerName);
            string CommandLine = "cmd /e:on /c " + $"pushd {app.WorkingDirectory}&&{app.CommandLine}";
            InvokeCMD(password, ComputerName, CommandLine, Username, UserDomain);
        }

        public static void RunTS(SecureString password, ts TS, string ShareLocation, string ComputerName, string Username, string UserDomain)
        {
            Debug.WriteLine("InstallApp called with a password, ts: " + TS.Name + " and computer: " + ComputerName);
            string CommandLine = "cmd /e:on /c " + $"pushd {ShareLocation}\\&&cscript.exe Scripts\\LiteTouch.wsf /OSDCompterName:%COMPUTERNAME% /TaskSequenceID:{TS.ID} /SKIPTaskSequence:YES /SKIPComputerName:YES";
            InvokeCMD(password, ComputerName, CommandLine, Username, UserDomain);
        }

        public static void InvokeCMD(SecureString password, string ComputerName, string CommandLine, string UserName, string UserDomain)
        {
            // I'm so not ready for dependencys yet...

            Debug.WriteLine("Setting up CredSSP");
            string EnableCSSPClientPScmd = $"powershell.exe /c Enable-WSManCredSSP -Role Client -DelegateComputer {ComputerName} -Force";
            CimSession localenablecsspsession = CimSession.Create("localhost");
            CimInstance localwinprocessinstance = new("Win32_Process", "root\\cimv2");
            CimMethodParametersCollection EnableCSSPmethodparameters = new CimMethodParametersCollection { 
                CimMethodParameter.Create("commandLine", EnableCSSPClientPScmd, CimFlags.In)
            };
            localenablecsspsession.InvokeMethod(localwinprocessinstance, "create", EnableCSSPmethodparameters);

            Debug.WriteLine("Enabled CredSSP on Local Computer");
            Debug.WriteLine("Enabling CredSSP on remote");

            string EnableCSSPRemotePScmd = "powershell.exe /c Enable-WSManCredSSP -Role Server -Force";
            CimSession remoteenablecsspsession = CimSession.Create(ComputerName);
            CimInstance remotewinprocessinstance = new("Win32_Process", $"\\\\{ComputerName}\\root\\cimv2");
            EnableCSSPmethodparameters = new CimMethodParametersCollection { CimMethodParameter.Create("commandLine", EnableCSSPRemotePScmd, CimFlags.In) };
            remoteenablecsspsession.InvokeMethod(remotewinprocessinstance, "create", EnableCSSPmethodparameters);

            Debug.WriteLine("Enabled CredSSP on Remote");

            //            Debug.WriteLine("Sleeping for 10 s");
            //            Thread.Sleep(10000);
            //            Debug.WriteLine("Finished Sleeping");


            //* do the thing
            Debug.WriteLine(CommandLine);

            SecureString p = new NetworkCredential(UserName, password, UserDomain).SecurePassword;
            CimCredential cred = new CimCredential(PasswordAuthenticationMechanism.CredSsp, UserDomain, UserName, p);

            WSManSessionOptions options = new WSManSessionOptions();
            options.AddDestinationCredentials(cred);

            CimSession session = CimSession.Create(ComputerName, options);

            string ns = $"\\\\{ComputerName}\\root\\cimv2";
            CimInstance instance = new("Win32_Process", ns);

            CimMethodParametersCollection methodParameters = new CimMethodParametersCollection {
                CimMethodParameter.Create("commandLine", CommandLine, CimFlags.In)
            };

            bool didInvoke = false;

            while (!didInvoke)
            {
                try
                {
                    session.InvokeMethod(instance, "create", methodParameters);
                    didInvoke = true;
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); }
            }

            //            Debug.WriteLine("Finished Invoking command");
            //            Debug.WriteLine("sleeping");
            //            Thread.Sleep(10000);
            //            Debug.WriteLine("finished sleeping");

            session.Close();

            //*Unsetup CredSSP
            string DisableCSSPClientPScmd = "powershell.exe /c Disable-WSManCredSSP -Role Client";
            //CimSession localenablecsspsession = CimSession.Create("localhost");
            //CimInstance localwinprocessinstance = new("Win32_Process", "root\\cimv2");
            CimMethodParametersCollection DisableCSSPmethodparameters = new CimMethodParametersCollection { CimMethodParameter.Create("commandLine", DisableCSSPClientPScmd, CimFlags.In) };
            localenablecsspsession.InvokeMethod(localwinprocessinstance, "create", DisableCSSPmethodparameters);

            string DisableCSSPRemotePScmd = "powershell.exe /c Disable-WSManCredSSP -Role Server";
            //CimSession remoteenablecsspsession = CimSession.Create("ander-02");
            //CimInstance remotewinprocessinstance = new("Win32_Process", "root\\cimv2");
            DisableCSSPmethodparameters = new CimMethodParametersCollection { CimMethodParameter.Create("commandLine", DisableCSSPRemotePScmd, CimFlags.In) };
            remoteenablecsspsession.InvokeMethod(remotewinprocessinstance, "create", DisableCSSPmethodparameters);

            Debug.WriteLine("Disabled CredSSP, finished.");
        }
    }
}