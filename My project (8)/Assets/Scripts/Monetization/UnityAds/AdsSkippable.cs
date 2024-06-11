using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsSkippable : MonoBehaviour
{
#if UNITY_IOS
    public string gameId = "Your-Apple-ID";
    public string mySurfacingId = "Interstitial_iOS";
#elif UNITY_ANDROID
    public string gameId = "Your-Google-ID";
    public string mySurfacingId = "Interstitial_Android";
#endif
    public bool testMode = true; //Leave this as True UNTIL you publicly release your game!!!
    void Start()
    {
        Advertisement.Initialize(gameId, testMode); //Prepares Everything Immediately
    }
    public void ShowInterstitialAd()
    {
        //if (Advertisement.IsReady()) // Check if UnityAds ready before calling Show method:
        {
            Advertisement.Show(mySurfacingId);
            Debug.Log("You're watching an AD!!!");
        }
        //else 
        Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
    }
}