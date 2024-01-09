using CommandLine;

namespace GRpcServer
{
    public class Options
    {
        [Option('p', "port", Required = false, HelpText = "gRPC Port")]
        public int GRpcPort { get; set; } = 50001;

        [Option('d', "diff", Required = false, HelpText = "Use different port for agents, gRPCPort + Unum, coachUnum=12, trainerUnum=13")]
        public bool DiffPort { get; set; }

        [Option('r', "right", Required = false, HelpText = "Use different port for sides, GRpcPort + 20 for right")]
        public bool RightPort { get; set; }
    }
}
