using UnityEngine;

public class TypePlatform : MonoBehaviour
{
    [HideInInspector] public int typePlatform;

    private void Update()
    {
        if(GetComponent<PlatformForDots>().currentPoint != null && GetComponent<PlatformForDots>().currentPoint.transform.GetComponentInChildren<Transform>() != null)
        {
            if (GetComponent<PlatformForDots>().currentPoint.transform.GetChild(0).name == "Plex")
                typePlatform = -1;
            else if(GetComponent<PlatformForDots>().currentPoint.transform.GetChild(0).name == "Wops")
                typePlatform = 1;
        }
        else
        {
            typePlatform = 0;
        }
    }
}
