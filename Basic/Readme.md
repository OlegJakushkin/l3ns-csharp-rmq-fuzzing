This tutorial demonstrates how to do the following in Docker: 

1. Compile, run, and debug a CSharp application in a Linux Docker container from Visual Studio.

## Creating the app
Create a console application and enable Docker deployment using the [Visual Studio](https://docs.microsoft.com/en-us/visualstudio/containers/edit-and-refresh?view=vs-2022) and [Rider](https://blog.jetbrains.com/dotnet/2018/07/18/debugging-asp-net-core-apps-local-docker-container/) guidelines from your preferred editor.

## Steps:

1. Create console app with .Net SDK 5+ (Generates hello world app)
2. RMB on .Net project -> Add -> Docker Support, Select Linux platform (Adds Docker file to it)
3. Run Docker configuration (See that it can  compile and run outputting `Hello World!`)
4. Be creative, add new code that fails at runtime, and see that Debug functionality of VS is still with you. We can also see our app in the Docker containers list.

We will add the IP address listing code:
```CSharp
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

5. Let's now add an infinite loop, recompile our app, and test it in the editor. Now stop the container from the Docker manager app. We will say that our app was named `BaseApp`, and the docker image created `baseapp:dev`

6. Now open CMD or PowerShell and cd into our project bin/Debug/ folder. Run `docker run -v "%cd%":"/app" -it  baseapp:dev /bin/bash -c "dotnet BaseApp.dll"`. This will start our image as a 

7. Set debug point in your infinite loop code. Attach to the container from the editor using Debug->Attach to Process. Connection type: Docker (Linux Container), Connection target: Find->Select your container (it will have randomly generated name), select process in it (it shall start with `dotnet`. Click Attach. BreakPoint shall fire.