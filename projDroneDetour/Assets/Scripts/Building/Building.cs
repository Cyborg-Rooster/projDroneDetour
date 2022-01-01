using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
class Building
{
    private static float LastY = 0f;

    public static Sprite[] Sprites;
    public static Sprite Sprite
    {
        get => Sprites[UnityEngine.Random.Range(0, 6)];
    }

    public static float Y
    {
        get => ReturnY();
    }

    private static float ReturnY()
    {
        float y = LastY;
        while(y == LastY) y = UnityEngine.Random.Range(-0.41f, 0.41f);
        LastY = y;
        return y;
    }
    

}
