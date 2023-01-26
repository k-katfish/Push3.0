using System.Collections;

namespace CimSessionHelperLibrary
{
    public class CimSession
    {
        public string ComputerName;
        public ArrayList ComputerArray;
        private string Authentication;
        private uint OperationTimeoutSec;
        private bool SkipTestConnection;
        private uint Port;
        private bool WhatIf;

        public CimSession() { ComputerName = "localhost"; }

// public CimSession(string ComputerName) => this.ComputerName = ComputerName;

        public CimSession(string ComputerName, string Authentication = "Kerberos", uint OperationTimeoutSec = 5, bool SkipTestConnection = false, uint Port = 5985, bool WhatIf = false)
        {
            if (ComputerName == null) { this.ComputerName = "null"; }
            else if (ComputerName.Contains(','))
            {
                ArrayList ComputerArray = new ArrayList(ComputerName.Split(','));
                this.ComputerArray = ComputerArray;
            }
            else { this.ComputerName = ComputerName; }
            this.Authentication = Authentication;
            this.OperationTimeoutSec = OperationTimeoutSec; 
            this.SkipTestConnection = SkipTestConnection; 
            this.Port = Port;
            this.WhatIf = WhatIf;
        }

        public CimSession(ArrayList ComputerArray, string Authentication = "Kerberos", uint OperationTimeoutSec = 5, bool SkipTestConnection = false, uint Port = 5985, bool WhatIf = false)
        {
            this.ComputerArray = ComputerArray;
            this.Authentication = Authentication;
            this.OperationTimeoutSec = OperationTimeoutSec;
            this.SkipTestConnection = SkipTestConnection;
            this.Port = Port;
            this.WhatIf = WhatIf;
        }

        /* These methods are not used during normal operation, and are mostly here for testing */
        public string GetComputerName() { return this.ComputerName; }
        public int GetComputerArrayCount() { return this.ComputerArray.Count; }
        public string GetAuthentication() { return this.Authentication; }
        public uint GetOperationTimeoutSec() { return this.OperationTimeoutSec; }
        public bool GetSkipTestConnection() { return this.SkipTestConnection; }
        public uint GetPort() { return this.Port; }
        public bool GetWhatIf() { return this.WhatIf; }

        ~CimSession() { Console.WriteLine($"Finalizing cimsessionobject for {ComputerName}"); }
    }
}