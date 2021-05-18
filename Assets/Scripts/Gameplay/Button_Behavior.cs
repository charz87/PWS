using UnityEngine;
using System.Collections;

[System.Serializable]
public class Button_Behavior : MonoBehaviour
{
	public enum typeOfAction
	{
		turnOnObject,
		turnOffObject,
		switchObject,
		createObject,
        startRoutine,
        turnOnCollider,
        turnOffCollider,
        turnOnRenderer,
        turnOffRenderer,
        turnOnColliderAndRenderer,
        turnOffColliderAndRenderer,
        destroyObj,
        none
    }
    public bool loadScene;
	public string sceneName;
    
    public bool severalActions;
    	
	public typeOfAction action;
	public typeOfAction[] actions;
	
	public bool severalObjects;
	
	public GameObject objectToInteract;
	public GameObject[] objectsToInteract;
	
	public GameObject objectToCreate;
	
	public bool addActionDelay;
	public float actionDelay;
	public float[] actionsDelay;
	
	public string routineName;
	
	public bool playAudio;
	public AudioClip audioToPlay;
	
	public bool playAnimation;
	public string animationToPlay;
		
	public bool worksOnce;
	
	public bool destroySelf;
	public float selfDestroyDelay;
	
    public bool addAudioDelay;
    public bool addAnimationDelay;
    
    public bool blink;
    public float blinkRate;
    
    void Start()
    {
		if(blink)
		{
			StartCoroutine("blinkRoutine");
		}
    }
    
    public IEnumerator blinkRoutine()
    {
		while(blink)
		{
			if(renderer.enabled)
			{
				yield return new WaitForSeconds(blinkRate);
				renderer.enabled = false;
			}
			else
			{
				yield return new WaitForSeconds(blinkRate*.75f);
				renderer.enabled = true;
			}	
		}
    }
    
	void OnMouseDown ()
	{
		StartCoroutine("buttonAction");
    }
    
