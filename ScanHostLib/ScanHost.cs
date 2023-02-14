using ScanHostLib;
using Microsoft.Management.Infrastructure;

namespace ScanHost
{
    public class ComputerInfo
    {
        private string ComputerName;
        private CPUInfo CPUInfo;
        private DiskInfo DiskInfo;
        private HardwareInfo HardwareInfo;
        private NICInfo NICInfo;
        private OSInfo OSInfo;

        public ComputerInfo() => this.ComputerName = "localhost";
        public ComputerInfo(string ComputerName) => this.ComputerName = ComputerName;

        public string Name { get => this.ComputerName; set => this.ComputerName = value;  }
        public CPUInfo CPU { get => this.CPUInfo; set => this.CPUInfo = value; }
        public DiskInfo Disk { get => this.DiskInfo; set => this.DiskInfo = value; }
        public HardwareInfo Hardware { get => this.HardwareInfo; set => this.HardwareInfo = value; }
        public NICInfo NIC { get => this.NICInfo; set => this.NICInfo = value; }
        public OSInfo OS { get => this.OSInfo; set => this.OSInfo = value; }
    }

    public static class ScanHostHelper
    {
        public static ComputerInfo Scan(string ComputerName)
        {
            CimSession cs = CimSession.Create(ComputerName);
            CPUInfo cpuInfo = CPUInfoHelper.GetInfo(cs);
            OSInfo osInfo = OSInfoHelper.GetOSInfo(cs);
            NICInfo nicInfo = NICInfoHelper.GetNICInfo(cs);

            ComputerInfo computer = new();
            computer.CPU = cpuInfo;
            computer.OS = osInfo;
            computer.NIC = nicInfo;

            return computer;
            //                string Namespace = @"root\cimv2";
            //                string OSQuery = "SELECT * FROM Win32_OperatingSystem";
            //               CimSession mySession = CimSession.Create("Computer_B");
            //               IEnumerable<CimInstance> queryInstance = mySession.QueryInstances(Namespace, "WQL", OSQuery);
        }
    }
}