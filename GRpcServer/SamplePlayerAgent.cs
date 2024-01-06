using CyrusGrpc;
using Grpc.Core;
using Microsoft.Extensions.Options;
using System;
using Action = CyrusGrpc.Action;

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

        public Task<Actions> GetActions(State request)
        {
            Actions actions = new();
            if (request.WorldModel.Self.IsGoalie)
            {
                actions.Actions_.Add(new Action
                {
                    HeliosGoalie = new HeliosGoalie()
                });
            }
            else if (request.WorldModel.GameModeType == GameModeType.PlayOn)
            {
                if (request.WorldModel.Self.IsKickable)
                {
                    actions.Actions_.Add(new Action
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
                    actions.Actions_.Add(new Action
                    {
                        HeliosBasicMove = new HeliosBasicMove()
                    });
                }
            }
            else if (request.WorldModel.IsPenaltyKickMode)
            {
                actions.Actions_.Add(new Action
                {
                    HeliosPenalty = new HeliosPenalty()
                });
            }
            else
            {
                actions.Actions_.Add(new Action
                {
                    HeliosSetPlay = new HeliosSetPlay()
                });
            }
            return Task.FromResult(actions);
        }
    }
}
