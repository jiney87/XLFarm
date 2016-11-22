using UnityEngine;
using System.Collections;
	
public class FlashLight : MonoBehaviour {
		
	[Range(0,1f)]
	public float maxValue=0.5f;
	[Range(0,1f)]
	public float minValue=0.5f;
	private Color color;
	private Material[] mats;

	// Use this for initialization
	void Start () {
		color = GetComponent<Renderer>().material.GetColor("_Color");
		mats = GetComponent<Renderer> ().materials;

		StartCoroutine(WaitForColor());
	}

	void Update()
	{
		if (minValue > maxValue) {
			maxValue = minValue;
		}
	}

	IEnumerator WaitForColor() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(0.0f, 0.1f));
			color.a = Random.Range(minValue, maxValue);
			for (int k = 0; k < mats.Length; k++) {
				mats[k].SetColor("_Color", color);
			}
			//GetComponent<Renderer>().material.SetColor("_TintColor", color);
		}
	}
}
