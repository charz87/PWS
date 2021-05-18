using UnityEngine;
using System.Collections;

public class WormTrap : MonoBehaviour
{
	public int repetitions;
	public float range;
	public LayerMask thisLayer;
	
	void Start()
	{
		StartCoroutine("poison");
	}
	// Use this for initialization
	public IEnumerator poison()
	{
		yield return new WaitForSeconds(1);
		for (int i = 0; i<repetitions; i++)
		{
			Collider[] inRange = Physics.OverlapSphere(transform.position, range, thisLayer);
			foreach (Collider col in inRange)
			{
				Health mushieHealth = col.GetComponent<Health>();
				if(mushieHealth)
				{
					Health.DamageInfo damageInfo = new Health.DamageInfo(25, col.transform.position, col.transform.position);
					mushieHealth.StartCoroutine("Damage", damageInfo);
				}
			}
			yield return new WaitForSeconds(1);
		}
		Destroy(gameObject);
	}
}
