using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;
using UnityEngine.SceneManagement;

public class AdsService : MonoBehaviour, IUnityAdsListener
{
    GameManager game;
    [SerializeField]
    GameObject fade;
    SoundManager sound;

    const string gameId = "3773081";
    const string myPlacementId = "rewardedVideo";
    public bool testMode = false;

    // Initialize the Ads listener and service:
    void Start()
    {
        game = GetComponent<GameManager>();
        sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
    }

    public IEnumerator ShowRewardedVideo()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(myPlacementId))
        {
            fade.SetActive(true);
            fade.GetComponent<Fade>().StartCoroutine("FadeIn");

            sound.isPlaying = false;
            sound.StartCoroutine(sound.StartFade(0f));
            yield return new WaitForSeconds(.5f);
            Advertisement.Show(myPlacementId);
        }
        else
        {
            Debug.Log("O anuncio não foi carregado");
            game.ReturnOffline();
        }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            game.AdSuccesful();
        }
        else if (showResult == ShowResult.Skipped)
        {
            game.AdCancelled();
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == myPlacementId)
        {
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}