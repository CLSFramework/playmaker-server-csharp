using CyrusGrpc;
using Grpc.Core;
using Microsoft.Extensions.Options;
using System;
//using Action = CyrusGrpc.PlayerAction;

namespace GRpcServer
{
    public class SamplePlayerAgent
    {
        private ServerParam _serverParam;
        private PlayerParam _playerParam;
        private readonly Dictionary<int, PlayerType> _playerTypes;

        public SamplePlayerAgent()
        {
            _serverParam = null;
            _playerParam = null;
            _playerTypes = new();
        }

        public void SetServerParam(ServerParam serverParam)
        {
            this._serverParam = serverParam;
        }

        public void SetPlayerParam(PlayerParam playerParam)
        {
            this._playerParam = playerParam;
        }

        public void SetPlayerType(PlayerType playerType)
        {
            _playerTypes[playerType.Id] = playerType;
        }

        public Task<PlayerActions> GetActions(State request)
        {
            PlayerActions actions = new();
            if (request.WorldModel.Self.IsGoalie)
            {
                actions.Actions.Add(new PlayerAction
                {
                    HeliosGoalie = new HeliosGoalie()
                });
            }
            else if (request.WorldModel.GameModeType == GameModeType.PlayOn)
            {
                if (request.WorldModel.Self.IsKickable)
                {
                    actions.Actions.Add(new PlayerAction
                    {
                        HeliosChainAction = new HeliosChainAction
                        {
                            Cross = true,
                            DirectPass = true,
                            LeadPass = true,
                            ThroughPass = true,
                            ShortDribble = true,
                            LongDribble = true,
                            SimplePass = true,
                            SimpleDribble = true,
                            SimpleShoot = true
                        }
                    });
                }
                else
                {
                    actions.Actions.Add(new PlayerAction
                    {
                        HeliosBasicMove = new HeliosBasicMove()
                    });
                }
            }
            else if (request.WorldModel.IsPenaltyKickMode)
            {
                actions.Actions.Add(new PlayerAction
                {
                    HeliosPenalty = new HeliosPenalty()
                });
            }
            else
            {
                actions.Actions.Add(new PlayerAction
                {
                    HeliosSetPlay = new HeliosSetPlay()
                });
            }
            return Task.FromResult(actions);
        }
    }
}
