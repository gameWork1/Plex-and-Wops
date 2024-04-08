using UnityEngine;
using Mirror;
using UnityEngine.UI;
using Mirror.Discovery;
using System;
using UnityEngine.SceneManagement;

public class CustomNetworkDiscoveryHUD : NetworkDiscoveryHUD
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private float offset;
    [SerializeField] private GameObject discoveryButton;
    private Transform canvas;
    [SerializeField] private string canvasTag;
    private GameObject findObj;

    public void FindGames()
    {
        discoveredServers.Clear();
        networkDiscovery.StartDiscovery();

        float currentOffset = 0;
        FindObjectFromActiveScene(canvasTag);
        canvas = findObj.transform;
        Debug.Log(canvas);
        foreach (ServerResponse info in discoveredServers.Values)
        {
            //Vector2 pos = new Vector2(startPosition.position.x, startPosition.position.y + currentOffset);
            //Button button = Instantiate(discoveryButton, pos, Quaternion.identity).GetComponent<Button>();
            ////button.onClick.AddListener(() => ConnectToServer(info));
            
            ////button.GetComponentInChildren<Text>().text = info.EndPoint.Address.ToString();
            //currentOffset += offset;
            //Debug.Log(123);
        }
        Vector2 pos = new Vector2(startPosition.position.x, startPosition.position.y + currentOffset);
        Button button = Instantiate(discoveryButton, pos, Quaternion.identity).GetComponent<Button>();
        //button.onClick.AddListener(() => ConnectToServer(info));

        //button.GetComponentInChildren<Text>().text = info.EndPoint.Address.ToString();
        currentOffset += offset;
        Debug.Log(123);
    }

    private void ConnectToServer(ServerResponse info)
    {
        networkDiscovery.StopDiscovery();
        NetworkManager.singleton.StartClient(info.uri);
    }

    private void FindObjectFromActiveScene(string tag)
    {
        foreach (GameObject obj in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (obj.transform.tag == tag)
            {
                findObj = obj;
            }
        }
    }


}
