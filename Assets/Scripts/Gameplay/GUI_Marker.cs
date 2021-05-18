using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GUITexture))]
public class GUI_Marker : MonoBehaviour {

	public GameObject target;
	public bool show;
	Vector3 offset = new Vector3(0.0f, 0.5f, 0.0f);
 
    void Update()
    {
		if(show)
		{
			if(!guiTexture.enabled)
				guiTexture.enabled = true;
		}
		else
		{
			if(guiTexture.enabled)
				guiTexture.enabled = false;
		}
 		if (target == null)
		{
			Destroy(this.gameObject);
		}
		else
		{
			Vector3 pos = target.transform.position + offset;
			transform.position = Camera.main.WorldToViewportPoint(pos);
        }
    }
}
