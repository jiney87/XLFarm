using UnityEngine;
using System.Collections;

public class DrawBoundary : MonoBehaviour {

	public Color color=Color.red;
	public float x0;
	public float x1;
	public float length=1;
	public bool isLocal=false;

	public readonly Vector3 direction = new Vector3 (0,0,-1);

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
			Vector3 from0 = transform.localPosition + new Vector3 (x0, 0, 0);
			Vector3 from1 = transform.localPosition + new Vector3 (x1, 0, 0);

			Gizmos.DrawRay (from0, direction * length);
			Gizmos.DrawRay (from1, direction * length);
		} else {
			Vector3 from0 = transform.position + new Vector3 (x0,0,0);
			Vector3 from1 = transform.position + new Vector3 (x1,0,0);

			Gizmos.DrawRay (from0, direction*length);
			Gizmos.DrawRay (from1, direction*length);
		}
	}

	public Vector3 getBirthPos()
	{
		float x = Random.Range (x0, x1);

		if (isLocal) {
			return new Vector3 (x, transform.localPosition.y, transform.localPosition.z);
		} else {
			return new Vector3 (x,transform.position.y,transform.position.z);
		}
	}
}
