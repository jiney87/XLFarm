using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// 移形换位
/// </summary>
public class Skill_1006 : SkillRelease {
	
	protected override void releaseSkill ()
	{
		GameObject target = targets [0];

		Vector3 pos = new Vector3 (releaser.transform.localPosition.x, 0, releaser.transform.localPosition.z);
		releaser.transform.localPosition = new Vector3 (target.transform.localPosition.x, 0, target.transform.localPosition.z);
		target.transform.localPosition = pos;

		skillOver ();
	}

	public override void skillOver ()
	{
		base.skillOver ();
	}
}
