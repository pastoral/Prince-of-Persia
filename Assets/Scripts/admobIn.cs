using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AdMobPlugin))]
public class admobIn : MonoBehaviour {

	private const string AD_UNIT_ID = "ca-app-pub-4365083222822400/5806288230";
	private const string INTERSTITIAL_ID = "ca-app-pub-4365083222822400/1421579847";
	private AdMobPlugin admob;
	private bool isShow;
    private int adNumber;
	void Start() {
        adNumber = Random.Range(0, 2);
        admob = GetComponent<AdMobPlugin>();
		admob.CreateBanner(adUnitId: AD_UNIT_ID,
			adSize: AdMobPlugin.AdSize.SMART_BANNER,
			isTopPosition: true,
			interstitialId: INTERSTITIAL_ID,
			isTestDevice: false);
		admob.RequestAd();
        #if !UNITY_EDITOR
		    if(adNumber == 0) admob.RequestInterstitial();
        #endif
    }

    void OnEnable() {
		AdMobPlugin.AdClosed += () => { Debug.Log ("AdClosed"); };
		AdMobPlugin.AdFailedToLoad += () => { Debug.Log ("AdFailedToLoad"); };
		AdMobPlugin.AdLeftApplication += () => { Debug.Log ("AdLeftApplication"); };
		AdMobPlugin.AdOpened += () => { Debug.Log ("AdOpened"); };

		AdMobPlugin.InterstitialClosed += () => { Debug.Log ("InterstitialClosed"); };
		AdMobPlugin.InterstitialFailedToLoad += () => { Debug.Log ("InterstitialFailedToLoad"); };
		AdMobPlugin.InterstitialLeftApplication += () => { Debug.Log ("InterstitialLeftApplication"); };
		AdMobPlugin.InterstitialOpened += () => { Debug.Log ("InterstitialOpened"); };

		AdMobPlugin.InterstitialLoaded += () => { isShow = false; };
    }

		

	void Update(){
		if(isShow == false && GameControll.showAd) {
            if (adNumber == 0) {
                admob.ShowInterstitial();
                Debug.Log("Bunner!!");
            } 
            isShow = true;
		} 
	}

}
