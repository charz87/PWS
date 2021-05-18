using UnityEngine;
using System.Collections;

[System.Serializable]
public class Stats
{
	public int totalScore = 0;
	public int totalCoins = 0;
	public int currentLanguage = 0;
	public float currentGunSpeed = .8f;
	public float currentBulletSpeed = 20;
	public int currentBulletDamage = 50;
	//PowerUp amount
	public int spiderAmount = 0;
	public int wormAmount = 0;
	public int seedBombAmount = 0;
	public int crystalRoundsAmount = 0;
	//PowerUp level
	public int turretSpeedLevel = 1;
	public int bulletSpeedLevel = 1;
	public int bulletDamageLevel = 1;
	public int spiderLevel = 0;
	public int wormLevel = 0;
	public int seedBombLevel = 0;
	public int crystalRoundsLevel = 0;
	//PowerUp Available
	public bool spiderAvailable = false;
	public bool wormAvailable = false;
	public bool seedAvailable = false;
	public bool crystalAvailable = false;
}
