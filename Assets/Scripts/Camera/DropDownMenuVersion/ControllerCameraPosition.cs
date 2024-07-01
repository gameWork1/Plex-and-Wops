using UnityEngine;
using UnityEngine.UI;

public class ControllerCameraPosition : MonoBehaviour
{
    public enum TypePosition
    {
        Near,
        Far
    }
    [SerializeField] private TypePosition typePos = TypePosition.Near;
    [SerializeField] private TypePositionId[] typeId;
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private string playerPrefsName;

    private void Start()
    {
        if(!PlayerPrefs.HasKey(playerPrefsName))
        {
            PlayerPrefs.SetString(playerPrefsName, nameof(typePos));
        }

        for (int i = 0; i < typeId.Length; i++)
        {
            if(typeId[i].name == PlayerPrefs.GetString(playerPrefsName))
            {
                dropdown.value = typeId[i].id;
                break;
            }
        }
    }

    public void ChangeTypePosition(int value)
    {
        switch (value)
        {
            case 0:
                typePos = TypePosition.Near;
                PlayerPrefs.SetString(playerPrefsName, nameof(TypePosition.Near));
                break;
            case 1:
                typePos = TypePosition.Far;
                PlayerPrefs.SetString(playerPrefsName, nameof(TypePosition.Far));
                break;
        }
    }
}

[System.Serializable]
public struct TypePositionId
{
    public string name;
    public int id;
}