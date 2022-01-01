using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Configuration
{
    public static void SetLanguage()
    {
        if (Application.systemLanguage == SystemLanguage.Portuguese) Options.Language = "Português";
        else if (Application.systemLanguage == SystemLanguage.Spanish) Options.Language = "Español";
        else Options.Language = "English";
    }

    public static void SetSounds()
    {
        if (!Options.Sound) AudioListener.volume = 0;
        else AudioListener.volume = 1;
    }
}
