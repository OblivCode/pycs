using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace pycs
{
    public static class pycs
    {
        private static bool is_num<T>(T value)
        {
            if (value is int || value is double || value is float || value is decimal)
                return true;
            else
                return false;
            
        }
        //pub
        public static string input() => Console.ReadLine();
        public static void print(object value, bool newline = true) => Console.WriteLine(value);
        //format
        public static string format(string value, string[] spec)
        { 
            int matches = Regex.Matches(value, "{}").Count;
            for(int i =0; i < matches; i++)
            {
                string word = spec[i];
                var regex = new Regex(Regex.Escape("{}"));
                value = regex.Replace(value, word, 1);            }
            return value;
        }
        //types
        public static bool False { get { return false; } }
        public static bool True { get { return true; } }
        public static object object_() => new object();
        public static List<T> list<T>() => new List<T>();
        //type conversions
        public static string str(object obj) => obj.ToString();
        public static int int_<T>(T value) => int.Parse(value.ToString());
        public static float float_<T>(T value) => float.Parse(value.ToString());
        public static bool bool_<T>(T value)
        {
            if (is_num(value))
                return (decimal.Parse(value.ToString()) == 0 ? false : true);
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
        public static bool not(int value) => false ? value == 0 : true;
        public static bool not(string value) => false ? value.Length == 0 : true;

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
        public static int len<TKey,TValue>(SortedList<TKey, TValue> obj) => obj.Count;
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
                ascii_text += unicode < 128 ? ((char)unicode) : "\\";
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
            foreach(T val in value)
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
        public static bool all<TKey,TValue>(Dictionary<TKey, TValue> value)
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
        public static decimal round(decimal number, int decimals = -1)
        {
            if (decimals < 0)
                return Math.Round(number);
            else
                return Math.Round(number);
        }
        
       
        //abs
        public static int abs(int value)
        {
            if (value >= 0)
                return value;
            else
                return value * -1;
        }
    //pow
    public static double pow(double a, double b)
        {
           
            for (int i = 0; i < b; i++)
            {
                a *= a;
            }
            return a;
        }
        public static float pow(float a, float b)
        {

            for (int i = 0; i < b; i++)
            {
                a *= a;
            }
            return a;
        }
        public static int pow(int a, int b)
        {

            for (int i = 0; i < b; i++)
            {
                a *= a;
            }
            return a;
        }
        public static decimal pow(decimal a, decimal b)
        {

            for (int i = 0; i < b; i++)
            {
                a *= a;
            }
            return a;
            
        }

        //file
        /// <summary>
        /// Modes: 't', 'b' and 'a' are not supported. 
        /// Mode 'a' will do the same as mode 'w'.
        /// Returns null if mode is incorrect.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="mode"></param>
        /// <returns>FileStream</returns>
        public static FileStream open(string filename, char mode)
        {
            switch (mode)
            {
                case 'r': return File.OpenRead(filename);
                case 'w': return File.OpenWrite(filename);
                case 'a': return File.OpenWrite(filename);
                case 'x': return File.Create(filename);
                default: return null;
            }
        }
        
    
        
    public static class os
        {
            //enviroment variables
            public static void setenv(string key, string? value) => Environment.SetEnvironmentVariable(key, value);
            public static string? getenv(string key) => Environment.GetEnvironmentVariable(key);
            
            public static Dictionary<string, string> globals()
            {
                var env_vars = Environment.GetEnvironmentVariables();
                var dict = new Dictionary<string, string>();
                foreach (var key in env_vars.Keys)
                {
                    var value = env_vars[key];
                    dict.Add(key.ToString(), value.ToString());
                }
                return dict;
            }
        }

    }
}
