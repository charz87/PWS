using UnityEngine;
using System.Collections;

public class miniAcornBomb_PU : PowerUp
{
	public GameObject explossion;
	
	void Update ()
	{
		transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
	}

	void OnCollisionEnter(Collision vCollision)
	{
		if(vCollision.collider.tag != "Environment")
		{
			Instantiate(explossion, vCollision.transform.position, Quaternion.identity);
			Collider[] inRange = Physics.OverlapSphere(transform.position, areaOfEffect, myLayer);
			foreach (Collider col in inRange)
			{
				Health mushieHealth = col.GetComponent<Health>();
				if(mushieHealth)
				{
					Health.DamageInfo damageInfo = new Health.DamageInfo(100, col.transform.position, col.transform.position);
					mushieHealth.StartCoroutine("Damage", damageInfo);
				}
			}
			Destroy(gameObject);
		}
	}
}
