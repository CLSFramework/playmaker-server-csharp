using CLSF;
using CommandLine;
using Grpc.Core;

namespace GRpcServer;

internal class Program
{
    // Get windows ip from wsl2: grep -m 1 nameserver /etc/resolv.conf | awk '{print $2}'
    // in wsl powershell.exe -Command "ipconfig" and find wifi ip
    private static async Task Main(string[] args)
    {
        var gRpcPort = 50051;
        var diffPort = false;
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(o =>
            {
                gRpcPort = o.GRpcPort;
                if (o.RightPort)
                {
                    gRpcPort += 20;
                }

                if (o.DiffPort)
                {
                    diffPort = true;
                }
            });

        if (!diffPort)
        {
            var server = new Server
            {
                Services = { Game.BindService(new MainService()) },
                Ports = { new ServerPort("0.0.0.0", gRpcPort, ServerCredentials.Insecure) }
            };
            server.Start();
        }
        else
        {
            List<Server> servers = [];
            for (var i = 1; i <= 11; i++)
                servers.Add(new Server
                {
                    Services = { Game.BindService(new MainService()) },
                    Ports = { new ServerPort("0.0.0.0", gRpcPort + i - 1, ServerCredentials.Insecure) }
                });

            servers.Add(new Server
            {
                Services = { Game.BindService(new MainService()) },
                Ports = { new ServerPort("0.0.0.0", gRpcPort + 12 - 1, ServerCredentials.Insecure) }
            });
            servers.Add(new Server
            {
                Services = { Game.BindService(new MainService()) },
                Ports = { new ServerPort("0.0.0.0", gRpcPort + 13 - 1, ServerCredentials.Insecure) }
            });

            foreach (var server in servers) server.Start();
        }

        Console.ReadLine();
    }
}