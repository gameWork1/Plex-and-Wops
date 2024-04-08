using Mirror;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MirrorSettings : MonoBehaviour
{
    public int[] idsScene;
    public string btnQuitGame;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().name != "Menu")
        {
            foreach(int id in idsScene)
            {
                if(SceneManager.GetActiveScene().buildIndex == id)
                    GameObject.FindWithTag(btnQuitGame).GetComponent<Button>().onClick.AddListener(StopGame); break;
            }
        }
            
    }


    public void StartGame()
    {
        //if (!NetworkClient.isConnected && !NetworkServer.active)
        //{
        //    NetworkManager.singleton.StartHost();
        //}
        //else
        //{
        //    NetworkManager.singleton.StartClient();
        //}
        NetworkManager.singleton.StartClient();


    }
    public void StopGame()
    {
        //if (NetworkServer.active && NetworkClient.isConnected)
        //{
        //    NetworkManager.singleton.StopHost();
        //}
        //else if (NetworkClient.isConnected)
        //{
        //    NetworkManager.singleton.StopClient();
        //}
        NetworkManager.singleton.StopClient();
    }
}
