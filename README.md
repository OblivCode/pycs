# pycs
python functions in c#, net 5.0

0.12.0  
Added time, random, logging, json, socket                        
Note: See examples folder to see how socket class is used
         
0.11.0  
Added string, threading, math

0.1.0  
Added datetime, sys

Install with .NET CLI  
dotnet add package pycs --version [version]

Nuget page:
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

        }

    }
}
```
