using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Net;

public class GameSerializer : MonoBehaviour
{
	public enum idWorld
	{
		WORLD_1,
		WORLD_2,
		WORLD_3,
		WORLD_4
	}
	
	public idWorld worldId;
	
	public List<InGameLevel> world_1_levels;
	public List<InGameLevel> world_2_levels;
	public static List<Level> loadedLevels_2 = new List<Level>();
	public static Stats gameStats;
	
	public Texture availableTexture;
	public Texture unavailableTexture;
	public Texture bossTexture;
	
	public GameObject initialSettingsMenu;
	public GameObject startMenu;	
	
	static SaveSystem mySaveSystem = new SaveSystem();
	
	void Start()
	{
		/*if (Advertisement.isSupported) {
			Advertisement.allowPrecache = true;
			Advertisement.Initialize("22061");
		} else {
			Debug.Log("Platform not supported");
		}*/
		loadGame();
		if(GameMaster.currentlyPlaying)
		{
			GetComponent<InitialSettings>().setTexturesToLanguage();
		}
		else
		{
			if(!PlayerPrefs.HasKey("playedOnce"))
			{
				initialSettingsMenu.SetActive(true);
			}
			else
			{
				if(!GameMaster.currentlyPlaying)
				{
					//loadGame();
					GetComponent<InitialSettings>().setTexturesToLanguage();
					startMenu.SetActive(true);
				}
				/*else
				{
					//loadGame();
					GetComponent<InitialSettings>().setTexturesToLanguage();
				}*/
			}
		}
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			PlayerPrefs.DeleteAll();
		}
	}
	
	public static void saveGame()
	{
		SaveLoad.Save();
		if(!PlayerPrefs.HasKey("playedOnce"))
		{
			PlayerPrefs.SetInt("playedOnce", 1);
		}
	}
	
	public void loadGame()
	{
		if(PlayerPrefs.HasKey("playedOnce"))
		{
			if(!GameMaster.currentlyPlaying)
			{
				Debug.Log("I have played");
				SaveLoad.Load();
				for(int i = 0; i < world_1_levels.Count; i++)
				{
					world_1_levels[i].cleared = Game.current.world1Levels[i].cleared;
					world_1_levels[i].available = Game.current.world1Levels[i].available;
					if(Game.current.world1Levels[i].available)
					{
						world_1_levels[i].gameObject.guiTexture.texture = availableTexture;
					}
					else
					{
						world_1_levels[i].gameObject.guiTexture.texture = unavailableTexture;
					}
					world_1_levels[i].localScore = Game.current.world1Levels[i].localScore;
					world_1_levels[i].bestTime = Game.current.world1Levels[i].bestTime;
					world_1_levels[i].accuracy = Game.current.world1Levels[i].accuracy;
				}
				world_1_levels[world_1_levels.Count-1].gameObject.guiTexture.texture = bossTexture;
			}
			else
			{
				for(int i = 0; i < world_1_levels.Count; i++)
				{
					world_1_levels[i].cleared = Game.current.world1Levels[i].cleared;
					world_1_levels[i].available = Game.current.world1Levels[i].available;
					if(Game.current.world1Levels[i].available)
					{
						world_1_levels[i].gameObject.guiTexture.texture = availableTexture;
					}
					else
					{
						world_1_levels[i].gameObject.guiTexture.texture = unavailableTexture;
					}
					world_1_levels[i].localScore = Game.current.world1Levels[i].localScore;
					world_1_levels[i].bestTime = Game.current.world1Levels[i].bestTime;
				}
				world_1_levels[world_1_levels.Count-1].gameObject.guiTexture.texture = bossTexture;
				//GameMaster.mainScore = gameStats.totalScore;
				//GameMaster.coinCount = gameStats.totalCoins;
			}
		}
		else
		{
			Debug.Log("Not Played");
			Game.current = new Game();
			Game.current.world1Levels = new List<Level>();
			for(int i = 0; i < 31; i++)
			{
				Game.current.world1Levels.Add(new Level());
			}
			for(int i = 0; i < world_1_levels.Count; i++)
			{
				Game.current.world1Levels[i].worldID = 1;
				Game.current.world1Levels[i].levelID = i+1;
				world_1_levels[i].cleared = Game.current.world1Levels[i].cleared;
				world_1_levels[i].available =Game.current.world1Levels[i].available;
				world_1_levels[i].gameObject.guiTexture.texture = unavailableTexture;
				world_1_levels[i].localScore = Game.current.world1Levels[i].localScore;
				world_1_levels[i].bestTime = Game.current.world1Levels[i].bestTime;
			}
			world_1_levels[0].available = true;
			world_1_levels[0].gameObject.guiTexture.texture = availableTexture;
			world_1_levels[world_1_levels.Count-1].gameObject.guiTexture.texture = bossTexture;
			
		}
	}
}
