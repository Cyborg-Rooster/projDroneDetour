using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    [SerializeField]
    Sprite spriteChecked, spriteUnchecked;
    [SerializeField]
    AudioClip audioClip;

    AudioSource audioSource;
    SpriteRenderer sprRender;

    public bool boxChecked;
    public bool canTouch = true;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(tag == "CheckBox")
            sprRender = GetComponent<SpriteRenderer>();
        
        audioSource.clip = audioClip;
    }

    IEnumerator Click()
    {
        if (canTouch)
        {
            yield return new WaitForSeconds(.1f);
            canTouch = false;

            if (boxChecked)
            {
                sprRender.sprite = spriteUnchecked;
                boxChecked = false;
            }
            else
            {
                sprRender.sprite = spriteChecked;
                boxChecked = true;
            }

            yield return new WaitForSeconds(.1f);
            canTouch = true;
        }
    }

    public void PlayAudio()
    {
        audioSource.Play();
    }
}
