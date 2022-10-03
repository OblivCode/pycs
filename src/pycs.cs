using pycs.modules;
using pycs.modules.datetime;
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
using static pycs.modules.io;

namespace pycs
{
    public static class pycs
    {

        //pub
        public static str input(string prompt = "")
        {
            if (prompt.Length > 0)
                Console.Write(prompt);
            return Console.ReadLine();
        }
        //print
        public static void print(object value) => Console.WriteLine(value);
        //repr -------------------------------------------------------------------
        public static str repr(byte[] obj) => Encoding.UTF8.GetString(obj);
        public static str repr<T>(T[] obj)
        {
            string str = "[";
            for (int i = 0; i < obj.Length; i++)
            {
                str += obj[i] + ", ";
            }
            str = str.Trim().Trim(',') + "]";

            return str;
        }
        public static str repr<T>(List<T> obj)
        {
            string str = "[";
            for (int i = 0; i < obj.Count; i++)
            {
                str += obj[i] + ", ";
            }
            str = str.Trim().Trim(',') + "]";

            return str;
        }
        public static str repr<TKey, TValue>(Dictionary<TKey, TValue> obj)
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
        public static str repr(date d, string format = "dd/mm/yyyy")
        {
            format = format.Replace("/", "//");
            string[] arr = format.Split('/');
            string new_str = "";
            foreach(string s in arr)
            {
                string sect = s;
                if (string.IsNullOrEmpty(sect))
                    sect = "/";
                switch (sect) {
                    //day
                    case "d": new_str += d.day % 7;
                        break;
                    case "dd": new_str += d.day;
                        break;
                    //week
                    case "ww": new_str += d.day / 7;
                        break;
                    //month
                    case "m": new_str += d.month >= 10 ? d.month.ToString().Substring(1) : d.month;
                        break;
                    case "mm": new_str += d.month < 10 ? d.month.ToString().Insert(0, "0") : d.month;
                        break;
                    //year
                    case "y": new_str += d.year.ToString().Substring(d.year.ToString().Length - 1);
                        break;
                    case "yy": new_str += d.year >= 10 ? d.year.ToString().Substring(d.year.ToString().Length - 2) : d.year;
                        break;
                    case "yyy": new_str += d.year >= 100 ? d.year.ToString().Substring(d.year.ToString().Length - 3) : d.year;
                        break;
                    case "yyyy": new_str += d.year;
                        break;
                    default: new_str += sect;
                        break;
                }
            }
            return new_str;
        }
        public static str repr(date d, str format) => repr(d, format);
        public static str repr(modules.datetime.time t, string format = "ss/mm/hh")
        {
            format = format.Replace("/", "//");
            string[] arr = format.Split('/');
            string new_str = "";
            foreach (string s in arr)
            {
                string sect = s;
                if (string.IsNullOrEmpty(sect))
                    sect = "/";
                switch(sect)
                {
                    default:
                        new_str += sect;
                        break;
                }
            }
            return new_str;
        }
        public static str repr(modules.datetime.time t, str format) => repr(t, format);
        public static T copy<T>(T obj) => obj; 
        /* Formats for time
         * s - seconds as 1 digit unless > 9
         * ss - seconds as 2 digits
         * m - minutes as 1 digit unless > 9
         * mm - minutes as 2 digits
         * h - hours as 1 digit unless > 9
         * hh - hours as 2 digits
         */
        //-----------------------------------------------------------------------
        //format
        public static str format(string value, string[] spec)
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

        public static str type<T>(T obj)
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
        //public static str str(object obj) => obj.ToString();
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
        public static byte[] b(string value) => bytearray(value);
        public static char chr(int unicode) => char.Parse(char.ConvertFromUtf32(unicode));
        public static str hex(int number) => number.ToString("X");
        public static int hash(object obj) => obj.GetHashCode();
        //eval
        public static decimal eval(string expression) => (decimal)new DataTable().Compute(expression, "");
        //not
        public static bool not(bool value) => !value;
        public static bool not(int value) => value == 0 ? false : true;
        public static bool not(string value) => string.IsNullOrEmpty(value) ? false : true;

