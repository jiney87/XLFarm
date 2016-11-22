using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class TweenAlpha : MonoBehaviour {

	public AnimationCurve curve;

	private MaskableGraphic[] graphics; 

	private float curTime;
	private float from;
	private float to;
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
					for (int k = 0; k < graphics.Length; k++) {
						Color c = graphics [k].color;
						graphics [k].color = new Color (c.r, c.g, c.b, Mathf.LerpUnclamped(from, to, curve.Evaluate(1)));
					}
					if (callback != null) {
						callback ();
					}
					enabled = false;
					//收尾
					if (to == 0) {
						gameObject.SetActive (false);
					}
					canTween = false;
				} else {
					for (int k = 0; k < graphics.Length; k++) {
						Color c = graphics [k].color;
						graphics [k].color = new Color (c.r, c.g, c.b, Mathf.LerpUnclamped (from, to, curve.Evaluate (temp / during)));
					}
				}
			}
		}
	}

	public void tween(float from,float to,float during,float delay,UnityAction callback)
	{
		enabled = true;

		graphics = gameObject.GetComponentsInChildren<MaskableGraphic> ();
		if (graphics == null || graphics.Length==0) {
			return;
		}
		this.from = from;
		this.to = to;
		this.during = during;
		this.delay = delay;
		this.callback = callback;
		gameObject.SetActive (true);
		curTime = 0;

		for (int k = 0; k < graphics.Length; k++) {
			Color c = graphics [k].color;
			graphics [k].color = new Color (c.r, c.g, c.b, from);
		}
		canTween = true;
	}
}
