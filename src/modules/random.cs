<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using static pycs.pycs;

namespace pycs.modules
{
    public static class random 
    {
        private static Random _Random = new Random();
        public static void seed(int seed) => _Random = new Random(seed);
        
        public static int randint(int min, int max) => _Random.Next(min, max);
        public static int randrange(int start, int stop) => randint(start, stop - 1);
        public static double random_() => _Random.NextDouble();

        public static double uniform(double min, double max)
        {
            int _min = int.Parse(math.trunc(min).ToString());
            int _max = int.Parse(math.trunc(max).ToString());
            int _ranint = randint(_min, _max);
            int _randec = randint(0, 999999999);
            return double.Parse($"{_ranint}.{_randec}");
        }
        //shuffle
        public static T[] shuffle<T>(T[] arr)
        {
            T[] new_arr = new T[arr.Length];
            List<T> list = arr.ToList();
          
            for(int new_index =0; new_index <new_arr.Length; new_index++)
            {
                //get random index from arr
                int old_index = randint(0, list.Count-1);
                new_arr[new_index] = list[old_index];
                list.RemoveAt(old_index);
            }

            return new_arr;
        }

        public static List<T> shuffle<T>(List<T> list)
        {
            return shuffle(list.ToArray()).ToList();
        }
        public static Dictionary<TKey, TValue> shuffle<TKey, TValue>(Dictionary<TKey, TValue> dict)
        {
            Dictionary<TKey, TValue> new_dict = new Dictionary<TKey, TValue>();

            for (int new_index = 0; new_index < dict.Count; new_index++)
            {
                //get random index from arr
                int old_index = randint(0, dict.Count - 1);
                var selected_key = dict.Keys.ElementAt(old_index);
                var selected_val = dict[selected_key];
                new_dict.Add(selected_key, selected_val);
                dict.Remove(selected_key);;
            }

            return new_dict;
        }
        //sample
        public static T[] sample<T>(T[] arr, int length)
        {
            if (length > arr.Length) throw new IndexError("Length greater than array's length");
            T[] sample_arr = new T[length];

            for (int i = 0; i < length; i++)
            {
                //get random index from arr
                int ran_index = randint(0, arr.Length-1);
                T selected = arr[ran_index];
                if(sample_arr.Contains(selected))
                {
                    i--;
                    continue;
                }
                sample_arr.Append(selected);
                
            }
            return sample_arr;
        }

        public static List<T> sample<T>(List<T> list, int length) => sample(list.ToArray(), length).ToList();
        public static Dictionary<TKey, TValue> sample<TKey, TValue>(Dictionary<TKey, TValue> dict, int length)
        {
            if (length > dict.Count) throw new IndexError("Length greater than array's length");
            var sample_dict = new Dictionary<TKey, TValue>();

            for (int i = 0; i < length; i++)
            {
                //get random index from arr
                int ran_index = randint(0, dict.Count - 1);
                var selected = dict.ElementAt(ran_index);
                if (sample_dict.Contains(selected))
                {
                    i--;
                    continue;
                }
                sample_dict.Append(selected);

            }
            return sample_dict;
        }

