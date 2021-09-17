using pycs.modules;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pycs
{
    public static class pycs
    {

        //pub
        public static string input(string prompt = "")
        {
            if (prompt.Length > 0)
                Console.Write(prompt);
            return Console.ReadLine();
        }
        //print
        public static void print(object value) => Console.WriteLine(value);
        public static string repr(byte[] obj) => Encoding.UTF8.GetString(obj);
        public static string repr<T>(T[] obj)
        {
            string str = "[";
            for (int i = 0; i < obj.Length; i++)
            {
                str += obj[i] + ", ";
            }
            str = str.Trim().Trim(',') + "]";

            return str;
        }
        public static string repr<T>(List<T> obj)
        {
            string str = "[";
            for (int i = 0; i < obj.Count; i++)
            {
                str += obj[i] + ", ";
            }
            str = str.Trim().Trim(',') + "]";

            return str;
        }
        public static string repr<TKey, TValue>(Dictionary<TKey, TValue> obj)
        {
            string str = "{";
            foreach (var val in obj)
            {
                var key = val.Key;
                var value = val.Value;
                str += $"{val.Key}: {val.Value}, ";
            }
            str = str.Trim().Trim(',') + "}";

            return str;
        }
        //format
        public static string format(string value, string[] spec)
        {
            int matches = Regex.Matches(value, "{}").Count;
            for (int i = 0; i < matches; i++)
            {
                string word = spec[i];
                var regex = new Regex(Regex.Escape("{}"));
                value = regex.Replace(value, word, 1); }
            return value;
        }

        //types
        public const bool False = false;
        public const bool True = true;


        public static object Object() => new object();
        public static List<T> list<T>() => new List<T>();

        public static string type<T>(T obj)
        {
            string type = obj.GetType().Name.ToLower();
            if (type.StartsWith("int"))
                return "int";
            else if (type.Contains('`'))
                return type.Substring(0, type.IndexOf('`'));
            else if (type == "single")
                return "float";


            return type;
        }
        //type conversions
        //public static string str(object obj) => obj.ToString();
        public static int Int<T>(T value) => int.Parse(value.ToString());
        public static float Float<T>(T value) => float.Parse(value.ToString());
        public static bool Bool<T>(T value)
        {
            if (!math.isnan(value))
                return (double.Parse(value.ToString()) == 0 ? false : true);
            else if (value is string)
                return value.ToString().Length == 0 ? false : true;
            else if (value is bool)
                return bool.Parse(value.ToString());
            return false;

        }
        public static byte[] bytearray(string value) => Encoding.UTF8.GetBytes(value);
        public static char chr(int unicode) => char.Parse(char.ConvertFromUtf32(unicode));
        public static string hex(int number) => number.ToString("X");
        public static int hash(object obj) => obj.GetHashCode();
        //eval
        public static decimal eval(string expression) => (decimal)new DataTable().Compute(expression, "");
        //not
        public static bool not(bool value) => !value;
        public static bool not(int value) => value == 0 ? false : true;
        public static bool not(string value) => string.IsNullOrEmpty(value) ? false : true;

        //len
        public static int len<T>(T[] obj) => obj.Length;
        public static int len<T>(List<T> obj) => obj.Count;
        public static int len(ArrayList obj) => obj.Count;
        public static int len<T>(Queue<T> obj) => obj.Count;
        public static int len<T>(ConcurrentQueue<T> obj) => obj.Count;
        public static int len<T>(Stack<T> obj) => obj.Count;
        public static int len<T>(ConcurrentStack<T> obj) => obj.Count;
        public static int len<T>(LinkedList<T> obj) => obj.Count;
        public static int len(SortedList obj) => obj.Count;
        public static int len<TKey, TValue>(SortedList<TKey, TValue> obj) => obj.Count;
        public static int len<TKey, TValue>(Dictionary<TKey, TValue> obj) => obj.Count;
        public static int len<TKey, TValue>(ConcurrentDictionary<TKey, TValue> obj) => obj.Count;
        public static int len(CollectionBase obj) => obj.Count;
        //bin
        public static string bin(int number) => Convert.ToString(number, 2);
        //ascii
        public static string ascii(object value)
        {
            string text = value.ToString();
            string ascii_text = "";
            foreach (char c in text)
            {
                int unicode = c;
                ascii_text += unicode < 128 ? Char.ConvertFromUtf32(unicode) : "\\";
            }
            return ascii_text;
        }
        //ord
        public static int ord(char c) => c;
        //any
        public static bool any<T>(T[] value)
        {
            if (value.Length == 0) return false;
            bool boolean = false;
            foreach (T val in value)
            {
                if (val is bool)
                    boolean = val.Equals(true);
                else if (val is string || val is char)
                    boolean = val.ToString().Length == 0 ? false : true;
                else if (val is int || val is double || val is float || val is decimal)
                    boolean = decimal.Parse(val.ToString()) == 0 ? false : true;
                if (boolean)
                    break;
            }
            return boolean;
        }
        public static bool any<T>(List<T> value)
        {
            if (value.Count == 0) return false;
            bool boolean = false;
            foreach (T val in value)
            {
                if (val is bool)
                    boolean = val.Equals(true);
                else if (val is string || val is char)
                    boolean = val.ToString().Length == 0 ? false : true;
                else if (val is int || val is double || val is float || val is decimal)
                    boolean = decimal.Parse(val.ToString()) == 0 ? false : true;
                if (boolean)
                    break;
            }
            return boolean;
        }
        public static bool any<TKey, TValue>(Dictionary<TKey, TValue> value)
        {
            if (value.Count == 0) return false;
            bool boolean = false;
            foreach (var pair in value)
            {
                TValue val = pair.Value;
                if (val is bool)
                    boolean = val.Equals(true);
                else if (val is string || val is char)
                    boolean = val.ToString().Length == 0 ? false : true;
                else if (val is int || val is double || val is float || val is decimal)
                    boolean = decimal.Parse(val.ToString()) == 0 ? false : true;
                if (boolean)
                    break;
            }
            return boolean;
        }
        //all
        public static bool all<T>(T[] value)
        {
            if (value.Length == 0) return true;
            bool boolean = true;
            foreach (T val in value)
            {
                if (val is bool)
                    boolean = val.Equals(true);
                else if (val is string || val is char)
                    boolean = val.ToString().Length == 0 ? false : true;
                else if (val is int || val is double || val is float || val is decimal)
                    boolean = decimal.Parse(val.ToString()) == 0 ? false : true;
                if (!boolean)
                    break;
            }
            return boolean;
        }
        public static bool all<T>(List<T> value)
        {
            if (value.Count == 0) return true;
            bool boolean = true;
            foreach (T val in value)
            {
                if (val is bool)
                    boolean = val.Equals(true);
                else if (val is string || val is char)
                    boolean = val.ToString().Length == 0 ? false : true;
                else if (val is int || val is double || val is float || val is decimal)
                    boolean = decimal.Parse(val.ToString()) == 0 ? false : true;
                if (!boolean)
                    break;
            }
            return boolean;
        }
        public static bool all<TKey, TValue>(Dictionary<TKey, TValue> value)
        {
            if (value.Count == 0) return true;
            bool boolean = true;
            foreach (var pair in value)
            {
                TValue val = pair.Value;
                if (val is bool)
                    boolean = val.Equals(true);
                else if (val is string || val is char)
                    boolean = val.ToString().Length == 0 ? false : true;
                else if (val is int || val is double || val is float || val is decimal)
                    boolean = decimal.Parse(val.ToString()) == 0 ? false : true;
                if (!boolean)
                    break;
            }
            return boolean;
        }
        //maths--------------
        //round
        public static double round(double number, int decimals = -1)
        {
            if (decimals < 0)
                return Math.Round(number);
            else
                return Math.Round(number, decimals);
        }


        //abs
        public static double abs(double value)
        {
            if (value >= 0)
                return value;
            else
                return value * -1;
        }
        //pow
        public static double pow(double b, double exp)
        {
            exp--;
            double init = b;
            for (int i = 0; i < exp; i++)
            {
                b *= init;
            }
            return b;
        }

        //sum
        public static decimal sum<T>(T[] obj)
        {
            decimal value = 0;
            if (!math.isnan(obj[0]))
            {
                for (int i = 0; i < obj.Length; i++)
                    value += decimal.Parse(obj[i].ToString());
            }
            return value;
        }
        //range
        public static int[] range(int until)
        {
            var array = new int[until];
            for (int i = 0; i < until; i++)
            {
                array[i] = i;
            }

            return array;

        }

        //file
        /// <summary>
        /// FileNotFoundError only possible with 'r' mode
        /// returns null (if mode is invalid)
        /// </summary>
        /// <exception cref="FileNotFoundError"
        public static TextIOWrapper? open(string filename, string mode = "w+")
        {
            if (mode.StartsWith('r'))
                if (!os.path.exists(filename))
                    throw new FileNotFoundError("File does not exist.");

            switch (mode)
            {
                case "r": try { return new TextIOWrapper(path: filename, read: true); } catch (Exception e) { throw new OSError(e.Message, e); }
                case "r+": try { return new TextIOWrapper(path: filename, read: true, write: true); } catch (Exception e) { throw new OSError(e.Message, e); }
                case "w": try { return new TextIOWrapper(path: filename, write: true); } catch (Exception e) { throw new OSError(e.Message, e); }
                case "w+": try { return new TextIOWrapper(path: filename, write: true, read: true); } catch (Exception e) { throw new OSError(e.Message, e); }
                case "a": try { return new TextIOWrapper(path: filename, append: true); } catch (Exception e) { throw new OSError(e.Message, e); }
                case "a+": try { return new TextIOWrapper(path: filename, append: true, read: true); } catch (Exception e) { throw new OSError(e.Message, e); }
                default: return null;
            }
        }


        public class TextIOWrapper : TextIO
        {
            private string filepath = null;
            private string invalid_perm = "Invalid permissions";
            private bool read_ = true, write_ = true, append_ = false;
            public TextIOWrapper(string path, bool read = false, bool write = false, bool append = false)
            {
                if (!os.path.exists(path)) File.Create(path);
                filepath = path;
                read_ = read;
                write_ = write;
                append_ = append;
            }

            public void append(string content)
            {
                if (!append_ && !write_) throw new PermissionError(invalid_perm);
                File.AppendAllText(filepath, content);
            }

            public async Task appendasync(string content)
            {
                if (!append_ && !write_) throw new PermissionError(invalid_perm);
                await File.AppendAllTextAsync(filepath, content);
            }
            public void write(string content)
            {
                if (!write_) throw new PermissionError(invalid_perm);
                File.WriteAllText(filepath, content);
            }

            public async Task writeasync(string content)
            {
                if (!write_) throw new PermissionError(invalid_perm);
                await File.WriteAllTextAsync(filepath, content);
            }
            public string read()
            {
                if (!read_) throw new PermissionError(invalid_perm);
                return File.ReadAllText(filepath);
            }
            public async Task<string> readasync()
            {
                if (!read_) throw new PermissionError(invalid_perm);
                return await File.ReadAllTextAsync(filepath);
            }
        }
        public class TextIO : MemoryStream
        {

            public async void write(string content)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(content);
                await this.WriteAsync(buffer, 0, buffer.Length);
            }
            public string read()
            {
                string output = "";
                using (TextReader TR = new StreamReader(this))
                {
                    string line;
                    while ((line = TR.ReadLine()) != null)
                        output += line;
                }
                return output;
            }
            public async Task<string> readasync()
            {
                string output = "";
                using (TextReader TR = new StreamReader(this))
                {
                    string line;
                    while ((line = await TR.ReadLineAsync()) != null)
                        output += line;
                }
                return output;
            }

            public void close() => this.Close();
        }
        //type
        public class str {
            private string value;
            public str(string value) => this.value = value;
            public static implicit operator str(string value) => new str(value);
            public static implicit operator str(char value) => new str(value.ToString());
            public static implicit operator str(byte value) => new str(value.ToString());
            public static implicit operator str(byte[] value) => new str(Encoding.UTF8.GetString(value));
            public override string ToString() => value;
            public override int GetHashCode() => value.GetHashCode();
            public override bool Equals(object obj) => value.Equals(obj);
        }
        //delegates
        public delegate EventHandler EmptyHandler();
        //exceptions
        public class Error : Exception {
            public Error(string message) : base(message) {  }
            public Error(string message, Exception inner) : base(message,inner) { }
        }
        public class OSError : Error
        {
            public string Error() => this.Message;
            public OSError(string message)
                : base(message)
            {
            }

            public OSError(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        public class RuntimeError : Exception
        {
            public string Error() => this.Message;

            public RuntimeError(string message)
                : base(message)
            {
            }

            public RuntimeError(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

        public class FileNotFoundError : Exception
        {
            public string Error() => this.Message;

            public FileNotFoundError(string message)
                : base(message)
            {
            }
            public FileNotFoundError(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        public class FileExistsError : Exception
        {
            public string Error() => this.Message;

            public FileExistsError(string message)
                : base(message)
            {
            }
            public FileExistsError(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        public class BufferError : Exception
        {
            public string Error() => this.Message;

            public BufferError(string message)
                : base(message)
            {
            }
            public BufferError(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

        public class IndexError : Exception
        {
            public string Error() => this.Message;

            public IndexError(string message)
                : base(message)
            {
            }
            public IndexError(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

        public class KeyError : Exception
        {
            public string Error() => this.Message;

            public KeyError(string message)
                : base(message)
            {
            }
            public KeyError(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        public class NotADirectoryError : Exception
        {
            public string Error() => this.Message;
            public NotADirectoryError(string message)
                : base(message)
            {
            }

            public NotADirectoryError(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

        public class PermissionError : Exception
        {
            public string Error() => this.Message;
            public PermissionError(string message)
                : base(message)
            {
            }

            public PermissionError(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        public class ConnectionError : Error
        {
            public string Error() => this.Message;
            public Type ErrorType;

            public ConnectionError(string message, Type type)
                : base(message)
            {
                ErrorType = type;
            }
            public ConnectionError(string message, Type type, Exception inner)
                : base(message, inner)
            {
                ErrorType = type;
            }

            public enum Type
            {
                BrokenPipeError, ConnectionAbortedError, ConnectionRefusedError, ConnectionResetError
            }
        }
        //SystemExit

    }
}
