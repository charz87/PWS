using UnityEngine;
using System.Collections;

public class RoundScore_Behavior : MonoBehaviour {

	//Local Variables
	int accuracy; // used for accuracy calculations
	
	public GameObject clearedSet; // group of objects to activate when you win
	public GameObject buttonsWin; // The buttons to activate when you win
	public GameObject buttonsLose; // The buttons to activate when you lose
	
	public GUITexture victorySign; // The object that appears and tells if you win or loose
	public Texture[] victoryTexture; // textures for victory, english and spanish
	public Texture[] defeatTexture; // textures for defeat, english and spanish

	public GUITexture workshopVButton; //The button that leads to the workshop when victory
	public GUITexture workshopDButton;  //The button that leads to the workshop when defeat
	public Texture[] workshopTextures; // Textures for workshop button, english and spanish

	public GUITexture backToMenuVButton;  //Button to go back to main menu when victory
	public GUITexture backToMenuDButton;  //Button to go back to main menu when defeat
	public Texture[] backToMenuTextures;  //Textures for backtomenu button, english and spanish.
	
	public GUITexture nextSign; // Next button
	public Texture[] nextTexture; //Next button textures, EN and SP
	
	public GUIText myScoreText; // text that says your score
	public GUIText myAccuracyText; // text that says your accuracy
	public GUIText myCoinText; // text that says your coins
	
	/*void Awake() {
		if (Advertisement.isSupported)
		{
			Advertisement.allowPrecache = true;
			Advertisement.Initialize("22061");
		} else {
			Debug.Log("Platform not supported");
		}
	}
	
	void Start() {
		if (Advertisement.isSupported) {
			Advertisement.allowPrecache = true;
			Advertisement.Initialize("22061");
		} else {
			Debug.Log("Platform not supported");
		}
	}*/
	
	public IEnumerator showScore()
	{
		/*if(Advertisement.isReady())
		{
			Advertisement.Show(null, new ShowOptions {
				pause = true,
				resultCallback = result => {
					Debug.Log(result.ToString());
				}
			});
		}*/
		//Activate Menu and Set Propper Textures
		clearedSet.SetActive(true);
		victorySign.texture = victoryTexture[Game.current.generalStats.currentLanguage];
		nextSign.texture = nextTexture[Game.current.generalStats.currentLanguage];
		workshopVButton.texture = workshopTextures[Game.current.generalStats.currentLanguage];
		backToMenuVButton.texture = backToMenuTextures[Game.current.generalStats.currentLanguage];
		
		//Add Base Score Coins
		Game.current.generalStats.totalCoins += (int)GameManager.localScore/100;
		
		//Accuracy Calculus
		//Debug.Log(GameManager.localHitCount + ", " + GameManager.localFiredBullets);
		float accuracyCalculus = ((float)GameManager.localHitCount/GameManager.localFiredBullets) * 100;
		//Debug.Log(accuracyCalculus);
		accuracy = (int)accuracyCalculus;
		
		//Check if perfect, add bonus and accuracy based Score. Add perfect and accuracy based Coins
		if(GameManager.damageReceived == 0)
		{
			GameManager.localScore += GameManager.localScore;
			Game.current.generalStats.totalCoins += 150;
		}
		GameManager.localScore += accuracy;
		Game.current.generalStats.totalCoins += (accuracy * 2);
		
		//Show score and wait
		myScoreText.text = "" + GameManager.localScore;
		//Debug.Log("Current Score: " + GameMaster.mainScore);
		yield return new WaitForSeconds(.1f);
		
		//Show Accuracy and wait
		myAccuracyText.text = accuracy + "%";
		//Debug.Log("My Accuracy was: " + accuracy);
		yield return new WaitForSeconds(.1f);
		
		//Show Coins and wait
		myCoinText.text = "" + Game.current.generalStats.totalCoins;
		//Debug.Log("Coins: " + GameMaster.coinCount);
		yield return new WaitForSeconds(.1f);
		
		//Add local Score to Global Score if I did a better score
		int scoreDifference = GameManager.localScore - Game.current.world1Levels[GameManager.currentLevel].localScore;
		if(scoreDifference > 0) 
		{
			Game.current.generalStats.totalScore += GameManager.localScore - Game.current.world1Levels[GameManager.currentLevel].localScore;
			Game.current.world1Levels[GameManager.currentLevel].localScore = GameManager.localScore;
		}
		
		//Add accuracy if better accuracy
		int accuracyDifference = accuracy - Game.current.world1Levels[GameManager.currentLevel].accuracy;
		if(accuracyDifference > 0)
		{
			Game.current.world1Levels[GameManager.currentLevel].accuracy = accuracy;
		}
		
		//Set Level as Cleared and Next Level as Available
		Game.current.world1Levels[GameManager.currentLevel].available = true;
		Game.current.world1Levels[GameManager.currentLevel].cleared = true;
		//if it is not a Boss Battle then make next available, else destroy next level btn
		if(GameManager.currentLevel < 30)
		{
			Game.current.world1Levels[GameManager.currentLevel + 1].available = true;
		}
		else
		{
			//Destroy(nextSign.gameObject);
		}
		Debug.Log (Game.current.generalStats.totalScore);
		//Reset variables and increase Level identifier
		GameManager.hitPointsToAdd = 4;
		GameManager.currentLevel++;
		//Save Game
		GameManager.saveGame();
		//Activate Buttons
		buttonsWin.SetActive(true);
	}
	
	public void showReplay()
	{
		/*if(Advertisement.isReady())
		{
			Advertisement.Show(null, new ShowOptions {
				pause = true,
				resultCallback = result => {
					Debug.Log(result.ToString());
                }
            });
        }*/
		victorySign.texture = defeatTexture[Game.current.generalStats.currentLanguage];
		workshopDButton.texture = workshopTextures[Game.current.generalStats.currentLanguage];
		backToMenuDButton.texture = backToMenuTextures[Game.current.generalStats.currentLanguage];
		GameManager.hitPointsToAdd = 2;
		buttonsLose.SetActive(true);
	}
}
