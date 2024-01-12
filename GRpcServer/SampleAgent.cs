using CyrusGrpc;


namespace GRpcServer;

public abstract class SampleAgent
{
    protected ServerParam ServerParam = null!;
    protected PlayerParam PlayerParam = null!;
    protected readonly Dictionary<int, PlayerType> PlayerTypes = new();
    protected bool Debug;

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
}