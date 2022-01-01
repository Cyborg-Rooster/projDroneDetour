using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class BlinkController : MonoBehaviour
{
    public float time;
    public bool isBlinking;
    bool isVisible;
    float timeRange;
    Text spr;

    void Start()
    {
        spr = GetComponent<Text>();
    }

    void Update()
    {
        if (isBlinking && Time.time > timeRange)
        {
            timeRange = Time.time + time;
            ChangeVisibility();
        }
    }

    void ChangeVisibility()
    {
        Color col = new Color();
        col = spr.color;

        if (isVisible)
        {
            col.a = 0;
            spr.color = col;
            isVisible = false;
        }
        else
        {
            col.a = 1;
            spr.color = col;
            isVisible = true;
        }
    }

    public void StartBlink()
    {
        isBlinking = true;
    }

    public void StopBlink()
    {
        isBlinking = false;

        if (!isVisible) ChangeVisibility();
    }

}