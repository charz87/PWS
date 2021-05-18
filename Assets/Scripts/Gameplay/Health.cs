using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Mushie_AI))]

public class Health : MonoBehaviour
{
	NavMeshAgent agent;
	Mushie_AI myMushie_AI;

	public enum typeOfEnemy
	{
		purpleMushie,
		redMushie,
		blueMushie,
		giantPurpleMushie,
		brute_BOSS
	}

	public typeOfEnemy enemyType;

	public float healtMax = 100f;
	public int damage;
	[HideInInspector]
	public float health = 0f;

	public AudioClip bornSound;
	public AudioClip dieSound;
	public AudioClip damageSound;
	public GameObject hitEffect;
	public GameObject dieEffect;
	public GameObject damageEffect;
	public string[] animationToPlay;
	public GameObject lifeItem;
	
	public int awardedPoints;
	
	GameManager myGameManager;

	bool doneAction = false;
	
	//CLASE! :D 
	public class DamageInfo
	{
		public float damage = 0f;
		public Vector3 position = Vector3.zero;
		public Vector3 normal = Vector3.zero;
		//Mushie attacker = null;
		
		public DamageInfo(float _damage,Vector3 _position,  Vector3 _normal)//, Mushie _attacker)
		{
			damage = _damage;
			position = _position;
			normal = _normal;
			//attacker = _attacker;
		}
	}
	// Use this for initialization
	void Start ()
	{	
		int randInt = Random.Range(0, 10);
		if(randInt < 3)
			audio.PlayOneShot(bornSound);
		agent = GetComponent<NavMeshAgent>();
		myMushie_AI = GetComponent<Mushie_AI>();
		health = healtMax;
		GameManager.addRemoveMapUnit(true);
		myGameManager = GameObject.Find("_GameManager").GetComponent<GameManager>();
		if(enemyType == typeOfEnemy.giantPurpleMushie)
			myMushie_AI.StartCoroutine("throwRoutine");
	}
	
	public IEnumerator Damage(DamageInfo damageInfo)
	{
		if (health != -1f)
		{
			health = Mathf.Clamp (health - damageInfo.damage, 0f, healtMax);
			//Debug.Log("Me pegaron y me bajaron " + damageInfo.damage);
			//Debug.Log("Mi vida ahora es" + health);
		}
		
		if (hitEffect && damageInfo.damage >0f)
		{
			Instantiate(hitEffect, damageInfo.position, Quaternion.LookRotation(damageInfo.normal));
		}

		switch(enemyType)
		{
		case typeOfEnemy.blueMushie:
			if (health <= healtMax * 0.5f && !doneAction)
			{
				myMushie_AI.StartCoroutine("stealthRoutine");
				doneAction = true;
			}
			break;
		case typeOfEnemy.giantPurpleMushie:
			if (health <= healtMax * 0.3f && !doneAction)
			{
				animation.CrossFade("Entrance");
				myMushie_AI.StartCoroutine(myMushie_AI.rageRoutine(animation["Entrance"].length));
				doneAction = true;
			}
			break;
		}
		
		if (health == 0f)
		{
			if(enemyType == typeOfEnemy.brute_BOSS)
			{
				myMushie_AI.StopCoroutine("accountDistance");
			}
			agent.speed = 0;
			animation.CrossFade(animationToPlay[Random.Range(0, animationToPlay.Length)]);
			audio.PlayOneShot(dieSound);
			collider.enabled = false;
			yield return new WaitForSeconds(animation[animationToPlay[Random.Range(0, animationToPlay.Length)]].length);
			GameManager.addRemoveMapUnit(false);
			myGameManager.StartCoroutine(myGameManager.checkRoundState(myGameManager.nextRoundMenu));
			GameManager.increaseScore(awardedPoints);
			Instantiate(dieEffect, transform.position, Quaternion.identity);
			if(GameManager.bossAlive && enemyType == typeOfEnemy.brute_BOSS)
			{
				GameObject.Find("Invoker").GetComponent<Invoker>().active = false;	
			}
			int randInt = Random.Range(0,10);
			//int randInt = 0;
			/*if(randInt < 3)
				Instantiate(lifeItem, transform.position + new Vector3(0, 0.2f, 0), lifeItem.transform.rotation);*/
			Destroy(gameObject);
		}
		yield return null;	
	}
	
	void OnTriggerEnter(Collider vCollider)
	{
		if(vCollider.name.Contains("OutOfRange"))
		{
			Instantiate(damageEffect, transform.position, transform.rotation);
			audio.PlayOneShot(damageSound);
			GameManager.addRemoveMapUnit(false);
			GameManager.damageReceived++;
			GameManager.hitPoints = Mathf.Clamp (GameManager.hitPoints - damage, 0, 10);
			myGameManager.updateGUI();
			myGameManager.StartCoroutine(myGameManager.checkRoundState(myGameManager.nextRoundMenu));
			//Destroy(gameObject);
		}
		else if(vCollider.name.Contains("EndOfTheLine"))
		{
			//GameManager.addRemoveMapUnit(false);
			//GameManager.damageReceived++;
			//GameManager.hitPoints = Mathf.Clamp (GameManager.hitPoints - damage, 0, 10);
			//myGameManager.updateGUI();
			//myGameManager.StartCoroutine(myGameManager.checkRoundState(myGameManager.nextRoundMenu));
			Destroy(gameObject);
		}
		
	}
}
