using System.Collections;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MDTTSLib
{
    [SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class ts
    {
        private string nameField;
        private string commentsField;
        private string createdTimeField;
        private string createdByField;
        private string lastModifiedTimeField;
        private string lastModifiedByField;
        private string idField;
        private string versionField;
        private string taskSequenceTemplateField;
        private string guidField;
        private string enableField;
        private string hideField;

        public string Name { get { return this.nameField; } set { this.nameField = value; } }
        public string Comments { get => this.commentsField; set { this.commentsField = value; } }
        public string CreatedTime { get { return this.createdTimeField; } set { this.createdTimeField = value; } }
        public string CreatedBy { get { return this.createdByField; } set { this.createdByField = value; } }
        public string LastModifiedTime { get { return this.lastModifiedTimeField; } set { this.lastModifiedTimeField = value; } }
        public string LastModifiedBy { get { return this.lastModifiedByField; } set { this.lastModifiedByField = value; } }
        public string Id { get { return this.idField; } set { this.idField = value; } }
        public string Version { get { return this.versionField; } set { this.versionField = value; } }
        public string TaskSequenceTemplate { get { return this.taskSequenceTemplateField; } set { this.taskSequenceTemplateField = value; } }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Guid { get { return this.guidField; } set { this.guidField = value; } }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Enable { get { return this.enableField; } set { this.enableField = value; } }

        public bool Enabled
        {
            get
            {
                if (this.enableField.Equals("false")) { return false; }
                else { return true; }
            }

            set
            {
                if (value.Equals(true)) { this.enableField = "true"; }
                else if (value.Equals(false)) { this.enableField = "false"; }
                else { throw new ArgumentException($"task sequence.Enabled was expecting true or false, saw {value}"); }
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Hide { get { return this.hideField; } set { this.hideField = value; } }

        public bool Hidden
        {
            get
            {
                if (this.hideField.Equals("false")) { return false; }
                else { return true; }
            }

            set
            {
                if (value.Equals(true)) { this.hideField = "true"; }
                else if (value.Equals(false)) { this.hideField = "false"; }
                else { throw new ArgumentException($"task sequence.Hidden was expecting true or false, saw {value}"); }
            }
        }

        public ts() { }
        public ts(string Name) => this.Name = Name;
        public bool Equals(ts other) { return this.Guid == other.Guid; }
        public override string ToString()
        {
            return this.Name;
        }
    }

    [SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class tss : IEnumerable<ts>
    {
        List<ts> taskss = new List<ts>();

        public IEnumerator<ts> GetEnumerator() { return ((IEnumerable<ts>)taskss).GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return ((IEnumerable)taskss).GetEnumerator(); }

        public void Add(ts app) { this.taskss.Add(app); }
        //TODO: AddRange
        public void Remove(ts app) { this.taskss.Remove(app); }
        //TODO: RemoveAt
        public void Clear() { this.taskss = new List<ts>(); }
        public bool Contains(ts app) { return this.taskss.Contains(app); }
        public int Count() { return this.taskss.Count; }
        public ts GetAt(int index) { return this.taskss[index]; }

        public ts this[int index] { get => this.taskss[index]; set => this.taskss[index] = value; }
    }

    public static class tssHelper
    {
        public static tss? GetTaskSequencesFromShare(string ShareLocation)
        {
            if(!File.Exists($"{ShareLocation}\\Control\\TaskSequences.xml")) { return null; }
            XmlSerializer MDTData = new XmlSerializer(typeof(tss));
            FileStream MDTTSXML = new FileStream($"{ShareLocation}\\Control\\TaskSequences.xml", FileMode.Open);
            tss MDTTS = MDTData.Deserialize(MDTTSXML) as tss;
            MDTTSXML.Close();
            return MDTTS;
        }
    }
}