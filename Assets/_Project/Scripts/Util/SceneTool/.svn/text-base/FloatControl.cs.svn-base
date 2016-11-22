using UnityEngine;
using System.Collections;

public class FloatControl : MonoBehaviour {

	[Header("y位移曲线")]
	public AnimationCurve yCurve;
	[Header("y位移时间倍率"),Range(0.1f,1f)]
	public float yTimeMul=0.1f;
	[Header("y位移倍率"),Range(0.01f,0.1f)]
	public float yMul=0.01f;

	[Header("旋转曲线")]
	public AnimationCurve rotateCurve;
	[Header("旋转时间倍率"),Range(0.1f,1f)]
	public float rotateTimeMul=0.1f;
	[Header("旋转角度倍率"),Range(1f,10f)]
	public float rotateAngleMul=1f;

	private float curTime;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		curTime += Time.deltaTime;
		//y
		float yValue = yCurve.Evaluate ((curTime * yTimeMul) % 1);
		transform.localPosition = Vector3.zero + new Vector3 (0,yValue*yMul,0);

		//rotate
		float rotate=rotateCurve.Evaluate((curTime * rotateTimeMul) % 1);
		transform.localRotation = Quaternion.Euler (new Vector3(0,0,rotate*rotateAngleMul));
	}
}
