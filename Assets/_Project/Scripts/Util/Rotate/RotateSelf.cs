using UnityEngine;
using System.Collections;

public class RotateSelf : MonoBehaviour {

	public enum AxisType:int
	{
		X=0,
		Y,
		Z
	}
	public AxisType axisType;
	public float speed=300;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
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
}
