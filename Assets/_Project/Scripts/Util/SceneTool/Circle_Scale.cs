using UnityEngine;
using System.Collections;

public class Circle_Scale : MonoBehaviour {

	public AnimationCurve curve;
	[Range(0.1f,3f)]
	public float scaleMul=1;
	private float curTime;
	private Vector3 originScale;

	// Use this for initialization
	void Start () {
		originScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		curTime += Time.deltaTime;
		float value = curve.Evaluate (curTime % 1) * scaleMul;
		transform.localScale = originScale * value;
	}
}
