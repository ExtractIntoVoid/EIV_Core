#if SERVER
using EIV_Common.Extensions;
using ExtractIntoVoid.Managers;
using ExtractIntoVoid.Modding;
using ExtractIntoVoid.Modding.Mutliplayer;
using ExtractIntoVoid.Physics;
using Godot;

namespace EIV_Core.Server;

public class Connection
{
    static List<long> ClientIds = new();

    public static void PeerConnected(OnServerPeerConnect onServerPeerConnect)
    {
        if (onServerPeerConnect.Disable)
            return;

        GameManager.Instance.logger.Information(onServerPeerConnect.Id + " Joined!");
        ClientIds.Add(onServerPeerConnect.Id);

        // set to min to and start spawning
        if (ClientIds.Count >= onServerPeerConnect.World.SubWorld.MinPlayerCount)
        {
            var Transform = onServerPeerConnect.World.SubWorld.SpawnPoints.GetRandom().GlobalTransform;
            onServerPeerConnect.World.Spawner.Spawn(new Godot.Collections.Dictionary<string, Variant>()
            {
                { "id", onServerPeerConnect.Id },
                { "spawn_pos", Transform },
            });
        }
    }

    public static void PeerDisconnected(OnServerPeerDisconnect onServerPeerDisconnect)
    {
        if (onServerPeerDisconnect.Disable)
            return;

        GameManager.Instance.logger.Information(onServerPeerDisconnect.Id + " Left!");
        ClientIds.Remove(onServerPeerDisconnect.Id);
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
        var player = myNode as BasePlayer;
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