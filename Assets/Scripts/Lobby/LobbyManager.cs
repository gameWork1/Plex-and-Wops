using Mirror;
using UnityEngine;

public class LobbyManager : NetworkRoomManager
{
    public override void OnRoomClientEnter()
    {
        base.OnRoomClientEnter();
        if (roomSlots.Count >= minPlayers)
        {
            ServerChangeScene(GameplayScene);
        }
    }
    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        if (roomSlots.Count < minPlayers && networkSceneName != RoomScene)
        {
            ServerChangeScene(RoomScene);
        }
    }
}
