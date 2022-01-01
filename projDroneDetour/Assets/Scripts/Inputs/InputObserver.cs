using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputObserver
{
    public static Collider2D ReturnObjectTouched()
    {
        return Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition), LayerMask.GetMask("UI"));
    }
}
