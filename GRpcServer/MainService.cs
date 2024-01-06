using CyrusGrpc;
using Grpc.Core;
using System;

namespace GRpcServer
{
    public class MainService : Game.GameBase
    {
        private static SamplePlayerAgent _agent = new();

        public MainService()
        {
            Console.WriteLine("MainService created");
        }
        public override Task<Actions> GetActions(State request, ServerCallContext context)
        {
            return _agent.GetActions(request);
        }

        public override Task<Empty> SendInitMessage(InitMessage request, ServerCallContext context)
        {
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> SendPlayerParams(PlayerParam request, ServerCallContext context)
        {
            _agent.SetPlayerParam(request);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> SendPlayerType(PlayerType request, ServerCallContext context)
        {
            _agent.SetPlayerType(request);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> SendServerParams(ServerParam request, ServerCallContext context)
        {
            _agent.SetServerParam(request);
            return Task.FromResult(new Empty());
        }
    }
}
