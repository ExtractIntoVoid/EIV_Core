#if CLIENT
using ExtractIntoVoid.Modding.Mutliplayer;
using ExtractIntoVoid.Modding;
using ExtractIntoVoid.Physics;
using ExtractIntoVoid.Managers;
using Godot;

namespace EIV_Core.Client;

public class Connection
{
    public static void PeerConnected(OnClientPeerConnect onClientPeerConnect)
    {
        if (onClientPeerConnect.Disable)
            return;

        GameManager.Instance.logger.Information(onClientPeerConnect.Id + " Joined!");
    }
    public static void PeerDisconnected(OnClientPeerDisconnect onClientPeerDisconnect)
    {
        if (onClientPeerDisconnect.Disable)
            return;

        GameManager.Instance.logger.Information(onClientPeerDisconnect.Id + " Left!");
    }

    public static void OnSpawnNode(WorldEvents.OnSpawnNode onSpawnNode)
    {
        if (onSpawnNode.Disable)
            return;
        GameManager.Instance.logger.Information(onSpawnNode.InputVariant + " OnSpawn!");

        var Dict = (Godot.Collections.Dictionary<string, Variant>)onSpawnNode.InputVariant;
        var scene = onSpawnNode.World.Spawner.GetSpawnableScene(0);

        if (!ResourceLoader.Exists(scene))
        {
            GD.PrintErr("Scene is not exist!");
            return;
        }
        var myNode = ResourceLoader.Load<PackedScene>(scene).Instantiate();
        myNode.Name = (Dict["id"].AsInt32()).ToString();
        var player = myNode as Player;
        if (player == null)
        {
            GD.PrintErr("myNode should be the player but not!");
            // myNode should be the player but not. That is not good so we just return.
            return;
        }
        player.GlobalTransform = Dict["spawn_pos"].AsTransform3D();
        onSpawnNode.ReturnerNode = player;
        GD.Print("Should spawn");
    }
}
#endif