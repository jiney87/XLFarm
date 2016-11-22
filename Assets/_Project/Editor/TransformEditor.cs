using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Transform))]
public class TansformEditor : Editor {

	public override void OnInspectorGUI()
	{
		Transform tf=(Transform)this.target;

		EditorGUILayout.BeginVertical();

		EditorGUILayout.BeginHorizontal();
		if(GUILayout.Button("P",GUILayout.Width(20)))
		{
			tf.localPosition=Vector3.zero;
		}
		//EditorGUILayout.LabelField("",GUILayout.Width(80));
		tf.localPosition=EditorGUILayout.Vector3Field("Position",tf.localPosition);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		if(GUILayout.Button("R",GUILayout.Width(20)))
		{
			tf.localRotation=Quaternion.Euler(Vector3.zero);
		}
		//EditorGUILayout.LabelField("",GUILayout.Width(80));
		tf.localRotation=Quaternion.Euler(EditorGUILayout.Vector3Field("Rotation",tf.localRotation.eulerAngles));
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		if(GUILayout.Button("S",GUILayout.Width(20)))
		{
			tf.localScale=Vector3.one;
		}
		//EditorGUILayout.LabelField("",GUILayout.Width(80));
		tf.localScale=EditorGUILayout.Vector3Field("Scale",tf.localScale);
		EditorGUILayout.EndHorizontal();


		EditorGUILayout.EndVertical();

		if(GUI.changed)
		{
			EditorUtility.SetDirty(tf);
		}
	}

	//private Rect rect=new Rect(20,20,300,200);
	private string[] names=new string[]{"这个B","装的还行","再来一个"};
	private int selectedIndex=0;

	void OnSceneGUI()
	{
		//rect=GUI.Window(0,rect,windowFunction,"快看,搞起来了");
	}

	private void windowFunction(int id)
	{
		selectedIndex=GUILayout.Toolbar(selectedIndex,names);
		switch(selectedIndex)
		{
		case 0:
			showDescribe();
			break;
		case 1:
			showAnimation();
			break;
		case 2:
			showParticleSystem();
			break;
		}
		GUI.DragWindow();
	}

	private void showDescribe()
	{
		GUILayout.Label("!@$@!%$RTU^&&(%$&$()*&");
	}

	private void showAnimation()
	{
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("play"))
		{

		}
		if(GUILayout.Button("pause"))
		{
			
		}
		if(GUILayout.Button("stop"))
		{

		}
		GUILayout.EndHorizontal();
	}

	private void showParticleSystem()
	{

	}

}
