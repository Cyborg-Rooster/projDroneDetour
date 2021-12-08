using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //variaveis
    bool onStart;
    bool clickableTextAreClickable = true;
    int menuOption = 0;

    public string language;

    //objetos
    [SerializeField]
    GameObject[] objectCollection = new GameObject[16];

    GameObject logo, drone, dialogBox, fade, version, copyright, pressStart,
    btnStart, btnOption, btnCredits, mediumDialogBox, options, statistics, smallDialogBox,
    btnStatistics, btnDeleteStatistics, btnBack, credits;

    Text txtStart, txtOptions, txtCredits, txtTitle, txtSound, txtLanguage, txtLang,
    txtTutorial, txtStatistic, txtCleanStatistic, txtBestScore, txtNDeath, txtNDeathObstacles, 
    txtNDeathFall, txtNClicks, txtDeleteStatistcs, txtDev, txtMusics, txtDistribution;

    CheckBox chbSom, chbTutorial;

    AudioSource audioSource;
    SoundManager soundManager;

    //objetos para descobrir o que eu to clicando
    Vector3 pos;
    Collider2D hitCollider;

    void Start()
    {
        //le os arquivos salvos
        //PlayerPrefs.LoadData();

        audioSource = GetComponent<AudioSource>();

        logo = objectCollection[0];
        drone = objectCollection[1];
        dialogBox = objectCollection[2];
        txtStart = objectCollection[3].GetComponent<Text>();
        txtOptions = objectCollection[4].GetComponent<Text>();
        txtCredits = objectCollection[5].GetComponent<Text>();
        fade = objectCollection[6];
        version = objectCollection[7];
        copyright = objectCollection[8];
        pressStart = objectCollection[9];
        btnStart = objectCollection[10];
        btnOption = objectCollection[11];
        btnCredits = objectCollection[12];
        mediumDialogBox = objectCollection[13];

        chbSom = objectCollection[14].GetComponent<CheckBox>() ;
        chbTutorial = objectCollection[15].GetComponent<CheckBox>();

        txtTitle = objectCollection[16].GetComponent<Text>();
        txtSound = objectCollection[17].GetComponent<Text>();
        txtLanguage = objectCollection[18].GetComponent<Text>();
        txtTutorial = objectCollection[19].GetComponent<Text>();
        txtStatistic = objectCollection[20].GetComponent<Text>();
        txtCleanStatistic = objectCollection[21].GetComponent<Text>();
        txtLang = objectCollection[22].GetComponent<Text>();
        options = objectCollection[23];
        statistics = objectCollection[24];

        txtBestScore = objectCollection[25].GetComponent<Text>();
        txtNDeath = objectCollection[26].GetComponent<Text>();
        txtNDeathFall = objectCollection[27].GetComponent<Text>();
        txtNDeathObstacles = objectCollection[28].GetComponent<Text>();
        txtNClicks = objectCollection[29].GetComponent<Text>();
        txtDeleteStatistcs = objectCollection[30].GetComponent<Text>();

        smallDialogBox = objectCollection[31];
        btnStatistics = objectCollection[32];
        btnDeleteStatistics = objectCollection[33];
        btnBack = objectCollection[34];
        credits = objectCollection[35];

        txtDev = objectCollection[36].GetComponent<Text>();
        txtMusics = objectCollection[37].GetComponent<Text>();
        txtDistribution = objectCollection[38].GetComponent<Text>();
        

        soundManager = GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<SoundManager>();

        //vai checar as opções e deixar tudo conforme as mesmas

        //som
        chbSom.boxChecked = PlayerPrefs.options[0] == "1" ? true : false;
        chbSom.StartCoroutine("Click");

        /*if(PlayerPrefs.options[0] == "0")
            AudioListener.volume = 0f;

        else if(PlayerPrefs.options[0] == "1")
            AudioListener.volume = 1f;*/

        //tutorial
        chbTutorial.boxChecked = PlayerPrefs.options[2] == "1" ? true : false;
        chbTutorial.StartCoroutine("Click");

        //linguagem, se nao for a primeira vez vai pegar das opções
        if (PlayerPrefs.options[1] == "xxx")
            language = CheckLanguage.CheckLang();
        else
        {
            if (PlayerPrefs.options[1] == "pt-br")
                language = "português";
            else if (PlayerPrefs.options[1] == "en-us")
                language = "english";
            else
                language = PlayerPrefs.options[1];
        }

        //depois de carregar a lingua vai mudar o texto da variavel
        txtLang.text = language;

        //vai setar o valor do texto das variaveis
        if (language == "en-us" || language == "english")
        {
            Strings.ChangeToEnglish();
        }
        else if (language == "pt-br" || language == "português")
        {
            Strings.ChangeToPortuguese();
        }
        else if(language == "español")
        {
            Strings.ChangeToSpanish();
        }
        SetStrings();

        //vai pegar a versão do jogo
        version.GetComponent<Text>().text = "v"+Application.version;

        //começa o jogo
        StartCoroutine("StartGame");
    }
    
    void Update()
    {
        //quando for clicado
        if (Input.GetMouseButtonDown(0))
        {
            //vai checar o que está sendo tocado
            pos = Input.mousePosition;
            hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pos), LayerMask.GetMask("UI"));

            //se nao for em nada e estiver no começo vai trazer o menu
            if (hitCollider == null && onStart)
            {
                dialogBox.GetComponent<MoveObject>().isMoving = true;
                audioSource.Play();
                pressStart.SetActive(false);
                onStart = false;
            }


            //se for um botao
            else if (hitCollider != null && hitCollider.CompareTag("Button"))
                StartCoroutine("ButtonClick");

            //se for um check box
            else if (hitCollider != null && hitCollider.CompareTag("CheckBox"))
                StartCoroutine("CheckBoxClick");

            //se for um texto clicavel
            else if (hitCollider != null && hitCollider.CompareTag("ClickableText"))
                ClickableTextClick();
        }
    }

    IEnumerator StartGame()
    {
        //quando o fade é ativado ele começa o fadeOut
        fade.SetActive(true);
        //caso não esteja tocando, vai colocar o volume padrão e vai tocar
        if (!soundManager.isPlaying)
        {
            soundManager.gameObject.GetComponent<AudioSource>().volume = 0.8f;
            soundManager.StartMusic();
        }

        yield return new WaitForSeconds(.4f);

        //traz o logo
        logo.GetComponent<MoveObject>().isMoving = true;
        drone.GetComponent<MoveObject>().isMoving = true;

        yield return WaitForStopMoving(logo.GetComponent<MoveObject>().posEnd);

        //mostra outros compenentes de UI
        version.SetActive(true);
        copyright.SetActive(true);
        pressStart.SetActive(true);

        //começa o menu
        onStart = true;
    }

    IEnumerator WaitForStopMoving(Vector2 finalPos)
    {
        bool done = false;
        while(!done)
        {
            if (logo.GetComponent<RectTransform>().position == (Vector3)finalPos)
                done = true;
            yield return null;
        }
        
    }

    IEnumerator ButtonClick()
    {
        hitCollider.gameObject.GetComponent<Button>().StartCoroutine("Click");

        if(hitCollider.gameObject.GetComponent<Button>().canTouch)
        {
            yield return new WaitForSeconds(.2f);

            if(hitCollider.name == "Comecar")
            {
                fade.SetActive(true);

                fade.GetComponent<Fade>().StartCoroutine("FadeIn");
                soundManager.isPlaying = false;
                soundManager.StartCoroutine(soundManager.StartFade(0f));

                yield return new WaitForSeconds(.5f);
                SceneManager.LoadScene("sceGame");
            }

            else if(hitCollider.name == "Opcoes")
            {
                btnStart.GetComponent<Button>().canTouch = false;
                btnOption.GetComponent<Button>().canTouch = false;
                btnCredits.GetComponent<Button>().canTouch = false;

                mediumDialogBox.GetComponent<MoveObject>().isMoving = true;
            }

            else if(hitCollider.name == "btnVoltar")
            {
                if (menuOption == 0)
                {
                    //menu padrao
                    PlayerPrefs.SaveData();
                    mediumDialogBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(mediumDialogBox.transform.position.x, -550f);

                    btnStart.GetComponent<Button>().canTouch = true;
                    btnOption.GetComponent<Button>().canTouch = true;
                    btnCredits.GetComponent<Button>().canTouch = true;

                    hitCollider.GetComponent<Button>().canTouch = true;
                }
                else if(menuOption == 1)
                {
                    //estatisticas
                    txtTitle.text = Strings.options;
                    statistics.SetActive(false);
                    options.SetActive(true);
                    menuOption = 0;
                }
                else if(menuOption == 2)
                {
                    //creditos
                    PlayerPrefs.SaveData();
                    mediumDialogBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(mediumDialogBox.transform.position.x, -550f);

                    btnStart.GetComponent<Button>().canTouch = true;
                    btnOption.GetComponent<Button>().canTouch = true;
                    btnCredits.GetComponent<Button>().canTouch = true;

                    hitCollider.GetComponent<Button>().canTouch = true;

                    txtTitle.text = Strings.options;
                    credits.SetActive(false);
                    options.SetActive(true);
                    menuOption = 0;
                }
                hitCollider.gameObject.GetComponent<Button>().canTouch = true;
            }

            else if(hitCollider.name == "btnEstatisticas")
            {
                options.SetActive(false);
                txtTitle.text = Strings.statistic;
                statistics.SetActive(true);
                menuOption = 1;

                hitCollider.gameObject.GetComponent<Button>().canTouch = true;
            }

            else if(hitCollider.name == "btnLimparEstatisticas")
            {
                chbSom.canTouch = false;
                clickableTextAreClickable = true;
                chbTutorial.canTouch = false;

                btnStatistics.GetComponent<Button>().canTouch = false;
                btnDeleteStatistics.GetComponent<Button>().canTouch = false;
                btnBack.GetComponent<Button>().canTouch = false;

                smallDialogBox.GetComponent<MoveObject>().isMoving = true;
            }

            else if(hitCollider.name == "btnSim")
            {
                PlayerPrefs.statistics[0] = "0";
                PlayerPrefs.statistics[1] = "0";
                PlayerPrefs.statistics[2] = "0";
                PlayerPrefs.statistics[3] = "0";
                PlayerPrefs.statistics[4] = "0";

                PlayerPrefs.SaveData();
                SetStrings();

                smallDialogBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(smallDialogBox.transform.position.x, -550f);

                hitCollider.gameObject.GetComponent<Button>().canTouch = true;

                chbSom.canTouch = true;
                clickableTextAreClickable = true;
                chbTutorial.canTouch = true;

                btnStatistics.GetComponent<Button>().canTouch = true;
                btnDeleteStatistics.GetComponent<Button>().canTouch = true;
                btnBack.GetComponent<Button>().canTouch = true;
            }

            else if(hitCollider.name == "btnNao")
            {
                smallDialogBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(smallDialogBox.transform.position.x, -550f);

                hitCollider.gameObject.GetComponent<Button>().canTouch = true;

                chbSom.canTouch = true;
                clickableTextAreClickable = true;
                chbTutorial.canTouch = true;

                btnStatistics.GetComponent<Button>().canTouch = true;
                btnDeleteStatistics.GetComponent<Button>().canTouch = true;
                btnBack.GetComponent<Button>().canTouch = true;
            }

            else if(hitCollider.name == "creditos")
            {
                options.SetActive(false);
                txtTitle.text = Strings.credits;
                credits.SetActive(true);
                menuOption = 2;

                btnStart.GetComponent<Button>().canTouch = false;
                btnOption.GetComponent<Button>().canTouch = false;
                btnCredits.GetComponent<Button>().canTouch = false;

                mediumDialogBox.GetComponent<MoveObject>().isMoving = true;
            }
        }

        hitCollider.gameObject.GetComponent<Button>().BackState();
    }

    IEnumerator CheckBoxClick()
    {
        CheckBox checkBox = hitCollider.gameObject.GetComponent<CheckBox>();
        checkBox.StartCoroutine("Click");
        checkBox.PlayAudio();
        yield return new WaitForSeconds(.2f);

        if(hitCollider.name == "chbSom")
        {
            if(checkBox.boxChecked)
            {
                PlayerPrefs.options[0] = "0";
                AudioListener.volume = 0f;
            }
            else
            {
                PlayerPrefs.options[0] = "1";
                AudioListener.volume = 1f;
            }
        }
        else if(hitCollider.name == "chbTutorial")
        {
            if(checkBox.boxChecked)
            {
                PlayerPrefs.options[2] = "0";
            }
            else
            {
                PlayerPrefs.options[2] = "1";
            }
        }
    }

    void ClickableTextClick()
    {
        if (clickableTextAreClickable)
        {
            CheckBox clickableText = hitCollider.gameObject.GetComponent<CheckBox>();
            clickableText.PlayAudio();
            if (language == "português")
            {
                Strings.ChangeToEnglish();
                PlayerPrefs.options[1] = "english";
            }
            else if (language == "english")
            {
                Strings.ChangeToSpanish();
                PlayerPrefs.options[1] = "español";
            }
            else if (language == "español")
            {
                Strings.ChangeToPortuguese();
                PlayerPrefs.options[1] = "português";
            }
            language = PlayerPrefs.options[1];
            SetStrings();
            txtLang.text = language;
        }
    }

    void SetStrings()
    {
        txtStart.text = Strings.start;
        txtOptions.text = Strings.options;
        txtCredits.text = Strings.credits;
        pressStart.GetComponent<Text>().text = Strings.pressStart;

        txtTitle.text = Strings.options;
        txtSound.text = Strings.sound;
        txtLanguage.text = Strings.language;
        txtTutorial.text = Strings.txtTutorial;
        txtStatistic.text = Strings.statistic;
        txtCleanStatistic.text = Strings.cleanStatistic;

        txtBestScore.text = Strings.bestScore + PlayerPrefs.statistics[0]; 
        txtNDeath.text = Strings.nDeaths + PlayerPrefs.statistics[1];
        txtNDeathObstacles.text = Strings.nDeathsOstacles + PlayerPrefs.statistics[3];
        txtNDeathFall.text = Strings.nDeathsFall + PlayerPrefs.statistics[2];
        txtNClicks.text = Strings.nClicks + PlayerPrefs.statistics[4];

        txtDeleteStatistcs.text = Strings.deleteStatistics;

        txtDev.text = Strings.dev;
        txtMusics.text = Strings.music;
        txtDistribution.text = Strings.distribution;
    }
}
