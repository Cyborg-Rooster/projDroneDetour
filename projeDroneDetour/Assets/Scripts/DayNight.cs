using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public string dayAnimation;
    public string nightAnimation;

    //public bool trocar;
    //public static int isNight;

    Animator anim;
    
    SpriteRenderer sprRender;
    public Sprite nightSprite;
    public Sprite daySprite;

    GameManager game;

    
    void SetComponents()
    {
        game = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (tag == "Animated")
            anim = GetComponent<Animator>();

        else if (tag == "SpriteChange")
            sprRender = GetComponent<SpriteRenderer>();
    }

    public void Change(int isNight)
    {
        SetComponents();
        if(isNight == 0)
        {
            ChangeToDay();
        }
        else
        {
            ChangeToNight();
        }
    }

    void ChangeToNight()
    {
        if (tag == "Animated")
            anim.SetTrigger("anoitecer");

        else if (tag == "SpriteChange")
            sprRender.sprite = nightSprite;
    }

    void ChangeToDay()
    {
        if (tag == "Animated")
            anim.SetTrigger("amanhecer");

        else if (tag == "SpriteChange")
            sprRender.sprite = daySprite;
    }
}
