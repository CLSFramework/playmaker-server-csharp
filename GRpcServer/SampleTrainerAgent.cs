using CyrusGrpc;

namespace GRpcServer;

public class SampleTrainerAgent : SampleAgent
{
    public Task<TrainerActions> GetActions(State request)
    {
        TrainerActions actions = new();

        actions.Actions.Add(new TrainerAction
        {
            DoMoveBall = new DoMoveBall
            {
                Position = new Vector2D { X = 0, Y = 0 },
                Velocity = new Vector2D { X = 0, Y = 0 }
            }
        });

        return Task.FromResult(actions);
    }
}