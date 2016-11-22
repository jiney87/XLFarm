using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QueueTipsControl : MonoBehaviour {

	public Text text;
	private float time;
	private int intTime;

	// Use this for initialization
	void Start () {
		time = 0;
		intTime = 0;
		setText ();
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		int temp = (int)time;
		if (temp > intTime) {
			intTime = temp;
			setText ();
		}
	}

	private void setText()
	{
		text.text="排队中...("+intTime+")";
	}
}
