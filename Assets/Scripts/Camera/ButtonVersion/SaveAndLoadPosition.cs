using UnityEngine;

public class SaveAndLoadPosition : MonoBehaviour
{
    private void Start()
    {
        if(!PlayerPrefs.HasKey("CameraX") || !PlayerPrefs.HasKey("CameraZ"))
        {
            SavePosition();
        }
        LoadPosition();
    }

    public void SavePosition()
    {
        PlayerPrefs.SetFloat("CameraX", transform.position.x);
        PlayerPrefs.SetFloat("CameraZ", transform.position.z);
    }
    private void LoadPosition()
    {
        transform.position = new Vector3(PlayerPrefs.GetFloat("CameraX"), transform.position.y, PlayerPrefs.GetFloat("CameraZ"));
    }
}
