using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Game { 
	
	public static Game current;
	public Stats generalStats;
	public List<Level> world1Levels;
	//public List<Level> world2Levels;
	
	public Game ()
	{
		generalStats = new Stats();
		world1Levels = new List<Level>();
		//world2Levels = new List<Level>();
	}
}
