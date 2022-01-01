using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pauseBackground;
    [SerializeField] GameObject btnUnpause;
    [SerializeField] ButtonController btnPause;

    public void SetPause(bool pause)
    {
        if (pause) Pause();
        else Unpause();
    }

    void Pause()
    {
        pauseBackground.SetActive(true);
        btnUnpause.SetActive(true);
    }

    void Unpause()
    {
        pauseBackground.SetActive(false);
        btnUnpause.SetActive(false);
        btnPause.SetButtonState(true);
    }

}
