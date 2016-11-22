using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomPropertyDrawer(typeof(SeparateProperty))]
public class SeparatePropertyDrawer : PropertyDrawer {

//	public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
//	{
//		//return this.;
//	}

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		SeparateProperty sp = this.attribute as SeparateProperty;
		EditorGUI.LabelField (position, sp.msg);
		//base
		EditorGUI.PropertyField (position, property);
	}
}