    public IEnumerator buttonAction()
    {
		if(loadScene)
		{
			Application.LoadLevel(sceneName);
		}
		else
		{
			if(severalActions)
			{
				for (int i = 0; i<actions.Length; i++)
				{
					if(addActionDelay)
					{
						yield return new WaitForSeconds(actionsDelay[i]);
					}
					if(severalObjects)
					{
						switch(actions[i])
						{
						case typeOfAction.turnOnObject:
							objectsToInteract[i].SetActive(true);
							break;
						case typeOfAction.turnOffObject:
							objectsToInteract[i].SetActive(false);
							break;
						case typeOfAction.switchObject:
							objectsToInteract[i].SetActive(!objectsToInteract[i].activeSelf);
							break;
						case typeOfAction.createObject:
							Instantiate(objectToCreate, transform.position, transform.rotation);
							break;
						case typeOfAction.startRoutine:
							objectsToInteract[i].SendMessage(routineName);
							break;
						case typeOfAction.turnOnCollider:
							objectsToInteract[i].collider.enabled = true;
							break;
						case typeOfAction.turnOffCollider:
							objectsToInteract[i].collider.enabled = false;
							break;
						case typeOfAction.turnOnRenderer:
							objectsToInteract[i].renderer.enabled = true;
							break;
						case typeOfAction.turnOffRenderer:
							objectsToInteract[i].renderer.enabled = false;
							break;
						case typeOfAction.turnOnColliderAndRenderer:
							objectsToInteract[i].collider.enabled = true;
							objectsToInteract[i].renderer.enabled = true;
							break;
						case typeOfAction.turnOffColliderAndRenderer:
							objectsToInteract[i].collider.enabled = false;
							objectsToInteract[i].renderer.enabled = false;
							break;
						case typeOfAction.destroyObj:
							Destroy(objectsToInteract[i]);
							break;
						}
					}
					else
					{
						switch(actions[i])
						{
						case typeOfAction.turnOnObject:
							objectToInteract.SetActive(true);
							break;
						case typeOfAction.turnOffObject:
							objectToInteract.SetActive(false);
							break;
						case typeOfAction.switchObject:
							objectToInteract.SetActive(!objectToInteract.activeSelf);
							break;
						case typeOfAction.createObject:
							Instantiate(objectToCreate, transform.position, transform.rotation);
							break;
						case typeOfAction.startRoutine:
							objectToInteract.SendMessage(routineName);
							break;
						case typeOfAction.turnOnCollider:
							objectToInteract.collider.enabled = true;
							break;
						case typeOfAction.turnOffCollider:
							objectToInteract.collider.enabled = false;
							break;
						case typeOfAction.turnOnRenderer:
							objectToInteract.renderer.enabled = true;
							break;
						case typeOfAction.turnOffRenderer:
							objectToInteract.renderer.enabled = false;
							break;
						case typeOfAction.turnOnColliderAndRenderer:
							objectToInteract.collider.enabled = true;
							objectToInteract.renderer.enabled = true;
							break;
						case typeOfAction.turnOffColliderAndRenderer:
							objectToInteract.collider.enabled = false;
							objectToInteract.renderer.enabled = false;
							break;
						case typeOfAction.destroyObj:
							Destroy(objectToInteract);
							break;
						}
					}
				}
			}
			else
			{
				if(addActionDelay)
				{
					yield return new WaitForSeconds(actionDelay);
				}
				
				if(severalObjects)
				{
					for (int i = 0; i<objectsToInteract.Length; i++)
					{
						switch(action)
						{
						case typeOfAction.turnOnObject:
							objectsToInteract[i].SetActive(true);
							break;
						case typeOfAction.turnOffObject:
							objectsToInteract[i].SetActive(false);
							break;
						case typeOfAction.switchObject:
							objectsToInteract[i].SetActive(!objectsToInteract[i].activeSelf);
							break;
						case typeOfAction.createObject:
							Instantiate(objectToCreate, transform.position, transform.rotation);
							break;
						case typeOfAction.startRoutine:
							objectsToInteract[i].SendMessage(routineName);
							break;
						case typeOfAction.turnOnCollider:
							objectsToInteract[i].collider.enabled = true;
							break;
						case typeOfAction.turnOffCollider:
							objectsToInteract[i].collider.enabled = false;
							break;
						case typeOfAction.turnOnRenderer:
							objectsToInteract[i].renderer.enabled = true;
							break;
						case typeOfAction.turnOffRenderer:
							objectsToInteract[i].renderer.enabled = false;
							break;
						case typeOfAction.turnOnColliderAndRenderer:
							objectsToInteract[i].collider.enabled = true;
							objectsToInteract[i].renderer.enabled = true;
							break;
						case typeOfAction.turnOffColliderAndRenderer:
							objectsToInteract[i].collider.enabled = false;
							objectsToInteract[i].renderer.enabled = false;
							break;
						case typeOfAction.destroyObj:
							Destroy(objectsToInteract[i]);
							break;
						}
					}
				}
				else
				{
					switch(action)
					{
					case typeOfAction.turnOnObject:
						objectToInteract.SetActive(true);
						break;
					case typeOfAction.turnOffObject:
						objectToInteract.SetActive(false);
						break;
					case typeOfAction.switchObject:
						objectToInteract.SetActive(!objectToInteract.activeSelf);
						break;
					case typeOfAction.createObject:
						Instantiate(objectToCreate, transform.position, transform.rotation);
						break;
					case typeOfAction.startRoutine:
						objectToInteract.SendMessage(routineName);
						break;
					case typeOfAction.turnOnCollider:
						objectToInteract.collider.enabled = true;
						break;
					case typeOfAction.turnOffCollider:
						objectToInteract.collider.enabled = false;
						break;
					case typeOfAction.turnOnRenderer:
						objectToInteract.renderer.enabled = true;
						break;
					case typeOfAction.turnOffRenderer:
						objectToInteract.renderer.enabled = false;
						break;
					case typeOfAction.turnOnColliderAndRenderer:
						objectToInteract.collider.enabled = true;
						objectToInteract.renderer.enabled = true;
						break;
					case typeOfAction.turnOffColliderAndRenderer:
						objectToInteract.collider.enabled = false;
						objectToInteract.renderer.enabled = false;
						break;
					case typeOfAction.destroyObj:
						Destroy(objectToInteract);
						break;
					}
				}
			}
			if(playAudio)
			{
				audio.PlayOneShot(audioToPlay);
				if(addAudioDelay)
				{
					yield return new WaitForSeconds(audioToPlay.length);
				}
			}
			if(playAnimation)
			{
				animation.CrossFade(animationToPlay);
				if(addAnimationDelay)
				{
					yield return new WaitForSeconds(animation[animationToPlay].length);
				}
			}
			if(worksOnce)
			{
				gameObject.collider.enabled = false;
				if(destroySelf)
				{
					yield return new WaitForSeconds(selfDestroyDelay);
					Destroy(gameObject);
				}
			}
			
		}
    }
}
