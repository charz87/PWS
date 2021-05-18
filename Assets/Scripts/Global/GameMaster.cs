using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour
{
	//Game stats
	//public static int mainScore;
	//public static int coinCount;
	//public static int currentLanguage; //0 for english 1 for spanish
	//public static float currentGunSpeed = 1;
	//PowerUp amount
	//public static int spiderAmount;
	//public static int wormAmount;
	//public static int seedBombAmount;
	//public static int crystalRoundsAmount;
	//PowerUp level
	//public static int turretLevel = 1;
	//public static int spiderLevel;
	//public static int wormLevel = 1;
	//public static int seedBombLevel;
	//public static int crystalRoundsLevel;
	//Ingame stats
	public static bool currentlyPlaying = false;
	public static bool cheering = false;
	//PlayerData
	public static string userName;
	
	public static void saveAppInstance()
	{
		PlayerPrefs.SetString("userName", userName);
	}
	
	public static void loadAppInstance()
	{
		if(PlayerPrefs.HasKey("userName"))
			userName = PlayerPrefs.GetString("userName");
	}
}
