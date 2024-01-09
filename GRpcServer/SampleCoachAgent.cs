using CyrusGrpc;

namespace GRpcServer;

public class SampleCoachAgent : SampleAgent
{
    public Task<CoachActions> GetActions(State request)
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
}