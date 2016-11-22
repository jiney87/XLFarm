using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SceneView2GameView : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnRenderObject()
	{
		//SceneCamera
		//Preview Camera
		Camera cam = Camera.current;
		if (cam != null && cam.name=="SceneCamera") {
			//Debug.Log ("cam:"+cam.name);
			transform.position = cam.transform.position;
			transform.rotation = cam.transform.rotation;
			transform.localScale = cam.transform.localScale;
		}
	}
}
