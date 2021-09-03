# pycs
python functions in c#, net 5.0

```csharp
using static pycs.pycs;
using pycs.modules;

namespace my_app
{
    class Program
    {
        static void Main(string[] args)
        {
            print("Hello world");
            string dir = os.getcwd();
            print("Current directory: " + dir);
        }
    }
}

```