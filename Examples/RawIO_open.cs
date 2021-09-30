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
            RawIO rIO = open<RawIO>("hello_kitty.jpg");
            
        }
    }
}
