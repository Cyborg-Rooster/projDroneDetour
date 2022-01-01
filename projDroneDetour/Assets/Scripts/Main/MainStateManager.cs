using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class MainStateManager
{
    static MovementController Logo;
    static MovementController Drone;
    static MainController Manager;
    static GameObject PreMain;

    static MovementController DialogBoxBig;
    static MovementController DialogBoxSmall;
    static GameObject Main;
    static GameObject OptionsMenu;
    static GameObject StatisticMenu;
    static GameObject Credits;
    static GameObject BtnBack;
    static GameObject TxtTitle;

    static CheckBoxController ckbSound;
    static CheckBoxController ckbTutorial;

    static BoxCollider2D[] buttonColliders;

    public static void SetComponents(MovementController _logo, MovementController _drone, MainController _main, GameObject _preMain)
    {
        Logo = _logo;
        Drone = _drone;
        Manager = _main;
        PreMain = _preMain;
    }
    public static void SetComponents(MovementController _dialogBoxBig, GameObject _main, GameObject _options, GameObject _statistic, GameObject _credits, GameObject _btnBack, GameObject _txtTitle)
    {
        DialogBoxBig = _dialogBoxBig;
        Main = _main;
        OptionsMenu = _options;
        StatisticMenu = _statistic;
        Credits = _credits;
        BtnBack = _btnBack;
        TxtTitle = _txtTitle;
    }
    public static void SetComponents(CheckBoxController _ckbSound, CheckBoxController _ckbTutorial, MovementController _dialogBoxSmall, BoxCollider2D[] _buttonColliders)
    {
        ckbSound = _ckbSound;
        ckbTutorial = _ckbTutorial;
        buttonColliders = _buttonColliders;
        DialogBoxSmall = _dialogBoxSmall;
    }

    public static void StartMainAnimation()
    {
        Manager.StartCoroutine(MainAnimation());
    }

    public static void BringMain()
    {
        TextManager.SetText(TxtTitle, "DRONE DETOUR");
        Main.SetActive(true);
        DialogBoxBig.SetIsMoving(true);
    }

    public static void ShowStatistic()
    {
        TextManager.SetText(TxtTitle, Strings.statistic);
        Manager.statistic = true;
        OptionsMenu.SetActive(false);
        StatisticMenu.SetActive(true);
    }

    public static void ShowOptions()
    {
        TextManager.SetText(TxtTitle, Strings.options);
        Manager.statistic = false;
        StatisticMenu.SetActive(false);
        Main.SetActive(false);

        OptionsMenu.SetActive(true);
        BtnBack.SetActive(true);

        if (!Options.Sound) ckbSound.Uncheck();
        if (!Options.Tutorial) ckbTutorial.Uncheck();
    }

    public static void BringClearStatistic()
    {
        SetColliderActive(buttonColliders, false);
        DialogBoxSmall.SetIsMoving(true);
    }

    public static void BackClearStatistic(bool clear)
    {
        if (clear) Statistics.ReturntoDefault();
        SetColliderActive(buttonColliders, true);
        DialogBoxSmall.ReturntoStartPosition(true);
    }

    public static void ShowCredits()
    {
        TextManager.SetText(TxtTitle, Strings.credits);
        Main.SetActive(false);
        Credits.SetActive(true);
        BtnBack.SetActive(true);
    }

    public static void BackMain()
    {
        TextManager.SetText(TxtTitle, "DRONE DETOUR");
        OptionsMenu.SetActive(false);
        Credits.SetActive(false);
        Main.SetActive(true);
        BtnBack.SetActive(false);
        DatabaseController.SaveData();
    }

    static IEnumerator MainAnimation()
    {
        Logo.SetIsMoving(true);
        yield return Logo.WaitForComeToTargetPosition();

        Drone.SetIsMoving(true);
        yield return Drone.WaitForComeToTargetPosition();

        yield return new WaitForSeconds(0.2f);

        PreMain.SetActive(true);
    }

    static void SetColliderActive(BoxCollider2D[] cols, bool active)
    {
        foreach (BoxCollider2D col in cols) col.enabled = active;
    }
}