        //len
        public static int len<T>(IEnumerable<T> obj) => obj.Count();
        
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
        public static str bin(int number) => Convert.ToString(number, 2);
        //ascii
        public static str ascii(object value)
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
                else if (val is str || val is char)
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
                else if (val is str || val is char)
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
                else if (val is str || val is char)
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
                else if (val is str || val is char)
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
                else if (val is str || val is char)
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
                else if (val is str || val is char)
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
        public static double round(double number, int decimals = 0) => Math.Round(number, decimals);


        //abs
        public static double abs(double value) => Math.Abs(value);
        //pow
        public static double pow(double b, double exp) => Math.Pow(exp, b);
        //sum
        public static double sum<T>(T[] obj)
        {
            double value = 0;
            if (!math.isnan(obj[0]))
            {
                for (int i = 0; i < obj.Length; i++)
                    value += double.Parse(obj[i].ToString());
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
        public static int[] range(int start, int stop, int step = 1)
        { 
            List<int> list = new List<int>();
            for(int i = start;  i < stop; i += step) {
                list.Add(i);
            }
            return list.ToArray();
            /* start = 4
             * stop = 10
             * step = 1
             * 
             * 4, 5, 6, 7
             * 
             * step = 2
             * 4, 6, 8
             */
        }
        /// <summary>
        /// FileNotFoundError only possible with 'r' mode.
        /// </summary>
        /// <exception cref="FileNotFoundError" ></exception>
        public static TextIO open(string filename, string mode = "w+")
        {
            return open<TextIO>(filename, mode);
        }
        /// <summary>
        /// FileNotFoundError only possible with 'r' mode.
        /// </summary>
        /// <exception cref="FileNotFoundException" ></exception>
        public static T open<T>(string filename, string mode = "w+")
        {
            Stream stream;
            
            if (mode.StartsWith('r'))
                if (!os.path.exists(filename))
                    throw new FileNotFoundException("File does not exist.");
            
            switch (mode)
            {
                case "r":
                    stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    break;
                case "w":
                    {
                        if (os.path.exists(filename))
                            stream = new FileStream(filename, FileMode.Open, FileAccess.Write);
                        else
                            stream = new FileStream(filename, FileMode.CreateNew, FileAccess.Write);
                        break;
                    }
                case "w+":
                    {
                        if (os.path.exists(filename))
                            stream = new FileStream(filename, FileMode.Open, FileAccess.Write);
                        else
                            stream = new FileStream(filename, FileMode.CreateNew, FileAccess.Write);
                        break;
                    }
                case "a":
                    stream = new FileStream(filename, FileMode.Append, FileAccess.Write);
                    break;
                case "a+":
                    stream = new FileStream(filename, FileMode.Append, FileAccess.ReadWrite);
                    break;
                default:
                    stream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
                    break;
            }

            IOBase _class = new RawIO(stream);

            return (T)Convert.ChangeType(_class, typeof(T));
        }

        public class TextIO : io.IOBase
        {
            public TextIO(Stream stream, int pos = 0)
            {
                _stream = stream;
                _stream.Position = pos;
            }
            private TextReader reader { get { return new StreamReader(_stream);  } }
            public str encoding { get {
                    Stream clone_stream = new MemoryStream();
                    _stream.CopyTo(clone_stream);
                    var bom = new byte[4];
                    clone_stream.Read(bom, 0, 4);
                    clone_stream.Position -= 4;

                    // Analyze the BOM
                    if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return "UTF-7";
                    if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return "UTF-8";
                    if (bom[0] == 0xff && bom[1] == 0xfe && bom[2] == 0 && bom[3] == 0) return "UTF-32LE"; 
                    if (bom[0] == 0xff && bom[1] == 0xfe) return "UTF-16LE"; 
                    if (bom[0] == 0xfe && bom[1] == 0xff) return "UTF-16BE"; 
                    if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return "UTF-32BE";  

                    return "ASCII";
                } }
            /// <exception cref="Exception" />
            public TextIO write(string content)
            {
                try {
                    byte[] buffer = Encoding.UTF8.GetBytes(content);
                    _stream.Write(buffer, 0, buffer.Length);
                }
                catch (Exception e) { throw new Exception(e.Message, e); }
                return this;
            }
            /// <exception cref="Exception" />
            public async Task<TextIO> write_async(string content)
            {
                
                try {
                    byte[] buffer = Encoding.UTF8.GetBytes(content);
                    await _stream.WriteAsync(buffer, 0, buffer.Length); 
                }
                catch (Exception e) { throw new Exception(e.Message,e);  }
                return this;
            }
            public str read()
            {
                string output = "";
                string line;
                while ((line = reader.ReadLine()) != null)
                    output += line;
                return output;
            }
            public async Task<string> read_async()
            {
                string output = "";
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                    output += line;
                return output;
            }
        }
        public class RawIO : IOBase
        {
            public RawIO(Stream stream)
            {
                _stream = stream;
            }

            public byte[] readall()
            {
                long pos = _stream.Position;
                _stream.Position = 0;
                byte[] buffer = new byte[_stream.Length];
                _stream.Read(buffer, 0, buffer.Length);
                _stream.Position = pos;
                return buffer;
            }

            public RawIO readinto(ref byte[] array)
            {
                byte[] bytes = readall();
                bytes.CopyTo(array, array.Length);
                return this;
            }
        }
       

        //type
        /// <summary>
        /// A byte array object presented as bytes
        /// </summary>
        public class bytes : IEnumerable<byte>
        {
            private byte[] value;
            private int current_index;
            private Encoding encoding = Encoding.UTF8;
            public bytes(byte[] value) => this.value = value;
            public static implicit operator bytes(string value) => new bytes(Encoding.UTF8.GetBytes(value));
            public static implicit operator bytes(str value) => new bytes(Encoding.UTF8.GetBytes(value.ToStandard()));
            public static implicit operator bytes(double value) => new bytes(BitConverter.GetBytes(value));
            public static implicit operator bytes(char value) => new bytes(new byte[] { Convert.ToByte(value) });
            public static implicit operator bytes(byte value) => new bytes(new byte[] { value });
            public static implicit operator bytes(byte[] value) => new bytes(value);
            /// <summary>
            /// Convert bytes to a different encoding
            /// </summary>
            /// <param name="encoding"></param>
            /// <exception cref="UnicodeError"></exception>
            public void SetEncoding(string encoding)
            {
                Encoding x;
                switch(encoding.ToLower())
                {
                    case "utf32":
                        x = Encoding.UTF32;
                        break;
                    case "utf":
                    case "utf8":
                        x = Encoding.UTF8;
                        break;
                    case "utf7":
                        x = Encoding.UTF7;
                        break;
                    case "ascii":
                        x = Encoding.ASCII;
                        break;
                    case "latin":
                    case "latin1":
                        x = Encoding.Latin1;
                        break;
                    case "bigendian":
                    case "utf16be":
                    case "utf-16-be":
                    case "utf16-be":
                    case "utf-16be":
                        x = Encoding.BigEndianUnicode;
                        break;
                    case "unicode":
                    case "utf16le":
                    case "utf-16-le":
                    case "utf16-le":
                    case "utf-16le":
                    case "utf16":
                        x = Encoding.Unicode;
                        break;
                    default:
                        throw new ArgumentException("Encoding not supported: "+encoding);

                }
                value = Encoding.Convert(this.encoding, x, value);
            }
            public static bytes fromhex(string hex)
            {
                byte[] val = Enumerable.Range(0, hex.Length)
                                 .Where(x => x % 2 == 0)
                                 .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                                 .ToArray();
                return new bytes(val);
            }
            public str hex() => BitConverter.ToString(value).Replace("-","");
            public byte[] get() => value;

            

            public byte[] ToStandard() => value;
            public override int GetHashCode() => value.GetHashCode();
            public override bool Equals(object obj) => value.Equals(obj);

            public IEnumerator<byte> GetEnumerator() => new BytesEnumerator(value);

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public class BytesEnumerator : IEnumerator<byte>
            {
                public BytesEnumerator(byte[] value) => this.value = value;
                private byte[] value;
                private int index = 0;
                public byte Current => value[index++];

                object IEnumerator.Current => Current;

                public void Dispose() => value = new byte[0];

                public bool MoveNext()
                {
                    if (index + 1 >= value.Length)
                        return False;
                    index++;
                    return True;
                }
                public bool MovePrevious()
                {
                    if (index == 0)
                        return False;
                    index--;
                    return True;
                }
                public void Reset() => index = 0;
            }
        }
        /// <summary>
        /// A number object.
        /// If bytes are passed, value will be converted as an integer.
        /// </summary>
        public class num
        {
            private object value;

            public num(decimal value) => this.value = value;

            public static implicit operator num(byte[] bytes_value)
            {
                long temp_value = bytes_value.Length switch
                {
                    2 => BitConverter.ToInt16(bytes_value),
                    4 => BitConverter.ToInt32(bytes_value),
                    8 => BitConverter.ToInt64(bytes_value),
                    _ => throw new ArgumentException("Invalid amount of bytes: " + len(bytes_value))
                };
                return new num(temp_value);
            }

            public static implicit operator num(bytes bytes_value)
            {
                long temp_value = len(bytes_value) switch
                {
                    2 => BitConverter.ToInt16(bytes_value.ToStandard()),
                    4 => BitConverter.ToInt32(bytes_value.ToStandard()),
                    8 => BitConverter.ToInt64(bytes_value.ToStandard()),
                    _ => throw new ArgumentException("Invalid amount of bytes: "+len(bytes_value))
                };
                return new num(temp_value);
            }
            public decimal get() => (decimal)value;
            public float ToStandard() => (float)value;
            public override int GetHashCode() => value.GetHashCode();
            public override bool Equals(object obj) => value.Equals(obj);
        }
        /// <summary>
        /// A string object presented as str
        /// </summary>
        public class str : IEnumerable<char> {
            private string value;
            public str(string value) => this.value = value;
            public static implicit operator str(string value) => new str(value);
            public static implicit operator str(char value) => new str(value.ToString());
            public static implicit operator str(byte value) => new str(value.ToString());
            public static implicit operator str(byte[] value) => new str(Encoding.UTF8.GetString(value));
            public static implicit operator str(bytes value) => new str(Encoding.UTF8.GetString(value.ToStandard()));
            public static implicit operator str(double value) => new str(value.ToString());

            public str get() => value;
            public override string ToString() => value;
            public string ToStandard() => value;
            public override int GetHashCode() => value.GetHashCode();
            public override bool Equals(object obj) => value.Equals(obj);
           
            //functions

            public str capitalize()
            {
                value = value[0].ToString().ToUpper() + value.Substring(1);
                return value;
            }
            public str casefold()
            {
                value = value.ToLower();
                return value;
            }
            public int count() => value.Length;
            public bool endswith(char suffix) => value.EndsWith(suffix);
            public bool endswith(string suffix) => value.EndsWith(suffix);
            public bool startswith(char prefix) => value.StartsWith(prefix);
            public bool startswith(string prefix) => value.StartsWith(prefix);
            public int find(char sub) => find(sub.ToString());
            public int find(string sub)
            {
                try { return value.IndexOf(sub); }
                catch(Exception) {  return -1; }
            }
            public int index(char ch) => value.IndexOf(ch);
            public int index(string sub) => value.IndexOf(sub);
            public str format(string spec)
            {
                int idx_start = value.IndexOf('{');
                int idx_end = value.IndexOf('}')+1;
                int length = idx_end - idx_start;
                value = value.Remove(idx_start, length).Insert(idx_start, spec);
                return value;
            }
            public str format(string[] spec)
            {
                foreach(string s in spec)
                {
                    if (value.IndexOf(s) == -1)
                        break;
                    value = value.Replace("{}", s);
                }
                return value;
            }

            public bool isalpha()
            {
                string alphanumeric = @string.digits + @string.ascii_letters;
                foreach(char c in  value)
                {
                    if (!alphanumeric.Contains(c))
                        return False;
                }
                return True;
            }

            public IEnumerator<char> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }

            public class StrEnumerator : IEnumerator<char>
            {
                private char[] value;
                private int index = 0;
                public StrEnumerator(string value) => this.value = value.ToArray();

                public char Current => value[index];

                object IEnumerator.Current => Current;

                public void Dispose() => value = new char[0];
                public bool MoveNext()
                {
                    if (index + 1 >= value.Length)
                        return False;
                    index++;
                    return True;
                }
                public bool MovePrevious()
                {
                    if (index == 0)
                        return False;
                    index--;
                    return True;
                }
                public void Reset() => index = 0;
            }
        }
        
        //delegates
        public delegate EventHandler EmptyHandler();
       

    }
}
