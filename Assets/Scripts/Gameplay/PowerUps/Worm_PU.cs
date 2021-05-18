using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Worm_PU : PowerUp
{
	GameObject target;
	List<GameObject> enemies = new List<GameObject>();
	NavMeshAgent agent;
	public int repetitions;
	public GameObject animatedObj;
	
	public GameObject venomPond;
	
	// Use this for initialization
	void Start ()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.speed = speed;
		getNearest();
		animatedObj.animation["Roll"].speed = speed;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(target)
		{
			agent.SetDestination(target.transform.position);
		}
		else
		{
			getNearest();
		}
	}
	
	public void getNearest()
	{
		GameObject[] nearest = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(GameObject go in nearest)
		{
			enemies.Add(go);
		}
		enemies.Sort (SortGameObjectsByDistance);
		if(enemies.Count != 0)
			target = enemies[0];
		enemies.Clear();
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
	
	void OnCollisionEnter(Collision vCollision)
	{
		if(vCollision.collider.tag == "Enemy")
		{
			Health myHealth = vCollision.gameObject.GetComponent<Health>();
			if(myHealth)
			{
				Health.DamageInfo damageInfo = new Health.DamageInfo(100, vCollision.contacts[0].point, vCollision.contacts[0].normal);
				myHealth.StartCoroutine("Damage", damageInfo);
				GameObject myTrap = Instantiate(venomPond, transform.position, Quaternion.identity) as GameObject;
				WormTrap myTrapComponent = myTrap.GetComponent<WormTrap>();
				myTrapComponent.thisLayer = myLayer;
				myTrapComponent.repetitions = repetitions;
				myTrapComponent.range = areaOfEffect;
				Destroy(gameObject);
			}
		}
	}
}
