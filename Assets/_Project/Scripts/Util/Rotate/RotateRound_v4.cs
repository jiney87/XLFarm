using UnityEngine;
using System.Collections;

public class RotateRound_v4 : MonoBehaviour {

	public bool canRotate;
	[Header("围绕中心")]
	public GameObject centerGo;

	[Header("自转")]
	public bool rotateSelf;
	[Header("旋转速度")]
	public float rotateSpeed=5;

	[Header("半径速度")]
	public float rSpeed=0.1f;
	[Header("最大半径")]
	public float maxR=2f;
	[Header("最小半径")]
	public float minR=1f;

	[Header("高度速度")]
	public float ySpeed=1;
	[Header("最大高度")]
	public float maxY=2;
	[Header("最小高度")]
	public float minY=0.5f;


	private float curTime;
	private float curR;
	private float curY;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(canRotate)
		{
			curTime+=Time.deltaTime;
			//R
			curR+=rSpeed*Time.deltaTime;
			if(curR>maxR)
			{
				curR=maxR-(curR-maxR);
				rSpeed*=-1;
			}
			else if(curR<minR)
			{
				curR=minR+(minR-curR);
				rSpeed*=-1;
			}
			//speed
			curY+=ySpeed*Time.deltaTime;
			if(curY>maxY)
			{
				curY=maxY-(curY-maxY);
				ySpeed*=-1;
			}
			else if(curY<minY)
			{
				curY=minY+(minY-curY);
				ySpeed*=-1;
			}
			//rotate
			if(rotateSelf)
			{
				transform.Rotate(new Vector3(0,1,0),-Time.deltaTime*180*rotateSpeed);
			}

			float radian=Mathf.PI*curTime*rotateSpeed;

			transform.localPosition=curR*new Vector3(Mathf.Cos(radian),0,Mathf.Sin(radian))+new Vector3(0,curY,0);
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
