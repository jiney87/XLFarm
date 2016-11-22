using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpiriteAnimation : MonoBehaviour {

	public List<Sprite> sps;
	public int fps=30;

	private int index;
	private float curTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float rate = 1f / fps;
		curTime += Time.deltaTime;
		if (curTime > rate) {
			curTime = 0;
			index = (index + 1) % sps.Count;
			GetComponent<Image> ().sprite = sps [index];
		}
	}
}
