using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {
	
	public List<Node> neighbors;
	public bool buildable = true;
	
	void Start ()
	{
		CheckNeighbors(1);
	}
	
	public void CheckNeighbors(float checkDistance)
	{
		Collider[] closeOjbs = Physics.OverlapSphere(transform.position, checkDistance*2.8284f);
		foreach(Collider coll in closeOjbs)
		{
			Node collNode = coll.GetComponent<Node>();
			if(collNode != null)
			{
				if(coll != this.collider)
					neighbors.Add(collNode);
			}
		}
	}
	
	void OnDrawGizmos()
	{
		Gizmos.DrawIcon (transform.position, "");
	}
}
