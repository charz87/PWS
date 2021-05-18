using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public static class SaveLoad
{
	public static Game savedGame = new Game();
	
	public static void Save()
	{
		savedGame = Game.current;
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/savedGame.gd");
		bf.Serialize(file, SaveLoad.savedGame);
		file.Close();
	}
	
	public static void Load()
	{
		if(File.Exists(Application.persistentDataPath + "/savedGame.gd"))
		{
			Debug.Log("File found");
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGame.gd", FileMode.Open);
			//Debug.Log(Application.persistentDataPath);
			SaveLoad.savedGame = (Game)bf.Deserialize(file);
			Game.current = SaveLoad.savedGame;
			Game.current.world1Levels = SaveLoad.savedGame.world1Levels;
			file.Close();
		}
	}
}
