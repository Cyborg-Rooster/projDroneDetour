using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public bool day = true;
    public bool raining = false;

    [SerializeField] BackgroundController backgroundController;
    [SerializeField] BuildingController buildingController;
    [SerializeField] PauseController pauseController;
    [SerializeField] DroneController drone;

    [SerializeField] GameObject txtScoreOutput;
    [SerializeField] GameObject txtStart;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject txtBestScore;
    [SerializeField] GameObject txtTutorial;

    [SerializeField] MovementController uiScore;
    [SerializeField] MovementController btnPause;
    [SerializeField] MovementController uiDialogBoxBig;
    [SerializeField] MovementController btnStart;
    [SerializeField] GameObject txtScore;
    [SerializeField] TransitionController transition;

    [SerializeField] TextGameController textController;
    [SerializeField] SoundController musicController;
    [SerializeField] SoundController rainController;
    [SerializeField] SoundController soundController;

    [SerializeField] MovementController dialogBoxSmall;
    [SerializeField] GameObject showAd;
    [SerializeField] GameObject error;
    [SerializeField] BoxCollider2D[] buttonColliders;

    [SerializeField] RewardedAdController adController;
    [SerializeField] ButtonController btnAd;

    private void Awake()
    {
        GameStateManager.SetComponents(uiScore, btnPause, drone, txtStart);
        GameStateManager.SetComponents(pauseController, backgroundController, buildingController);
        GameStateManager.SetComponents(uiDialogBoxBig, btnStart, txtTutorial, gameOver, txtScore, txtBestScore);
        GameStateManager.SetComponents(dialogBoxSmall, showAd, error, buttonColliders);

        if (PhaseConfiguration.AdShown) LoadPhaseData();
        else adController.LoadAd();
    }
    // Start is called before the first frame update
    void Start()
    {
        textController.SetText();
        TextManager.SetText(txtScoreOutput, score);
        transition.StartFade(false);
        GameStateManager.PrepareStart(Options.Tutorial);
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
    }

    void CheckInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D input = InputObserver.ReturnObjectTouched();

            if (input != null)
            {
                if (input.gameObject.tag == "Button") ButtonObserver.OnClick(input.gameObject, this);
                else if (input.gameObject.tag == "Checkbox") CheckBoxObserver.OnClick(input.gameObject);
            }
            else
            {
                if (txtStart.activeSelf && !GameStateManager.paused) GameStateManager.Start();
            }
        }
    }

    void ObserveScoreToChangeBackgroundState()
    {
        if (score % 10 == 0) SetDay();

        if (score % 8 == 0)
        {
            if (UnityEngine.Random.Range(1, 4) == 3 || raining && UnityEngine.Random.Range(1, 4) != 3)
                SetRain();
        }
    }

    void SetDay()
    {
        day = !day;
        backgroundController.SetDay(day);
    }

    void SetRain()
    {
        raining = !raining;

        if (raining) rainController.PlayAsMusic();
        else rainController.StartFade(0, 1f);

        backgroundController.SetRain(raining, day);
    }

    void LoadPhaseData()
    {
        score = PhaseConfiguration.Score;
        if (!PhaseConfiguration.Day) SetDay();
        if (PhaseConfiguration.Raining) SetRain();
        PhaseConfiguration.AdShown = false;
        btnAd.SetButtonState(false);
    }

    public void SavePhaseData()
    {
        PhaseConfiguration.Score = score;
        PhaseConfiguration.Day = day;
        PhaseConfiguration.Raining = raining;
        PhaseConfiguration.AdShown = true;
        PhaseConfiguration.lastTime = 0;
    }

    IEnumerator AdFade(bool fadeIn)
    {
        transition.StartFade(fadeIn);
        yield return new WaitForSeconds(1f);

        if (fadeIn) 
        {
            adController.ShowAd(); 
            /*SavePhaseData();
            AdRestart();*/
        }
    }

    IEnumerator Restart()
    {
        transition.StartFade(true);
        yield return new WaitForSeconds(1f);

        PhaseConfiguration.lastTime = musicController.ReturnActualTime();
        SceneManager.GoToScene("sceGame");
    }

    IEnumerator Main()
    {
        transition.StartFade(true);
        musicController.StartFade(0, 1f);
        yield return new WaitForSeconds(1f);
        PhaseConfiguration.lastTime = 0;
        SceneManager.GoToScene("sceMain");
    }

    public void AddPoint()
    {
        score++;
        TextManager.SetText(txtScoreOutput, score);
        soundController.Play(soundController.Audio[2]);
        ObserveScoreToChangeBackgroundState();
    }

    public void CloseTutorial()
    {
        GameStateManager.PrepareStart(false);
    }

    public void TurnOffDrone()
    {
        GameStateManager.TurnOffDrone();
    }

    public void StopGame()
    {
        if (score > Statistics.BestScore) Statistics.BestScore = score;
        GameStateManager.StopGame(score);
    }

    public void RestartGame()
    {
        StartCoroutine("Restart");
    }

    public void GoToMain()
    {
        StartCoroutine("Main");
    }

    public void AdRestart()
    {
        SceneManager.GoToScene("sceGame");
    }

    public void AdCancel()
    {
        StartCoroutine(AdFade(false));
        PhaseConfiguration.AdShown = false;
    }

    public void TryShowAd()
    {
        if (adController.CheckIfAdIsLoaded()) StartCoroutine(AdFade(true));
        else GameStateManager.ShowErrorAd();
    }
}
