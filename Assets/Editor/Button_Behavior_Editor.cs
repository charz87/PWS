using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Button_Behavior))]
public class Button_Behavior_Editor : Editor
{
	
	int createsObjCount = 0;
	int showsRoutineNameCount = 0;
	
	
	public SerializedProperty 
		loadScene_Prop,
		sceneName_Prop,
		severalActions_Prop,
		action_Prop,
		actions_Prop,
		severalObjects_Prop,
		objectToInteract_Prop,
		objectsToInteract_Prop,
		objectToCreate_Prop,
		addActionDelay_Prop,
		actionDelay_Prop,
		actionsDelay_Prop,
		routineName_Prop,
		playAudio_Prop,
		audioToPlay_Prop,
		playAnimation_Prop,
		animationToPlay_Prop,
		worksOnce_Prop,
		destroySelf_Prop,
		selfDestroyDelay_Prop,
		addAudioDelay_Prop,
		addAnimationDelay_Prop,
		blink_Prop,
		blinkRate_Prop;
		
	void OnEnable () {
		// Setup the SerializedProperties
		loadScene_Prop = serializedObject.FindProperty ("loadScene");
		sceneName_Prop = serializedObject.FindProperty ("sceneName");
		
		severalActions_Prop = serializedObject.FindProperty ("severalActions");
		action_Prop = serializedObject.FindProperty ("action");
		actions_Prop = serializedObject.FindProperty ("actions");
		
		severalObjects_Prop = serializedObject.FindProperty ("severalObjects");
		objectToInteract_Prop = serializedObject.FindProperty ("objectToInteract");
		objectsToInteract_Prop = serializedObject.FindProperty ("objectsToInteract");
		
		objectToCreate_Prop = serializedObject.FindProperty ("objectToCreate");
		
		addActionDelay_Prop = serializedObject.FindProperty ("addActionDelay");
		actionDelay_Prop = serializedObject.FindProperty ("actionDelay");
		actionsDelay_Prop = serializedObject.FindProperty ("actionsDelay");
		
		routineName_Prop = serializedObject.FindProperty ("routineName");
		
		playAudio_Prop = serializedObject.FindProperty ("playAudio");
		audioToPlay_Prop = serializedObject.FindProperty ("audioToPlay");
		
		playAnimation_Prop = serializedObject.FindProperty ("playAnimation");
		animationToPlay_Prop = serializedObject.FindProperty ("animationToPlay");
		
		worksOnce_Prop = serializedObject.FindProperty ("worksOnce");
		destroySelf_Prop = serializedObject.FindProperty ("destroySelf");
		selfDestroyDelay_Prop = serializedObject.FindProperty ("selfDestroyDelay");
		
		addAudioDelay_Prop = serializedObject.FindProperty ("addAudioDelay");
		addAnimationDelay_Prop = serializedObject.FindProperty ("addAnimationDelay");
		
		blink_Prop = serializedObject.FindProperty ("blink");
		blinkRate_Prop = serializedObject.FindProperty ("blinkRate");
	}
	
