using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLanguage : MonoBehaviour
{
    public static string CheckLang()
    {
        if (Application.systemLanguage == SystemLanguage.Portuguese)
        {
            Strings.ChangeToPortuguese();
            PlayerPrefs.options[1] = "português";
        }
        else if(Application.systemLanguage == SystemLanguage.Spanish)
        {
            Strings.ChangeToSpanish();
            PlayerPrefs.options[1] = "español";
        }
        else
        {
            Strings.ChangeToEnglish();
            PlayerPrefs.options[1] = "english";
        }
        return PlayerPrefs.options[1];
    }
}
