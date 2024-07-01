using UnityEngine;

public class Music : MonoBehaviour
{
    [HideInInspector]public static Music music;
    [SerializeField] private string volumeId;

    private void Awake()
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

    private void Start()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(volumeId);
    }

}
