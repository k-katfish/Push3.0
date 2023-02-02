using System.Collections;

namespace FixesAndScriptsLib
{
    public class Fix
    {
        private string _Name;
        private string _WorkingDirectory;
        private FileInfo[] _batchFiles;
        private FileInfo[] _executables;
        private FileInfo[] _powershellScripts;
        private FileInfo[] _allFiles;

        public Fix() { }
        public Fix(DirectoryInfo directoryInfo) 
        {
            _WorkingDirectory = directoryInfo.FullName;

            _batchFiles = directoryInfo.GetFiles("*.bat");
            _executables = directoryInfo.GetFiles("*.exe");
            _powershellScripts = directoryInfo.GetFiles("*.ps1");
            _allFiles = directoryInfo.GetFiles();
        }

        public string Name { get => _Name; }
        public string WorkingDirectory { get => _WorkingDirectory; }
        public FileInfo[] BatchFiles { get => _batchFiles; }
        public FileInfo[] Executables { get => _executables; }
        public FileInfo[] PowershellScripts { get => _powershellScripts; }
        public FileInfo[] AllFiles { get => _allFiles; }

        public override string ToString() { return _Name; }
    }

    public class Fixes : IEnumerable<Fix>
    {
        List <Fix> fixeslist = new List <Fix> ();

        public IEnumerator<Fix> GetEnumerator()
        { return ((IEnumerable<Fix>)fixeslist).GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator()
        { return ((IEnumerable)fixeslist).GetEnumerator(); }

        public void Add(Fix fix) { this.fixeslist.Add(fix); }
        public void Remove(Fix fix) { this.fixeslist.Remove(fix); }
        public void Clear() { this.fixeslist.Clear(); }
        public bool Contains(Fix fix) { return this.fixeslist.Contains(fix); }
        public int Count() { return this.fixeslist.Count; }

        public Fix GetAt(int index) { return this.fixeslist[index]; }
        public Fix this[int index] { get => this.fixeslist[index]; set => this.fixeslist[index] = value; }
    }

    public static class FixesHelper
    {
        public static Fixes GetFixes(string ShareLocation)
        {
            DirectoryInfo fixesdir = new DirectoryInfo($"{ShareLocation}\\Fixes");

            DirectoryInfo[] Folders = fixesdir.GetDirectories();
            Fixes fixes = new();

            foreach (DirectoryInfo folder in Folders )
            { fixes.Add(new Fix(folder)); }

            return fixes;
        }
    }
}