using UnityEngine;
using System.Collections;

public class RotateRound_v3 : MonoBehaviour {

	public bool canRotate;
	[Header("围绕中心")]
	public GameObject centerGo;
	[Header("基础半径")]
	public float rotateRadius=1;
	[Header("半径增量")]
	public float radiusAdd=0.1f;
	[Header("旋转速度")]
	public float rotateSpeed=5;
	[Header("基础高度")]
	public float baseY=0;
	[Header("上升速度")]
	public float directionMoveSpeed=1;


	private float curTime;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(canRotate)
		{
			curTime+=Time.deltaTime;
			float radian=Mathf.PI*curTime*rotateSpeed;
			float r=Mathf.Abs(rotateRadius+radiusAdd*curTime);
			transform.Rotate(new Vector3(0,1,0),-Time.deltaTime*180*rotateSpeed);
			transform.localPosition=centerGo.transform.position+r*new Vector3(Mathf.Cos(radian),0,Mathf.Sin(radian))+new Vector3(0,baseY+directionMoveSpeed*curTime,0);
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
