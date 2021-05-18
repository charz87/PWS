using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Invoker)), CanEditMultipleObjects]
public class InvokerEditor : Editor
{
	Object myObject;
	public SerializedProperty 
		invokerType_Prop,
		invokingPlaces_Prop,
		objectToInvoke_Prop,
		objectsToInvoke_Prop,
		originPosition_Prop,
		originPositions_Prop,
		maxObjectCount_Prop,
		frequency_Prop,
		active_Prop;
	
	void OnEnable () {
		// Setup the SerializedProperties
		invokerType_Prop = serializedObject.FindProperty ("invokerType");
		invokingPlaces_Prop = serializedObject.FindProperty("invokingPlaces");
		
		objectToInvoke_Prop = serializedObject.FindProperty ("objectToInvoke");
		objectsToInvoke_Prop = serializedObject.FindProperty ("objectsToInvoke");
		
		originPosition_Prop = serializedObject.FindProperty ("originPosition");
		originPositions_Prop = serializedObject.FindProperty ("originPositions");
		
		maxObjectCount_Prop = serializedObject.FindProperty ("maxObjectCount");
		frequency_Prop = serializedObject.FindProperty ("frequency");
		active_Prop = serializedObject.FindProperty ("active");
	}
	
	public override void OnInspectorGUI() {
		serializedObject.Update ();
		
		EditorGUILayout.PropertyField( invokerType_Prop );
		
		Invoker.kindOfInvoker kOi = (Invoker.kindOfInvoker)invokerType_Prop.enumValueIndex;
		switch( kOi )
		{
		case Invoker.kindOfInvoker.singleObject:
			EditorGUILayout.PropertyField( objectToInvoke_Prop, new GUIContent("objectToInvoke") );
			break;
			
		case Invoker.kindOfInvoker.multiObjectRandom:            
			EditorGUILayout.PropertyField( objectsToInvoke_Prop, new GUIContent("objectsToInvoke"),true );  
			break;
			
		case Invoker.kindOfInvoker.multiObjectFixed:            
			EditorGUILayout.PropertyField( objectsToInvoke_Prop, new GUIContent("objectsToInvoke"),true );  
			break;
		}
		
		EditorGUILayout.PropertyField( invokingPlaces_Prop );
		
		Invoker.placesOfInvoking pOi = (Invoker.placesOfInvoking)invokingPlaces_Prop.enumValueIndex;
		switch( pOi )
		{
		case Invoker.placesOfInvoking.singlePlaceDynamic:
			EditorGUILayout.PropertyField( originPosition_Prop, new GUIContent("originPosition") );
			break;
			
		case Invoker.placesOfInvoking.singlePlaceStatic:            
			EditorGUILayout.PropertyField( originPosition_Prop, new GUIContent("originPosition") );  
			break;
			
		case Invoker.placesOfInvoking.multiPlace:            
			EditorGUILayout.PropertyField( originPositions_Prop, new GUIContent("originPositions"),true );  
			break;
        }
		EditorGUILayout.PropertyField( maxObjectCount_Prop );
		EditorGUILayout.PropertyField( frequency_Prop );
		EditorGUILayout.PropertyField( active_Prop );
		
		
        serializedObject.ApplyModifiedProperties ();
	}
}