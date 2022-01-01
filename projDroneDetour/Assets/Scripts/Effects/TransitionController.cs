using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    Animator anim;
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void StartFade(bool fadeIn)
    {
        SetVisibility(true);
        if (fadeIn) StartCoroutine(FadeIn());
        else StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        anim.Play("fadeOut");
        yield return new WaitForSeconds(1);
        SetVisibility(false);
    }

    IEnumerator FadeIn()
    {
        anim.Play("fadeIn");
        yield return new WaitForSeconds(1);
    }

    void SetVisibility(bool visibility)
    {
        anim.enabled = visibility;
        rend.enabled = visibility;
    }
}
