using System.Collections;
using System.Xml.Serialization;

namespace MDTAppLib
{
    [SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class application
    {
        private string nameField;
        private string createdTimeField;
        private string createdByField;
        private string lastModifiedTimeField;
        private string lastModifiedByField;
        private string shortNameField;
        private string versionField;
        private string publisherField;
        private string languageField;
        private string sourceField;
        private string commandLineField;
        private string workingDirectoryField;
        private string rebootField;
        private string guidField;
        private string enableField;
        private string hideField;

        public string Name { get { return this.nameField; } set { this.nameField = value; } }

        public string CreatedTime { get { return this.createdTimeField; } set { this.createdTimeField = value; } }

        public string CreatedBy { get { return this.createdByField; } set { this.createdByField = value; } }

        public string LastModifiedTime { get { return this.lastModifiedTimeField; } set { this.lastModifiedTimeField = value; } }

        public string LastModifiedBy { get { return this.lastModifiedByField; } set { this.lastModifiedByField = value; } }

        public string ShortName { get { return this.shortNameField; } set { this.shortNameField = value; } }

        public string Version { get { return this.versionField; } set { this.versionField = value; } }

        public string Publisher { get { return this.publisherField; } set { this.publisherField = value; } }

        public string Language { get { return this.languageField; } set { this.languageField = value; } }

        public string Source { get { return this.sourceField; } set { this.sourceField = value; } }

        public string CommandLine { get { return this.commandLineField; } set { this.commandLineField = value; } }

        public string WorkingDirectory { get { return this.workingDirectoryField; } set { this.workingDirectoryField = value; } }

        public string Reboot { get { return this.rebootField; } set { this.rebootField = value; } }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string guid { get { return this.guidField; } set { this.guidField = value; } }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string enable { get { return this.enableField; } set { this.enableField = value; } }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string hide { get { return this.hideField; } set { this.hideField = value; } }

        public application() { }
        public application(string Name) => this.Name = Name;

        public bool Equals (application other) { return this.Name == other.Name; }

        public override string ToString()
        {
            return this.Name;
        }
    }

    [SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class applications : IEnumerable<application>
    {
        List<application> apps = new List<application>();

        public IEnumerator<application> GetEnumerator() { return ((IEnumerable<application>)apps).GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return ((IEnumerable)apps).GetEnumerator(); }

        public void Add(application app) { this.apps.Add(app); }
        public void Remove(application app) { this.apps.Remove(app); }
        public void Clear() { this.apps = new List<application>(); }
        public bool Contains(application app) { return this.apps.Contains(app); }
        public int Count() { return this.apps.Count; }

        public application GetAt(int index) { return this.apps[index]; }
        public application this[int index] { get => this.apps[index]; set => this.apps[index] = value; }
    }

    public static class applicationsHelper
    {
        public static applications? GetApplicationsFromShare(string ShareLocation)
        {
            if (!File.Exists($"{ShareLocation}\\Control\\Applications.xml")) { return null; }
            XmlSerializer MDTData = new XmlSerializer(typeof(applications));
            FileStream MDTApplicationsXML = new FileStream($"{ShareLocation}\\Control\\Applications.xml", FileMode.Open);
            applications MDTApplications = MDTData.Deserialize(MDTApplicationsXML) as applications;
            MDTApplicationsXML.Close();

            foreach (application app in MDTApplications)
            {
                if (app.WorkingDirectory == null) { continue; }
                if (app.WorkingDirectory.StartsWith(@".\"))
                {
                    app.WorkingDirectory = ShareLocation + app.WorkingDirectory.Substring(1);
                }
            }

            return MDTApplications;
        }
    }
}