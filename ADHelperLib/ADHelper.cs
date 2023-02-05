using System.DirectoryServices.AccountManagement;
using System.Collections;
using System.DirectoryServices;
using System.Diagnostics;
using System.Security;

namespace ADHelperLib
{
    public class Computer : IComparable<Computer>
    {
        private readonly string _Name;
        private ComputerPrincipal _computerPrincipal;
        private readonly string _DistinguishedName;
        private readonly string _FriendlyName;

        public Computer(ComputerPrincipal computerPrincipal)
        {
            _computerPrincipal = computerPrincipal;
            _Name = computerPrincipal.Name;
            _DistinguishedName = computerPrincipal.DistinguishedName;
            _FriendlyName = ADHelper.GetFriendlyName(_DistinguishedName);
        }

        public string Name { get => _Name; }
        public string DistinguishedName { get => _DistinguishedName; }
        public ComputerPrincipal Principal { get => _computerPrincipal; set => _computerPrincipal = value; }
        public string FriendlyName { get => _FriendlyName; }

        public override string ToString() { return _Name; }
        public override int GetHashCode() { return base.GetHashCode(); }
        public override bool Equals(object? obj) { return _DistinguishedName.Equals(((Computer)obj).DistinguishedName); }

        public int CompareTo(Computer? other) { return Name.CompareTo(other.Name); }
    }

    public class Computers : IEnumerable<Computer>
    {
        List<Computer> _computers;

        public IEnumerator<Computer> GetEnumerator() { return _computers.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public Computers() { _computers = new List<Computer>(); }
        public void Add(Computer computer) { _computers.Add(computer); }
        public void Remove(Computer computer) { _computers.Remove(computer); }
        public bool Contains(Computer computer) { return _computers.Contains(computer); }
        public int Count { get => _computers.Count; }

        public void Sort() { _computers.Sort(); }
    }

    public class OU : IEnumerable, IEnumerable<Computer>, IComparable //, IEnumerable<OU> 
    {
        private readonly string _Name;
        private readonly string _ADSearchBase;
        private readonly string _DistinguishedName;
        private readonly string _FriendlyString;

        private Computers _ChildComputers;
        //private List<OU> _ChildOUs;
        

        public OU(Principal p) 
        {
            _Name = p.Name;
            _ADSearchBase = p.DistinguishedName;
            _FriendlyString = ADHelper.GetFriendlyName(p.DistinguishedName);
            //_ChildComputers = ADHelper.GetComputersInOU(_ADSearchBase);
        }

        public OU(string name, string adSearchBase, string distinguishedName)
        {
            _Name = name;
            _ADSearchBase = adSearchBase;
            _FriendlyString = ADHelper.GetFriendlyName(distinguishedName);
            //_ChildComputers = ADHelper.GetComputersInOU(distinguishedName);
            _DistinguishedName = distinguishedName;
        }

        public IEnumerator<Computer> GetEnumerator() { 
            if (_ChildComputers == null) { _ChildComputers = ADHelper.GetComputersInOU(_DistinguishedName);  }
            return ((IEnumerable<Computer>)_ChildComputers).GetEnumerator(); 
        }
        IEnumerator IEnumerable.GetEnumerator() { return ((IEnumerable)_ChildComputers).GetEnumerator(); }

        //IEnumerator<OU> IEnumerable<OU>.GetEnumerator() { return ((IEnumerable<OU>)_ChildOUs).GetEnumerator(); }

        public string Name { get => _Name; }
        public string FriendlyString { get => _FriendlyString; }
        public string SearchBase { get => _ADSearchBase; }
        public string DistinguishedName { get => _DistinguishedName; }

        public override string ToString()
        { return _FriendlyString; }

        public override bool Equals(object? obj) { return _DistinguishedName.Equals(((OU)obj).DistinguishedName); }
        public int CompareTo(object? obj)
        {
            if (obj.GetType() != typeof(OU)) return -1;
            return _DistinguishedName.CompareTo(((OU)obj).DistinguishedName);
        }
    }

    public class OUs : IEnumerable<OU>
    {
        List<OU> _ous;

        public IEnumerator<OU> GetEnumerator() { return _ous.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public OUs() { _ous = new List<OU>(); }
        public void Add(OU OU) { _ous.Add(OU); }
        public void Remove(OU OU) { _ous.Remove(OU); }
        public bool Contains(OU OU) { return _ous.Contains(OU); }
        public int Count { get => _ous.Count; }

        public void Sort() { _ous.Sort(); }
    }

    public class DC
    {
        public DC() { }
    }

    public static class ADHelper
    {
        internal static string GetFriendlyName(string DN)
        {
            DN = DN.Substring(0, DN.IndexOf("DC=")-1);
            string[] names = DN.Split(',');

            string FriendlyName = "";
            foreach (string s in names.Reverse())
            {
                FriendlyName += "\\" + s.Substring(3);
            }

            return FriendlyName.Substring(1);
        }

