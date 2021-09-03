using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pycs.modules
{
    public static class os
    {
        
        //enviroment variables
        public static void setenv(string key, string? value) => Environment.SetEnvironmentVariable(key, value);
        public static string? getenv(string key) => Environment.GetEnvironmentVariable(key);

        public static string name()
        {
            return System.Runtime.InteropServices.RuntimeInformation.OSDescription;

        }
        public static void close(FileStream file) => file.Close();
        //dir
        public static string getcwd() => Directory.GetCurrentDirectory();
        public static void chdir(string path) => Directory.SetCurrentDirectory(path);
        public static void mkdir(string path)
        {
            if (Directory.Exists(path))
                throw new FileExistsError("Directory already exists.");
            Directory.CreateDirectory(path);
        }
        public static void remove(string filename)
        {
            if (!File.Exists(filename))
                throw new OSError("File does not exist.");
            File.Delete(filename);
        }
        public static void rmdir(string path)
        {
            if (!Directory.Exists(path))
                throw new OSError("Directory does not exist.");
            Directory.Delete(path);
        }
        public static string[] listdir(string path)
        {
            var dirs = Directory.GetDirectories(path);
            var files = Directory.GetFiles(path).ToList();
            for (int i = 0; i < files.Count; i++)
                files[i] = Path.GetFileName(files[i]);
            for(int i =0; i<dirs.Length; i++)
            {
                string dir = Path.GetDirectoryName(dirs[i]);
                int idx = dir.LastIndexOf('\\');
                dirs[i] = dir.Substring(idx + 1);
            }
            files.AddRange(dirs);
            return files.ToArray();
        }

        public static class path
        { 
            public static string join(string[] paths)
            { 
                string path = "/";
                foreach (var p in paths)
                {
                    if (p[0] != '/' && path.Last() != '/')
                        path += p;
                    
                }

                return path;
             
            }

            public static bool exists(string filename) => File.Exists(filename);
            public static long getsize(string filename)
            {
                if (!File.Exists(filename))
                    throw new OSError("File does not exist.");
                return new FileInfo(filename).Length;
            }
        }

        //exceptions
        public class OSError : Exception
        {
            public OSError()
            {
            }

            public OSError(string message)
                : base(message)
            {
            }

            public OSError(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        public class FileExistsError : Exception
        {
            public FileExistsError()
            {
            }

            public FileExistsError(string message)
                : base(message)
            {
            }

            public FileExistsError(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
    }
}
