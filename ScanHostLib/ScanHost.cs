using ScanHostLib;
using Microsoft.Management.Infrastructure;

namespace ScanHost
{
    public class ScanHost
    {
        private string ComputerName;
        private CPUInfo CPUInfo;
        private DiskInfo DiskInfo;
        private HardwareInfo HardwareInfo;
        private NICInfo NICInfo;
        private OSInfo OSInfo;

        public ScanHost() => this.ComputerName = "localhost";
        public ScanHost(string ComputerName) => this.ComputerName = ComputerName;

        public string Name { get => this.ComputerName; set => this.ComputerName = value;  }
        public CPUInfo CPU { get => this.CPUInfo; set => this.CPUInfo = value; }
        public DiskInfo Disk { get => this.DiskInfo; set => this.DiskInfo = value; }
        public HardwareInfo Hardware { get => this.HardwareInfo; set => this.HardwareInfo = value; }
        public NICInfo NIC { get => this.NICInfo; set => this.NICInfo = value; }
        public OSInfo Software { get => this.OSInfo; set => this.OSInfo = value; }


        private static class ScanHostHelper
        {
            public static ScanHost Scan(string ComputerName)
            {
                CimSession cs = CimSession.Create(ComputerName);
                OSInfo info = OSInfoHelper.GetOSInfo(cs);
                return new ScanHost();
//                string Namespace = @"root\cimv2";
//                string OSQuery = "SELECT * FROM Win32_OperatingSystem";
 //               CimSession mySession = CimSession.Create("Computer_B");
 //               IEnumerable<CimInstance> queryInstance = mySession.QueryInstances(Namespace, "WQL", OSQuery);
            }
        }
    }
}