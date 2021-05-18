using UnityEngine;
using System.Collections;

[System.Serializable]
public class Level
{
	public int worldID;
	public int levelID;
	public bool isBossLevel;
	public bool available;
	public bool cleared;
	public int localScore;
	public int accuracy;
	public float bestTime;
}
