using CyrusGrpc;
using Grpc.Core;
using System;

namespace GRpcServer
{
    public class MainService : Game.GameBase
    {
        private static readonly SamplePlayerAgent PlayerAgent = new();
        private static readonly SampleCoachAgent CoachAgent = new();
        private static readonly SampleTrainerAgent TrainerAgent = new();

        public MainService()
        {
            Console.WriteLine("MainService created");
        }

        public override Task<PlayerActions> GetPlayerActions(State request, ServerCallContext context)
        {
            return PlayerAgent.GetActions(request);
        }

        public override Task<CoachActions> GetCoachActions(State request, ServerCallContext context)
        {
            return CoachAgent.GetActions(request);
        }

        public override Task<TrainerActions> GetTrainerActions(State request, ServerCallContext context)
        {
            return TrainerAgent.GetActions(request);
        }

        public override Task<Empty> SendInitMessage(InitMessage request, ServerCallContext context)
        {
            switch (request.AgentType)
            {
                case AgentType.PlayerT:
                    PlayerAgent.SetDebug(request.DebugMode);
                    break;
                case AgentType.CoachT:
                    CoachAgent.SetDebug(request.DebugMode);
                    break;
                case AgentType.TrainerT:
                    //TODO
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> SendPlayerParams(PlayerParam request, ServerCallContext context)
        {
            switch (request.AgentType)
            {
                case AgentType.PlayerT:
                    PlayerAgent.SetPlayerParam(request);
                    break;
                case AgentType.CoachT:
                    CoachAgent.SetPlayerParam(request);
                    break;
                case AgentType.TrainerT:
                    //TODO
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> SendPlayerType(PlayerType request, ServerCallContext context)
        {
            switch (request.AgentType)
            {
                case AgentType.PlayerT:
                    PlayerAgent.SetPlayerType(request);
                    break;
                case AgentType.CoachT:
                    CoachAgent.SetPlayerType(request);
                    break;
                case AgentType.TrainerT:
                    //TODO
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> SendServerParams(ServerParam request, ServerCallContext context)
        {
            switch (request.AgentType)
            {
                case AgentType.PlayerT:
                    PlayerAgent.SetServerParam(request);
                    break;
                case AgentType.CoachT:
                    CoachAgent.SetServerParam(request);
                    break;
                case AgentType.TrainerT:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Task.FromResult(new Empty());
        }
    }
}
