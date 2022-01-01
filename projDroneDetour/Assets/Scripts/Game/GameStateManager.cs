using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class GameStateManager
{
    static PauseController pauseController;
    static BackgroundController backgroundController;
    static BuildingController buildingController;

    static DroneController drone;
    static GameObject txtStart;
    static MovementController score;
    static MovementController btnPause;

    static MovementController dialogBox;
    static MovementController btnStart;
    static GameObject txtBestScore;
    static GameObject txtTutorial;
    static GameObject gameOver;
    static GameObject txtScore;

    static MovementController dialogBoxSmall;
    static GameObject showAd;
    static GameObject error;
    static BoxCollider2D[] buttonColliders;

    public static bool paused = false;

    public static void SetComponents(MovementController _score, MovementController _btnPause, DroneController _drone, GameObject _txtStart)
    {
        drone = _drone;
        txtStart = _txtStart;
        score = _score;
        btnPause = _btnPause;
    }
    public static void SetComponents(PauseController _pauseController, BackgroundController _backgroundController, BuildingController _buildingController)
    {
        pauseController = _pauseController;
        backgroundController = _backgroundController;
        buildingController = _buildingController;
    }
    public static void SetComponents(MovementController _dialogBox, MovementController _btnStart, GameObject _txtTutorial, GameObject _gameOver, GameObject _txtScore, GameObject _bestScore)
    {
        dialogBox = _dialogBox;
        btnStart = _btnStart;
        txtTutorial = _txtTutorial;
        gameOver = _gameOver;
        txtScore = _txtScore;
        txtBestScore = _bestScore;
    }

    public static void SetComponents(MovementController _dialogBoxSmall, GameObject _showAd, GameObject _error, BoxCollider2D[] _buttons)
    {
        dialogBoxSmall = _dialogBoxSmall;
        showAd = _showAd;
        error = _error;
        buttonColliders = _buttons;
    }

    public static void PrepareStart(bool tutorial)
    {
        if (tutorial) ShowTutorial();
        else WaitForUser();
    }

    public static void Start()
    {
        backgroundController.SetMovingState(true);
        buildingController.SetMovingState(true);
        drone.SetPhysicalState(true);
        txtStart.SetActive(false);
    }

    public static void StopGame(int finalScore)
    {
        backgroundController.SetMovingState(false);
        buildingController.SetMovingState(false);

        btnPause.GetComponent<ButtonController>().SetButtonState(false);
        btnPause.ChangeTargetPositionAndMove(new Vector3(15.4f, 0, 0));
        score.ChangeTargetPositionAndMove(new Vector3(-32.5f, 0, 0));

        txtTutorial.SetActive(false);
        gameOver.SetActive(true);
        TextManager.SetText(txtScore, finalScore);
        TextManager.SetText(txtBestScore, $"{Strings.bestScore} {Statistics.BestScore}");
        DatabaseController.SaveData();
        dialogBox.SetIsMoving(true);
    }

    public static void Pause()
    {
        paused = true;
        pauseController.SetPause(true);
        backgroundController.SetMovingState(false);
        buildingController.SetMovingState(false);
        drone.SetPhysicalState(false);
    }

    public static void Unpause()
    {
        paused = false;
        pauseController.SetPause(false);
        txtStart.SetActive(true);
    }

    public static void TurnOffDrone()
    {
        backgroundController.SetMovingState(false);
        buildingController.SetMovingState(false);
    }

    static void ShowTutorial()
    {
        txtTutorial.SetActive(true);

        dialogBox.SetIsMoving(true);
        btnStart.SetIsMoving(true);

        buildingController.SetMovingState(false);
        drone.SetPhysicalState(false);
    }


    public static void BringAdDialogBox()
    {
        SetColliderActive(buttonColliders, false);
        showAd.SetActive(true);
        dialogBoxSmall.SetIsMoving(true);
    }

    public static void ShowErrorAd()
    {
        showAd.SetActive(false);
        error.SetActive(true);
    }

    public static void BackAdDialogBox()
    {
        dialogBoxSmall.ReturntoStartPosition(true);
        SetColliderActive(buttonColliders, true);
        showAd.SetActive(false);
        error.SetActive(false);
    }

    static void WaitForUser()
    {
        dialogBox.ReturntoStartPosition(true);
        btnStart.ReturntoStartPosition(true);

        score.SetIsMoving(true);
        btnPause.SetIsMoving(true);

        buildingController.SetMovingState(false);
        drone.SetPhysicalState(false);

        txtStart.SetActive(true);
        txtTutorial.SetActive(false);
    }

    static void SetColliderActive(BoxCollider2D[] cols, bool active)
    {
        foreach (BoxCollider2D col in cols) col.enabled = active;
    }
}
