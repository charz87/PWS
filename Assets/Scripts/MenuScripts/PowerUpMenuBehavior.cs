using UnityEngine;
using System.Collections;

public class PowerUpMenuBehavior : MonoBehaviour
{
	//Determine Power Up
	public enum typeOfPowerUP
	{
		spiderWeb,
		worm,
		seedBomb,
		crystalRound
	}
	public typeOfPowerUP powerUpType;
	//Locked Obj
	public GameObject lockedUI;
	//cost
	public int powerUpPrice;
	public GUIText powerUpPriceText;
			
	//buttons
	public GameObject plusButton;
	public GameObject minusButton;
	
	//amountText
	public GUIText amountText;
	
	//localVariables
	int powerUpCount = 0; //This must start in 0 each time menu pops up, the reason: you cannot sell powerUps, you can only buy new Ones or do nothing.
	GameManager myGameManager;
	
	void Start()
	{
		switch(powerUpType)
		{
		case typeOfPowerUP.spiderWeb:
			if(Game.current.generalStats.spiderAvailable)
			{
				lockedUI.SetActive(false);
			}
			break;
		case typeOfPowerUP.worm:
			if(Game.current.generalStats.wormAvailable)
			{
				lockedUI.SetActive(false);
			}
			break;
		case typeOfPowerUP.seedBomb:
			if(Game.current.generalStats.seedAvailable)
			{
				lockedUI.SetActive(false);
			}
			break;
		case typeOfPowerUP.crystalRound:
			if(Game.current.generalStats.crystalAvailable)
			{
				lockedUI.SetActive(false);
			}
			break;
		}
		powerUpPriceText.text = powerUpPrice.ToString();
		myGameManager = GameObject.Find("_GameManager").GetComponent<GameManager>();
	}
	
	//Used to increase Power Up
	void increasePowerUp ()
	{
		switch(powerUpType)
		{
		case typeOfPowerUP.spiderWeb:
			if(Game.current.generalStats.spiderAmount < 10 && Game.current.generalStats.totalCoins > powerUpPrice)
			{
				Game.current.generalStats.spiderAmount++;
				Game.current.generalStats.totalCoins -= powerUpPrice;
				powerUpCount++;
			}
			amountText.text = Game.current.generalStats.spiderAmount.ToString();
			break;
		case typeOfPowerUP.worm:
			if(Game.current.generalStats.wormAmount < 10 && Game.current.generalStats.totalCoins > powerUpPrice)
			{
				Game.current.generalStats.wormAmount++;
				Game.current.generalStats.totalCoins -= powerUpPrice;
				powerUpCount++;
			}
			amountText.text = Game.current.generalStats.wormAmount.ToString();
			break;
		case typeOfPowerUP.seedBomb:
			if(Game.current.generalStats.seedBombAmount < 10 && Game.current.generalStats.totalCoins > powerUpPrice)
			{
				Game.current.generalStats.seedBombAmount++;
				Game.current.generalStats.totalCoins -= powerUpPrice;
				powerUpCount++;
			}
			amountText.text = Game.current.generalStats.seedBombAmount.ToString();
			break;
		case typeOfPowerUP.crystalRound:
			if(Game.current.generalStats.crystalRoundsAmount < 100 && Game.current.generalStats.totalCoins > powerUpPrice)
			{
				Game.current.generalStats.crystalRoundsAmount += 10;
				Game.current.generalStats.crystalRoundsAmount = Mathf.Clamp(Game.current.generalStats.crystalRoundsAmount, 0, 100);
				Game.current.generalStats.totalCoins -= powerUpPrice;
				powerUpCount++;
			}
			amountText.text = Game.current.generalStats.crystalRoundsAmount.ToString();
			break;
		}
		myGameManager.UpdateCoinGUI();
	}
	
	// Update is called once per frame
	void decreasePowerUp ()
	{
		switch(powerUpType)
		{
		case typeOfPowerUP.spiderWeb:
			if(powerUpCount > 0)
			{
				Game.current.generalStats.spiderAmount--;
				Game.current.generalStats.totalCoins += powerUpPrice;
				powerUpCount--;
			}
			amountText.text = Game.current.generalStats.spiderAmount.ToString();
			break;
		case typeOfPowerUP.worm:
			if(powerUpCount > 0)
			{
				Game.current.generalStats.wormAmount--;
				Game.current.generalStats.totalCoins += powerUpPrice;
				powerUpCount--;
			}
			amountText.text = Game.current.generalStats.wormAmount.ToString();
			break;
		case typeOfPowerUP.seedBomb:
			if(powerUpCount > 0)
			{
				Game.current.generalStats.seedBombAmount--;
				Game.current.generalStats.totalCoins += powerUpPrice;
				powerUpCount--;
			}
			amountText.text = Game.current.generalStats.seedBombAmount.ToString();
			break;
		case typeOfPowerUP.crystalRound:
			if(powerUpCount > 0)
			{
				Game.current.generalStats.crystalRoundsAmount -= 10;
				Game.current.generalStats.totalCoins += powerUpPrice;
				powerUpCount--;
			}
			amountText.text = Game.current.generalStats.crystalRoundsAmount.ToString();
			break;
		}
		myGameManager.UpdateCoinGUI();
	}
	
	void resetPowerUp()
	{
		switch(powerUpType)
		{
		case typeOfPowerUP.spiderWeb:
			if(Game.current.generalStats.spiderAmount == 10)
				plusButton.SetActive(false);
			else
				plusButton.SetActive(true);
			amountText.text = Game.current.generalStats.spiderAmount.ToString();
			break;
		case typeOfPowerUP.worm:
			if(Game.current.generalStats.wormAmount == 10)
				plusButton.SetActive(false);
			else
				plusButton.SetActive(true);
			amountText.text = Game.current.generalStats.wormAmount.ToString();
			break;
		case typeOfPowerUP.seedBomb:
			if(Game.current.generalStats.seedBombAmount == 10)
				plusButton.SetActive(false);
			else
				plusButton.SetActive(true);
			amountText.text = Game.current.generalStats.seedBombAmount.ToString();
			break;
		case typeOfPowerUP.crystalRound:
			if(Game.current.generalStats.crystalRoundsAmount == 100)
				plusButton.SetActive(false);
			else
				plusButton.SetActive(true);
			amountText.text = Game.current.generalStats.crystalRoundsAmount.ToString();
			break;
		}
		powerUpCount = 0;
	}
}
