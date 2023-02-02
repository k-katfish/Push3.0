using MDTTSLib;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
#pragma warning disable CS8602
#pragma warning disable CS8600


namespace MDTTSLibTests
{
    [TestClass]
    public class MDTTSLibTests_tsObject
    {
        ts myTS = new();

        [TestMethod]
        public void TestTSObject()
        {
            Assert.IsNotNull(myTS);
        }

        [TestMethod]
        public void TestTSName()
        {
            myTS.Name = "myTS";
            Assert.AreEqual("myTS", myTS.Name);
        }

        [TestMethod]
        public void TestTSComments()
        {
            myTS.Comments = "myComment";
            Assert.AreEqual("myComment", myTS.Comments);
        }

        [TestMethod]
        public void TestTSCreatedTime()
        {
            myTS.CreatedTime = "11:53 10 June, 1984";
            Assert.AreEqual("11:53 10 June, 1984", myTS.CreatedTime);
        }

        [TestMethod]
        public void TestTSCreatedBy()
        {
            myTS.CreatedBy = "kkatfish";
            Assert.AreEqual("kkatfish", myTS.CreatedBy);
        }

        [TestMethod]
        public void TestTSLastModified()
        {
            myTS.LastModifiedTime = "11:53 10 June, 2004";
            myTS.LastModifiedBy = "kkatfishjr";
            Assert.AreEqual("11:53 10 June, 2004", myTS.LastModifiedTime);
            Assert.AreEqual("kkatfishjr", myTS.LastModifiedBy);
        }

        [TestMethod]
        public void TestIDField()
        {
            myTS.ID = "myTS";
            Assert.AreEqual("myTS", myTS.ID);
        }

        [TestMethod]
        public void TestVersionField()
        {
            myTS.Version = "21H2";
            Assert.AreEqual("21H2", myTS.Version);
        }

        [TestMethod]
        public void TestTSTemplate()
        {
            myTS.TaskSequenceTemplate = "MyTSTemplate.xml";
            Assert.AreEqual("MyTSTemplate.xml", myTS.TaskSequenceTemplate);
        }

        [TestMethod]
        public void TestGUID()
        {
            Guid guid = new Guid();
            myTS.Guid = guid.ToString();
            Assert.AreEqual(guid.ToString(), myTS.Guid);
        }

        [TestMethod]
        public void TestEnable()
        {
            myTS.Enable = "false";
            Assert.AreEqual("false", myTS.Enable);
        }

        [TestMethod]
        public void TestEnabledValidData()
        {
            myTS.Enabled = false;
            Assert.AreEqual(false, myTS.Enabled);
            Assert.AreEqual("false", myTS.Enable);
            myTS.Enabled = true;
            Assert.AreEqual(true, myTS.Enabled);
            Assert.AreEqual("true", myTS.Enable);
        }

        [TestMethod]
        public void TestHidden()
        {
            myTS.Hidden = false;
            Assert.AreEqual(false, myTS.Hidden);
            Assert.AreEqual("false", myTS.Hide);
            myTS.Hidden = true;
            Assert.AreEqual(true, myTS.Hidden);
            Assert.AreEqual("true", myTS.Hide);
        }
    }

    [TestClass]
    public class TestTSS
    {
        tss TaskSequences = new();
        ts TS = new("myTS0");
        ts TS1 = new("myTS1");

        [TestMethod]
        public void TestConstructor()
        {
            Assert.IsNotNull(TaskSequences);
        }

        [TestMethod]
        public void TestAddRemoveCount()
        {
            TaskSequences.Add(TS);
            Assert.AreEqual(1, TaskSequences.Count());
            TaskSequences.Add(TS1);
            Assert.AreEqual(2, TaskSequences.Count());
            TaskSequences.Add(TS);
            Assert.AreEqual(3, TaskSequences.Count());
            TaskSequences.Remove(TS1);
            Assert.AreEqual(2, TaskSequences.Count());
            TaskSequences.Clear();
            Assert.AreEqual(0, TaskSequences.Count());
        }

        [TestMethod]
        public void TestGet()
        {
            TS.Guid = "myGUID1";
            TS1.Guid = "myGUID2";

            TaskSequences.Add(TS);
            TaskSequences.Add(TS1);
            Assert.AreEqual(TS, TaskSequences.GetAt(0));
            Assert.AreEqual(TS1, TaskSequences[1]);
            Assert.AreEqual("myGUID1", TaskSequences[0].Guid);
        }
    }

    [TestClass]
    public class TestReadingTaskSequenceData
    {
        [TestMethod]
        [Ignore]
        public void TestSerializeDeserialize()
        {
            string TestDataFolderRoot = $"{AppDomain.CurrentDomain.BaseDirectory}";

            ts TS1 = new ts("MyApp1");
            ts TS2 = new ts("MyApp2");
            tss TSS = new tss { TS1, TS2 };
            XmlSerializer MySerializer = new XmlSerializer(typeof(tss));
            FileStream TestApplicationsFile = new FileStream($"{TestDataFolderRoot}\\TestTSData.xml", FileMode.OpenOrCreate);
            MySerializer.Serialize(TestApplicationsFile, TSS);
            TestApplicationsFile.Close();

            Assert.IsTrue(File.Exists($"{TestDataFolderRoot}\\TestTSData.xml"));

            //applications newApps = new applications();
            XmlSerializer xmlData = new XmlSerializer(typeof(tss));
            FileStream TestTSFile = new FileStream($"{TestDataFolderRoot}\\TestTSData.xml", FileMode.Open);
            tss newTSS = xmlData.Deserialize(TestTSFile) as tss;
            TestTSFile.Close();
            Assert.AreEqual(newTSS.Count(), 2);
            Assert.AreEqual(newTSS[0].Name, "MyApp1");
            Assert.AreEqual(newTSS[0].Name, TSS[0].Name);
            Assert.AreEqual(newTSS[1].Name, TSS[1].Name);
            Assert.AreNotEqual(newTSS[0].Name, TSS[1].Name);
        }

        [TestMethod]
        public void TestDeserializeSampleData()
        {
            XmlSerializer sampledata = new XmlSerializer(typeof(tss));
            FileStream SampleTSFile = new FileStream($"{AppDomain.CurrentDomain.BaseDirectory}..\\..\\..\\..\\SampleMDTShare\\Control\\TaskSequences.xml", FileMode.Open);
            tss sampleTSs = sampledata.Deserialize(SampleTSFile) as tss;
            SampleTSFile.Close();
            Assert.AreEqual("Thin Client Image", sampleTSs[0].Name);
            Assert.AreEqual("ReIamge.xml", sampleTSs[0].TaskSequenceTemplate);
            Assert.AreEqual(1, sampleTSs.Count());
        }

        [TestMethod]
        public void TestDeserializeMethod()
        {
            tss TSS = new();
            TSS = tssHelper.GetTaskSequencesFromShare($"{AppDomain.CurrentDomain.BaseDirectory}..\\..\\..\\..\\SampleMDTShare");
            Assert.AreEqual("Thin Client Image", TSS[0].Name);
            Assert.AreEqual("ReIamge.xml", TSS[0].TaskSequenceTemplate);
            Assert.AreEqual(1, TSS.Count());
        }
    }
}

#pragma warning restore CS8602
#pragma warning restore CS8600