using CommandLine;
using CyrusGrpc;
using Grpc.Core;

namespace GRpcServer;

internal class Program
{
    private static void Main(string[] args)
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

        List<Server> servers = new();
        if (!diffPort)
        {
            servers.Add(new Server
            {
                Services = { Game.BindService(new MainService()) },
                Ports = { new ServerPort("localhost", gRpcPort, ServerCredentials.Insecure) }
            });
        }
        else
        {
            for (var i = 1; i <= 13; i++)
            {
                servers.Add(new Server
                {
                    Services = { Game.BindService(new MainService()) },
                    Ports = { new ServerPort("localhost", gRpcPort + i - 1, ServerCredentials.Insecure) }
                });
            }
        }

        servers.ForEach(s => s.Start());

        Console.ReadLine();
    }
}