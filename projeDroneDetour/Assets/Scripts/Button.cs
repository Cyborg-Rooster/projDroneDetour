using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool canTouch = true;

    public Sprite spr;
    public Sprite firsSpr;

    [SerializeField]
    AudioClip[] audioCollection;
    AudioSource audioSource;
    SpriteRenderer sprRender;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sprRender = GetComponent<SpriteRenderer>();
        firsSpr = sprRender.sprite;
    }

    public IEnumerator Click()
    {
        yield return new WaitForSeconds(.1f);
        canTouch = false;

        audioSource.clip = audioCollection[0];
        audioSource.Play();

        sprRender.sprite = spr;

        yield return new WaitForSeconds(.1f);
    }

    public IEnumerator Unclickable()
    { 
        yield return new WaitForSeconds(.1f);

        audioSource.clip = audioCollection[1];
        audioSource.Play();

        yield return new WaitForSeconds(.1f);
    }

    public void BackState()
    {
        sprRender.sprite = firsSpr;
    }
}
