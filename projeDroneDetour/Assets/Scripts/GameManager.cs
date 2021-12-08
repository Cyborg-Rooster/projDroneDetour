using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float score;
    public float bestScore;
    public float buildVelocity;

    public bool start = false;
    public bool isAlive = true;
    public bool enabledTutorial = true;
    public bool oneTime = true;
    public bool droneIsDestroyed;
    public bool canChange;
    public bool canRain;
    public int isNight = 0;
    public int usedAd = 0;

    bool oneTimeRain;
    bool preStart;
    float rate = 0;
    public bool isPause;
    public bool cannotBuildingScroll;
    public bool areRaining = false;
    
    [SerializeField]
    GameObject dialogBox, smallDialogBox,tutorial,gameOver,pressStart,placar,
    btnPlay,btnPause,btnReturn,btnRestart,btnAd,btnMenu,btnYes,btnNot, btnSad,
    telaPreta, objFade, objScore, objBestScore, objRestart, objMenu, objAd, objPause, objWantAd,
    objDontShow, parCeu, parNuvem1, parNuvem2, parNuvemChuva, parCidade1, parCidade2, 
    parPoste1, parPoste2;

    Fade fade;
    
    Text txtScore,txtBestScore,txtRestart,txtMenu,txtAd,txtPause,txtWantAd,txtDontShow;

    public Vector3 pos;
    public Collider2D hitCollider;

    AdsService ad;

    SoundManager soundManager;

    Rain rain;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.LoadTempData();

        //pega o anuncio
        ad = GetComponent<AdsService>();
        
        txtScore = objScore.GetComponent<Text>();
        txtBestScore = objBestScore.GetComponent<Text>();
        txtRestart = objRestart.GetComponent<Text>();
        txtMenu = objMenu.GetComponent<Text>();
        txtAd = objAd.GetComponent<Text>();
        txtPause = objPause.GetComponent<Text>();
        txtWantAd = objWantAd.GetComponent<Text>();

        fade = objFade.GetComponent<Fade>();
        txtDontShow = objDontShow.GetComponent<Text>();
        soundManager = GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<SoundManager>();

        //escreve o texto
        txtBestScore.text = Strings.bestScore;
        txtRestart.text = Strings.restart;
        txtMenu.text = Strings.menu;
        txtAd.text = Strings.ad;
        txtPause.text = Strings.pause;
        txtWantAd.text = Strings.wantAd;

        btnPause.GetComponent<Button>().canTouch = false;

        rain = GetComponent<Rain>();

        if (PlayerPrefs.options[2] == "0")
            enabledTutorial = false;
        else
            enabledTutorial = true;

        //analisa os dados temporarios, se tiver sido usado um anuncio, ele prepara
        //o ambiente para tal
        if (PlayerPrefs.tempData[2] == "1")
        {
            score = float.Parse(PlayerPrefs.tempData[1]);
            usedAd = Convert.ToInt32(PlayerPrefs.tempData[2]);
            isNight = Convert.ToInt32(PlayerPrefs.tempData[3]);

            Changetime();

            btnAd.GetComponent<Button>().canTouch = false;
            btnAd.GetComponent<SpriteRenderer>().sprite = btnAd.GetComponent<Button>().spr;
            PlayerPrefs.ClearTempData();
        }

        if (enabledTutorial)
        {
            tutorial.transform.GetChild(0).gameObject.GetComponent<Text>().text = Strings.tutorial;
            txtDontShow.text = Strings.dontDisplay;
            tutorial.GetComponent<MoveObject>().isMoving = true;
            dialogBox.GetComponent<MoveObject>().isMoving = true;
            btnPlay.GetComponent<MoveObject>().isMoving = true;
        }

        else
            StartGame();

        //carrega o bestscore
        bestScore = Convert.ToInt32(PlayerPrefs.statistics[0]);

        //ativa o fade para começar o jogo
        fade.gameObject.SetActive(true);

        if (!soundManager.isPlaying)
        {
            soundManager.gameObject.GetComponent<AudioSource>().volume = 0.5f;
            soundManager.StartMusic();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //vai sempre carregar o score
        txtScore.text = score.ToString();

        //quando qualquer toque acontecer na tela, vai ser comparado qual é o objeto 
        //tocado
        if (Input.GetMouseButtonDown(0))
        {
            pos = Input.mousePosition;
            hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pos), LayerMask.GetMask("UI"));

            //se for um botão, vai ser invocado a rotina de botão
            if (hitCollider != null && hitCollider.CompareTag("Button"))
                StartCoroutine("ButtonClick");

            //se for um checkbox, vai ser invocado a rotina de checkbox
            else if (hitCollider != null && hitCollider.CompareTag("CheckBox"))
                StartCoroutine("CheckBoxClick");

            //se o drone nao tiver sido destruido
            else if (!droneIsDestroyed)
                StartCoroutine("OnDroneArentDestroyed");
        }

        //usando uma booleana chamada oneTime, posso usar uma função apenas uma vez
        //quando o drone for destruido
        if (droneIsDestroyed && oneTime)
        {
            //se o score for maior que o bestScore, ele vai ser atualizado
            if (score > bestScore)
            {
                bestScore = score;
                PlayerPrefs.statistics[0] = bestScore.ToString();
            }

            //o jogo será salvo
            PlayerPrefs.SaveData();

            //atualizará o texto do txtBestScore
            txtBestScore.text = Strings.bestScore + bestScore;

            //irá mover o placar para XY coordenada
            placar.GetComponent<MoveObject>().posEnd.x = placar.transform.position.x;
            placar.GetComponent<MoveObject>().posEnd.y = 0.4118f;
            placar.GetComponent<MoveObject>().velocity = .75f;
            placar.GetComponent<MoveObject>().isMoving = true;

            //irá mover o btnPause para XY coordenada
            btnPause.GetComponent<MoveObject>().posEnd.x = 0.876f;
            btnPause.GetComponent<MoveObject>().posEnd.y = btnPause.transform.position.y;
            btnPause.GetComponent<Button>().canTouch = false;
            btnPause.GetComponent<MoveObject>().isMoving = true;

            dialogBox.GetComponent<MoveObject>().isMoving = true;
            gameOver.GetComponent<MoveObject>().isMoving = true;

            oneTime = false;
        }

        if(score % 10 == 0 && score != 0 && canChange)
        {
            isNight = isNight == 0 ? 1 : 0;
            canChange = false;
            Changetime();
        }

        if (canRain && score > 3)
        {
            canRain = false;

            var random = new System.Random();
            int n = random.Next(1, 7);

            if (n == 1 && score > rate || n == 2 && score > rate)
            {
                Rain();
            }
        }
    }

    IEnumerator ButtonClick()
    {
        //se esse botão puder ser tocado
        if (hitCollider.gameObject.GetComponent<Button>().canTouch)
        {
            //quando qualquer botão for clicado, vai fazer a rotina visual do mesmo
            hitCollider.gameObject.GetComponent<Button>().StartCoroutine("Click");

            yield return new WaitForSeconds(.2f);

            if (hitCollider.name == "btnPlay")
            {
                dialogBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(dialogBox.transform.position.x, -550f);
                tutorial.GetComponent<RectTransform>().anchoredPosition = new Vector2(tutorial.transform.position.x, -550f);

                btnPlay.SetActive(false);

                StartGame();
            }

            else if (hitCollider.name == "btnPause")
            {
                start = false;
                isAlive = false;
                cannotBuildingScroll = true;

                if (areRaining)
                    rain.PauseGame();

                telaPreta.SetActive(true);
                btnReturn.SetActive(true);
                btnPause.SetActive(false);

                btnRestart.GetComponent<Button>().canTouch = false;
                btnMenu.GetComponent<Button>().canTouch = false;
                btnAd.GetComponent<Button>().canTouch = false;
            }

            else if (hitCollider.name == "btnReturn")
            {
                telaPreta.SetActive(false);
                btnReturn.SetActive(false);
                btnPause.SetActive(true);

                isPause = true;

                if (!droneIsDestroyed)
                    pressStart.SetActive(true);

                btnPause.GetComponent<Button>().canTouch = true;
                btnReturn.GetComponent<Button>().canTouch = true;

                btnRestart.GetComponent<Button>().canTouch = true;
                btnMenu.GetComponent<Button>().canTouch = true;
                btnAd.GetComponent<Button>().canTouch = true;
            }

            else if (hitCollider.name == "Recomecar")
            {
                fade.gameObject.SetActive(true);

                fade.StartCoroutine("FadeIn");
                
                yield return new WaitForSeconds(.5f);
                SceneManager.LoadScene("sceGame");
            }

            else if (hitCollider.name == "menu")
            {
                fade.gameObject.SetActive(true);

                fade.StartCoroutine("FadeIn");

                soundManager.isPlaying = false;
                soundManager.StartCoroutine(soundManager.StartFade(0f));

                yield return new WaitForSeconds(.5f);
                SceneManager.LoadScene("sceMenu");
            }

            else if (hitCollider.name == "anuncio")
            {
                btnRestart.GetComponent<Button>().canTouch = false;
                btnMenu.GetComponent<Button>().canTouch = false;
                btnAd.GetComponent<Button>().canTouch = false;

                smallDialogBox.GetComponent<MoveObject>().isMoving = true;
            }

            else if (hitCollider.name == "btnSim")
            {
                ad.StartCoroutine("ShowRewardedVideo");

                hitCollider.gameObject.GetComponent<Button>().canTouch = true;
            }

            else if (hitCollider.name == "btnNao")
            {
                smallDialogBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(smallDialogBox.transform.position.x, -550f);

                hitCollider.gameObject.GetComponent<Button>().canTouch = true;

                btnRestart.GetComponent<Button>().canTouch = true;
                btnMenu.GetComponent<Button>().canTouch = true;
                btnAd.GetComponent<Button>().canTouch = true;
            }

            else if(hitCollider.name == "btnSad")
            {
                smallDialogBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(smallDialogBox.transform.position.x, -550f);

                hitCollider.gameObject.GetComponent<Button>().canTouch = true;

                btnRestart.GetComponent<Button>().canTouch = true;
                btnMenu.GetComponent<Button>().canTouch = true;
                btnAd.GetComponent<Button>().canTouch = true;
                btnYes.GetComponent<Button>().canTouch = true;

                btnYes.SetActive(true);
                btnNot.SetActive(true);
                btnSad.SetActive(false);

                txtWantAd.text = Strings.wantAd;
            }

            //apos checar qual botão é, vai retornar ao seu estado
            hitCollider.gameObject.GetComponent<Button>().BackState();
        }
    }

    IEnumerator CheckBoxClick()
    {
        CheckBox checkBox = hitCollider.gameObject.GetComponent<CheckBox>();
        checkBox.StartCoroutine("Click");
        checkBox.PlayAudio();
        yield return new WaitForSeconds(.2f);

        if (enabledTutorial)
        {
            PlayerPrefs.options[2] = "0";
            enabledTutorial = false;
        }
        else
        {
            PlayerPrefs.options[2] = "1";
            enabledTutorial = true;
        }
    }

    IEnumerator OnDroneArentDestroyed()
    {
        yield return new WaitForSeconds(.3f);

        if (pressStart.activeSelf)
        {
            start = true;
            //e for a fase de press start, vai começar o jogo
            if (preStart)
            {
                preStart = false;

                btnPause.GetComponent<Button>().canTouch = true;
            }
            //se for numa pausa, vai despausar o jogo
            else if (isPause)
            {
                if (areRaining)
                    rain.BackToGame();

                isAlive = true;

                isPause = false;

                cannotBuildingScroll = false;
            }
            pressStart.SetActive(false);
        }
    }

    //faz aparecer o texto de offline
    public void ReturnOffline()
    {
        btnYes.SetActive(false);
        btnNot.SetActive(false);
        btnSad.SetActive(true);

        txtWantAd.text = Strings.offline;
    }

    void StartGame()
    {
        pressStart.GetComponent<Text>().text = Strings.pressToStart;

        pressStart.SetActive(true);

        placar.GetComponent<MoveObject>().isMoving = true;
        btnPause.GetComponent<MoveObject>().isMovingRect = true;

        preStart = true;
    }

    public void AdSuccesful()
    {
        PlayerPrefs.tempData[1] = score.ToString();
        PlayerPrefs.tempData[2] = "1";
        PlayerPrefs.tempData[3] = isNight.ToString();

        PlayerPrefs.SaveTempData();

        SceneManager.LoadScene("sceGame");
    }

    public void AdCancelled()
    {
        smallDialogBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(smallDialogBox.transform.position.x, -550f);
        
        fade.StartCoroutine("FadeOut");

        soundManager.isPlaying = true;
        soundManager.StartCoroutine(soundManager.StartFade(0.5f));

        btnYes.GetComponent<Button>().canTouch = true;
        btnRestart.GetComponent<Button>().canTouch = true;
        btnMenu.GetComponent<Button>().canTouch = true;
        btnAd.GetComponent<Button>().canTouch = true;

        PlayerPrefs.ClearTempData();
    }

    void Changetime()
    {
        parCeu.GetComponent<DayNight>().Change(isNight);
        parNuvem1.GetComponent<DayNight>().Change(isNight);
        parNuvem2.GetComponent<DayNight>().Change(isNight);
        parNuvemChuva.GetComponent<DayNight>().Change(isNight);
        parCidade1.GetComponent<DayNight>().Change(isNight);
        parCidade2.GetComponent<DayNight>().Change(isNight);
        parPoste1.GetComponent<DayNight>().Change(isNight);
        parPoste2.GetComponent<DayNight>().Change(isNight);
    }

    void Rain()
    {
        if (areRaining)
        {
            areRaining = false;
            rain.StartCoroutine(rain.StopRain(isNight));
        }
        else
        {
            areRaining = true;
            rain.StartCoroutine(rain.StartRain(isNight));
        }
        rate = score + 3;
    }
}
