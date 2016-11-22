﻿using UnityEngine;
using System.Collections;

public class BoatFlashControl : MonoBehaviour {

	public AnimationCurve curve;
	private float keepTime;
	private float curTime;
	private bool canFlash;

	private Renderer renderer;

	// Use this for initialization
	void Start () {
		//flash (2,Color.white);
	}
	
	// Update is called once per frame
	void Update () {
		if (canFlash) {
			curTime += Time.deltaTime;
			float value = curve.Evaluate (Mathf.Clamp01 (curTime / keepTime));
			renderer.material.SetFloat ("_FlashDiff",value);
			if (curTime > keepTime) {
				canFlash = false;
			}
		}
	}


	public void flash(float keepTime,Color flashColor)
	{
		if (renderer == null) {
			renderer = GetComponentInChildren<Renderer> ();
		}
		renderer.material.SetColor ("_FlashColor",flashColor);

		this.keepTime = keepTime;
		curTime = 0;
		canFlash = true;
	}
}