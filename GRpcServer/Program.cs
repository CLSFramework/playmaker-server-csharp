using CommandLine;
using CyrusGrpc;
using Grpc.Core;

namespace GRpcServer;

internal class Program
{
    // Get windows ip from wsl2: grep -m 1 nameserver /etc/resolv.conf | awk '{print $2}'
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
        List<SampleAgent> agents = new();
        if (!diffPort)
        {
            agents.Add(new SampleAgent("0.0.0.0", gRpcPort));
        }
        else
        {
            for (var i = 1; i <= 11; i++)
            {
                agents.Add(new SampleAgent("0.0.0.0", gRpcPort + i - 1));
            }

            agents.Add(new SampleAgent("0.0.0.0", gRpcPort + 12 - 1));
            agents.Add(new SampleAgent("0.0.0.0", gRpcPort + 13 - 1));
        }

        Console.ReadLine();
    }
}