using UnityEngine;
using System.Collections;

public class SpiderWeb_PU : PowerUp
{
	public GameObject webTrap;
	
	void Update ()
	{
		transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
	}
	
	void OnCollisionEnter(Collision vCollision)
	{
		if(vCollision.collider.tag == "Enemy")
		{
			Collider[] inRange = Physics.OverlapSphere(transform.position, areaOfEffect, myLayer);
			foreach (Collider col in inRange)
			{
				Mushie_AI mushieComp = col.GetComponent<Mushie_AI>();
				Health mushieHealth = col.GetComponent<Health>();
				if(mushieComp)
				{
					if (mushieHealth.enemyType != Health.typeOfEnemy.giantPurpleMushie && mushieHealth.enemyType != Health.typeOfEnemy.brute_BOSS)
					{
						mushieComp.StartCoroutine("webRoutine", effectTime);
						GameObject myTrapObj = Instantiate(webTrap, col.transform.position, col.transform.rotation) as GameObject;
						myTrapObj.GetComponent<SpiderWebTrap>().StartCoroutine("timedOutDestroy", effectTime);
					}
				}
			}
			Destroy(gameObject);
		}
	}
}
