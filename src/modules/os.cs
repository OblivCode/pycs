using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static pycs.pycs;

#nullable disable
namespace pycs.modules
{
    public static class os
    {
        
        //enviroment variables
        public static string setenv(string key, string value)
        {
            Environment.SetEnvironmentVariable(key, value);
            return key;
        }
        /// <exception cref="pycs.KeyError"></exception>
        public static string getenv(string key)
        {
            string val = Environment.GetEnvironmentVariable(key);
            if (val == null)
                throw new KeyError("Variable not found");
            return val;
        }
        public static Dictionary<string,string> environ
        {
            get {
                var envs = Environment.GetEnvironmentVariables();
                return (Dictionary<string, string>)envs;
            }
        }

        public static string name
        {
            get { return System.Runtime.InteropServices.RuntimeInformation.OSDescription; }

        }
        //dir
        public static string getcwd() => Directory.GetCurrentDirectory();
        public static string chdir(string path)
        {
            if (!Directory.Exists(path))
                throw new FileExistsError("Directory does not exist.");

            if (path == "..")
                path = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            Directory.SetCurrentDirectory(path);
            return path;
        }
        /// <exception cref="pycs.FileExistsError"></exception>
        public static string mkdir(string path)
        {
            if (Directory.Exists(path))
                throw new FileExistsError("Directory already exists.");
            return Directory.CreateDirectory(path).FullName;
        }
        /// <exception cref="FileExistsError"></exception>
        public static string remove(string filename)
        {
            if (!File.Exists(filename))
                throw new FileExistsError("File does not exist.");
            File.Delete(filename);
            return filename;
        }
        /// <exception cref="pycs.FileExistsError"></exception>
        public static string rmdir(string path)
        {
            if (!Directory.Exists(path))
                throw new FileExistsError("Directory does not exist.");
            Directory.Delete(path);
            return path;
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

       
        public static string dirname(string path) => Path.GetDirectoryName(path);

        //proc
        public static int getpid() => Process.GetCurrentProcess().Id;
        public static string system(string p, bool wait = false)
        {
            string args = "";
            string[] split = p.Split(' ');
            p = split[0];
            if (split.Length > 1)
                args = split.Where((s, i) => i != 0).ToString();
            Process proc = Process.Start(p, args);

            if (wait)
            {
                proc.WaitForExit();
                return proc.StandardOutput.ReadToEnd();
            }
            else
                return "";
            
        }

        public static (string, string[], string[]) walk(string dir)
        {
            var root = dir;
            var dirs = Directory.GetDirectories(dir);
            var files = Directory.GetFiles(dir);
            return (root, dirs, files);
        }
        public static class path
        { 
            public static string join(string a, string b)
            {
                if (a.LastOrDefault() != '/' && a.LastOrDefault() != '\\')
                    a += '\\';
                return Path.Combine(
                    Path.GetDirectoryName(Path.GetFullPath(a)), b);
             
            }

            public static bool isdir(string path) => Directory.Exists(path);
            public static bool exists(string filename) => File.Exists(filename);

            public static string basename(string path) => Path.GetFileName(path);
            public static string dirname(string path) => Path.GetDirectoryName(path);
                
            /// <exception cref="pycs.OSError"></exception>
            public static long getsize(string filename)
            {
                if (!File.Exists(filename))
                    throw new OSError("File does not exist.");
                return new FileInfo(filename).Length;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="path"></param>
            /// <returns>(root, tail)</returns>
            public static (string,string) split(string path)
            {
                int idx0 = path.LastIndexOf('/');
                int idx1 = path.LastIndexOf('\\');
                int idx = Math.Max(idx0, idx1);
                string tail = path.Substring(idx);
                string head = path.Substring(0, idx);
                return (head, tail);
            }
        }

       
    }
}
