using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
class TextManager
{
    public static void SetText(GameObject output, string text)
    {
        SetText(output.GetComponent<Text>(), text);
    }

    public static void SetText(GameObject output, int text)
    {
        SetText(output.GetComponent<Text>(), text.ToString());
    }

    static void SetText(Text output, string text)
    {
        output.text = text.ToString();
    }
}
