using UnityEngine;
using System.Collections;
using UnityEngine.Events;


public class MoveToPositionByAxis : MoveByAxis {

	[Header("位移时间"),Range(0.1f,3f)]
	public float moveTime=0.1f;
	private float curTime;

	public Vector3 fromPos;
	public Vector3 toPos;

	private Vector3 oldPos;
	private Vector3 rotateOffset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (!canRun) {
			return;
		}

		curTime += Time.deltaTime;

		float range = Mathf.Clamp01 (curTime / moveTime);
		//

		Vector3 temp = Vector3.Lerp (fromPos, toPos, range);

		float value = curve.Evaluate (range);
		Vector3 offset = Vector3.zero;
		switch (axis) {
		case Axis.x:
			offset = new Vector3 (value * offsetMul, 0, 0);
			break;
		case Axis.y:
			offset = new Vector3 (0, value * offsetMul, 0);
			break;
		case Axis.z:
			offset = new Vector3 (0, 0, value * offsetMul);
			break;
		}
		Vector3 newPos = temp + offset;
		getTf().localPosition = newPos;

		if (rotate) {
			Vector3 direction = (newPos - oldPos).normalized;
			getTf().forward = direction;
			getTf().Rotate (rotateOffset);
		}

		oldPos = newPos;

		if (range >= 1) {
			if (callback != null) {
				callback ();
			}
			canRun = false;
			if (selfDestory) {
				Destroy (this);
			}
		}
	}

	private void init()
	{
		//getTf().parent = null;
		fromPos = getTf().localPosition;
		oldPos = fromPos;

		canRun = true;
	}

	public void move(bool isForParent,Vector3 toPos,float moveTime,float delayTime,bool rotate,Vector3 rotateOffset,bool selfDestory,UnityAction callback)
	{
		enabled = true;

		this.isForParent = isForParent;
		this.toPos = toPos;
		this.moveTime = moveTime;
		this.rotate = rotate;
		this.rotateOffset = rotateOffset;
		this.callback = callback;
		this.axis = Axis.y;
		this.selfDestory = selfDestory;

		Invoke ("init", delayTime);
	}
}
