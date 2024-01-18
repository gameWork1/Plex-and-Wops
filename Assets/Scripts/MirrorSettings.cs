using Mirror;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MirrorSettings : MonoBehaviour
{
    public int[] idsScene;
    public string btnMultiplayer;
    public string btnQuitGame;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "Menu")
            GameObject.FindWithTag(btnMultiplayer).GetComponent<Button>().onClick.AddListener(StartGame);
        else
        {
            foreach(int id in idsScene)
            {
                if(SceneManager.GetActiveScene().buildIndex == id)
                    GameObject.FindWithTag(btnQuitGame).GetComponent<Button>().onClick.AddListener(StopGame); break;
            }
        }
            
    }

    void StartGame()
    {
        if(!NetworkServer.active && !NetworkClient.isConnected)
        {
            Debug.Log("Host");
            NetworkManager.singleton.StartHost();
        }
           
        else if(NetworkServer.active && !NetworkClient.isConnected)
        {
            Debug.Log("Client");
            NetworkManager.singleton.StartClient();
        }
            
    }
    void StopGame()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        }
        else if (NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopClient();
        }
        
    }
}
