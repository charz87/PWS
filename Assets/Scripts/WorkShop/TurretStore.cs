using UnityEngine;
using System.Collections;

public class TurretStore : MonoBehaviour
{
	public static int currentPrice;
	// Use this for initialization
	public enum typeOfUpgrade
	{
		turretSpeed,
		bulletSpeed,
		bulletDamage
	}
	public typeOfUpgrade upgradeType;
	// Level
	public int statLevel;
	// PowerUpLock
	public GameObject upgradeBtn;
	// Price System
	public GUIText priceText;
	public GUIText descriptionText;
	public int[] prices;
	//custom data
	public int[] turretSpeeds;
	public int[] bulletSpeeds;
	public int[] bulletDamages;
	//graphic Changes
	public GameObject[] cannons;
	public GameObject bulletPrefab;
	public Material bulletMaterial;
	public Texture[] bulletTextures;
	public MeshFilter bulletMesh;
	public Mesh[] bulletMeshes;
	//price
	public GUIText coinCount;
	
	void Start ()
	{
		switch(upgradeType)
		{
		case typeOfUpgrade.turretSpeed:
			statLevel = Game.current.generalStats.turretSpeedLevel;
			descriptionText.text = turretSpeeds[statLevel-1].ToString();
			for(int i = 0; i < statLevel; i++)
			{
				cannons[i].SetActive(true);
			}
			if(statLevel==turretSpeeds.Length)
			{
				upgradeBtn.SetActive(false);
			}
			break;
		case typeOfUpgrade.bulletSpeed:
			statLevel = Game.current.generalStats.bulletSpeedLevel;
			descriptionText.text = bulletSpeeds[statLevel-1].ToString();
			if(statLevel==bulletSpeeds.Length)
			{
				upgradeBtn.SetActive(false);
			}
			break;
		case typeOfUpgrade.bulletDamage:
			statLevel = Game.current.generalStats.bulletDamageLevel;
			Debug.Log(statLevel);
			descriptionText.text = bulletDamages[statLevel-1].ToString();
			bulletMaterial.mainTexture = bulletTextures[statLevel-1];
			bulletMesh.mesh = bulletMeshes[statLevel-1];
			if(statLevel==bulletDamages.Length)
			{
				upgradeBtn.SetActive(false);
			}
			break;
		}
		priceText.text = prices[statLevel-1].ToString();
		UpdateCoinGUI();
	}
	
	void buyUpgrade()
	{
		currentPrice = prices[statLevel-1];
		if(currentPrice <= Game.current.generalStats.totalCoins)
		{
			statLevel++;
			switch(upgradeType)
			{
			case typeOfUpgrade.turretSpeed:
				descriptionText.text = turretSpeeds[statLevel-1].ToString();
				Game.current.generalStats.turretSpeedLevel = statLevel;
				Game.current.generalStats.currentGunSpeed = (float) Game.current.generalStats.currentGunSpeed / 2;
				cannons[statLevel-1].SetActive(true);
				if(statLevel==turretSpeeds.Length)
				{
					upgradeBtn.SetActive(false);
					priceText.text = "MAX";
				}
				else
				{
					priceText.text = prices[statLevel-1].ToString();
				}
				break;
			case typeOfUpgrade.bulletSpeed:
				descriptionText.text = bulletSpeeds[statLevel-1].ToString();
				Game.current.generalStats.bulletSpeedLevel = statLevel;
				Game.current.generalStats.currentBulletSpeed = bulletSpeeds[statLevel-1];
				if(statLevel==bulletSpeeds.Length)
				{
					upgradeBtn.SetActive(false);
					priceText.text = "MAX";
				}
				else
				{
					priceText.text = prices[statLevel-1].ToString();
				}
				break;
			case typeOfUpgrade.bulletDamage:
				descriptionText.text = bulletDamages[statLevel-1].ToString();
				Game.current.generalStats.bulletDamageLevel = statLevel;
				Game.current.generalStats.currentBulletDamage = bulletDamages[statLevel-1];
				bulletMaterial.mainTexture = bulletTextures[statLevel-1];
				bulletMesh.mesh = bulletMeshes[statLevel-1];
				if(statLevel==bulletDamages.Length)
				{
					upgradeBtn.SetActive(false);
					priceText.text = "MAX";
				}
				else
				{
					priceText.text = prices[statLevel-1].ToString();
				}
				break;
			}
			Game.current.generalStats.totalCoins -= currentPrice;
			//priceText.text = prices[statLevel-1].ToString();
			UpdateCoinGUI();
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
