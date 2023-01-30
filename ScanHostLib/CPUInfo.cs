using Microsoft.Management.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanHostLib
{
    public class CPUInfo
    {
        private string CPUName;
        private string CPUSpeed;
        private int CPUCores;
        private int CPULogicalCores;

        public string Name { get => this.CPUName; set => this.CPUName = value; }
        public string Speed { get => this.CPUSpeed; set => this.CPUSpeed = value; }
        public int Cores { get => this.CPUCores; set => this.CPUCores = value; }
        public int LogicalProcessors { get => this.CPULogicalCores; set => this.CPULogicalCores = value; }

        public string CoresString { get => this.CPUCores.ToString(); set => this.CPUCores = int.Parse(value); }
        public string LogicalProcessorsString { get => this.CPULogicalCores.ToString(); set => this.CPULogicalCores = int.Parse(value); }
    }

    public static class CPUInfoHelper
    {
        public static CPUInfo GetInfo(string ComputerName)
        {
            CimSession cimSession = CimSession.Create(ComputerName);
            return GetInfo(cimSession);
        }

        public static CPUInfo GetInfo(CimSession cimSession)
        {
            string Namespace = @"root\cimv2";
            string OSQuery = "SELECT * FROM Win32_Processor";
            //            CimSession mySession = CimSession.Create("cs");
            IEnumerable<CimInstance> queryInstance = cimSession.QueryInstances(Namespace, "WQL", OSQuery);
            CimInstance cimInstance = queryInstance.FirstOrDefault();

            CPUInfo cpu = new();

            string[] cpuName = ParseCPUData(cimInstance.CimInstanceProperties["Name"].Value.ToString());

            cpu.Name = cpuName[0];
//            cpu.Name = cimInstance.CimInstanceProperties["Name"].Value.ToString();
            cpu.Speed = cimInstance.CimInstanceProperties["MaxClockSpeed"].Value.ToString();
            cpu.CoresString = (cimInstance.CimInstanceProperties["NumberOfCores"].Value.ToString());
            cpu.LogicalProcessorsString = (cimInstance.CimInstanceProperties["NumberOfLogicalProcessors"].Value.ToString());

            return cpu;
        }

        public static string[] ParseCPUData(string CPUNameSpeedString)
        {
            if (CPUNameSpeedString.StartsWith("Intel"))
            {
                string NameString = CPUNameSpeedString.Substring(0, CPUNameSpeedString.IndexOf('@'));
                string SpeedString = CPUNameSpeedString.Substring(CPUNameSpeedString.IndexOf('@') + 2);
                return new string[] { NameString, SpeedString };
            }

            return new string[] {CPUNameSpeedString, ""};
        }
    }
}
