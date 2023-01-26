using CimSessionHelperLibrary;
using System.Collections;

namespace CimSessionHelperLibraryTests
{
    [TestClass]
    public class CimSessionHelperDefaultTests
    {
        [TestMethod]
        public void TestEmptyConstructor()
        {
            CimSession session = new CimSession();
            Assert.IsNotNull(session);
        }

        [TestMethod]
        public void TestSingleComputerInConstructor()
        {
            CimSession session = new CimSession("localhost", WhatIf:true);
            Assert.IsTrue(session.ComputerName == "localhost");
        }

        [TestMethod]
        public void TestMultipleComputerInConstructor()
        {
            CimSession session = new CimSession("localhost, localhost");
            Assert.IsTrue(session.GetComputerArrayCount() == 2);

            ArrayList testlist = new ArrayList{ "localhost", "LocalHost" };
            CimSession MultiSession = new CimSession(testlist);
            Assert.IsTrue(MultiSession.GetComputerArrayCount() == 2);
        }

        [TestMethod]
        public void TestNullComputerInConstructor() 
        {
            CimSession session = new CimSession(ComputerName:null);
            Assert.IsTrue(session.ComputerName == "null");
        }

        [TestMethod]
        public void TestFullConstuctor()
        {
            CimSession session = new CimSession("localhost", Authentication: "CredSSP", OperationTimeoutSec:4, SkipTestConnection:true, Port:1234, WhatIf:true);
            Assert.IsTrue(session.GetWhatIf());
            Assert.IsTrue(session.GetSkipTestConnection());
            Assert.IsTrue(session.ComputerName == "localhost");
            Assert.IsTrue(session.GetAuthentication() == "CredSSP");
            Assert.IsTrue(session.GetOperationTimeoutSec() == 4);
            Assert.IsTrue(session.GetPort() == 1234);
        }
    }
}