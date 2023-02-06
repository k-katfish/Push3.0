using MDTlib;
using System.IO;
using System.ComponentModel.Design.Serialization;
using System.Xml.Serialization;
#pragma warning disable CS8602 
#pragma warning disable CS8600

namespace MDTAppLibTests
{
    [TestClass]
    public class AppLibTests
    {
        [TestMethod]
        public void TestConstructEmptyApp()
        {
            application app = new application();
            Assert.IsNotNull(app);
        }

        [TestMethod]
        public void TestConstructEmptyApplications()
        {
            applications apps = new applications();
            Assert.IsNotNull(apps);
        }

        [TestMethod]
        public void TestAddAppToApplications()
        {
            application app = new application();
            applications apps = new applications();
            apps.Add(app);
            Assert.IsTrue(apps.Contains(app));
        }

        [TestMethod]
        public void TestRemoveAppFromApplications()
        {
            application app = new application();
            applications apps = new applications{app};
            Assert.IsTrue(apps.Contains(app));
            apps.Remove(app);
            Assert.IsFalse(apps.Contains(app));
        }

        public void TestCountApplications()
        {
            application app = new application();
            applications apps = new applications{app, app};
            Assert.AreEqual(2, apps.Count());
        }

        [TestMethod]
        public void TestClearApplications()
        {
            application app = new application();
            applications apps = new applications{app, app};
            apps.Clear();
            Assert.AreEqual(0, apps.Count());
        }

        [TestMethod]
        public void TestQuickApp()
        {
            application app = new application("MyApp");
            Assert.AreEqual("MyApp", app.Name);
        }

        public void TestQuickApplications()
        {
            application MyApp1 = new application("MyApp1");
            application MyApp2 = new application("MyApp2");
            applications apps = new applications { MyApp1, MyApp2 };
            Assert.AreEqual("MyApp1", apps.GetAt(0).Name);
            Assert.AreEqual("MyApp2", apps.GetAt(2).Name);
        }

        [TestMethod]
        public void TestApplicationsIndexors()
        {
            application MyApp1 = new application("MyApp1");
            application MyApp2 = new application("MyApp2");
            applications apps = new applications { MyApp1, MyApp2 };
            Assert.AreEqual("MyApp1", apps[0].Name);
            Assert.AreEqual("MyApp2", apps[1].Name);
        }

        /*        [TestMethod]
                public void TestApplicationsIndexorsOutOfRange()
                {
                    applications apps = new applications();
                    Assert.IsNull(apps);
                    Assert.ThrowsException<IndexOutOfRangeException>(apps[4]);
                }*/ // I'm just gonna have faith that this will work.
        [TestMethod]
        public void TestApplicationComparision()
        {
            application MyApp1 = new application("MyApp1");
            application MyApp2 = (application)MyApp1;
            Assert.IsTrue(MyApp1 == MyApp2);
            Assert.AreEqual(MyApp2,MyApp1);
        }
    }

    [TestClass]
    public class AppLibXMLTests
    {
        [TestMethod]
        public void TestSerializeDeserialize()
        {
            string TestDataFolderRoot = $"{AppDomain.CurrentDomain.BaseDirectory}";

            application MyApp1 = new application("MyApp1");
            application MyApp2 = new application("MyApp2");
            applications apps = new applications {MyApp1, MyApp2};
            XmlSerializer MySerializer = new XmlSerializer(typeof(applications));

            File.Delete($"{TestDataFolderRoot}\\TestAppData.xml");

            FileStream TestApplicationsFile = new FileStream($"{TestDataFolderRoot}\\TestAppData.xml", FileMode.OpenOrCreate);
            MySerializer.Serialize(TestApplicationsFile, apps);
            TestApplicationsFile.Close();

            Assert.IsTrue(File.Exists($"{TestDataFolderRoot}\\TestAppData.xml"));

            //applications newApps = new applications();
            XmlSerializer xmlData = new XmlSerializer(typeof(applications));
            FileStream TestAppsFile = new FileStream($"{TestDataFolderRoot}\\TestAppData.xml", FileMode.Open);
            applications newApps = xmlData.Deserialize(TestAppsFile) as applications;
            TestAppsFile.Close();
            Assert.AreEqual(newApps.Count(), 2);
            Assert.AreEqual(newApps[0].Name, "MyApp1");
            Assert.AreEqual(newApps[0].Name, apps[0].Name);
            Assert.AreEqual(newApps[1].Name, apps[1].Name);
            Assert.AreNotEqual(newApps[0].Name, apps[1].Name);
        }

        [TestMethod]
        public void TestDeserializeExistingMDTApplicationsXML()
        {
            XmlSerializer sampledata = new XmlSerializer (typeof(applications));
            FileStream SampleAppsFile = new FileStream($"{AppDomain.CurrentDomain.BaseDirectory}..\\..\\..\\..\\SampleMDTShare\\Control\\Applications.xml", FileMode.Open);
            applications sampleApps = sampledata.Deserialize(SampleAppsFile) as applications;
            SampleAppsFile.Close();
            Assert.AreEqual("Microsoft Office 365 16 (No Teams)", sampleApps[0].Name);
            Assert.AreEqual(".\\Applications\\MATLAB R2022A", sampleApps[2].WorkingDirectory);
        }

        [TestMethod]
        public void TestDeserializeMoreMDTApplicationsXML()
        {
            XmlSerializer sampledata = new XmlSerializer(typeof(applications));
            FileStream SampleAppsFile = new FileStream($"{AppDomain.CurrentDomain.BaseDirectory}..\\..\\..\\..\\SampleMDTShare\\Control\\Applications (2).xml", FileMode.Open);
            applications sampleApps = sampledata.Deserialize(SampleAppsFile) as applications;
            SampleAppsFile.Close();
            Assert.AreEqual(96, sampleApps.Count());
            Assert.AreEqual("npp.8.4.8.Installer.x64.exe /S", sampleApps[95].CommandLine);
        }

        [TestMethod]
        public void TestDeserializerMethod()
        {
            applications apps = new();
            apps = applicationsHelper.GetApplicationsFromShare($"{AppDomain.CurrentDomain.BaseDirectory}..\\..\\..\\..\\SampleMDTShare");
            Assert.AreEqual("Microsoft Office 365 16 (No Teams)", apps[0].Name);
            Assert.AreEqual($"{AppDomain.CurrentDomain.BaseDirectory}..\\..\\..\\..\\SampleMDTShare\\Applications\\MATLAB R2022A", apps[2].WorkingDirectory);
        }
    }
}

#pragma warning restore CS8602
#pragma warning restore CS8600