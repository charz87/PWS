using UnityEngine;
using System.Collections;

public class PowerUpStore : MonoBehaviour
{
	public int currentPrice;
	
	public enum typeOfPowerUp
	{
		spiderWeb,
		worm,
		crystalRound,
		acornBomb
	}
	public typeOfPowerUp powerUpType;
	// Level
	public int powerUpLevel;
	// PowerUpLock
	public GameObject lockedPowerUp;
	public int unlockPrice;
	public GameObject upgradeBtn;
	// Price System
	public GUIText priceText;
	public int[] prices;
	//Actual Changes
	public miniAcornBomb_PU acornData;
	public float[] acornRanges;
	public float[] acornSpeeds;
	//
	public CrystalRound_PU crystalData;
	public float[] crystalSpeeds;
	public float[] crystalDamages;
	//
	public SpiderWeb_PU spiderData;
	public float[] spiderRanges;
	public float[] spiderSpeeds;
	public float[] spiderDurations;
	//
	public Worm_PU wormData;
	public Material wormMat;
	public Texture[] wormTextures;
	public float[] wormSpeeds;
	public int[] wormRepetitions;
	//ranks
	public GUITexture rankGui;
	public Texture[] rankTextures;
	//price
	public GUIText coinCount;

	void Start ()
	{
		GameMaster.currentlyPlaying = true;
		switch(powerUpType)
		{
		case typeOfPowerUp.spiderWeb:
			if(Game.current.generalStats.spiderAvailable)
			{
				lockedPowerUp.SetActive(false);
				powerUpLevel = Game.current.generalStats.spiderLevel;
				if(powerUpLevel == rankTextures.Length-1)
				{
					upgradeBtn.SetActive(false);
					priceText.text = "MAX";
				}
				else
				{
					priceText.text = prices[powerUpLevel].ToString();
				}
			}
			else
			{
				priceText.text = unlockPrice.ToString();
			}
			break;
		case typeOfPowerUp.worm:
			if(Game.current.generalStats.wormAvailable)
			{
				lockedPowerUp.SetActive(false);
				powerUpLevel = Game.current.generalStats.wormLevel;
				if(powerUpLevel == rankTextures.Length-1)
				{
					upgradeBtn.SetActive(false);
					priceText.text = "MAX";
				}
				else
				{
					priceText.text = prices[powerUpLevel].ToString();
				}
			}
			else
			{
				priceText.text = unlockPrice.ToString();
			}
			break;
		case typeOfPowerUp.crystalRound:
			if(Game.current.generalStats.crystalAvailable)
			{
				lockedPowerUp.SetActive(false);
				powerUpLevel = Game.current.generalStats.crystalRoundsLevel;
				if(powerUpLevel == rankTextures.Length-1)
				{
					upgradeBtn.SetActive(false);
					priceText.text = "MAX";
				}
				else
				{
					priceText.text = prices[powerUpLevel].ToString();
				}
			}
			else
			{
				priceText.text = unlockPrice.ToString();
			}
			break;
		case typeOfPowerUp.acornBomb:
			if(Game.current.generalStats.seedAvailable)
			{
				lockedPowerUp.SetActive(false);
				powerUpLevel = Game.current.generalStats.seedBombLevel;
				if(powerUpLevel == rankTextures.Length-1)
				{
					upgradeBtn.SetActive(false);
					priceText.text = "MAX";
				}
				else
				{
					priceText.text = prices[powerUpLevel].ToString();
				}
			}
			else
			{
				priceText.text = unlockPrice.ToString();
			}
			break;
		}
		rankGui.texture = rankTextures[powerUpLevel];
		UpdateCoinGUI();
		//GameSerializer.saveGame();
	}
	
	void unlockPowerUp ()
	{
		currentPrice = unlockPrice;
		if(currentPrice <= Game.current.generalStats.totalCoins)
		{
			switch(powerUpType)
			{
			case typeOfPowerUp.spiderWeb:
				Game.current.generalStats.spiderAvailable = true;
				break;
			case typeOfPowerUp.worm:
				Game.current.generalStats.wormAvailable = true;
				break;
			case typeOfPowerUp.crystalRound:
				Game.current.generalStats.crystalAvailable = true;
				break;
			case typeOfPowerUp.acornBomb:
				Game.current.generalStats.seedAvailable = true;
				break;
			}
			priceText.text = prices[powerUpLevel].ToString();
			lockedPowerUp.SetActive(false);
			Game.current.generalStats.totalCoins -= currentPrice;
			UpdateCoinGUI();
			GameSerializer.saveGame();
		}
		else
		{
			
		}
	}
	
	void buyUpgrade()
	{
		currentPrice = prices[powerUpLevel];
		if(currentPrice <= Game.current.generalStats.totalCoins)
		{
			switch(powerUpType)
			{
			case typeOfPowerUp.spiderWeb:
				Game.current.generalStats.spiderLevel++;
				spiderData.areaOfEffect = spiderRanges[Game.current.generalStats.spiderLevel];
				spiderData.speed = spiderSpeeds[Game.current.generalStats.spiderLevel];
				break;
			case typeOfPowerUp.worm:
				Game.current.generalStats.wormLevel++;
				wormData.speed = wormSpeeds[Game.current.generalStats.wormLevel];
				wormData.repetitions = wormRepetitions[Game.current.generalStats.wormLevel];
				wormMat.mainTexture = wormTextures[Game.current.generalStats.wormLevel];
				break;
			case typeOfPowerUp.crystalRound:
				Game.current.generalStats.crystalRoundsLevel++;
				crystalData.speed = crystalSpeeds[Game.current.generalStats.crystalRoundsLevel];
				crystalData.Bulletdamage = crystalDamages[Game.current.generalStats.crystalRoundsLevel];
				break;
			case typeOfPowerUp.acornBomb:
				Game.current.generalStats.seedBombLevel++;
				acornData.speed = acornSpeeds[Game.current.generalStats.seedBombLevel];
				acornData.areaOfEffect = acornRanges[Game.current.generalStats.seedBombLevel];
				break;
			}
			Game.current.generalStats.totalCoins -= currentPrice;
			powerUpLevel++;
			priceText.text = prices[powerUpLevel].ToString();
			rankGui.texture = rankTextures[powerUpLevel];
			UpdateCoinGUI();
			if(powerUpLevel == rankTextures.Length-1)
			{
				upgradeBtn.SetActive(false);
				priceText.text = "MAX";
			}
			GameSerializer.saveGame();
		}
		else
		{
			
		}
	}
	
	public void UpdateCoinGUI()
	{
		coinCount.text = Game.current.generalStats.totalCoins.ToString();
	}
}
