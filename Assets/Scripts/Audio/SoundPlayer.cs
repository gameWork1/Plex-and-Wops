using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private string volumeId;

    private void Awake()
    {
        if(PlayerPrefs.HasKey(volumeId)) PlayerPrefs.SetFloat(volumeId, 1.0f);
    }
    private void Start()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(volumeId);
        GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        if(!GetComponent<AudioSource>().isPlaying) Destroy(gameObject);
    }
}
