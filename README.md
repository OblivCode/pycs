# pycs
python functions in c#, net 5.0<br/>
<br/>
0.11.0<br/>
Added: string, threading, math
<br/><br/>
0.1.0<br/>
-Added datetime, sys<br/>
<br/>
Install with .NET CLI:<br/>
dotnet add package pycs --version [version]<br/>
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
            //print from another thread
            var thread1 = new threading.Thread(print, "Hello World");
            thread1.run();
        }

        static void print(object obj) 
        {
            string str = (string)obj;
            print(str)
        }
    }
}
```
