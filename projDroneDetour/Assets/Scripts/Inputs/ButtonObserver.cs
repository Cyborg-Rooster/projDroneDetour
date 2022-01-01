using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
class ButtonObserver
{
    public static void OnClick(GameObject button, GameManager manager)
    {
        ButtonController controller = button.GetComponent<ButtonController>();
        controller.OnClick();
        if (!controller.clicked)
        {
            if (button.name == "btnAd") GameStateManager.BringAdDialogBox();
            else if (button.name == "btnYes") manager.TryShowAd();
            else if (button.name == "btnNo" || button.name == "btnSad") GameStateManager.BackAdDialogBox();
            else if(button.name == "btnPause") GameStateManager.Pause();
            else if (button.name == "btnUnpause") GameStateManager.Unpause();
            else if (button.name == "btnPlay") manager.CloseTutorial();
            else if (button.name == "btnRestart") manager.RestartGame();
            else if (button.name == "btnMenu") manager.GoToMain();

            controller.SetButtonState(controller.multipleClicks);
        }
    }

    public static void OnClick(GameObject button, MainController manager)
    {
        ButtonController controller = button.GetComponent<ButtonController>();
        controller.OnClick();
        if (!controller.clicked)
        {
            if (button.name == "btnMain") MainStateManager.BringMain();
            else if (button.name == "btnStart") manager.StartGame();
            else if(button.name == "btnBack") manager.BackMain();
            else if (button.name == "btnOptions") MainStateManager.ShowOptions();
            else if (button.name == "btnStatistics") MainStateManager.ShowStatistic();
            else if (button.name == "btnCredits") MainStateManager.ShowCredits();
            else if (button.name == "btnClear") MainStateManager.BringClearStatistic();
            else if (button.name == "btnNo") MainStateManager.BackClearStatistic(false);
            else if (button.name == "btnYes") manager.ClearStatistic();
        }
    }
}
