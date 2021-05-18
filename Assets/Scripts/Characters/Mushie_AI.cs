using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mushie_AI : Mushie
{
	//Unit stats
	private Vector3 startPosition = Vector3.zero;
	//Unit variables
	private GameManager myGameManager;
	private Health myHealth;
	//Pathfinding
	public PathNav path;
	public Transform pathnode;
	//Blue routine Mats
	public Material stealthMat;
	public Material normalMat;
	public Renderer[] matChangeParts;
	//GiantMushie
	Mushie_AI miniMushie;
	List<GameObject> allyTargets = new List<GameObject>();
	GameObject allyTarget;
	public Transform hereToKick;
	//Boss
	private float distanceTraveled = 0;
	private int timesStopped = 0;
	public GameObject[] thrownMushies;
	public GameObject thrownBomb;
	public Transform bruteHand;
	public GameObject startEffect;
	// Use this for initialization
	public override void Start ()
	{
		base.Start ();
		//Get Components
		myGameManager = GameObject.Find("_GameManager").GetComponent<GameManager>();
		myHealth = GetComponent<Health>();
		
		path = GameObject.Find("EndOfTheLine").GetComponent<PathNav>();
		agent.angularSpeed = turnSpeed;
		agent.speed = walkSpeed;
		
		animation["Walk"].speed = walkSpeed;
		startPosition = transform.position;
		StartCoroutine (Think());
		if(myHealth && myHealth.enemyType == Health.typeOfEnemy.brute_BOSS)
			StartCoroutine("accountDistance");
		if(startEffect != null)
		{
			startEffect.transform.parent = null;
		}
	}
	
	// Update is called once per frame
	public override void Update ()
	{	
		base.Update ();	
	}
	
	public IEnumerator webRoutine(float effectTime)
	{
		float currentSpeed = agent.speed;
		agent.speed = 0;
		animation.CrossFade("Trapped");
		yield return new WaitForSeconds(effectTime);
		agent.speed = currentSpeed;
		animation.CrossFade("Walk");
	}
	
	//For Boss Only
	public IEnumerator accountDistance()
	{
		GameManager.bossAlive = true;
		GameManager.epicMode = false;
		yield return new WaitForSeconds(.1f);
		while(myHealth.health > 0)
		{
			distanceTraveled += walkSpeed * Time.deltaTime;
			distanceTraveled = Mathf.Clamp(distanceTraveled, 0, 2);
			if(distanceTraveled == 2)
			{
				agent.speed = 0;
				/*if(timesStopped != 2)
				{
					for (int i = 0; i < 3; i++)
					{
						animation.CrossFade("Throw");
						yield return new WaitForSeconds(animation["Throw"].length);
						if(myHealth.health != 0)
						{
							if(myHealth.health > myHealth.healtMax * 0.5f)
							{
								//transform.LookAt(transform.position + new Vector3(Random.Range(-1,1),0,Random.Range(-1,1)));
								GameObject newThrownMushie = Instantiate(thrownMushies[0], bruteHand.position, bruteHand.rotation) as GameObject;
								newThrownMushie.rigidbody.AddForce(transform.forward*300 + Vector3.up*250);
							}
							else
							{
								//transform.LookAt(transform.position + new Vector3(Random.Range(-1,1),0,Random.Range(-1,1)));
								GameObject newThrownMushie = Instantiate(thrownMushies[Random.Range(0, thrownMushies.Length)], bruteHand.position, bruteHand.rotation) as GameObject;
								newThrownMushie.rigidbody.AddForce(transform.forward*300 + Vector3.up*300);
							}
						}
					}
				}
				else
				{*/
				transform.LookAt(Vector3.zero);
				animation.CrossFade("Throw");
				if(myHealth.health != 0)
				{
					if(myHealth.health > myHealth.healtMax * 0.5f)
					{
						yield return new WaitForSeconds(animation["Throw"].length);
						GameObject newThrownMushie = Instantiate(thrownBomb, bruteHand.position, bruteHand.rotation) as GameObject;
						newThrownMushie.rigidbody.AddForce(-Vector3.forward*200 + Vector3.up*300);
					}
					else
					{
						GameManager.epicMode = true;
						for (int i = 0; i < 2; i++)
						{
							yield return new WaitForSeconds(animation["Throw"].length);
							GameObject newThrownMushie = Instantiate(thrownBomb, bruteHand.position, bruteHand.rotation) as GameObject;
							newThrownMushie.rigidbody.AddForce(-Vector3.forward*200 + Vector3.up*300);
						}
					}
				}
				//timesStopped = 0;
				//}
				animation.CrossFade("Walk");
				agent.speed = walkSpeed;
				distanceTraveled = 0;
				//timesStopped++;
			}
			yield return null;
		}
	}

	IEnumerator Think()
	{
		while(true)
		{
			if (path || pathnode)
				yield return StartCoroutine(FollowPath());
			
			yield return null; //Siempre se tiene que poner esto paa que no sea un ciclo infinito & no se trabe ///null significa que regresara cada frame
			
		}
	}

	IEnumerator FollowPath()
	{
		//Debug.Log("I have a path");
		agent.speed = walkSpeed;
		
		if (!pathnode)
		{
			pathnode = path.GetClosest(transform.position);
		}
		
		while (pathnode)
		{
			agent.SetDestination (pathnode.position);
			
			while (Vector3.Distance (transform.position, pathnode.position)>2f)
			{
				yield return new WaitForSeconds (1f);
			}
			
			if (path)
				pathnode = path.GetNext(pathnode);
			else 
				pathnode = null;
			
			yield return null;
		}
	}

	public IEnumerator stealthRoutine()
	{
		collider.enabled = false;
		foreach (Renderer myRenderer in matChangeParts)
			myRenderer.material = stealthMat;
		agent.speed = runSpeed;
		animation["Walk"].speed = runSpeed;
		yield return new WaitForSeconds(1);
		collider.enabled = true;
		foreach (Renderer myRenderer in matChangeParts)
			myRenderer.material = normalMat;
		agent.speed = walkSpeed;
		animation["Walk"].speed = walkSpeed;
	}

	public IEnumerator rageRoutine(float waitTime)
	{
		agent.speed = 0;
		yield return new WaitForSeconds(waitTime);
		agent.speed = runSpeed;
		animation["Walk"].speed = runSpeed;
		animation.CrossFade("Walk");
	}
	
	public IEnumerator throwRoutine()
	{
		while(agent.speed != runSpeed)
		{
			if(!allyTarget)
			{
				
				allyTargets.Clear();
				Collider[] colliders = Physics.OverlapSphere(transform.position, awareRange, myLayer);
				foreach (Collider c in colliders)
				{
					if (c == collider)
						continue;				
					allyTargets.Add(c.gameObject);
				}
				
				if (allyTargets.Count>1)
				{
					allyTargets.Sort (SortGameObjectsByDistance);
				}
				
				if (allyTargets.Count > 0)
				{
					agent.speed = 0;
					if(allyTargets[0] && allyTargets[0].GetComponent<Health>().enemyType != Health.typeOfEnemy.giantPurpleMushie)
					{
						allyTarget = allyTargets[0];
						allyTarget.collider.enabled = false;
						miniMushie = allyTarget.GetComponent<Mushie_AI>();
						miniMushie.agent.speed = 0;
						animation.CrossFade("Kick");
						allyTarget.animation.CrossFade("Ready");
						allyTarget.transform.position = hereToKick.position;
						yield return new WaitForSeconds(animation["Kick"].length);
						allyTarget.animation["Roll"].speed = miniMushie.walkSpeed*3;
						allyTarget.animation.CrossFade("Roll");
						miniMushie.agent.speed = miniMushie.walkSpeed*3;
						animation.CrossFade("Walk");
						agent.speed = walkSpeed;
						agent.angularSpeed = 120;
                        yield return new WaitForSeconds(2);
                        miniMushie.agent.speed = miniMushie.walkSpeed;
                        allyTarget.animation.CrossFade("Walk");
                        allyTarget.collider.enabled = true;
                        allyTarget = null;
					}
				}
				yield return new WaitForSeconds (1f);
			}
			yield return null;
		}
	}
	
	int SortGameObjectsByDistance(GameObject a, GameObject b)
	{
		if (!a || b)
			return 0;
		
		float ad = Vector3.Distance (transform.position, a.transform.position);
		float bd = Vector3.Distance (transform.position, b.transform.position);
		
		if (ad < bd)
		{
			return -1;
		}
		
		return 1;
		
	}
}
