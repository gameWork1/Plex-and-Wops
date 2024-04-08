using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckerPlayer : MonoBehaviour
{
    [HideInInspector] public bool isWops;
    [SerializeField] private bool wopsBall;
    private bool checkedPlayer;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void LateUpdate()
    {
        if (checkedPlayer) return;
        if( (NetworkServer.connections.Count) % 2 == 0)
            isWops = false;
        else
            isWops = true;

        if (wopsBall)
        {
            if (!isWops)
            {
                Destroy(gameObject);
                checkedPlayer = true;
            }
                
        }
        else
        {
            if (isWops)
            {
                Destroy(gameObject);
                checkedPlayer = true;
            }
        }
        
    }
}
