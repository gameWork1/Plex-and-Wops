using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Direction dirRay;
    private Ray ray;
    public enum Direction { Right, Left, Forward, Back}
    public void MovePoint()
    {
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.GetComponent<PlatformForDots>())
            {
                PlatformForDots platform = hit.transform.gameObject.GetComponent<PlatformForDots>();
                platform.nextPoint = GetComponent<PlatformForDots>().currentPoint;
                GetComponent<PlatformForDots>().currentPoint = null;
            }
        }
            
    }
    private void Update()
    {
        if (dirRay == Direction.Forward)
            ray = new Ray(transform.position, -transform.forward);
        else if (dirRay == Direction.Back)
            ray = new Ray(transform.position, -transform.forward);
        else if (dirRay == Direction.Left)
            ray = new Ray(transform.position, -transform.forward);
        else if (dirRay == Direction.Right)
            ray = new Ray(transform.position, -transform.forward);
    }
}
