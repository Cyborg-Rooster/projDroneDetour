using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
class CheckBoxController : MonoBehaviour
{
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite checkedSprite;
    [SerializeField] SoundController audioSource;

    public bool Checked;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnClick()
    {
        StartCoroutine(Click());
    }

    public void Uncheck()
    {
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
        Checked = !Checked;
    }


    IEnumerator Click()
    {
        yield return new WaitForSeconds(0.1f);

        audioSource.Play(audioSource.Audio[1]);

        if (Checked) spriteRenderer.sprite = defaultSprite;
        else spriteRenderer.sprite = checkedSprite;
        Checked = !Checked;
    }
}
