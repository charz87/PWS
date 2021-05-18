using UnityEngine;
using System.Collections;

public class Projectile_Behavior : MonoBehaviour
{
	float speed;
	public float lifetime;
	float Bulletdamage;

	void Start ()
	{
		speed = Game.current.generalStats.currentBulletSpeed;
		//speed=20;
		//Bulletdamage=50;
		Bulletdamage = Game.current.generalStats.currentBulletDamage;
		GameManager.localFiredBullets++;
		StartCoroutine("killByTime");
	}
	
	void FixedUpdate()
	{
		transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
	}

	public IEnumerator killByTime ()
	{
		yield return new WaitForSeconds(lifetime);
		Destroy(gameObject);
	}
	
	void OnCollisionEnter(Collision vCollision)
	{
		Health health = vCollision.collider.GetComponent<Health>();
		
		if (health)
		{
			Health.DamageInfo damageInfo = new Health.DamageInfo(Bulletdamage, vCollision.contacts[0].point, vCollision.contacts[0].normal);
			health.StartCoroutine("Damage", damageInfo);
			GameManager.localHitCount++;
			Destroy(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
