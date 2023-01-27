using System.Collections;

namespace MDTAppLib
{
    /*[System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class applications
    {
        private applicationsApplication[] applicationField;

        [System.Xml.Serialization.XmlElementAttribute("application")]
        public applicationsApplication[] application { get => this.applicationField; set => this.applicationField = value; }

        public applicationsApplication this[int index] { get => this.applicationField[index]; set => this.applicationField[index] = value; }
        public int Count() { return this.applicationField.Count(); }

        public void add(applicationsApplication applicationsApplication) { this.applicationField}
    }


    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
    public enum ItemsChoiceType { CommandLine, Comments, CreatedBy, CreatedTime, Dependency, DisplayName, 
        Language, LastModifiedBy, LastModifiedTime, Name, Publisher, Reboot,
        ShortName, Source, UninstallKey, Version,  WorkingDirectory
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class applicationsApplication
    {
        //private object[] itemsField;
        //private ItemsChoiceType[] itemsElementNameField;
        private string guidField;
        private string enableField;
        private string hideField;
        private string nameField;
        private string commandLineField;
        private string commentsField;
        private string createdByField;
        private string createdTimeField;
        private string dependencyField;
        private string displayNameField;
        private string languageField;
        private string lastModifiedByField;
        private string lastModifiedTimeField;
        private string publisherField;
        private string rebootField;
        private string shortNameField;
        private string versionField;
        private string workingDirectoryField;
        private string uninstallKeyField;

        /*[System.Xml.Serialization.XmlElementAttribute("CommandLine", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Comments", typeof(object))]
        [System.Xml.Serialization.XmlElementAttribute("CreatedBy", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("CreatedTime", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Dependency", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DisplayName", typeof(object))]
        [System.Xml.Serialization.XmlElementAttribute("Language", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("LastModifiedBy", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("LastModifiedTime", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Name", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Publisher", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Reboot", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("ShortName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Source", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("UninstallKey", typeof(object))]
        [System.Xml.Serialization.XmlElementAttribute("Version", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("WorkingDirectory", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items { get => this.itemsField; set => this.itemsField = value; }

        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType[] ItemsElementName { get => this.itemsElementNameField; set => this.itemsElementNameField = value; }
        *//*
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string guid { get => this.guidField; set => this.guidField = value; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string enable { get => this.enableField; set => this.enableField = value; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string hide { get => this.hideField; set => this.hideField = value; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string commandLine { get => this.commandLineField; set => this.commandLineField = value; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name { get => this.nameField;set => this.nameField = value; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string comments { get => this.commentsField; set => this.commentsField = value; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string createdBy { get => this.createdByField; set => this.createdByField = value; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string createdTime { get => this.createdTimeField; set => this.createdTimeField = value;
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string dependency { get => this.dependencyField;set => this.dependencyField = value; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string displayName { get => this.displayNameField;set => this.displayNameField = value; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string language { get => this.languageField; set => this.languageField = value; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string lastModifiedBy { get => this.lastModifiedByField; set => this.lastModifiedByField = value; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string lastModifiedTime { get=> this.lastModifiedTimeField;set => this.lastModifiedTimeField = value; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string publisher { get => this.publisherField;set => this.publisherField = value; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string reboot { get => this.rebootField;set => this.rebootField = value; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string shortName { get => this.shortNameField;set => this.shortNameField = value; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string version { get => this.versionField;set => this.versionField = value; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string workingDirectory { get => this.workingDirectoryField;set => this.workingDirectoryField = value;}

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string uninstallKey { get => this.uninstallKeyField;set => this.uninstallKeyField = value;}
    }*/

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
    }

    [SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class applications : IEnumerable<application>
    {
        List<application> apps = new System.Collections.Generic.List<application>();

        public IEnumerator<application> GetEnumerator()
        {
            return ((IEnumerable<application>)apps).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)apps).GetEnumerator();
        }

        public void Add(application app) { this.apps.Add(app); }
        public void Remove(application app) { this.apps.Remove(app); }

        public void Clear() { this.apps = new System.Collections.Generic.List<application>(); }
        public bool Contains(application app) { return this.apps.Contains(app); }

        public int Count() { return this.apps.Count; }

        public application GetAt(int index) { return this.apps[index]; }

        public application this[int index] { get => this.apps[index]; set => this.apps[index] = value; }

        //public MDTApp At(int index) { return this.apps[index]; }
    }
}