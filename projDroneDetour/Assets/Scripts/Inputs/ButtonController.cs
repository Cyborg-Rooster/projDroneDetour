using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
class ButtonController : MonoBehaviour
{
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite clickedSprite;
    [SerializeField] SoundController audioSource;

    public bool clicked;
    public bool multipleClicks;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnClick()
    {
        if (clicked) audioSource.Play(audioSource.Audio[audioSource.Audio.Length - 1]);
        else
        {
            audioSource.Play(audioSource.Audio[0]);
            StartCoroutine(Click());
        }
    }

    public void SetButtonState(bool state)
    {
        clicked = !state;

        if(clicked)
        {
            GetComponent<SpriteRenderer>().sprite = clickedSprite;
        }
    }

    IEnumerator Click()
    {
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = clickedSprite;

        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = defaultSprite;
    }
}
