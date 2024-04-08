using Mirror;
using UnityEngine;

public class LobbyManager : NetworkRoomManager
{
    public static LobbyManager instance;

    private void Awake()
    {
        base.Awake();
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

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
