using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public bool statistic;

    [SerializeField] MovementController logo;
    [SerializeField] MovementController drone;
    [SerializeField] GameObject preMain;

    [SerializeField] MovementController dialogBoxBig;
    [SerializeField] MovementController dialogBoxSmall;
    [SerializeField] GameObject main;
    [SerializeField] GameObject statistics;
    [SerializeField] GameObject options;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject btnBack;
    [SerializeField] GameObject txtTitle;

    [SerializeField] TransitionController transition;

    [SerializeField] TextMainController textController;

    [SerializeField] CheckBoxController ckbSound;
    [SerializeField] CheckBoxController ckbTutorial;
    [SerializeField] BoxCollider2D[] buttonColliders;

    [SerializeField] SoundController soundController;


    private void Awake()
    {
        Strings.Translate(Options.Language);
        textController.SetText();
        MainStateManager.SetComponents(logo, drone, this, preMain);
        MainStateManager.SetComponents(dialogBoxBig, main, options, statistics, credits, btnBack, txtTitle);
        MainStateManager.SetComponents(ckbSound, ckbTutorial, dialogBoxSmall, buttonColliders);
    }
    // Start is called before the first frame update
    void Start()
    {
        transition.StartFade(false);
        MainStateManager.StartMainAnimation();
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
                else if (input.gameObject.tag == "Textbox") ChangeLanguage();
            }
        }
    }

    public void StartGame()
    {
        StartCoroutine(InitiateGame());
    }

    public void BackMain()
    {
        if (statistic) MainStateManager.ShowOptions();
        else MainStateManager.BackMain();
    }

    public void ClearStatistic()
    {
        MainStateManager.BackClearStatistic(true);
        textController.SetText();
    }

    IEnumerator InitiateGame()
    {
        transition.StartFade(true);
        soundController.StartFade(0, 1f);
        yield return new WaitForSeconds(1f);
        SceneManager.GoToScene("sceGame");
    }

    void ChangeLanguage()
    {
        if (Options.Language == "English") Options.Language = "Português";
        else if (Options.Language == "Português") Options.Language = "Español";
        else if (Options.Language == "Español") Options.Language = "English";

        Strings.Translate(Options.Language);
        textController.SetText();
    }
}

