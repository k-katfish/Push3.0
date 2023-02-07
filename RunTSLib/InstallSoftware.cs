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

        public static void InstallAppEmailResult(SecureString password, application app, string ComputerName, string UserName, string UserDomain, string smtpServer, string smtpToAddy, string smtpToName)
        {
            string CommandLine = "cmd /e:on /c " + $"pushd {app.WorkingDirectory}\\&&{app.CommandLine}";
            EmailBuilder email = new EmailBuilder(smtpServer, smtpToAddy, smtpToName, $"Runing {app.Name} on {ComputerName} via Push");
            InvokeCMDWait(password, ComputerName, CommandLine, UserName, UserDomain, email);
            email.Send();
        }

        public static void RunTSEmailResult(SecureString password, ts TS, string ShareLocation, string ComputerName, string UserName, string UserDomain, string smtpServer, string smtpToAddy, string smtpToName)
        {
            string CommandLine = "cmd /e:on /c " + $"pushd {ShareLocation}\\&&cscript.exe Scripts\\LiteTouch.wsf /OSDComputerName:%COMPUTERNAME% /TaskSequenceID:{TS.ID} /SKIPTaskSequence:YES /SKIPComputerName:YES";
            EmailBuilder email = new EmailBuilder(smtpServer, smtpToAddy, smtpToName, $"Runing {TS.Name} on {ComputerName} via Push");
            InvokeCMDWait(password, ComputerName, CommandLine, UserName, UserDomain, email);
            email.Send();
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

        public static void InvokeCMDWait(SecureString password, string ComputerName, string CommandLine, string UserName, string UserDomain, EmailBuilder email)
        {
            Debug.WriteLine("Starting Invoke CMD Wait...");
            email.AddLine("Setting up CredSSP from this computer to " + ComputerName);
            string EnableCSSPClientPScmd = $"powershell.exe /c Enable-WSManCredSSP -Role Client -DelegateComputer {ComputerName} -Force";
            CimSession localenablecsspsession = CimSession.Create("localhost");
            CimInstance localwinprocessinstance = new("Win32_Process", "root\\cimv2");
            CimMethodParametersCollection EnableCSSPmethodparameters = new CimMethodParametersCollection {
                CimMethodParameter.Create("commandLine", EnableCSSPClientPScmd, CimFlags.In)
            };
            localenablecsspsession.InvokeMethod(localwinprocessinstance, "create", EnableCSSPmethodparameters);

            email.AddLine("Enabled CredSSP on Local Computer");
            email.AddLine("Enabling CredSSP on: " + ComputerName);

            string EnableCSSPRemotePScmd = "powershell.exe /c Enable-WSManCredSSP -Role Server -Force";
            CimSession remoteenablecsspsession = CimSession.Create(ComputerName);
            CimInstance remotewinprocessinstance = new("Win32_Process", $"\\\\{ComputerName}\\root\\cimv2");
            EnableCSSPmethodparameters = new CimMethodParametersCollection { CimMethodParameter.Create("commandLine", EnableCSSPRemotePScmd, CimFlags.In) };
            remoteenablecsspsession.InvokeMethod(remotewinprocessinstance, "create", EnableCSSPmethodparameters);

            email.AddLine("Enabled CredSSP on Remote");

            //            Debug.WriteLine("Sleeping for 10 s");
            //            Thread.Sleep(10000);
            //            Debug.WriteLine("Finished Sleeping");

            Debug.WriteLine("Enabled CredSSP");

            //* do the thing
            email.AddLine("Planning to run the following command: ");
            email.AddLine(CommandLine);

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
            UInt32 ProcessID = 0;

            while (!didInvoke)
            {
                try
                {
                    CimMethodResult createdSession = session.InvokeMethod(instance, "create", methodParameters);
                    ProcessID = (UInt32)createdSession.OutParameters.FirstOrDefault().Value;
                    didInvoke = true;
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); Thread.Sleep(1000); }
            }

                        Debug.WriteLine("Finished Invoking command, started process with ID: " + ProcessID);
            //            Debug.WriteLine("sleeping");
            //            Thread.Sleep(10000);
            //            Debug.WriteLine("finished sleeping");

            session.Close();

            if (ProcessID == 0)
            {
                Debug.WriteLine("Found PID of 0, that's weird. Quitting.");
                email.AddError("Unable to invoke the command on " + ComputerName);
                email.Send();
                remoteenablecsspsession.Close();
                localenablecsspsession.Close();
                return;
            }

            email.AddLine("Finished invoking " + CommandLine + " on " + ComputerName);
            email.AddLine("waiting for process to finish...");

            Debug.WriteLine("Waiting for process " + ProcessID + " to finish.");

            bool processIsFinished = false;
            string Namespace = $"\\\\{ComputerName}\\root\\cimv2";
            string PSQuery = $"SELECT * FROM Win32_Process WHERE ProcessId = {ProcessID}";
            IEnumerable<CimInstance> processes = remoteenablecsspsession.QueryInstances(Namespace, "WQL", PSQuery);

            while (!processIsFinished)
            {
                try
                {
                    Debug.WriteLine($"Process {ProcessID} is still not finished...");
                    if (processes.Count() > 0)
                    {
                        foreach (var proc in processes)
                        {
                            Debug.WriteLine($"Process {proc.CimInstanceProperties["Name"].Value} exists, waiting...");
                        }
                        Thread.Sleep(1000);
                        try { processes = remoteenablecsspsession.QueryInstances(Namespace, "WQL", PSQuery); }
                        catch (CimException e) { email.AddLine($"Lost contact with computer. Assuming a reboot occured."); Debug.WriteLine(e); processIsFinished = true; }
                    }
                    else { processIsFinished = true; }
                } catch (Exception e)
                {
                    email.AddLine($"Lost contact with computer. Assuming a reboot occured.");
                    Debug.WriteLine(e);
                    processIsFinished = true;
                }
            }

            email.AddLine($"Process with id {ProcessID} finished running on {ComputerName}");
            Debug.WriteLine($"Process with id {ProcessID} finished running on {ComputerName}");

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

            localenablecsspsession.Close();
            remoteenablecsspsession.Close();

            email.AddLine("Disabled CredSSP for this & remote computer.");
            email.AddLine($"Successfully ran {CommandLine} on {ComputerName}");
            Debug.WriteLine($"Done invoking {CommandLine} on {ComputerName}.");
        }
    }
}