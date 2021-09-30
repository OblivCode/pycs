<<<<<<< HEAD
﻿
using System;
using static pycs.pycs;
using pycs.modules.datetime;
using System.IO;

namespace pycs.modules
{
    public class logging
    {
        //variables
        private Stream stream = null;
        private string format = "%(name)s:%(levelname):%(message)";
        private string date_format = "%d-%b-%y %H:%M:%S";
        private string name = "root";
        private string levelname;
        //static functions
        public static logging basicConfig(string filename = null, string format = null, severity level = severity.none) => new logging(stream: open(filename)._stream, format: format, level: level);
        public static logging getLogger(string name, severity level = severity.none) => new logging(name: name, format: "%(message)");
        public static logging StreamHandler(Stream stream = null) => new logging(stream: stream);
        public static logging FileHandler(string filename) => new logging(stream: open(filename)._stream);
        private string format_string(string msg)
        {
            string value(string key)
            {
                switch (key)
                {
                    case "message": return msg;
                    case "name": return name;
                    case "levelname": return levelname;
                    case "asctime": return new datetime.datetime(DateTime.Now).strftime(date_format);
                    default: return key;
                }
        }
            int i;
            string new_str = "";
            for (i = 0; i < format.Length; i++)
            {
                int c_idx = i;
                char ch = format[i];
                    if (ch == '%')
                    {

                        if (format[i + 1] == '(' && format.Substring(i).Contains(')')) //format 
                        {
                            string key = "";
                        int c_last = i;
                            i += 2;
                            for (i = i; format[i] != ')'; i++)
                            {
                                key += format[i];
                            }
                            string val = value(key);
                            new_str += val;
                        try {
                            if (format[i + 1] == 's')
                                i++;
                        }catch(Exception) { }
                    }
                    }
                    else
                        new_str += ch;

            }
            return new_str;
        }
        //functions
        public logging(Stream stream = null, string name = null, string format = null, severity level = severity.none)
        {
            
            if (stream != null)
                this.stream = stream;
            if (format != null)
                this.format = format;
            if (name != null)
                this.name = name;
            if (level != severity.none)
                levelname = level.ToString().ToUpper();
            else
                levelname = null;
        }

        public void setFormatter(string format) => this.format = format;
        public void setLevel(severity level) => this.levelname = level.ToString().ToUpper();
        public void setStream(Stream stream) => this.stream = stream;
        public void flush()
        {
            if (stream == null)
                throw new OSError("Stream not assigned");
            stream.Flush();
        }
        public void close()
        {
            if (stream == null)
                throw new OSError("Stream not assigned.");
            stream.Close();
        }
        //out
        public void debug(string msg) => log_print(msg, "DEBUG");
        public void info(string msg) => log_print(msg, "INFO");
        public void warning(string msg) => log_print(msg, "WARNING");
        public void error(string msg, Exception e = null)
        {
            if(e != null)
            {
                msg += "\n" + e.StackTrace;
            }
            log_print(msg, "ERROR");
        }
        public void critical(string msg) => log_print(msg, "CRITICAL");
        public void print(string msg) => log_print(msg);
        /// <exception cref="pycs.OSError"></exception>
        private void log_print(string msg, string override_level = null)
        {
            
            string def = levelname;
            if (override_level != null)
                levelname = override_level;
            if (stream != null)
                try { stream.Write(bytearray(msg), 0, msg.Length); } 
                catch(Exception e) { throw new OSError("Failed to write to stream", e); }
            else
                pycs.print(format_string(msg));
            levelname = def;
        }
        public enum severity
        {
            debug, info, warning, error, critical, none
        }
    }
}
=======
﻿
using System;
using static pycs.pycs;
using pycs.modules.datetime;
using System.IO;

namespace pycs.modules
{
    public class logging
    {
        //variables
        private Stream stream = null;
        private string format = "%(name)s:%(levelname):%(message)";
        private string date_format = "%d-%b-%y %H:%M:%S";
        private string name = "root";
        private string levelname;
        //static functions
        public static logging basicConfig(string filename = null, string format = null, severity level = severity.none) => new logging(stream: open(filename), format: format, level: level);
        public static logging getLogger(string name, severity level = severity.none) => new logging(name: name, format: "%(message)");
        public static logging StreamHandler(Stream stream = null) => new logging(stream: stream);
        public static logging FileHandler(string filename) => new logging(stream: open(filename));
        private string format_string(string msg)
        {
            string value(string key)
            {
                switch (key)
                {
                    case "message": return msg;
                    case "name": return name;
                    case "levelname": return levelname;
                    case "asctime": return new datetime.datetime(DateTime.Now).strftime(date_format);
                    default: return key;
                }
        }
            int i;
            string new_str = "";
            for (i = 0; i < format.Length; i++)
            {
                int c_idx = i;
                char ch = format[i];
                    if (ch == '%')
                    {

                        if (format[i + 1] == '(' && format.Substring(i).Contains(')')) //format 
                        {
                            string key = "";
                        int c_last = i;
                            i += 2;
                            for (i = i; format[i] != ')'; i++)
                            {
                                key += format[i];
                            }
                            string val = value(key);
                            new_str += val;
                        try {
                            if (format[i + 1] == 's')
                                i++;
                        }catch(Exception) { }
                    }
                    }
                    else
                        new_str += ch;

            }
            return new_str;
        }
        //functions
        public logging(Stream stream = null, string name = null, string format = null, severity level = severity.none)
        {
            if (stream != null)
                this.stream = stream;
            if (format != null)
                this.format = format;
            if (name != null)
                this.name = name;
            if (level != severity.none)
                levelname = level.ToString().ToUpper();
            else
                levelname = null;
        }

        public void setFormatter(string format) => this.format = format;
        public void setLevel(severity level) => this.levelname = level.ToString().ToUpper();
        public void setStream(Stream stream) => this.stream = stream;
        public void flush()
        {
            if (stream == null)
                throw new OSError("Stream not assigned");
            stream.Flush();
        }
        public void close()
        {
            if (stream == null)
                throw new OSError("Stream not assigned.");
            stream.Close();
        }
        //out
        public void debug(string msg) => log_print(msg, "DEBUG");
        public void info(string msg) => log_print(msg, "INFO");
        public void warning(string msg) => log_print(msg, "WARNING");
        public void error(string msg, Exception e = null)
        {
            if(e != null)
            {
                msg += "\n" + e.StackTrace;
            }
            log_print(msg, "ERROR");
        }
        public void critical(string msg) => log_print(msg, "CRITICAL");
        public void print(string msg) => log_print(msg);
        /// <exception cref="pycs.OSError"></exception>
        private void log_print(string msg, string override_level = null)
        {
            
            string def = levelname;
            if (override_level != null)
                levelname = override_level;
            if (stream != null)
                try { stream.Write(bytearray(msg), 0, msg.Length); } 
                catch(Exception e) { throw new OSError("Failed to write to stream", e); }
            else
                pycs.print(format_string(msg));
            levelname = def;
        }
        public enum severity
        {
            debug, info, warning, error, critical, none
        }
    }
}
>>>>>>> origin/master
