using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private static Music music;

    private void Start()
    {
        if(music == null)
        {
            music = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public bool ChangeMute()
    {
        GetComponent<AudioSource>().mute = !GetComponent<AudioSource>().mute;
        return GetComponent<AudioSource>().mute;
    }
}
