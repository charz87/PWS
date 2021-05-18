using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class NodeMap
{
	public static int xSize = 10;
	public static int zSize = 10;
	public static float spaceBetween = 2;
	public Node[] nodes;
	
	[MenuItem("Nerizzo/CreateNodeMap")]
	public static void Create()
	{
		GameObject myMap = new GameObject("NodeMap");
		float xpos = 0;
		float zpos = 0;
		for (int i = 0; i<xSize; i++)
		{
			for (int j = 0; j<zSize; j++)
			{
				GameObject go = new GameObject("Node", typeof(Node));
				go.AddComponent<BoxCollider>();
				go.collider.isTrigger = true;
				go.GetComponent<BoxCollider>().size = new Vector3(.2f, .2f, .2f);
				go.tag = "Nodes";
				go.transform.position = new Vector3(xpos, 0, zpos);
				go.transform.parent = myMap.transform;
				zpos += spaceBetween;
			}
			zpos = 0;
			xpos += spaceBetween;
		}
	}
	
	/*[MenuItem("MyTools/DeleteCurrentNodes")]
	public static void Delete()
	{
		GameObject[] destroyable = GameObject.FindGameObjectsWithTag("Nodes");
		
		foreach(GameObject go in destroyable)
		{
			GameObject.DestroyImmediate(go);
		}
	}*/
}
