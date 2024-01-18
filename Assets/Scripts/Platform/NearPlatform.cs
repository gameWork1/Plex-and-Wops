using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearPlatform : MonoBehaviour
{
    public List<PlatformForDots> nearPlatforms;
    [SerializeField] private Vector3[] directions;

    private void FixedUpdate()
    {
        List<PlatformForDots> platforms = new List<PlatformForDots>();

        foreach (Vector3 direction in directions)
        {
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit) && hit.transform.GetComponent<PlatformForDots>())
            {
                if (!hit.transform.GetComponent<PlatformForDots>().IsFull)
                    platforms.Add(hit.transform.GetComponent<PlatformForDots>());
            }
        }
        nearPlatforms = platforms;
    }

}
