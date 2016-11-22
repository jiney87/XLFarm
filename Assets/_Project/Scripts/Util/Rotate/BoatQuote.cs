using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class BoatQuote : MonoBehaviour {

	public AnimationCurve angleCurve;
	public float angleMul=180;
	public float rotateTime;

	private UnityAction callback;
	private float curTime;
	private bool canRotate;

	// Use this for initialization
	void Start () {
		//rotate (1f, 0.3f, null);
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

		float angle = angleCurve.Evaluate (curTime / rotateTime) * angleMul;
		transform.localRotation = Quaternion.Euler (0,0,angle);
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
