using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
	bool go = false;
	int healthSpeed = 4;
	GameObject anthy;
	GameObject brute;
	GameManager myGameManager;
	
	public GameObject boomParticles;

	void Start()
	{
		anthy = GameObject.Find("Character");
		brute = GameObject.Find("Brute(Clone)");
		myGameManager = GameObject.Find("_GameManager").GetComponent<GameManager>();
		StartCoroutine("kaboomBaby");
	}
	void OnCollisionEnter (Collision vCollision)
	{
		if(vCollision.collider.tag == "Environment")
		{
			rigidbody.velocity = Vector3.zero;
			go = true;
		}
		else if(vCollision.collider.tag == "Bullet")
		{
			if(Game.current.generalStats.currentBulletDamage == 50)
			{
				healthSpeed -= 1;
			}
			else if(Game.current.generalStats.currentBulletDamage == 100)
			{
				healthSpeed -= 2;
			}
			else if(Game.current.generalStats.currentBulletDamage == 150)
			{
				healthSpeed -= 3;
			}
		}
		else if(vCollision.collider == brute.collider || vCollision.collider == anthy.collider || vCollision.collider.tag == "Enemy")
		{
			instaBoom();	
		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(go)
		{
			if(healthSpeed > 0)
			{
				transform.position = Vector3.MoveTowards(transform.position, anthy.transform.position, healthSpeed*Time.deltaTime*.75f);
			}
			else if(healthSpeed < 0 && GameManager.bossAlive)
			{
				transform.position = Vector3.MoveTowards(transform.position, brute.transform.position, -healthSpeed*Time.deltaTime*.75f);
			}
			else if(!GameManager.bossAlive)
			{
				instaBoom();
			}
		}
		if(!brute)
		{
			Instantiate(boomParticles, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
	
	IEnumerator kaboomBaby()
	{
		yield return new WaitForSeconds(10);
		Instantiate(boomParticles, transform.position, Quaternion.identity);
		Collider[] damagedObjs = Physics.OverlapSphere(transform.position, 3);
		foreach (Collider col in damagedObjs)
		{
			Health healthComp = col.GetComponent<Health>();
			if(healthComp)
			{
				Health.DamageInfo damageInfo = new Health.DamageInfo(500, transform.position, transform.position);
				healthComp.StartCoroutine("Damage", damageInfo);
			}
			if(col.name == "Character")
			{
				GameManager.hitPoints = Mathf.Clamp (GameManager.hitPoints - 2, 0, 10);
				myGameManager.updateGUI();
				myGameManager.StartCoroutine(myGameManager.checkRoundState(myGameManager.nextRoundMenu));
			}
		}
		Destroy(gameObject);
	}
	
	void instaBoom()
	{
		Instantiate(boomParticles, transform.position, Quaternion.identity);
		Collider[] damagedObjs = Physics.OverlapSphere(transform.position, 3);
		foreach (Collider col in damagedObjs)
		{
			Health healthComp = col.GetComponent<Health>();
			if(healthComp)
			{
				Health.DamageInfo damageInfo = new Health.DamageInfo(500, transform.position, transform.position);
				healthComp.StartCoroutine("Damage", damageInfo);
			}
			if(col.name == "Character")
			{
				GameManager.hitPoints = Mathf.Clamp (GameManager.hitPoints - 2, 0, 10);
				myGameManager.updateGUI();
				myGameManager.StartCoroutine(myGameManager.checkRoundState(myGameManager.nextRoundMenu));
			}
		}
		Destroy(gameObject);
	}
}
