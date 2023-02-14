using PushSettingsLib;

namespace PushSettingsTests
{
    [TestClass]
    public class SettingsTests
    {
        [TestMethod]
        public void TestGetShare()
        {
            Settings settings = new Settings();
            Assert.IsNotNull(settings);
            Assert.IsNotNull(settings.MDTShareLocation);
            Assert.IsTrue(settings.MDTShareLocation.StartsWith("\\\\"), "Expected MDTShare Location to start with \\\\ but MDTShareLocation was: " + settings.MDTShareLocation);
        }
    }
}