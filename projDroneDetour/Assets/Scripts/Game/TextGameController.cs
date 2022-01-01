using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class TextGameController : MonoBehaviour
{
    [SerializeField] GameObject txtTitle;
    [SerializeField] GameObject txtTutorial;
    [SerializeField] GameObject txtDontShow;
    [SerializeField] GameObject txtStart;
    [SerializeField] GameObject txtPause;
    [SerializeField] GameObject txtBestScore;
    [SerializeField] GameObject txtRestart;
    [SerializeField] GameObject txtAd;
    [SerializeField] GameObject txtMenu;
    [SerializeField] GameObject txtShowAd;
    [SerializeField] GameObject txtErrorAd;

    public void SetText()
    {
        TextManager.SetText(txtTitle, Strings.txtTutorial);
        TextManager.SetText(txtTutorial, Strings.tutorial);
        TextManager.SetText(txtDontShow, Strings.dontDisplay);
        TextManager.SetText(txtStart, Strings.pressToStart);
        TextManager.SetText(txtPause, Strings.pause);
        TextManager.SetText(txtBestScore, Strings.bestScore);
        TextManager.SetText(txtRestart, Strings.restart);
        TextManager.SetText(txtAd, Strings.ad);
        TextManager.SetText(txtMenu, Strings.menu);
        TextManager.SetText(txtShowAd, Strings.wantAd);
        TextManager.SetText(txtErrorAd, Strings.offline);
    }
}
