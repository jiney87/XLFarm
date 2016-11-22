using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// 加速
/// </summary>
public class Skill_1002 : SkillRelease {

	protected override void releaseSkill ()
	{
		//TODO duang

		skillOver();
	}

	public override void skillOver ()
	{
		base.skillOver ();

		if (isSelfRelease) {
			//加速通知
			Message_SkillEffect.create(skillId,releaser.GetComponentInChildren<BoatControl>().playerId).send();
		}
	}

}
