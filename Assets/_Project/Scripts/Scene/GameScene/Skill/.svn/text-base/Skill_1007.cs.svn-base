using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 跳跃
/// </summary>
public class Skill_1007 : SkillRelease {

	[Header("跳跃距离")]
	public float distance = 2f;
	[Header("跳跃时间")]
	public float useTime = 0.5f;

	public MoveToPositionByAxis moveComponent;

	protected override void releaseSkill ()
	{
		GameObject target = targets [0];

		Vector3 toPos = new Vector3 (target.transform.localPosition.x, 0, target.transform.localPosition.z + distance);
		moveComponent.move (true, toPos, useTime, 0, false, Vector3.zero, true, skillOver);

		tempTarget = target;
	}


	private GameObject tempTarget;
	public override void skillOver ()
	{
		base.skillOver ();

		if (tempTarget != null) {
			tempTarget.transform.localPosition = new Vector3 (tempTarget.transform.localPosition.x, 0, tempTarget.transform.localPosition.z);
		}
	}
}
