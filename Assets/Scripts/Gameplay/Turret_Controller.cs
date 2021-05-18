using UnityEngine;
using System.Collections;

public class Turret_Controller : MonoBehaviour
{
	//These are the joystick and action button.
	public NewJoystick moveJoystick;
	public NewJoystick trigger_BTN;
	
	//These are the powerUp Buttons
	public NewJoystick spiderWeb_BTN;
	public NewJoystick worm_BTN;
	public NewJoystick crystalRound_BTN;
	public NewJoystick miniAcornBomb_BTN;
    
    //These are the powerUp texts
	public GUIText spiderWeb_Text;
	public GUIText worm_Text;
	public GUIText crystal_Text;
	public GUIText miniAcorn_Text;

	public GameObject []cannons;
	
	//Objects that will be controlled via scripting
	public GameObject chamberObj;
	public GameObject anthyObj;
	public Transform []muzzlePos;
	public Transform cannonNode;
	
	//Objects to instantiate
	public GameObject bullet;
	public GameObject shotParticles;
	//PowerUps
	public GameObject spiderWebObj;
	public GameObject wormObj;
	public GameObject crystalRoundObj;
	public GameObject miniAcornBombObj;
	
	//Audio for the bullets
	public AudioClip shotSound;
	
	//Boundaries for movement
	public float shootingArc;
	public float pitchArc;
	public float turnSpeed;
	public float pitchSpeed;
	
	//limit bullet frequency
	public float fireSpeed;
	
	//
	float nextFireTime;
	float turretRotator;
	float turretPitch;
	Quaternion initialRotation;
	int muzzleID=0;
			
