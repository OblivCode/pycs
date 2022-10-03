# pycs
python functions in c#, net 6.0  
Current Version: 0.14.0

Nuget page:
https://www.nuget.org/packages/pycs/

Install with .NET CLI:   
dotnet add package pycs --version [version]
  
See Example folders for demonstrations.  

Simple usage:

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

        }

    }
}
```
