using UnityEngine;
using System.Collections;

public class FollowTarget_V2: MonoBehaviour {

	public enum Axis
	{
		x,y,z
	}

	public Transform target;
	public Vector3 offset;
	public Axis axis;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (target == null) {
			return;
		}
		Vector3 pos = transform.position;
		Vector3 targetPos = target.position;
		switch (axis) {
		case Axis.x:
			targetPos = new Vector3 (target.position.x, 0, 0);
			break;
		case Axis.y:
			targetPos = new Vector3 (0, target.position.y, 0);
			break;
		case Axis.z:
			targetPos = new Vector3 (0, 0, target.position.z);
			break;
		}
		transform.position = targetPos + offset;
	}
}
