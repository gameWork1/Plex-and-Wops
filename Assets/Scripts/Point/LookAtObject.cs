using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public LookAt lookAt;
    private Transform targetObject;
    public string targetObjectTag;
    [SerializeField] private float rotationLeft;


    public enum LookAt { AxisX, AxisZ }


    private void Start()
    {
        targetObject = GameObject.FindWithTag(targetObjectTag).GetComponent<Transform>();
        if(lookAt == LookAt.AxisX)
        {
            if(transform.position.x < targetObject.position.x)
            {
                transform.Rotate(new Vector3(0, rotationLeft, 0));
            }
            else
            {
                transform.Rotate(new Vector3(0, -rotationLeft, 0));
            }
        }else if(lookAt == LookAt.AxisZ)
        {
            if (transform.position.z < targetObject.position.z)
            {
                transform.Rotate(new Vector3(0, 0, rotationLeft));
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, -rotationLeft));
            }
        }
    }
}
