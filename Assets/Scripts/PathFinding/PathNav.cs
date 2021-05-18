using UnityEngine;
using System.Collections;

public class PathNav : MonoBehaviour {

	// Use this for initialization
	public Transform[] pathNodes;
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	public Transform GetClosest(Vector3 position)
	{
		if(pathNodes.Length == 0)
			return null;
		if(pathNodes.Length == 1)
			return pathNodes[0];
		
		Transform closest = null;
		float distance = 0f;
		
		foreach(Transform pathnode in pathNodes)
		{
			float d = Vector3.Distance(position, pathnode.position);
			
			if(!closest || d < distance)
			{
				closest = pathnode;
				distance = d;
			}
		}
		
		return closest;
	}
	
	public Transform GetNext (Transform current)
	{
		if(pathNodes.Length == 0)
			return null;
		if(pathNodes.Length == 1)
			return pathNodes[0];
		if(!current)
			return pathNodes[0];
		
		int index = System.Array.IndexOf (pathNodes, current);
		
		if(index == -1)
			return pathNodes[0];
		
		index++;
		
		if (index >= pathNodes.Length)
			return pathNodes[0];
		
		return pathNodes[index];
	}
}
