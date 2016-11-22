using UnityEngine;
using System.Collections;

public class LightRotate : MonoBehaviour {

	public AnimationCurve curve=AnimationCurve.EaseInOut(0,0,1,1);
	public float during=3f;

	public Vector3 eulerStart=new Vector3(-40,0,-40);
	public Vector3 eulerEnd=new Vector3(40,0,40);

	public bool pause;

	private float curTime;
	private int direct = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (pause) {
			return;
		}

		curTime += Time.deltaTime*direct;
		if (curTime > during) {
			direct = -1;
			curTime = during;
		} else if (curTime < 0) {
			direct = 1;
			curTime = 0;
		}
		float percent = curTime / during;
		float lerpValue = curve.Evaluate (percent);
		Vector3 euler=Vector3.Lerp (eulerStart,eulerEnd,lerpValue);
		transform.localRotation = Quaternion.Euler (euler);
	}
}
