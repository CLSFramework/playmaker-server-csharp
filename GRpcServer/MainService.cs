using CLSF;
using Grpc.Core;

namespace GRpcServer
{
    public class MainService : Game.GameBase
    {
        private readonly SampleAgent _sampleAgent;

        public MainService(SampleAgent sampleAgent)
        {
            Console.WriteLine("MainService created");
            this._sampleAgent = sampleAgent;
        }

        public override Task<PlayerActions> GetPlayerActions(State request, ServerCallContext context)
        {
            return _sampleAgent.GetPlayerActions(request);
        }

        public override Task<CoachActions> GetCoachActions(State request, ServerCallContext context)
        {
            return _sampleAgent.GetCoachActions(request);
        }

        public override Task<TrainerActions> GetTrainerActions(State request, ServerCallContext context)
        {
            return _sampleAgent.GetTrainerActions(request);
        }

        public override Task<Empty> SendInitMessage(InitMessage request, ServerCallContext context)
        {
            Console.WriteLine("SendInitMessage");
            _sampleAgent.SetDebug(request.DebugMode);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> SendPlayerParams(PlayerParam request, ServerCallContext context)
        {
            Console.WriteLine("SendPlayerParams");
            _sampleAgent.SetPlayerParam(request);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> SendPlayerType(PlayerType request, ServerCallContext context)
        {
            Console.WriteLine("SendPlayerType");
            _sampleAgent.SetPlayerType(request);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> SendServerParams(ServerParam request, ServerCallContext context)
        {
            Console.WriteLine("SendServerParams");
            _sampleAgent.SetServerParam(request);
            return Task.FromResult(new Empty());
        }

        public override Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
        {
            return base.Register(request, context);
        }

        public override Task<Empty> SendByeCommand(RegisterResponse request, ServerCallContext context)
        {
            return base.SendByeCommand(request, context);
        }

        public override Task<BestPlannerActionResponse> GetBestPlannerAction(BestPlannerActionRequest request,
            ServerCallContext context)
        {
            return base.GetBestPlannerAction(request, context);
        }
    }
}