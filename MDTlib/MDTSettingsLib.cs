using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;

namespace MDTlib
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Settings
    {
        private string versionField;
        private string descriptionField;
        private object commentsField;
        private string enableMulticastField;
        private string supportX86Field;
        private string supportX64Field;
        private string uNCPathField;
        private string physicalPathField;
        private string monitorHostField;
        private ushort monitorEventPortField;
        private ushort monitorDataPortField;
        private string skipWimSplitField;
        private string bootx86UseBootWimField;
        private byte bootx86ScratchSpaceField;
        private string bootx86IncludeAllDriversField;
        private string bootx86IncludeNetworkDriversField;
        private string bootx86IncludeMassStorageDriversField;
        private string bootx86IncludeVideoDriversField;
        private string bootx86IncludeSystemDriversField;
        private string bootx86BackgroundFileField;
        private object bootx86ExtraDirectoryField;
        private string bootx86GenerateGenericWIMField;
        private string bootx86GenerateGenericISOField;
        private string bootx86GenericWIMDescriptionField;
        private string bootx86GenericISONameField;
        private string bootx86GenerateLiteTouchISOField;
        private string bootx86LiteTouchWIMDescriptionField;
        private string bootx86LiteTouchISONameField;
        private string bootx86SelectionProfileField;
        private string bootx86SupportUEFIField;
        private string bootx86FeaturePacksField;
        private string bootx64UseBootWimField;
        private byte bootx64ScratchSpaceField;
        private string bootx64IncludeAllDriversField;
        private string bootx64IncludeNetworkDriversField;
        private string bootx64IncludeMassStorageDriversField;
        private string bootx64IncludeVideoDriversField;
        private string bootx64IncludeSystemDriversField;
        private string bootx64BackgroundFileField;
        private object bootx64ExtraDirectoryField;
        private string bootx64GenerateGenericWIMField;
        private string bootx64GenerateGenericISOField;
        private string bootx64GenericWIMDescriptionField;
        private string bootx64GenericISONameField;
        private string bootx64GenerateLiteTouchISOField;
        private string bootx64LiteTouchWIMDescriptionField;
        private string bootx64LiteTouchISONameField;
        private string bootx64SelectionProfileField;
        private string bootx64SupportUEFIField;
        private string bootx64FeaturePacksField;
        private object databaseSQLServerField;
        private object databaseInstanceField;
        private object databasePortField;
        private object databaseNetlibField;
        private object databaseNameField;
        private object databaseSQLShareField;

        public string Version { get => this.versionField; set => this.versionField = value; }
        public string Description { get => this.descriptionField; set => this.descriptionField = value; }
        public object Comments { get => this.commentsField; set => this.commentsField = value; }
        public string EnableMulticast { get => this.enableMulticastField; set => this.enableMulticastField = value; }
        public string SupportX86 { get => this.supportX86Field; set => this.supportX86Field = value; }

        /// <remarks/>
        public string SupportX64
        {
            get
            {
                return this.supportX64Field;
            }
            set
            {
                this.supportX64Field = value;
            }
        }

        /// <remarks/>
        public string UNCPath
        {
            get
            {
                return this.uNCPathField;
            }
            set
            {
                this.uNCPathField = value;
            }
        }

        /// <remarks/>
        public string PhysicalPath
        {
            get
            {
                return this.physicalPathField;
            }
            set
            {
                this.physicalPathField = value;
            }
        }

        /// <remarks/>
        public string MonitorHost
        {
            get
            {
                return this.monitorHostField;
            }
            set
            {
                this.monitorHostField = value;
            }
        }

        /// <remarks/>
        public ushort MonitorEventPort
        {
            get
            {
                return this.monitorEventPortField;
            }
            set
            {
                this.monitorEventPortField = value;
            }
        }

        /// <remarks/>
        public ushort MonitorDataPort
        {
            get
            {
                return this.monitorDataPortField;
            }
            set
            {
                this.monitorDataPortField = value;
            }
        }

        /// <remarks/>
        public string SkipWimSplit
        {
            get
            {
                return this.skipWimSplitField;
            }
            set
            {
                this.skipWimSplitField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x86.UseBootWim")]
        public string Bootx86UseBootWim
        {
            get
            {
                return this.bootx86UseBootWimField;
            }
            set
            {
                this.bootx86UseBootWimField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x86.ScratchSpace")]
        public byte Bootx86ScratchSpace
        {
            get
            {
                return this.bootx86ScratchSpaceField;
            }
            set
            {
                this.bootx86ScratchSpaceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x86.IncludeAllDrivers")]
        public string Bootx86IncludeAllDrivers
        {
            get
            {
                return this.bootx86IncludeAllDriversField;
            }
            set
            {
                this.bootx86IncludeAllDriversField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x86.IncludeNetworkDrivers")]
        public string Bootx86IncludeNetworkDrivers
        {
            get
            {
                return this.bootx86IncludeNetworkDriversField;
            }
            set
            {
                this.bootx86IncludeNetworkDriversField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x86.IncludeMassStorageDrivers")]
        public string Bootx86IncludeMassStorageDrivers
        {
            get
            {
                return this.bootx86IncludeMassStorageDriversField;
            }
            set
            {
                this.bootx86IncludeMassStorageDriversField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x86.IncludeVideoDrivers")]
        public string Bootx86IncludeVideoDrivers
        {
            get
            {
                return this.bootx86IncludeVideoDriversField;
            }
            set
            {
                this.bootx86IncludeVideoDriversField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x86.IncludeSystemDrivers")]
        public string Bootx86IncludeSystemDrivers
        {
            get
            {
                return this.bootx86IncludeSystemDriversField;
            }
            set
            {
                this.bootx86IncludeSystemDriversField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x86.BackgroundFile")]
        public string Bootx86BackgroundFile
        {
            get
            {
                return this.bootx86BackgroundFileField;
            }
            set
            {
                this.bootx86BackgroundFileField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x86.ExtraDirectory")]
        public object Bootx86ExtraDirectory
        {
            get
            {
                return this.bootx86ExtraDirectoryField;
            }
            set
            {
                this.bootx86ExtraDirectoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x86.GenerateGenericWIM")]
        public string Bootx86GenerateGenericWIM
        {
            get
            {
                return this.bootx86GenerateGenericWIMField;
            }
            set
            {
                this.bootx86GenerateGenericWIMField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x86.GenerateGenericISO")]
        public string Bootx86GenerateGenericISO
        {
            get
            {
                return this.bootx86GenerateGenericISOField;
            }
            set
            {
                this.bootx86GenerateGenericISOField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x86.GenericWIMDescription")]
        public string Bootx86GenericWIMDescription
        {
            get
            {
                return this.bootx86GenericWIMDescriptionField;
            }
            set
            {
                this.bootx86GenericWIMDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x86.GenericISOName")]
        public string Bootx86GenericISOName
        {
            get
            {
                return this.bootx86GenericISONameField;
            }
            set
            {
                this.bootx86GenericISONameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x86.GenerateLiteTouchISO")]
        public string Bootx86GenerateLiteTouchISO
        {
            get
            {
                return this.bootx86GenerateLiteTouchISOField;
            }
            set
            {
                this.bootx86GenerateLiteTouchISOField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x86.LiteTouchWIMDescription")]
        public string Bootx86LiteTouchWIMDescription
        {
            get
            {
                return this.bootx86LiteTouchWIMDescriptionField;
            }
            set
            {
                this.bootx86LiteTouchWIMDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x86.LiteTouchISOName")]
        public string Bootx86LiteTouchISOName
        {
            get
            {
                return this.bootx86LiteTouchISONameField;
            }
            set
            {
                this.bootx86LiteTouchISONameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x86.SelectionProfile")]
        public string Bootx86SelectionProfile
        {
            get
            {
                return this.bootx86SelectionProfileField;
            }
            set
            {
                this.bootx86SelectionProfileField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x86.SupportUEFI")]
        public string Bootx86SupportUEFI
        {
            get
            {
                return this.bootx86SupportUEFIField;
            }
            set
            {
                this.bootx86SupportUEFIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x86.FeaturePacks")]
        public string Bootx86FeaturePacks
        {
            get
            {
                return this.bootx86FeaturePacksField;
            }
            set
            {
                this.bootx86FeaturePacksField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x64.UseBootWim")]
        public string Bootx64UseBootWim
        {
            get
            {
                return this.bootx64UseBootWimField;
            }
            set
            {
                this.bootx64UseBootWimField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x64.ScratchSpace")]
        public byte Bootx64ScratchSpace
        {
            get
            {
                return this.bootx64ScratchSpaceField;
            }
            set
            {
                this.bootx64ScratchSpaceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x64.IncludeAllDrivers")]
        public string Bootx64IncludeAllDrivers
        {
            get
            {
                return this.bootx64IncludeAllDriversField;
            }
            set
            {
                this.bootx64IncludeAllDriversField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x64.IncludeNetworkDrivers")]
        public string Bootx64IncludeNetworkDrivers
        {
            get
            {
                return this.bootx64IncludeNetworkDriversField;
            }
            set
            {
                this.bootx64IncludeNetworkDriversField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x64.IncludeMassStorageDrivers")]
        public string Bootx64IncludeMassStorageDrivers
        {
            get
            {
                return this.bootx64IncludeMassStorageDriversField;
            }
            set
            {
                this.bootx64IncludeMassStorageDriversField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x64.IncludeVideoDrivers")]
        public string Bootx64IncludeVideoDrivers
        {
            get
            {
                return this.bootx64IncludeVideoDriversField;
            }
            set
            {
                this.bootx64IncludeVideoDriversField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x64.IncludeSystemDrivers")]
        public string Bootx64IncludeSystemDrivers
        {
            get
            {
                return this.bootx64IncludeSystemDriversField;
            }
            set
            {
                this.bootx64IncludeSystemDriversField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x64.BackgroundFile")]
        public string Bootx64BackgroundFile
        {
            get
            {
                return this.bootx64BackgroundFileField;
            }
            set
            {
                this.bootx64BackgroundFileField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x64.ExtraDirectory")]
        public object Bootx64ExtraDirectory
        {
            get
            {
                return this.bootx64ExtraDirectoryField;
            }
            set
            {
                this.bootx64ExtraDirectoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x64.GenerateGenericWIM")]
        public string Bootx64GenerateGenericWIM
        {
            get
            {
                return this.bootx64GenerateGenericWIMField;
            }
            set
            {
                this.bootx64GenerateGenericWIMField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x64.GenerateGenericISO")]
        public string Bootx64GenerateGenericISO
        {
            get
            {
                return this.bootx64GenerateGenericISOField;
            }
            set
            {
                this.bootx64GenerateGenericISOField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x64.GenericWIMDescription")]
        public string Bootx64GenericWIMDescription
        {
            get
            {
                return this.bootx64GenericWIMDescriptionField;
            }
            set
            {
                this.bootx64GenericWIMDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x64.GenericISOName")]
        public string Bootx64GenericISOName
        {
            get
            {
                return this.bootx64GenericISONameField;
            }
            set
            {
                this.bootx64GenericISONameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x64.GenerateLiteTouchISO")]
        public string Bootx64GenerateLiteTouchISO
        {
            get
            {
                return this.bootx64GenerateLiteTouchISOField;
            }
            set
            {
                this.bootx64GenerateLiteTouchISOField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x64.LiteTouchWIMDescription")]
        public string Bootx64LiteTouchWIMDescription
        {
            get
            {
                return this.bootx64LiteTouchWIMDescriptionField;
            }
            set
            {
                this.bootx64LiteTouchWIMDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x64.LiteTouchISOName")]
        public string Bootx64LiteTouchISOName
        {
            get
            {
                return this.bootx64LiteTouchISONameField;
            }
            set
            {
                this.bootx64LiteTouchISONameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x64.SelectionProfile")]
        public string Bootx64SelectionProfile
        {
            get
            {
                return this.bootx64SelectionProfileField;
            }
            set
            {
                this.bootx64SelectionProfileField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x64.SupportUEFI")]
        public string Bootx64SupportUEFI
        {
            get
            {
                return this.bootx64SupportUEFIField;
            }
            set
            {
                this.bootx64SupportUEFIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Boot.x64.FeaturePacks")]
        public string Bootx64FeaturePacks
        {
            get
            {
                return this.bootx64FeaturePacksField;
            }
            set
            {
                this.bootx64FeaturePacksField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Database.SQLServer")]
        public object DatabaseSQLServer
        {
            get
            {
                return this.databaseSQLServerField;
            }
            set
            {
                this.databaseSQLServerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Database.Instance")]
        public object DatabaseInstance
        {
            get
            {
                return this.databaseInstanceField;
            }
            set
            {
                this.databaseInstanceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Database.Port")]
        public object DatabasePort
        {
            get
            {
                return this.databasePortField;
            }
            set
            {
                this.databasePortField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Database.Netlib")]
        public object DatabaseNetlib
        {
            get
            {
                return this.databaseNetlibField;
            }
            set
            {
                this.databaseNetlibField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Database.Name")]
        public object DatabaseName
        {
            get
            {
                return this.databaseNameField;
            }
            set
            {
                this.databaseNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Database.SQLShare")]
        public object DatabaseSQLShare
        {
            get
            {
                return this.databaseSQLShareField;
            }
            set
            {
                this.databaseSQLShareField = value;
            }
        }
    }

    public static class SettingsHelper
    {
        public static Settings? GetSettingsFromShare(string ShareLocation)
        {
            if (!File.Exists($"{ShareLocation}\\Control\\Settings.xml")) { return null; }
            XmlSerializer MDTData = new XmlSerializer(typeof(Settings));
            FileStream MDTSXML = new FileStream($"{ShareLocation}\\Control\\Settings.xml", FileMode.Open);
            Settings MDTS = MDTData.Deserialize(MDTSXML) as Settings;
            MDTSXML.Close();
            return MDTS;
        }
    }
}