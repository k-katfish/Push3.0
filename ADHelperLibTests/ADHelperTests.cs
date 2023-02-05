using ADHelperLib;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;

namespace ADHelperLibTests
{
    [TestClass]
    public class ComputerTests
    {
        [TestMethod]
        public void TestComputerConstructor()
        {
            Computer computer = new Computer(ADHelper.GetComputerPrincipal("CN=MAGELLAN1-01,OU=Studio 1,OU=Magellan,OU=Workstations,OU=Labs,DC=engr,DC=ColoState,DC=EDU"));
            Assert.AreEqual("MAGELLAN1-01", computer.Name);
            Assert.AreEqual(computer.Principal, ADHelper.GetComputerPrincipal("CN=MAGELLAN1-01,OU=Studio 1,OU=Magellan,OU=Workstations,OU=Labs,DC=engr,DC=ColoState,DC=EDU"));
        }

        [TestMethod]
        public void TestComputerFriendlyName() 
        {
            Computer computer = new Computer(ADHelper.GetComputerPrincipal("CN=MAGELLAN1-01,OU=Studio 1,OU=Magellan,OU=Workstations,OU=Labs,DC=engr,DC=ColoState,DC=EDU"));
            Assert.AreEqual("Labs\\Workstations\\Magellan\\Studio 1\\MAGELLAN1-01", computer.FriendlyName);
        }
    }

    [TestClass]
    public class ADHelperTests
    {
        [TestMethod]
        public void TestGetCurrentDomain()
        {
            Assert.AreEqual("DC=engr,DC=ColoState,DC=EDU", ADHelper.GetCurrentDomain());
        }

        [TestMethod]
        public void TestGetComputerPrincipal()
        {
            ComputerPrincipal newCP = ADHelper.GetComputerPrincipal("CN=MAGELLAN1-01,OU=Studio 1,OU=Magellan,OU=Workstations,OU=Labs,DC=engr,DC=ColoState,DC=EDU");
            Assert.IsNotNull(newCP);
            Assert.AreEqual("MAGELLAN1-01", newCP.Name);
        }

        [TestMethod]
        public void TestGetComputersInOU()
        {
            Computers studio = ADHelper.GetComputersInOU("OU=Studio 1,OU=Magellan,OU=Workstations,OU=Labs,DC=engr,DC=ColoState,DC=EDU");
            Assert.AreEqual(9, studio.Count);

            Computer testComputer = new Computer(ADHelper.GetComputerPrincipal("CN=MAGELLAN1-01,OU=Studio 1,OU=Magellan,OU=Workstations,OU=Labs,DC=engr,DC=ColoState,DC=EDU"));

            Debug.WriteLine("test -- testcomputer has name: " + testComputer.Name);
            //Debug.WriteLine("test -- test has name: " + test.DistinguishedName);
            foreach(Computer c in studio)
            {
                Debug.WriteLine("test -- studio contains: " + c.DistinguishedName);
            }

            

            Assert.IsTrue(studio.Contains(testComputer));
        }

        [TestMethod]
        public void TestGetADOU()
        {
            OUs ous = ADHelper.GetOUs("OU=Labs,DC=engr,DC=ColoState,DC=EDU");
            OU testOU = new OU("Workstations", "LDAP://OU=Workstations,OU=Labs,DC=engr,DC=ColoState,DC=EDU", "OU=Workstations,OU=Labs,DC=engr,DC=ColoState,DC=EDU");
            Assert.IsTrue(ous.Contains(testOU));
        }

        [TestMethod]
        public void TestGetDomainString() 
        {
            Assert.AreEqual("engr.ColoState.EDU", ADHelper.GetDomainString());
        }

        [TestMethod]
        public void TestGetFriendlyName()
        {
            Assert.AreEqual("Labs\\Workstations\\Magellan\\Studio 1", ADHelper.GetFriendlyName("OU=Studio 1,OU=Magellan,OU=Workstations,OU=Labs,DC=engr,DC=ColoState,DC=EDU"));
            Assert.AreEqual("Labs\\Workstations\\Magellan\\Studio 1\\Magellan1-01", ADHelper.GetFriendlyName("CN=Magellan1-01,OU=Studio 1,OU=Magellan,OU=Workstations,OU=Labs,DC=engr,DC=ColoState,DC=EDU"));
        }
    }
}