	public override void OnInspectorGUI() {
		Button_Behavior myTarget = target as Button_Behavior;
		
		serializedObject.Update ();
		
		myTarget.loadScene = EditorGUILayout.Toggle(new GUIContent("Load Scene"), myTarget.loadScene);
		if(myTarget.loadScene)
		{
			EditorGUILayout.PropertyField( sceneName_Prop, new GUIContent("sceneName") );
		}
		else
		{
			myTarget.severalActions = EditorGUILayout.Toggle(new GUIContent("Various Actions"), myTarget.severalActions);
			if(myTarget.severalActions)
			{
				EditorGUILayout.PropertyField( actions_Prop, true);
				for (int i = 0; i < actions_Prop.arraySize; i++)
				{
					Button_Behavior.typeOfAction tOa = (Button_Behavior.typeOfAction)actions_Prop.GetArrayElementAtIndex(i).enumValueIndex;
					switch(tOa)
					{
					/*case Button_Behavior.typeOfAction.turnOnObject:
						
						break;
					case Button_Behavior.typeOfAction.turnOffObject:
					
						break;
					case Button_Behavior.typeOfAction.switchObject:
						
						break;*/
					case Button_Behavior.typeOfAction.createObject:
						createsObjCount++;
						break;
					case Button_Behavior.typeOfAction.startRoutine:
						showsRoutineNameCount++;
						break;
					}
				}
				if(showsRoutineNameCount != 0)
				{
					EditorGUILayout.PropertyField( routineName_Prop);
					showsRoutineNameCount = 0;
				}
				if(createsObjCount != 0)
				{
					EditorGUILayout.PropertyField( objectToCreate_Prop);
					createsObjCount = 0;
				}
				//Button_Behavior.typeOfAction tOa = (Button_Behavior.typeOfAction)actions_Prop.;
			}
			else
			{
				EditorGUILayout.PropertyField(action_Prop);
				Button_Behavior.typeOfAction tOa = (Button_Behavior.typeOfAction)action_Prop.enumValueIndex;
				switch(tOa)
				{
				/*case Button_Behavior.typeOfAction.turnOnObject:
					
					break;
				case Button_Behavior.typeOfAction.turnOffObject:
					
					break;
				case Button_Behavior.typeOfAction.switchObject:
					
					break;*/
				case Button_Behavior.typeOfAction.createObject:
					EditorGUILayout.PropertyField( objectToCreate_Prop);
					break;
				case Button_Behavior.typeOfAction.startRoutine:
					EditorGUILayout.PropertyField( routineName_Prop);
					break;
				}
			}
			
			myTarget.severalObjects = EditorGUILayout.Toggle(new GUIContent("Various Objects"), myTarget.severalObjects);
			if(myTarget.severalObjects)
			{
				EditorGUILayout.PropertyField( objectsToInteract_Prop, true);
			}
			else
			{
				EditorGUILayout.PropertyField( objectToInteract_Prop, true);
			}
			
			myTarget.addActionDelay = EditorGUILayout.Toggle(new GUIContent("Add Action Delay"), myTarget.addActionDelay);
			if(myTarget.addActionDelay)
			{
				if(myTarget.severalActions)
				{
					EditorGUILayout.PropertyField(actionsDelay_Prop, true);
				}
				else
				{
					EditorGUILayout.PropertyField(actionDelay_Prop);
				}
			}
			
			myTarget.playAudio = EditorGUILayout.Toggle(new GUIContent("Play Audio"), myTarget.playAudio);
			if(myTarget.playAudio)
			{
				EditorGUILayout.PropertyField(audioToPlay_Prop);
				myTarget.addAudioDelay = EditorGUILayout.Toggle(new GUIContent("Add Audio Delay"), myTarget.addAudioDelay);
			}
			
			myTarget.playAnimation = EditorGUILayout.Toggle(new GUIContent("Play Animation"), myTarget.playAnimation);
			if(myTarget.playAnimation)
			{
				EditorGUILayout.PropertyField(animationToPlay_Prop);
				myTarget.addAnimationDelay = EditorGUILayout.Toggle(new GUIContent("Add Animation Delay"), myTarget.addAnimationDelay);
			}
			
			myTarget.worksOnce = EditorGUILayout.Toggle(new GUIContent("Works Once"), myTarget.worksOnce);
			if(myTarget.worksOnce)
			{
				myTarget.destroySelf = EditorGUILayout.Toggle(new GUIContent("Destroys Self"), myTarget.destroySelf);
				if(myTarget.destroySelf)
				{
					EditorGUILayout.PropertyField(selfDestroyDelay_Prop);
				}
			}
			
			myTarget.blink = EditorGUILayout.Toggle(new GUIContent("Blink"), myTarget.blink);
			if(myTarget.blink)
			{
				EditorGUILayout.PropertyField(blinkRate_Prop);
			}
		}
		EditorUtility.SetDirty(myTarget);
		serializedObject.ApplyModifiedProperties ();
	}
}
