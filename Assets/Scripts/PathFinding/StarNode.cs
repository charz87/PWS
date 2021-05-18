using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarNode : SearchNode {
	
	public float fx;
	public float gx;
	public float hx;
	
	public StarNode(LinkedList<Node> parentList, Node current, StarNode parent) : base(parentList, current) {
		
		if(parent == null)
		{
			gx = 0;
		}
		else
		{	
			float distance = Vector3.Distance(current.transform.position,
						parent.current.transform.position);
			
			gx = parent.gx + distance;
		}
	}
	
	public float updateFx(Node target)
	{
		hx = Vector3.Distance(current.transform.position, target.transform.position);
		fx = gx + hx;
		return fx;
	}
	
}
