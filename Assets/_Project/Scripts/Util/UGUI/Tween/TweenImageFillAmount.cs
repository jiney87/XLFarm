using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TweenImageFillAmount : MonoBehaviour {

	private Image image;
	private float totalTime;
	private float curTime;

	private UnityAction callback;
	private bool canTween;

	// Use this for initialization
	void Start () {
		if (image == null) {
			image = GetComponent<Image> ();
		}
		image.fillAmount = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (canTween) {
			curTime += Time.deltaTime;
			float diff = curTime / totalTime;
			image.fillAmount = 1 - diff;
			if (diff >= 1) {
				canTween = false;
				gameObject.SetActive (false);
				if (callback != null) {
					callback ();
				}
			}
		}
	}

	public void tween(float totalTime,UnityAction callback)
	{
		gameObject.SetActive (true);
		if (image == null) {
			image = GetComponent<Image> ();
		}
		this.totalTime = totalTime;
		this.callback = callback;

		curTime = 0;
		canTween = true;
	}

}
