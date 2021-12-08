using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public bool rain = false, oneTime = true, stopRain = false, canThunder, soundRain;
    float thunderRate = 3.5f;
    float nextThunder;

    [SerializeField]
    GameObject nuvem, chuva;
    Animator aniNuv, aniChu;

    [SerializeField]
    GameObject trovaoCeu, trovaoCidade1, trovaoCidade2, trovaoPoste1, trovaoPoste2;

    GameManager game;

    AudioSource audTro, audChu;

    // Start is called before the first frame update
    void Start()
    {
        game = GetComponent<GameManager>();
        audTro = nuvem.GetComponent<AudioSource>();
        audChu = chuva.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(game.areRaining && Time.time > nextThunder && canThunder && !game.cannotBuildingScroll)
        {
            nextThunder = Time.time + thunderRate;
            StartCoroutine("Thunder");
            audTro.Play();
        }
    }

    void FixedUpdate()
    {
        if (soundRain && audChu.time >= 5f)
        {
            audChu.time = 2f;
        }
    }

    public IEnumerator StartRain(int isNight)
    {
        nuvem.SetActive(true);
        
        aniNuv = nuvem.GetComponent<Animator>();
        aniChu = chuva.GetComponent<Animator>();

        if (isNight == 1)
            aniNuv.Play("nuvemNoiteAbrindo");
        yield return new WaitForSeconds(1.7f);

        canThunder = true;
        chuva.SetActive(true);

        soundRain = true;
        
        audChu.time = 0f;
        audChu.Play();

        Test(false);
    }

    public IEnumerator StopRain(int isNight)
    {
        canThunder = false;
        soundRain = false;

        audChu.time = 6f;
        aniChu.SetTrigger("acabou");

        yield return new WaitForSeconds(2f);
        chuva.SetActive(false);

        Test(true);
        if (isNight == 0)
            aniNuv.SetBool("dia", true);
        else
            aniNuv.SetBool("dia", false);
        aniNuv.SetTrigger("pararDeChover");
        yield return new WaitForSeconds(1.5f);
        nuvem.SetActive(false);
    }

    IEnumerator Thunder()
    {
        trovaoCeu.SetActive(true);
        trovaoCidade1.SetActive(true);
        trovaoCidade2.SetActive(true);
        trovaoPoste1.SetActive(true);
        trovaoPoste2.SetActive(true);


        yield return new WaitForSeconds(.5f);

        trovaoCeu.SetActive(false);
        trovaoCidade1.SetActive(false);
        trovaoCidade2.SetActive(false);
        trovaoPoste1.SetActive(false);
        trovaoPoste2.SetActive(false);
    }

    void Test(bool cond)
    {
        aniNuv.enabled = cond;
    }

    public void PauseGame()
    {
        aniChu.speed = 0f;
        audChu.volume = 0.0f;
        canThunder = false;
    }

    public void BackToGame()
    {
        aniChu.speed = 1f;
        audChu.volume = 1.0f;
        canThunder = true;
    }
}
