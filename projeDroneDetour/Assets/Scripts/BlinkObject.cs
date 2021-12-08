using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkObject : MonoBehaviour
{
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        InvokeRepeating("Blink", .1f, .5f);
    }

    void Blink()
    {
        Color col = text.color;

        if (text.color.a == 1)
        {
            col.a = 0;
            text.color = col;
        }
        else
        {
            col.a = 1;
            text.color = col;
        }

    }
}
