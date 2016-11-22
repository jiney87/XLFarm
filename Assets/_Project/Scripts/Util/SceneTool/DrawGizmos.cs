using UnityEngine;
using System.Collections;

public class DrawGizmos : MonoBehaviour {

	public Color color=Color.red;
	public float radius;
	public bool isLocal=false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos()
	{
		Gizmos.color = color;
		if (isLocal) {
			Gizmos.DrawWireSphere (transform.localPosition, radius);
		} else {
			Gizmos.DrawWireSphere (transform.position, radius);
		}
	}
}
