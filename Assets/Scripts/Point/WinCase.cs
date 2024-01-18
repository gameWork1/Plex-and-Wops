using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCase : MonoBehaviour
{
    public Vector3 offsetRotation;
    public int lenghtPoint;
    private RaycastHit[] hits;

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, transform.forward + offsetRotation);

        hits = Physics.RaycastAll(ray, 10f);

        Debug.DrawRay(transform.position, ray.direction, Color.red);
    }
    public bool CheckWin(bool isWops)
    {
        if (hits != null && hits.Length >= lenghtPoint)
        {
            List<Point> points = new List<Point>();
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.gameObject.GetComponent<Point>() && hit.transform.gameObject.GetComponent<Point>().isWops == isWops)
                {
                    points.Add(hit.transform.gameObject.GetComponent<Point>());
                }
            }
            if (points.Count >= lenghtPoint)
            {
                return true;
            }
        }
        return false;
    }
}
