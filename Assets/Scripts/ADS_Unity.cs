using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;
using UnityEngine.UI;

public class ADS_Unity : MonoBehaviour {

	private int coinsc2;
	private int BookCount, EmeraldCount, PipeCount, GoldCount;


	[SerializeField] string gameID = "2704863";
    public GameObject buttonBon;
    public Text bonusText;
	private string bonusName;
	private int bonuseNumber;
	private bool generated;
    private bool freecoin;

	void Awake()
	{
		Advertisement.Initialize (gameID, false);
		generated = false;
	}
    
    void Start()
    {
        freecoin = false;
        if (ProtectedPrefs.HasKey("Coins"))
        {
            coinsc2 = ProtectedPrefs.GetInt("Coins");
        }
        else {
            coinsc2 = 0;
        }
        BookCount = ProtectedPrefs.GetInt("mBook");
        GoldCount = ProtectedPrefs.GetInt("mGold");
        EmeraldCount = ProtectedPrefs.GetInt("mEmerald");
        PipeCount = ProtectedPrefs.GetInt("mPipe");
        bonuseNumber = Random.Range(0, 7);
        switch (bonuseNumber)
        {
            case 1:
                bonusName = "Take 250 coins";
                break;
            case 2:
                bonusName = "Take 3 books";
                break;
            case 3:
                bonusName = "Take 2 pipes";
                break;
            case 4:
                bonusName = "Take 4 emeralds";
                break;
            case 5:
                bonusName = "Take 2 golds";
                break;
            default:
                bonusName = "Take 100 coins";
                break;

        }
    }
	
	public void ShowAd(string zone = "")
	{
        #if UNITY_EDITOR
          StartCoroutine(WaitForAd());
        #endif

        if (string.Equals(zone, ""))
            zone = null;

        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCallbackhandler;

        if (Advertisement.IsReady(zone))
            Advertisement.Show(zone, options);
    }
	
	void AdCallbackhandler (ShowResult result)
	{
		switch(result)
		{
		case ShowResult.Finished:
			Debug.Log ("Ad Finished. Rewarding player...");
                if (freecoin)
                {
                    ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") + 500);
                    freecoin = false;
                }
                else
                {
                    InstansShow();
                }
			
			break;
		case ShowResult.Skipped:
			Debug.Log ("Ad skipped. Son, I am dissapointed in you");
			break;
		case ShowResult.Failed:
			Debug.Log("I swear this has never happened to me before");
			break;
		}
	}
	
	IEnumerator WaitForAd()
	{
        float currentTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        yield return null;

        while (Advertisement.isShowing)
            yield return null;

        Time.timeScale = currentTimeScale;
    }

	void Update(){
		if (Controller.iDie && !generated){
			buttonBon.SetActive (true);
			generated = true;
			bonusText.text = Advertisement.IsReady() ? bonusName : "No bonus :(";
		}

	}

	void InstansShow(){
		switch(bonuseNumber){
		case 1:
			ProtectedPrefs.SetInt ("Coins", coinsc2 + 250);
			break;
		case 2:
			ProtectedPrefs.SetInt("mBook", BookCount + 3);
			break;
		case 3:
			ProtectedPrefs.SetInt("mPipe", PipeCount + 2);
			break;
		case 4:
			ProtectedPrefs.SetInt("mEmerald", EmeraldCount+4);
			break;
		case 5:
			ProtectedPrefs.SetInt("mGold", GoldCount +2);
			break;
		default:
			ProtectedPrefs.SetInt ("Coins", coinsc2 + 100);
			break;
			
		}
		buttonBon.SetActive (false);
		ProtectedPrefs.Save ();
	}

    public void GetCoin() {
        freecoin = true;
    }

}
