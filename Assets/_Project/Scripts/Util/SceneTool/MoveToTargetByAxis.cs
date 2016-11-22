using UnityEngine;
using System.Collections;
using UnityEngine.Events;


public class MoveToTargetByAxis : MoveByAxis {

	[Header("移动速度")]
	public float moveSpeed;

	private float distance;
	private float curDis;

	public GameObject target;

	private Vector3 selfPos;
	private float range;
	private Vector3 oldPos;
	private Vector3 rotateOffset;
	private bool pasteTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (!canRun) {
			return;
		}

		Vector3 targetPos = target.transform.localPosition;

		Vector3 foreDir = (targetPos - selfPos).normalized;

		Vector3 temp =selfPos+ foreDir * moveSpeed * Time.deltaTime;

		float curDis = Vector3.Distance (temp, targetPos);
		float rangeNew = Mathf.Clamp01 (1 - (curDis / distance));
		range = rangeNew > range ? rangeNew : range;

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
		transform.localPosition = newPos;

		if (rotate) {
			Vector3 direction = (newPos - oldPos).normalized;
			transform.forward = direction;
			transform.Rotate (rotateOffset);
		}

		selfPos = temp;
		oldPos = newPos;

		//如果1帧过后朝向目标的向量与1帧之前朝向目标的向量夹角大于90度，说明已经接触到目标
		Vector3 bgDir = (targetPos - selfPos).normalized;
		if (Vector3.Dot (foreDir, bgDir) <= 0) {
			range = 1f;
		}
		if (range >= 1f) {
			if (callback != null) {
				callback ();
			}
			if (pasteTarget) {
				//道具挂载点
				Util.AddChild(gameObject,target.GetComponentInChildren<BoatControl>().itemParent);
			}
			canRun = false;
		}
	}

	private void init()
	{
		Util.clearParent (gameObject);
		distance = Vector3.Distance (transform.localPosition, target.transform.localPosition);
		moveSpeed = 5 + 1 * distance;
		selfPos = transform.localPosition;
		oldPos = transform.localPosition;
		range = 0;

		canRun = true;
	}

	public void move(GameObject target,float delayTime,bool rotate,Vector3 rotateOffset,bool pasteTarget,UnityAction callback)
	{
		enabled = true;

		this.target = target;
		this.rotate = rotate;
		this.rotateOffset = rotateOffset;
		this.callback = callback;
		this.axis = Axis.y;
		this.pasteTarget = pasteTarget;

		Invoke ("init", delayTime);
	}
}
