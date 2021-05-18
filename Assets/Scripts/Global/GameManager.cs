using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
	//Game Stats
	[HideInInspector]
	public static bool audioOn=true;
	public static bool isPaused=false;
	public static int mapUnitCount;
	public static int localScore;
	public static int localFiredBullets;
	public static int localHitCount;
	public static int hitPoints = 0;
	public static int damageReceived = 0;
	public static int hitPointsToAdd;
	public static int currentLevel = 0;
	public static bool fromGameToWorkshop=false;
	//Interface Management
	public GUITexture livesGUI;
	public Texture[] livesTextures;
	public GameObject nextRoundMenu;
	public GameObject gameInterface;
	public GUIText coinCount;
	public GUIText levelID;
	//Round Control
	public LevelSerializer mySerializer;
	public float endRoundPause = 1f;
	public Invoker myInvoker;
	public static bool turretActive = false;
	//Anthy
	public GameObject anthy;
	public AudioClip cheerClip;
	public AudioClip loseClip;
	//powerUps
	public GameObject[] powerUps;
	public GameObject powerUpContainer;
	//Boss
	public static bool epicMode = false;
	public static bool bossAlive = false;
	//Unity Ads
	public string gameID;
	public bool disableTestMode;
	public bool showInfoLogs;
	public bool showDebugLogs;
	public bool showWarningLogs = true;
	public bool showErrorLogs = true;
	string zone = null;

	public GameObject nextLevelButton;
	AnimationState mixAnimation;
	public Transform Torax;
	
	void Awake ()
	{
		// Make sure a game ID is provided.
		if (string.IsNullOrEmpty(gameID))
		{
			Debug.LogError("A valid game ID is required to initialize Unity Ads.");
		}
		// Check if the player is running on a supported platform: editor, iOS, Android.
		else if (Advertisement.isSupported) 
		{
			// NOTE: Assigning a value to allowPrecache doesn't actually do anything, 
			//        and calling the allowPrecache property always returns true.
			Advertisement.allowPrecache = true;
			
			// NOTE: Development Build must be set in Build Settings 
			//        to show additional debug levels from Unity Ads.
			Advertisement.debugLevel = Advertisement.DebugLevel.NONE;	
			if (showInfoLogs) Advertisement.debugLevel    |= Advertisement.DebugLevel.INFO;
			if (showDebugLogs) Advertisement.debugLevel   |= Advertisement.DebugLevel.DEBUG;
			if (showWarningLogs) Advertisement.debugLevel |= Advertisement.DebugLevel.WARNING;
			if (showErrorLogs) Advertisement.debugLevel   |= Advertisement.DebugLevel.ERROR;
			
			// Enable test mode by default when Development Build is set in Build Settings.
			//  Disable it only when production mode testing is necessary. By checking to
			//  see if Development Build is set, we avoid accidentally submitting a 
			//  production build for review with test mode enabled.
			bool enableTestMode = Debug.isDebugBuild && !disableTestMode; 
			Debug.Log(string.Format("Initializing Unity Ads for game ID {0} with test mode {1}.",
			                        gameID, enableTestMode ? "enabled" : "disabled"));
            
            // Only call Initialize once throughout your game.
            Advertisement.Initialize(gameID,enableTestMode);
        } 
        else 
        {
            Debug.Log("Platform not supported with Unity Ads.");
        }
	}

	void Start ()
	{
		
		if(GameMaster.currentlyPlaying)
			hitPoints = Mathf.Clamp (GameManager.hitPoints + 2, 0, 10);
		else
			hitPoints = 10;	
		StartCoroutine("prepareNextLevel");
		mixAnimation = anthy.animation["Cheer"];
		mixAnimation.weight = 1.0f;
		mixAnimation.layer = 10;
		mixAnimation.blendMode = AnimationBlendMode.Blend;
		mixAnimation.AddMixingTransform(Torax);
		mixAnimation.enabled = false;
		myInvoker = GameObject.Find("Invoker").GetComponent<Invoker>();
	}
	
	public IEnumerator prepareNextLevel()
	{
		//------------------------------ADS
		// If the value for zone is an empty, set it to null.
		//  When the zone value is null, the default zone will be used.
		if (string.IsNullOrEmpty(zone)) zone = null;
		
		ShowOptions options = new ShowOptions();
		
		// With the pause option set to true, the timeScale and AudioListener 
		//  volume for your game is set to 0 while the ad is shown.
		// NOTE: The current version of Unity Ads always pauses the app.
		options.pause = true;
		
		options.resultCallback = HandleShowResult;
        
        // Show the ad with the specified zone and options.
        Advertisement.Show(zone,options);
		
		//------------------------------
		if(currentLevel == 31)
		{
			Application.LoadLevel("4_EndScene");
		}
		damageReceived = 0;
		localScore = 0;
		localHitCount = 0;
		localFiredBullets = 0;
		levelID.text = (currentLevel+1).ToString();
		setLevelInfo(currentLevel, hitPointsToAdd);
		yield return mySerializer.StartCoroutine("setNextLevel");
		powerUpContainer.SetActive(true);
		foreach(GameObject go in powerUps)
		{
			go.SendMessage("resetPowerUp");
		}
		updateGUI();
		nextLevelButton.SetActive(true);
		UpdateCoinGUI();		
	}
	
	// Set level, add hitPoints, fill enemy list in Invoker
	public static void setLevelInfo(int levelToLoad, int livesToAdd)
	{
		currentLevel = levelToLoad;
		hitPoints = Mathf.Clamp (hitPoints + livesToAdd, 0, 10);
	}
	
	
	public static void addRemoveMapUnit(bool add)
	{
		if(add)
			mapUnitCount += 1;
		else
			mapUnitCount -= 1;
	}
	
	public IEnumerator checkRoundState(GameObject menu)
	{
		if(hitPoints == 0)
		{
			myInvoker.active = false;
			mapUnitCount = 0;
			GameObject[] mushiesInScene = GameObject.FindGameObjectsWithTag("Enemy");
			foreach(GameObject go in mushiesInScene)
			{
				Destroy(go);
			}
			anthy.audio.PlayOneShot(loseClip);
			yield return new WaitForSeconds(anthy.animation["Cheer"].length);
			gameInterface.SetActive(false);
			menu.SetActive(true);
			menu.SendMessage("showReplay");
			turretActive = false;
		}
		else if(mapUnitCount == 0 && !myInvoker.active)
		{
			mixAnimation.enabled = true;
			anthy.audio.PlayOneShot(cheerClip);
			yield return new WaitForSeconds(anthy.animation["Cheer"].length);
			mixAnimation.enabled = false;
			gameInterface.SetActive(false);
			menu.SetActive(true);
			if(hitPoints > 0)
			{
				menu.SendMessage("showScore");
			}
			turretActive = false;
		}
	}
	
	public void saveNExit()
	{
		isPaused=false;
		turretActive=false;
		myInvoker.active=false;
		mapUnitCount = 0;
		GameSerializer.saveGame();
		Time.timeScale=1;
		Application.LoadLevel("1_MenuScene");
	}

	public static void saveGame()
	{
		//SaveLoad.Save();
		GameSerializer.saveGame();	
	}
	
	public static void increaseScore(int amount)
	{
		localScore += amount;
	}
	
	public void updateGUI()
	{
		livesGUI.texture = livesTextures[hitPoints];
	}
	public void UpdateCoinGUI()
	{
		coinCount.text = Game.current.generalStats.totalCoins.ToString();
	}
	
	private void HandleShowResult (ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log("The ad was successfully completed.");
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}
