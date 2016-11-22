using UnityEngine;
using System.Collections;

public class ColorCircle : MonoBehaviour {

	public Gradient gradient;
	[Range(0.1f,10f)]
	public float mul;

	private Material[] mats;
	private float curTime;

	// Use this for initialization
	void Start () {
		mats = GetComponent<Renderer> ().materials;
	}
	
	// Update is called once per frame
	void Update () {
		curTime += Time.deltaTime * mul;
		curTime = curTime % 1;

		Color color = gradient.Evaluate (curTime);
		for (int k = 0; k < mats.Length; k++) {
			mats[k].SetColor("_Color", color);
		}
	}
}
