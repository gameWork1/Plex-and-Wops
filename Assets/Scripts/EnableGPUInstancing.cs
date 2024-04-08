using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class EnableGPUInstancing : MonoBehaviour
{
    private void Awake()
    {
        MaterialPropertyBlock materialProperty = new MaterialPropertyBlock();
        GetComponent<MeshRenderer>().SetPropertyBlock(materialProperty);
    }
}
