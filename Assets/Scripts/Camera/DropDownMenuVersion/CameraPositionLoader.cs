using System;
using UnityEngine;

public class CameraPositionLoader : MonoBehaviour
{
    [SerializeField] private TypePosCamera[] typePos;
    [SerializeField] private string PlayerPrefsName;
    
    private void Start()
    {
        for(int i = 0; i < typePos.Length; i++)
        {
            if(typePos[i].name == PlayerPrefs.GetString(PlayerPrefsName))
            {
                transform.position = typePos[i].position;
                break;
            }
        }
    }
    
    [System.Serializable]
    private struct TypePosCamera
    {
        public string name;
        public Vector3 position;
    }
}