        internal static Computers GetComputersInOU(string SearchBase)
        {
            //List<Computer> computers = new();
            Computers computers = new(); 
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain, GetDomainString(), SearchBase);
                ComputerPrincipal queryComputer = new ComputerPrincipal(context);
                queryComputer.Enabled = true;

                PrincipalSearcher search = new PrincipalSearcher(queryComputer);
                PrincipalSearchResult<Principal> result = search.FindAll();
                foreach (ComputerPrincipal found in result)
                {
                    //Debug.WriteLine("Found an item: " + found.DistinguishedName);
                    computers.Add(new Computer(GetComputerPrincipal(found.DistinguishedName)));
                }
            }
            catch (DirectoryServicesCOMException e)
            {
                Debug.WriteLine("An error occured in enumerating OU: " + e.Message);
            }

            computers.Sort();

            foreach (Computer computer in computers)
            {
                Debug.WriteLine("Computers contains: " + computer.DistinguishedName);
            }

            return computers;
        }

        internal static List<OU> GetADOUs(string SearchBase)
        {
            DirectoryEntry directory = new DirectoryEntry("LDAP://" + SearchBase);//GetLDAPDomainPath());
            DirectorySearcher search = new DirectorySearcher(directory);
            search.Filter = "(objectClass=organizationalUnit)";
            search.SearchScope = SearchScope.Subtree;
            search.PropertiesToLoad.Add("distinguishedName");
            search.PropertiesToLoad.Add("name");

            List<OU> result = new List<OU>();

            foreach(SearchResult entry in search.FindAll())
            {
                /*Debug.WriteLine("found an entry: " + entry.Properties["displayName"].ToString());

                foreach (DictionaryEntry item in entry.Properties)
                {
                    Debug.WriteLine("found a thing: " + item.Key + " : " + item.Value);
                    Debug.WriteLine(":: " + entry.Properties[item.Key.ToString()][0].ToString());
                }*/
                result.Add(new OU((entry.Properties["name"][0].ToString()), (entry.Properties["adspath"][0].ToString()), (entry.Properties["distinguishedName"][0].ToString())));
            }

            return result;
        }

        public static OUs GetOUs(string SearchBase)
        {
            DirectoryEntry directory = new DirectoryEntry("LDAP://" + SearchBase);//GetLDAPDomainPath());
            DirectorySearcher search = new DirectorySearcher(directory);
            search.Filter = "(objectClass=organizationalUnit)";
            search.SearchScope = SearchScope.Subtree;
            search.PropertiesToLoad.Add("distinguishedName");
            search.PropertiesToLoad.Add("name");

            OUs ous = new();

            foreach (SearchResult entry in search.FindAll())
            { ous.Add(new OU((entry.Properties["name"][0].ToString()), (entry.Properties["adspath"][0].ToString()), (entry.Properties["distinguishedName"][0].ToString()))); }

            return ous;
        }

        internal static ComputerPrincipal GetComputerPrincipal(string SearchBase)
        {
            PrincipalContext domain = new PrincipalContext(ContextType.Domain, GetDomainString(), SearchBase);
            ComputerPrincipal cp = new ComputerPrincipal(domain);
            PrincipalSearcher searcher = new PrincipalSearcher(cp);
            PrincipalSearchResult<Principal> result = searcher.FindAll();

            ComputerPrincipal FoundComputer = (ComputerPrincipal)result.FirstOrDefault();

            return FoundComputer;
        }

        public static string GetCurrentDomain()
        {
            DirectoryEntry de = new DirectoryEntry("LDAP://RootDSE");
            return de.Properties["defaultNamingContext"][0].ToString();
        }

        public static string GetLDAPDomainPath()
        { return "LDAP://" + GetCurrentDomain(); }

        public static string GetDomainString() 
        {
            //DirectoryEntry de = new DirectoryEntry("LDAP://ROOTDSE");
            string DomainString = "";
            string[] host = GetCurrentDomain().Split(',');
            host.Reverse();
            foreach(string s in host)
            { DomainString += "." + s.Substring(3); }

            return DomainString.Substring(1);
        }

        internal static string GetDomainName()
        {
            DirectoryEntry de = new DirectoryEntry("LDAP://RootDSE");

            foreach(PropertyValueCollection item in de.Properties)
            {
//                Debug.WriteLine(item.ToString());
                Debug.WriteLine(item.PropertyName + "=" + item.Value);
            }

            foreach(var item in de.Properties["namingContexts"])
            {
                Debug.WriteLine("NC: " + item);
            }

            return de.Properties["dnsHostName"][0].ToString();
        }

        internal static string GetLDAPServiceName()
        {
            DirectoryEntry de = new DirectoryEntry("LDAP://RootDSE");
            return de.Properties["ldapServiceName"][0].ToString();
        }
    }
}