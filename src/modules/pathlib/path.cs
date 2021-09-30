using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static pycs.pycs;

namespace pycs.modules.pathlib
{
    public class Path
    {
        string path;
        public Path(string path)
        {
            if (path[0] == '/' || path[0] == '\\')
                path = os.getcwd() + path;
            else if (path[0] == '.')
            {


                int dot_count = 0;
                for (int i = 0; i < path.Length; i++)
                {
                    char ch = path[i];
                    if (ch == '.')
                        dot_count++;
                    else
                        break;
                }
                if (dot_count == 1)
                    path = os.getcwd();
                else
                {
                    for (int i = 1; i < dot_count; i++)
                        path = Directory.GetParent(path).FullName;
                }
            }
            else if (path[1] == ':')
            {
                string drive = path.Substring(0, 2).ToLower();
                string[] drives = Directory.GetLogicalDrives().Where(x => x == drive.ToLower()).ToArray();
                if (!drives.Contains(drive))
                    throw new FileNotFoundError("Path does not exist");
            }
            this.path = path;
        }

        public string get() => path;
        public string parent { get { return Directory.GetParent(this.path).FullName; } }

        public bool is_dir {  get {  return os.path.isdir(path); } }
        public bool is_file { get { if (!exists) return false;
                return !os.path.isdir(path);
            } }
        public bool exists { get { return os.path.exists(path);  } }

        public TextIO open()
        {
            return pycs.open(this.path);
        }

        public Path[] iterdir()
        {
            List<Path> paths= new List<Path>();
            var subs = os.listdir(path);
            for(int i =0; i < subs.Length; i++)
            {
                string sub = subs[i];
                if (os.path.isdir(sub))
                    paths.Add(new(sub));
            }

            return paths.ToArray();
        }
    }
}