	// Use this for initialization
	void Start ()
	{
		initialRotation = transform.localRotation;
		nextFireTime = Time.timeSinceLevelLoad;
		fireSpeed = Game.current.generalStats.currentGunSpeed;

		if(Game.current.generalStats.turretSpeedLevel==3)
		{
			cannons[1].SetActive(true);
			cannons[2].SetActive(true);
			cannons[3].SetActive(true);
		}
		else if(Game.current.generalStats.turretSpeedLevel==2){
			cannons[1].SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameManager.turretActive)
		{
			spiderWeb_Text.text = Game.current.generalStats.spiderAmount.ToString();
			worm_Text.text = Game.current.generalStats.wormAmount.ToString();
			crystal_Text.text = Game.current.generalStats.crystalRoundsAmount.ToString();
			miniAcorn_Text.text = Game.current.generalStats.seedBombAmount.ToString();
			float amountX = moveJoystick.position.x;
			amountX = Mathf.Clamp(amountX, -1, 1);
			if ( moveJoystick.position.x != 0 && moveJoystick.IsFingerDown())
			{
				turretRotator += turnSpeed * Time.deltaTime * amountX;
				turretRotator = Mathf.Clamp(turretRotator, -shootingArc, shootingArc);
				transform.rotation = Quaternion.Euler(new Vector3(0, turretRotator, 0));
				if(moveJoystick.position.x > 0)
					anthyObj.animation.CrossFade("WalkLeft");
				else
					anthyObj.animation.CrossFade("WalkRight");
			}
			else if(!GameMaster.cheering)
			{
				anthyObj.animation.CrossFade("Idle");
			}
			if (trigger_BTN.IsFingerDown() && Time.timeSinceLevelLoad > nextFireTime)
	        {
				nextFireTime = Time.timeSinceLevelLoad + fireSpeed;
	            shoot();
			}
			
			if(Game.current.generalStats.spiderAmount > 0)
			{
				if(spiderWeb_BTN.IsFingerDown() && spiderWeb_BTN.active)
				{
					Instantiate(spiderWebObj, muzzlePos[0].position, muzzlePos[0].rotation);
					spiderWeb_BTN.StartCoroutine("cooldown", 1);
					Game.current.generalStats.spiderAmount--;
				}
			}
			if(Game.current.generalStats.wormAmount > 0)
			{
				if(worm_BTN.IsFingerDown() && worm_BTN.active)
				{
					Instantiate(wormObj, new Vector3(muzzlePos[0].position.x, 0, muzzlePos[0].position.z), muzzlePos[0].rotation);
					worm_BTN.StartCoroutine("cooldown", 1);
					Game.current.generalStats.wormAmount--;
				}	
			}
			if(Game.current.generalStats.crystalRoundsAmount > 0)
			{
				if(crystalRound_BTN.IsFingerDown() && crystalRound_BTN.active)
				{
					Instantiate(crystalRoundObj, muzzlePos[0].position, muzzlePos[0].rotation);
					crystalRound_BTN.StartCoroutine("cooldown", 1);
					Game.current.generalStats.crystalRoundsAmount--;
				}	
			}
			if(Game.current.generalStats.seedBombAmount > 0)
			{
				if(miniAcornBomb_BTN.IsFingerDown() && miniAcornBomb_BTN.active)
				{
					Instantiate(miniAcornBombObj, muzzlePos[0].position, muzzlePos[0].rotation);
					miniAcornBomb_BTN.StartCoroutine("cooldown", 1);
					Game.current.generalStats.seedBombAmount--;
				}	
			}
	#if UNITY_EDITOR
			spiderWeb_Text.text = Game.current.generalStats.spiderAmount.ToString();
			worm_Text.text = Game.current.generalStats.wormAmount.ToString();
			crystal_Text.text = Game.current.generalStats.crystalRoundsAmount.ToString();
			miniAcorn_Text.text = Game.current.generalStats.seedBombAmount.ToString();
			float horMov = Input.GetAxis("Horizontal");
			float verMov = Input.GetAxis("Vertical");
			if(horMov != 0)
			{
				turretRotator += turnSpeed * Time.deltaTime * horMov;
				turretRotator = Mathf.Clamp(turretRotator, -shootingArc, shootingArc);
				transform.localRotation = Quaternion.Euler(new Vector3(0, turretRotator, 0));
				if(horMov > 0)
					anthyObj.animation.CrossFade("WalkLeft");
				else
					anthyObj.animation.CrossFade("WalkRight");
			}
			else if(!GameMaster.cheering)
			{
				anthyObj.animation.CrossFade("Idle");
			}
			/*poner cuando esta el menu entre niveles
			else
			{
				
			}
			*/
			if(verMov != 0)
			{
				turretPitch -= pitchSpeed * Time.deltaTime * verMov;
				turretPitch = Mathf.Clamp(turretPitch, -pitchArc, pitchArc);
				chamberObj.transform.localRotation = Quaternion.Euler(new Vector3(turretPitch, 0, 0));
			}
			else
			{
				chamberObj.transform.rotation = Quaternion.RotateTowards(chamberObj.transform.rotation, transform.rotation, pitchSpeed * Time.deltaTime);
				turretPitch = chamberObj.transform.localRotation.x * Mathf.Rad2Deg;
			}

			//borrar esta linea, es solo para testing
			if(Input.GetKey(KeyCode.P))
			   {
				Game.current.generalStats.totalCoins+=200;
				}

			if(Input.GetKey(KeyCode.Space) && Time.timeSinceLevelLoad > nextFireTime)
			{
				nextFireTime = Time.timeSinceLevelLoad + fireSpeed;
				shoot();
			}
			if(Game.current.generalStats.spiderAmount > 0)
			{
				if(Input.GetKey(KeyCode.Q) && spiderWeb_BTN.active)
				{
					Instantiate(spiderWebObj, muzzlePos[0].position, muzzlePos[0].rotation);
					spiderWeb_BTN.StartCoroutine("cooldown", 1);
					Game.current.generalStats.spiderAmount--;
				}
			}
			if(Game.current.generalStats.wormAmount > 0)
			{
				if(Input.GetKey(KeyCode.W) && worm_BTN.active)
				{
					Instantiate(wormObj, new Vector3(muzzlePos[0].position.x, 0, muzzlePos[0].position.z), muzzlePos[0].rotation);
					worm_BTN.StartCoroutine("cooldown", 1);
					Game.current.generalStats.wormAmount--;
				}	
			}
			if(Game.current.generalStats.crystalRoundsAmount > 0)
			{
				if(Input.GetKey(KeyCode.E) && crystalRound_BTN.active)
				{
					Instantiate(crystalRoundObj, muzzlePos[0].position, muzzlePos[0].rotation);
					crystalRound_BTN.StartCoroutine("cooldown", 1);
					Game.current.generalStats.crystalRoundsAmount--;
				}	
			}
			if(Game.current.generalStats.seedBombAmount > 0)
			{
				if(Input.GetKey(KeyCode.R) && miniAcornBomb_BTN.active)
				{
					Instantiate(miniAcornBombObj, muzzlePos[0].position, muzzlePos[0].rotation);
					miniAcornBomb_BTN.StartCoroutine("cooldown", 1);
					Game.current.generalStats.seedBombAmount--;
				}	
			}
			#endif
		}
		else
		{
			anthyObj.animation.CrossFade("Idle");
			transform.localRotation = Quaternion.RotateTowards(transform.localRotation, initialRotation, turnSpeed * Time.deltaTime);
			turretRotator = transform.localRotation.y * Mathf.Rad2Deg;
		}
	}
	
	public void shoot ()
	{
		if(GameManager.isPaused==false)
		{
			instantiateProjectile (muzzleID);
			if(Game.current.generalStats.turretSpeedLevel==3)
			{
				cannonNode.Rotate(0,0,90);
				if(muzzleID==3){
					muzzleID=0;
				}
				else {
					muzzleID++;
				}
			} 
			else if(Game.current.generalStats.turretSpeedLevel==2)
			{
				cannonNode.Rotate(0,0,180);
				if(muzzleID==0){
					muzzleID=2;
				} else if(muzzleID==2){
					muzzleID=0;
				}
			}
		}
	}

	public void instantiateProjectile(int muzzleid){
		Instantiate(shotParticles, muzzlePos[muzzleid].position, muzzlePos[muzzleid].rotation);
		Instantiate(bullet, muzzlePos[muzzleid].position, muzzlePos[muzzleid].rotation);
		muzzlePos[muzzleid].audio.PlayOneShot(shotSound);
	}

}