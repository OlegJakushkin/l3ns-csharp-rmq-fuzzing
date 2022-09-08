This tutorial demonstrates how to do the following in Docker: 

1. Compile, run, and debug a CSharp application in a Linux Docker container from Visual Studio.

## Creating the app
Create a console application and enable Docker deployment using the [Visual Studio](https://docs.microsoft.com/en-us/visualstudio/containers/edit-and-refresh?view=vs-2022) and [Rider](https://blog.jetbrains.com/dotnet/2018/07/18/debugging-asp-net-core-apps-local-docker-container/) guidelines from your preferred editor.

## Steps:

1. Create console app with .Net SDK 5+ (Generates hello world app)
2. RMB on .Net project -> Add -> Docker Support, Select Linux platform (Adds Docker file to it)
3. Run Docker configuration (See that it can  compile and run outputting `Hello World!`)
4. Be creative, add new code that fails at runtime, and see that Debug functionality of VS is still with you.

We will add IP address listing code:
```csharp
//... refs
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
//... in main
            NetworkInterface
                .GetAllNetworkInterfaces()
                .Select(e=>e.GetIPProperties().UnicastAddresses
                    .FirstOrDefault(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork)?.Address)
                .Where(e=> e!= null)
                .ToList()
                .ForEach(e=>
                    Console.WriteLine("IP Address {0}", e)
                );
```


Tested with VS2019 and sdk5. The generated output of our sample app

```bash
Hello World!
IP Address 127.0.0.1
IP Address 172.17.0.2
```