using FixesAndScriptsLib;
using System.Diagnostics.Tracing;

namespace FixesAndScriptsLibTests
{
    [TestClass]
    public class FixesLibTests
    {
        public string SampleShare = $"{AppDomain.CurrentDomain.BaseDirectory}..\\..\\..\\..\\SampleMDTShare";

        [TestMethod]
        public void TestReadFolders()
        {
            Fixes fixes = FixesHelper.GetFixes(SampleShare);
            DirectoryInfo testdir = new DirectoryInfo($"{SampleShare}\\Fixes\\Fix a License");
            Assert.IsNotNull( fixes );
            Assert.AreEqual(fixes[0].WorkingDirectory, testdir.FullName);
        }

        [TestMethod]
        public void TestGetFixes()
        {
            Fixes fixes = FixesHelper.GetFixes(SampleShare);
            FileInfo licTheFixBat = new FileInfo($"{SampleShare}\\Fixes\\Fix a License\\TheFix.bat");
            Assert.IsNotNull(fixes);
            Assert.AreEqual(fixes[0].BatchFiles[0].FullName, licTheFixBat.FullName);
        }

        [TestMethod]
        public void TestIncludingHiddenItems() {
            Fixes fixes = FixesHelper.GetFixes(SampleShare);
            DirectoryInfo HiddenDir = new DirectoryInfo($"{SampleShare}\\Fixes\\Hidden Fix"); 
            Assert.IsNotNull(fixes);
            Assert.AreEqual(fixes[2].WorkingDirectory, HiddenDir.FullName);
        }
    }
}