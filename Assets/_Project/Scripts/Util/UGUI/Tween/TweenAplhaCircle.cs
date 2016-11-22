using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TweenAplhaCircle : MonoBehaviour {

	public AnimationCurve curve;
	public float mul=1.3f;
	private MaskableGraphic[] graphics;
	private float curTime;

	// Use this for initialization
	void Start () {
		graphics = GetComponentsInChildren<MaskableGraphic> ();
	}
	
	// Update is called once per frame
	void Update () {
		curTime += Time.deltaTime;
		float alpha = curve.Evaluate ((curTime * mul) % 1f);
		if (graphics != null && graphics.Length > 0) {
			for (int k = 0; k < graphics.Length; k++) {
				Color c = graphics [k].color;
				graphics [k].color = new Color (c.r, c.g, c.b, alpha);
			}
		}

	}
}
