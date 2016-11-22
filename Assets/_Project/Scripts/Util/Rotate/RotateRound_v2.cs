using UnityEngine;
using System.Collections;

public class RotateRound_v2 : MonoBehaviour {

	public bool canRotate;
	public GameObject centerGo;
	public Vector3 rotateEulerAngle=new Vector3(0,0,1f);
	public float rotateSpeed=100f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(canRotate)
		{
			transform.RotateAround(centerGo.transform.position,rotateEulerAngle,rotateSpeed*Time.deltaTime);
		}
	}

//	public void init(float originAngle,float rotateSpeed)
//	{
//		originAngle=originAngle%360f;
//		transform.RotateAround(middleGo.transform.position,new Vector3(0,0,1),originAngle);
//		this.rotateSpeed=rotateSpeed;
//		canRotate=true;
//	}
}
