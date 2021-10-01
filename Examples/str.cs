using static pycs.pycs;
using pycs.modules;
using pycs.modules.zipfile;
using pycs.modules.pathlib;
using System.Threading.Tasks;

namespace my_app
{
    class Program
    {

        static void Main()
        {
            str bytes_str = new byte[12];
            
            int[] r = range(4, 10);

            str print_array = "Array: {0}";
            print_array.format(repr(r));
            
            print(print_array);
        }
    }
}
