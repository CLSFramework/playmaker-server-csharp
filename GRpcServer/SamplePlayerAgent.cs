using CyrusGrpc;

//using Action = CyrusGrpc.PlayerAction;

namespace GRpcServer;

public class SamplePlayerAgent : SampleAgent
{
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
            else
                actions.Actions.Add(new PlayerAction
                {
                    HeliosBasicMove = new HeliosBasicMove()
                });
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