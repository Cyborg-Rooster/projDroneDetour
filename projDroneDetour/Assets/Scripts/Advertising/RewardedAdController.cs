using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;
class RewardedAdController : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] GameManager manager;
    public string AdUnitId;

    bool isReady;

    private void Awake()
    {
        //manager = GameObject.Find("GameController").GetComponent<GameManager>();
    }

    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + AdUnitId);
        Advertisement.Load(AdUnitId, this);
    }

    public bool CheckIfAdIsLoaded()
    {
        return isReady;
    }

    public void OnUnityAdsAdLoaded(string _adUnitId)
    {
        Debug.Log("Ad Loaded: " + _adUnitId);

        if (_adUnitId.Equals(AdUnitId)) isReady = true;
    }

    // Implement a method to execute when the user clicks the button.
    public void ShowAd()
    {
        Advertisement.Show(AdUnitId, this);
    }

    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (_adUnitId.Equals(AdUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            manager.SavePhaseData();
            manager.AdRestart();
            //manager.RestartGame();
            //Advertisement.Load(_adUnitId, this);
        }
    }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        isReady = false;
        Debug.Log($"Error loading Ad Unit {_adUnitId}: {error} - {message}");
    }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        manager.AdCancel();
    }

    public void OnUnityAdsShowStart(string _adUnitId) { }
    public void OnUnityAdsShowClick(string _adUnitId) { }
}
