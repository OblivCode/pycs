using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using static pycs.pycs;

#nullable enable
namespace pycs.modules
{
    public static class json
    {
        public static T? loads<T>(string json) => JsonSerializer.Deserialize<T>(json);

        public static object? loads(string json) => JsonSerializer.Deserialize<object>(json);

        public static T? load<T>(Stream stream) => loads<T>(new TextIO(stream).read());
        public static object? load(Stream stream) => loads(new TextIO(stream).read());

        public static string dumps<T>(T obj) => JsonSerializer.Serialize(obj);
        ///<exception cref="FileNotFoundError" />
        public static void dump<T>(T obj, string path)
        {
            
            TextIO file = open(path);
            file.write(dumps(obj));
            file.close();
        }

    }

}
