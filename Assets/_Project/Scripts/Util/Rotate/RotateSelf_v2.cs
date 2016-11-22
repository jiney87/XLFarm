using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class RotateSelf_v2 : MonoBehaviour {

	public enum AxisType:int
	{
		X=0,
		Y,
		Z
	}
	public AxisType axisType = AxisType.Y;
	public AnimationCurve speedCurve;
	public float speedMul;
	public float rotateTime;

	private UnityAction callback;
	private float curTime;
	private bool canRotate;

	// Use this for initialization
	void Start () {
		//rotate (2f, 0.3f, null);
	}
	
	// Update is called once per frame
	void Update () {
		if (!canRotate) {
			return;
		}
		curTime += Time.deltaTime;
		if (curTime > rotateTime) {
			canRotate = false;
			enabled = false;
			if (callback != null) {
				callback ();
			}
			return;
		}

		float speed = speedCurve.Evaluate (curTime / rotateTime) * speedMul;
		Vector3 euler=Vector3.zero;
		switch(axisType)
		{
		case AxisType.X:
			euler=new Vector3(speed*Time.deltaTime,0,0);
			break;
		case AxisType.Y:
			euler=new Vector3(0,speed*Time.deltaTime,0);
			break;
		case AxisType.Z:
			euler=new Vector3(0,0,speed*Time.deltaTime);
			break;
		}
		transform.Rotate(euler);
	}

	private void init()
	{
		curTime = 0;
		canRotate = true;
		enabled = true;
	}

	public void rotate(float rotateTime,float delayTime,UnityAction callback)
	{
		this.rotateTime = rotateTime;
		this.callback = callback;

		Invoke ("init", delayTime);
	}


}
