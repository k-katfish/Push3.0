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
        public static void InvokeCMDWait(SecureString password, string ComputerName, string CommandLine, string UserName, string UserDomain, EmailBuilder email)
        {
            DateTime StartTime = DateTime.Now;
            Debug.WriteLine("Starting Invoke CMD Wait...");
            email.AddLine("Setting up CredSSP from this computer to " + ComputerName);
            string EnableCSSPClientPScmd = $"powershell.exe /c Enable-WSManCredSSP -Role Client -DelegateComputer {ComputerName} -Force";
            CimSession localenablecsspsession = CimSession.Create("localhost");
            CimInstance localwinprocessinstance = new("Win32_Process", "\\\\localhost\\root\\cimv2");
            CimMethodParametersCollection EnableCSSPmethodparameters = new CimMethodParametersCollection {
                CimMethodParameter.Create("commandLine", EnableCSSPClientPScmd, CimFlags.In)
            };
            localenablecsspsession.InvokeMethod(localwinprocessinstance, "create", EnableCSSPmethodparameters);

            email.AddLine("Enabled CredSSP on Local Computer with delegation to " + ComputerName);
            email.AddLine("Enabling CredSSP on: " + ComputerName);

            string EnableCSSPRemotePScmd = "powershell.exe /c Enable-WSManCredSSP -Role Server -Force";
            CimSession remoteenablecsspsession = CimSession.Create(ComputerName);
            CimInstance remotewinprocessinstance = new("Win32_Process", $"\\\\{ComputerName}\\root\\cimv2");
            EnableCSSPmethodparameters = new CimMethodParametersCollection { CimMethodParameter.Create("commandLine", EnableCSSPRemotePScmd, CimFlags.In) };
            remoteenablecsspsession.InvokeMethod(remotewinprocessinstance, "create", EnableCSSPmethodparameters);

            email.AddLine("Enabled CredSSP on Remote");
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
            CimInstance instance = new("Win32_Process", $"\\\\{ComputerName}\\root\\cimv2");
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
#pragma warning disable CS8602
                    ProcessID = (UInt32)createdSession.OutParameters.FirstOrDefault().Value;
#pragma warning restore CS8602
                    didInvoke = true;
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); Thread.Sleep(1000); }
            }

            Debug.WriteLine("Finished Invoking command, started process with ID: " + ProcessID);
            session.Close();

            if (ProcessID == 0)
            {
                DateTime FailEndTime = DateTime.Now;
                Debug.WriteLine("Found PID of 0, that's weird. Quitting.");
                email.AddTLDR("Process failed.");
                email.AddTLDR("Total Time Taken:" + (FailEndTime - StartTime));
                email.AddError("Unable to invoke the command on " + ComputerName);
                email.AddError("Info: Found PID of 0, which is weird.");
                email.Send();
                remoteenablecsspsession.Close();
                localenablecsspsession.Close();
                return;
            }

            email.AddLine("Finished invoking " + CommandLine + " on " + ComputerName);
            email.AddLine($"waiting for process {ProcessID} to finish...");

            Debug.WriteLine("Waiting for process " + ProcessID + " to finish.");

            bool processIsFinished = false;
            bool lostContact = false;
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
                        catch (CimException e) { email.AddLine($"Lost contact with computer. Assuming a reboot occured."); Debug.WriteLine(e); processIsFinished = true; lostContact = true; }
                    }
                    else { processIsFinished = true; }
                } catch (Exception e)
                {
                    email.AddLine($"Lost contact with computer. Assuming a reboot occured.");
                    Debug.WriteLine(e);
                    processIsFinished = true;
                    lostContact = true;
                }
            }

            email.AddLine($"Process with id {ProcessID} finished running on {ComputerName}");
            Debug.WriteLine($"Process with id {ProcessID} finished running on {ComputerName}");

            //*Unsetup CredSSP
            string DisableCSSPClientPScmd = "powershell.exe /c Disable-WSManCredSSP -Role Client";
            CimMethodParametersCollection DisableCSSPmethodparameters = new CimMethodParametersCollection { CimMethodParameter.Create("commandLine", DisableCSSPClientPScmd, CimFlags.In) };
            localenablecsspsession.InvokeMethod(localwinprocessinstance, "create", DisableCSSPmethodparameters);

            if (!lostContact)
            {
                string DisableCSSPRemotePScmd = "powershell.exe /c Disable-WSManCredSSP -Role Server";
                DisableCSSPmethodparameters = new CimMethodParametersCollection { CimMethodParameter.Create("commandLine", DisableCSSPRemotePScmd, CimFlags.In) };
                remoteenablecsspsession.InvokeMethod(remotewinprocessinstance, "create", DisableCSSPmethodparameters);
                email.AddLine("Disabled CredSSP on remote computer, it was still online.");
            }

            localenablecsspsession.Close();
            remoteenablecsspsession.Close();

            email.AddLine("Disabled CredSSP for this & remote computer.");
            email.AddLine($"Successfully ran {CommandLine} on {ComputerName}");
            Debug.WriteLine($"Done invoking {CommandLine} on {ComputerName}.");

            email.AddTLDR($"Installation was successful!");
            DateTime EndTime = DateTime.Now;
            email.AddTLDR("Total Time Taken:" + (EndTime - StartTime));
        }
    }
}