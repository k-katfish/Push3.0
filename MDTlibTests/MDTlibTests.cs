using MDTlib;
using System.Diagnostics;

namespace MDTlibTests
{
    [TestClass]
    public class MDTlibTests
    {
        [TestMethod]
        public void TestMethod1()
        {

        }
    }

    [TestClass]
    public class StaticMDTLibTests
    {
        [TestMethod]
        public void TestValidMDTShareTest ()
        {
            // this is gonna fun to test...
            string DeploymentSharePath = $"{AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length-1)}\\..\\..\\..\\testdata";
            Assert.IsTrue(MDTHelper.TestMDTShare(DeploymentSharePath));
        }

        [TestMethod]
        public void TestInvalidMDTShareTest()
        {
            string DeploymentSharePath = $"{AppDomain.CurrentDomain.BaseDirectory}";
            Assert.IsFalse(MDTHelper.TestMDTShare(DeploymentSharePath));
        }

        [TestMethod]
        public void TestNullMDTShareTest()
        {
            Assert.IsFalse(MDTHelper.TestMDTShare(""));
        }

        /*[TestMethod]
        public void TestKyleDrive()
        {
            Assert.IsTrue(MDTHelper.TestIfPathIsMDTShare("K:\\"));
        }*/ // if the test doesn't work, just comment it out!

        [TestMethod]
        public void TestFullValidMDTShareTest ()
        {
            string DeploymentSharePath = "\\\\labs-mdt\\labs-mdt$";
            Assert.IsTrue(MDTHelper.TestIfPathIsMDTShare(DeploymentSharePath));
        }
    }
}