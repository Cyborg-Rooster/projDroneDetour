using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class TextMainController : MonoBehaviour
{
    [SerializeField] GameObject txtVersion;
    [SerializeField] GameObject txtPlay;
    [SerializeField] GameObject txtOptions;
    [SerializeField] GameObject txtCredits;

    [SerializeField] GameObject txtSound;
    [SerializeField] GameObject txtLanguage;
    [SerializeField] GameObject txtTutorial;
    [SerializeField] GameObject txtShowStatistics;
    [SerializeField] GameObject txtClear;

    [SerializeField] GameObject txtBestScore;
    [SerializeField] GameObject txtNumberDeaths;
    [SerializeField] GameObject txtFalls;
    [SerializeField] GameObject txtCrash;
    [SerializeField] GameObject txtClicks;
    [SerializeField] GameObject txtReally;

    [SerializeField] GameObject txtDev;
    [SerializeField] GameObject txtSounds;
    [SerializeField] GameObject txtTranslator;
    [SerializeField] GameObject txtDistribution;

    public void SetText()
    {
        TextManager.SetText(txtVersion, $"v{Application.version}");
        TextManager.SetText(txtPlay, Strings.start);
        TextManager.SetText(txtOptions, Strings.options);
        TextManager.SetText(txtCredits, Strings.credits);

        TextManager.SetText(txtSound, Strings.sound);
        TextManager.SetText(txtLanguage, $"{Strings.language} {Options.Language}");
        TextManager.SetText(txtTutorial, Strings.txtTutorial);
        TextManager.SetText(txtShowStatistics, Strings.statistic);
        TextManager.SetText(txtClear, Strings.cleanStatistic);

        TextManager.SetText(txtBestScore, $"{Strings.bestScore} {Statistics.BestScore}");
        TextManager.SetText(txtNumberDeaths, $"{Strings.nDeaths} {Statistics.Death}");
        TextManager.SetText(txtFalls, $"{Strings.nDeathsFall} {Statistics.Falls}");
        TextManager.SetText(txtCrash, $"{Strings.nDeathsOstacles} {Statistics.Crashes}");
        TextManager.SetText(txtClicks, $"{Strings.nClicks} {Statistics.Clicks}");
        TextManager.SetText(txtReally, Strings.deleteStatistics);

        TextManager.SetText(txtDev, Strings.dev);
        TextManager.SetText(txtSounds, Strings.music);
        TextManager.SetText(txtTranslator, Strings.translator);
        TextManager.SetText(txtDistribution, Strings.distribution);
    }
}