        //choice
        /// <exception cref="pycs.IndexError"
        public static T choice<T>(T[] arr)
        {
            if (arr.Length == 0) throw new IndexError("No element at index 0");
            int ran = randint(0, arr.Length - 1);
            return arr[ran];
        }
        /// <exception cref="pycs.IndexError"
        public static T choice<T>(List<T> list)
        {
            if(list.Count == 0 ) throw new IndexError("No element at index 0");
            int ran = randint(0, list.Count - 1);
            return list[ran];
        }
        /// <exception cref="pycs.IndexError"
        public static TKey choice<TKey,TValue>(Dictionary<TKey,TValue> dict)
        {
            if(dict.Count == 0) throw new IndexError("No element at index 0");
            int ran = randint(0, dict.Count - 1);
            return dict.Keys.ElementAt(ran); 
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using static pycs.pycs;

namespace pycs.modules
{
    public static class random 
    {
        private static Random _Random = new Random();
        public static void seed(int seed) => _Random = new Random(seed);
        
        public static int randint(int min, int max) => _Random.Next(min, max);
        public static int randrange(int start, int stop) => randint(start, stop - 1);
        public static double random_() => _Random.NextDouble();

        public static double uniform(double min, double max)
        {
            int _min = int.Parse(math.trunc(min).ToString());
            int _max = int.Parse(math.trunc(max).ToString());
            int _ranint = randint(_min, _max);
            int _randec = randint(0, 999999999);
            return double.Parse($"{_ranint}.{_randec}");
        }
        //shuffle
        public static T[] shuffle<T>(T[] arr)
        {
            T[] new_arr = new T[arr.Length];
            List<T> list = arr.ToList();
          
            for(int new_index =0; new_index <new_arr.Length; new_index++)
            {
                //get random index from arr
                int old_index = randint(0, list.Count-1);
                new_arr[new_index] = list[old_index];
                list.RemoveAt(old_index);
            }

            return new_arr;
        }

        public static List<T> shuffle<T>(List<T> list)
        {
            return shuffle(list.ToArray()).ToList();
        }
        public static Dictionary<TKey, TValue> shuffle<TKey, TValue>(Dictionary<TKey, TValue> dict)
        {
            Dictionary<TKey, TValue> new_dict = new Dictionary<TKey, TValue>();

            for (int new_index = 0; new_index < dict.Count; new_index++)
            {
                //get random index from arr
                int old_index = randint(0, dict.Count - 1);
                var selected_key = dict.Keys.ElementAt(old_index);
                var selected_val = dict[selected_key];
                new_dict.Add(selected_key, selected_val);
                dict.Remove(selected_key);;
            }

            return new_dict;
        }
        //sample
        public static T[] sample<T>(T[] arr, int length)
        {
            if (length > arr.Length) throw new IndexError("Length greater than array's length");
            T[] sample_arr = new T[length];

            for (int i = 0; i < length; i++)
            {
                //get random index from arr
                int ran_index = randint(0, arr.Length-1);
                T selected = arr[ran_index];
                if(sample_arr.Contains(selected))
                {
                    i--;
                    continue;
                }
                sample_arr.Append(selected);
                
            }
            return sample_arr;
        }

        public static List<T> sample<T>(List<T> list, int length) => sample(list.ToArray(), length).ToList();
        public static Dictionary<TKey, TValue> sample<TKey, TValue>(Dictionary<TKey, TValue> dict, int length)
        {
            if (length > dict.Count) throw new IndexError("Length greater than array's length");
            var sample_dict = new Dictionary<TKey, TValue>();

            for (int i = 0; i < length; i++)
            {
                //get random index from arr
                int ran_index = randint(0, dict.Count - 1);
                var selected = dict.ElementAt(ran_index);
                if (sample_dict.Contains(selected))
                {
                    i--;
                    continue;
                }
                sample_dict.Append(selected);

            }
            return sample_dict;
        }

        //choice
        /// <exception cref="pycs.IndexError"
        public static T choice<T>(T[] arr)
        {
            if (arr.Length == 0) throw new IndexError("No element at index 0");
            int ran = randint(0, arr.Length - 1);
            return arr[ran];
        }
        /// <exception cref="pycs.IndexError"
        public static T choice<T>(List<T> list)
        {
            if(list.Count == 0 ) throw new IndexError("No element at index 0");
            int ran = randint(0, list.Count - 1);
            return list[ran];
        }
        /// <exception cref="pycs.IndexError"
        public static TKey choice<TKey,TValue>(Dictionary<TKey,TValue> dict)
        {
            if(dict.Count == 0) throw new IndexError("No element at index 0");
            int ran = randint(0, dict.Count - 1);
            return dict.Keys.ElementAt(ran); 
        }
    }
}
>>>>>>> origin/master
