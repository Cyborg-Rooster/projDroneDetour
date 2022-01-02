using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;

class AdInitializerController : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] bool TestMode = true;
    string GameId = "3773081";

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        Debug.Log("Initializing Unity Ads.");
        Advertisement.Initialize(GameId, TestMode, true);
        StartCoroutine(WaitForInitialize());
    }

    IEnumerator WaitForInitialize()
    {
        while (!Advertisement.isInitialized)
        {
            yield return null;
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
