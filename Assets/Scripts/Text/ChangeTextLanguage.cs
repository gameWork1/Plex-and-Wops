using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextLanguage : MonoBehaviour
{
    public TextSettings[] textVariants;
    private Text text;
    [SerializeField] private string replaceText = "";

    private void Start()
    {
        if (!PlayerPrefs.HasKey("lang"))
            PlayerPrefs.SetString("lang", "EN");
        text = GetComponent<Text>();
    }
    private void LateUpdate()
    {
        if(replaceText == "")
        {
            foreach (TextSettings textVariant in textVariants)
            {
                if (textVariant.tittleLanguage == PlayerPrefs.GetString("lang"))
                {
                    string finalText = textVariant.text.Replace("@", System.Environment.NewLine);
                    text.text = finalText;
                    break;
                }
            }
        }
        else
        {
            replaceText = replaceText.Replace("@", System.Environment.NewLine);
            foreach (TextSettings textVariant in textVariants)
            {
                if (textVariant.tittleLanguage == PlayerPrefs.GetString("lang"))
                {
                    text.text = text.text.Replace(replaceText, textVariant.text).Replace("@", System.Environment.NewLine);
                    break;
                }
            }
        }
        
    }

    [System.Serializable]
    public struct TextSettings
    {
        public string tittleLanguage;
        public string text;
    }
}
