using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public float normalSpeed;
    public float speed;
    [HideInInspector]public bool isMoving;
     public Transform platform;
    private RaycastHit hit;
    [HideInInspector]public GameObject point;
    public Material normalMaterial;
    public Material transparentMaterial;
    public bool isWops;

    private void Start()
    {
        point = transform.GetChild(0).gameObject;
    }
    public void MoveToPlatform(Vector3 platform)
    {
        isMoving = true;
        transform.position = Vector3.MoveTowards(transform.position, platform, speed * Time.deltaTime);
    }

}
