using Microsoft.Management.Infrastructure;
using ScanHostLib;

namespace ScanHostLibTests
{
    [TestClass]
    public class TestCPUInfo
    {
        // TODO: write tests for CoresString and LogicalProcessorsString

        [TestMethod]
        public void TestGetCPUInfoLocalHostString()
        {
            CPUInfo info = CPUInfoHelper.GetInfo("localhost");
            Assert.IsNotNull(info);
            Assert.AreEqual(12, info.Cores);
            Assert.AreEqual("12th Gen Intel(R) Core(TM) i7-12700K", info.Name);
        }

        [TestMethod]
        public void TestGetInfoLocalHostCimSession()
        {
            CimSession session = CimSession.Create(null);
            CPUInfo info = CPUInfoHelper.GetInfo(session);
            Assert.IsNotNull(info);
            Assert.AreEqual(12, info.Cores);
            Assert.AreEqual("12th Gen Intel(R) Core(TM) i7-12700K", info.Name);
        }
    }
}