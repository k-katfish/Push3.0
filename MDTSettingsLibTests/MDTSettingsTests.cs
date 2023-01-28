using MDTSettingsLib;

namespace MDTSettingsLibTests
{
    [TestClass]
    public class MDTSettingsTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Settings settings = new();
            settings = SettingsHelper.GetSettingsFromShare($"{AppDomain.CurrentDomain.BaseDirectory}..\\..\\..\\..\\SampleMDTShare");
            Assert.IsNotNull(settings);
            Assert.AreEqual("My MDT Deployment Share", settings.Description);
        }
    }
}