﻿using CLSF;

namespace GRpcServer;

public class SampleAgent
{
    protected ServerParam ServerParam = null!;
    protected PlayerParam PlayerParam = null!;
    protected readonly Dictionary<int, PlayerType> PlayerTypes = new();
    protected bool Debug;
    public AgentType AgentType { get; }
    public int RpcVersion { get; }
    public string TeamName { get; }
    public int UniformNumber { get; }

    public SampleAgent(RegisterRequest request)
    {
        Console.WriteLine("Creating Sample Agent");
        AgentType = request.AgentType;
        RpcVersion = request.RpcVersion;
        TeamName = request.TeamName;
        UniformNumber = request.UniformNumber;
    }

    public void SetServerParam(ServerParam serverParam)
    {
        ServerParam = serverParam;
    }

    public void SetPlayerParam(PlayerParam playerParam)
    {
        PlayerParam = playerParam;
    }

    public void SetPlayerType(PlayerType playerType)
    {
        PlayerTypes.TryAdd(playerType.Id, playerType);
    }

    public void SetDebug(bool debug)
    {
        Debug = debug;
    }

    public Task<PlayerActions> GetPlayerActions(State request)
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
                    HeliosShoot = new HeliosShoot()
                });
                actions.Actions.Add(new PlayerAction
                {
                    HeliosOffensivePlanner = new HeliosOffensivePlanner
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

    public Task<CoachActions> GetCoachActions(State request)
    {
        CoachActions actions = new();
        actions.Actions.Add(new CoachAction
        {
            DoHeliosSayPlayerTypes = new DoHeliosSayPlayerTypes()
        });
        actions.Actions.Add(new CoachAction
        {
            DoHeliosSubstitute = new DoHeliosSubstitute()
        });
        return Task.FromResult(actions);
    }

    public Task<TrainerActions> GetTrainerActions(State request)
    {
        TrainerActions actions = new();

        actions.Actions.Add(new TrainerAction
        {
            DoMoveBall = new DoMoveBall
            {
                Position = new RpcVector2D { X = 0, Y = 0 },
                Velocity = new RpcVector2D { X = 0, Y = 0 }
            }
        });

        return Task.FromResult(actions);
    }
}