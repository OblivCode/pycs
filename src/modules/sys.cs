<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using static pycs.pycs;

namespace pycs.modules
{
    public static class sys
    {
        public static string[] argv
        {
            get{ return Environment.GetCommandLineArgs(); 
           }
        }

        public static void exit(int code = 0) => Environment.Exit(code);

        public static int getsizeof_unmanaged(object obj) => Marshal.SizeOf(obj);

        public static string version
        {
            get { return Environment.OSVersion.VersionString; }
        }
        public static string platform
        {
            get { return Environment.OSVersion.Platform.ToString(); }
        }
        //std
        public static TextIO stdin
        {
            get { return new(Console.OpenStandardInput()); }
        }
        public static TextIO stdout
        {
            get { return new(Console.OpenStandardOutput()); }
        }
        public static TextIO stderr
        {
            get { return new(Console.OpenStandardError()); }

        }
        public static string target { get { return AppContext.TargetFrameworkName; } }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using static pycs.pycs;

namespace pycs.modules
{
    public static class sys
    {
        public static string[] argv
        {
            get{ return Environment.GetCommandLineArgs(); 
           }
        }

        public static void exit(int code = 0) => Environment.Exit(code);

        public static int getsizeof_unmanaged(object obj) => Marshal.SizeOf(obj);

        public static string version
        {
            get { return Environment.OSVersion.VersionString; }
        }
        public static string platform
        {
            get { return Environment.OSVersion.Platform.ToString(); }
        }
        //std
        public static TextIO stdin
        {
            get { return (TextIO)Console.OpenStandardInput(); }
        }
        public static TextIO stdout
        {
            get { return (TextIO)Console.OpenStandardOutput(); }
        }
        public static TextIO stderr
        {
            get { return (TextIO)Console.OpenStandardError(); }

        }
        public static string target { get { return AppContext.TargetFrameworkName; } }
    }
}
>>>>>>> origin/master
