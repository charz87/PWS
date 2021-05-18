using UnityEngine;
using System.Collections;

public class CrystalRound_PU : PowerUp
{
	public float Bulletdamage = 50f;
	public int impactsLeft = 1;
	int decreaseRate;
	
	void Start ()
	{
		decreaseRate = Mathf.RoundToInt(Bulletdamage / (impactsLeft+1));
		StartCoroutine("killByTime");
	}
	
	void Update()
	{
		transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
	}
	
	public IEnumerator killByTime ()
	{
		yield return new WaitForSeconds(duration);
		Destroy(gameObject);
	}
	
	void OnCollisionEnter(Collision vCollision)
	{
		Health health = vCollision.collider.GetComponent<Health>();
		
		if (health)
		{
			Health.DamageInfo damageInfo = new Health.DamageInfo(Bulletdamage, vCollision.contacts[0].point, vCollision.contacts[0].normal);
			health.StartCoroutine("Damage", damageInfo);
			if(impactsLeft == 0)
			{
				Destroy(gameObject);
			}
			else
			{
				impactsLeft--;
				Bulletdamage = Bulletdamage - decreaseRate;
			}
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
