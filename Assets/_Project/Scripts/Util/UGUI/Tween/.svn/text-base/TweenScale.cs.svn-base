using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class TweenScale : MonoBehaviour {

	public AnimationCurve curve;

	private float curTime;
	private Vector3 from;
	private Vector3 to;
	private float during;
	private float delay;
	private UnityAction callback;
	private bool canTween;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (canTween) {
			curTime += Time.deltaTime;
			if (curTime < delay) {
				return;
			} else {
				float temp = curTime - delay;
				if (temp >= during) {
					transform.localScale = Vector3.LerpUnclamped(from, to, curve.Evaluate(1));
                    if (callback != null) {
						callback ();
					}
					enabled = false;
					canTween = false;
				} else {
					transform.localScale = Vector3.LerpUnclamped (from, to, curve.Evaluate (temp / during));
				}
			}
		}
	}

	public void tween(Vector3 from,Vector3 to,float during,float delay,UnityAction callback)
	{
		enabled = true;

		this.from = from;
		this.to = to;
		this.during = during;
		this.delay = delay;
		this.callback = callback;
		curTime = 0;
		gameObject.SetActive (true);
		transform.localScale = from;
		canTween = true;
	}
}
