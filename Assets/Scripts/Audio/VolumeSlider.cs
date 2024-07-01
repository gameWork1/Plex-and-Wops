using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private string volumeId;
    [SerializeField] private bool isMain;

    private void Start()
    {
        if(volumeId != "")
        {
            if(!PlayerPrefs.HasKey(volumeId))
            {
                PlayerPrefs.SetFloat(volumeId, 1f);
            }
        }
        GetComponent<Slider>().value = PlayerPrefs.GetFloat(volumeId);
    }

    public void ChangeVolume()
    {
        PlayerPrefs.SetFloat(volumeId, GetComponent<Slider>().value);
        if (isMain) Music.music.gameObject.GetComponent<AudioSource>().volume = GetComponent<Slider>().value;
    }
}
