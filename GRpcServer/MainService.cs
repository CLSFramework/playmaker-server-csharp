using CLSF;
using Grpc.Core;

namespace GRpcServer
{
    public class MainService : Game.GameBase
    {
        private readonly Dictionary<int, SampleAgent> _sampleAgents;

        private int _clientId;

        private readonly object _lock = new();

        public MainService()
        {
            Console.WriteLine("MainService created");
            _sampleAgents = [];
        }

        public override Task<PlayerActions> GetPlayerActions(State request, ServerCallContext context)
        {
            return _sampleAgents[request.RegisterResponse.ClientId].GetPlayerActions(request);
        }

        public override Task<CoachActions> GetCoachActions(State request, ServerCallContext context)
        {
            return _sampleAgents[request.RegisterResponse.ClientId].GetCoachActions(request);
        }

        public override Task<TrainerActions> GetTrainerActions(State request, ServerCallContext context)
        {
            return _sampleAgents[request.RegisterResponse.ClientId].GetTrainerActions(request);
        }

        public override Task<Empty> SendInitMessage(InitMessage request, ServerCallContext context)
        {
            Console.WriteLine("SendInitMessage");
            _sampleAgents[request.RegisterResponse.ClientId].SetDebug(request.DebugMode);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> SendPlayerParams(PlayerParam request, ServerCallContext context)
        {
            Console.WriteLine("SendPlayerParams");
            _sampleAgents[request.RegisterResponse.ClientId].SetPlayerParam(request);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> SendPlayerType(PlayerType request, ServerCallContext context)
        {
            Console.WriteLine("SendPlayerType");
            _sampleAgents[request.RegisterResponse.ClientId].SetPlayerType(request);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> SendServerParams(ServerParam request, ServerCallContext context)
        {
            Console.WriteLine("SendServerParams");

            _sampleAgents[request.RegisterResponse.ClientId].SetServerParam(request);
            return Task.FromResult(new Empty());
        }

        public override async Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
        {
            lock (_lock)
            {
                Console.WriteLine($"Register: ${request}");
                var agent = new SampleAgent(request);
                _sampleAgents.Add(_clientId, agent);
                var resp = new RegisterResponse
                {
                    AgentType = request.AgentType,
                    ClientId = _clientId,
                    RpcServerLanguageType = RpcServerLanguageType.Csharp,
                    TeamName = request.TeamName,
                    UniformNumber = request.UniformNumber
                };
                _clientId++;
                return resp;
            }
        }

        public override Task<Empty> SendByeCommand(RegisterResponse request, ServerCallContext context)
        {
            lock (_lock)
            {
                _sampleAgents.Remove(request.ClientId);
            }

            return Task.FromResult(new Empty());
        }

        public override Task<BestPlannerActionResponse> GetBestPlannerAction(BestPlannerActionRequest request,
            ServerCallContext context)
        {
            return base.GetBestPlannerAction(request, context);
        }
    }
}