using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {

	[Header("y位移曲线")]
	public AnimationCurve yCurve;
	[Header("y位移时间倍率"),Range(0.1f,1f)]
	public float yTimeMul=0.1f;
	[Header("y位移倍率"),Range(0.01f,1f)]
	public float yMul=0.01f;

	private float curTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		curTime += Time.deltaTime;
		//y
		float yValue = yCurve.Evaluate ((curTime * yTimeMul) % 1);
		Vector3 temp = transform.localPosition;
		transform.localPosition = new Vector3 (temp.x, yValue * yMul, temp.z);
	}
}
