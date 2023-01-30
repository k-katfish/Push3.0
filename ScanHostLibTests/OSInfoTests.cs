using ScanHostLib;
using Microsoft.Management.Infrastructure;

namespace ScanHostLibTests 
{
    [TestClass]
    public class TestOSInfo
    {
        [TestMethod]
        public void TestGetOSInfoForLocalHost()
        {
            OSInfo info = OSInfoHelper.GetOSInfo("localhost");
            Assert.IsNotNull(info);
            Assert.AreEqual("10.0.22621", info.Version);
        }

        [TestMethod]
        public void TestGetOSInfoForCimLocalHost()
        {
            CimSession lcimsession = CimSession.Create(null);
            OSInfo info = OSInfoHelper.GetOSInfo(lcimsession);
            Assert.IsNotNull(info);
            Assert.AreEqual("10.0.22621", info.Version);
        }
    }
}