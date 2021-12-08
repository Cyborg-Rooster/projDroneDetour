using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        StartCoroutine("FadeOut");
    }

    public IEnumerator FadeOut()
    {

        anim.Play("fadeOut");

        yield return new WaitForSeconds(0.375f);

        gameObject.SetActive(false);
    }

    public IEnumerator FadeIn()
    {
        anim.Play("fadeIn");

        yield return new WaitForSeconds(0.375f);
    }
}
