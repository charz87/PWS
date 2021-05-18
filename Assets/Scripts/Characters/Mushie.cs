using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]

public class Mushie : MonoBehaviour
{
	protected NavMeshAgent agent;

	public float walkSpeed = 2f;
	public float runSpeed = 6f;
	public float turnSpeed = 180f;
	public float awareRange = 5;
	public LayerMask myLayer;


	// Use this for initialization
	public virtual void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	public virtual void Update ()
	{
		
	}
}
