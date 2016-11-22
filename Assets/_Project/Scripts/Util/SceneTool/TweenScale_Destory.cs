using UnityEngine;
using System.Collections;

public class TweenScale_Destory : MonoBehaviour {

	public Vector3 fromScale = Vector3.one;
	public Vector3 toScale=Vector3.one;
	public AnimationCurve curve;
	[Range(0.3f,2f)]
	public float keepTime=1;
	private float curTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		curTime += Time.deltaTime;
		float value = curve.Evaluate (Mathf.Clamp01 (curTime / keepTime));
		transform.localScale = Vector3.LerpUnclamped (fromScale, toScale, value);
		if (curTime > keepTime) {
			Destroy (gameObject);
			//curTime=0;
		}
	}
}
