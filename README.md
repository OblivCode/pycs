# pycs
python functions in c#, net 5.0
<br/>
0.1.0<br/>
-Added datetime<br/>
<br/>
Install with .NET CLI:<br/>
dotnet add package pycs --version 0.1.0<br/>
<br/>
Nuget page:<br/>
https://www.nuget.org/packages/pycs/

```csharp
using static pycs.pycs;
using pycs.modules;
using pycs.modules.datetime;

namespace my_app
{
    class Program
    {
        static void Main(string[] args)
        {
            //print working directory
            string dir = os.getcwd();
            print(dir);
            //print date and time
            var now = datetime.now();
            print(now.strftime("%m/%d/%Y, %H:%M:%S"));
            //print platform
            string platform = sys.platform;
            print(platform);

        }
    }
}

```
