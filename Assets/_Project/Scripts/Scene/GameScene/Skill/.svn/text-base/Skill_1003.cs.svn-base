using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// 投掷西瓜
/// </summary>
public class Skill_1003 : SkillRelease {

	private GameObject spawn;
	private GameObject target;

	protected override void releaseSkill ()
	{
		target = targets [0];

		SkillData sd = SkillData.getData (skillId);
		spawn = ResourcesUtil.loadSkillSpwan (sd.spawnPrefabPath);

		//初始位置
		Util.AddChild (spawn, releaser);
		Util.resetTransform (spawn.transform);

		spawn.GetComponentInChildren<MoveToTargetByAxis> ().move (target, 0.3f, false, Vector3.zero, true, skillOver);
	}

	public override void skillOver ()
	{
		base.skillOver ();

		Destroy (spawn, 2f);

		if (isSelfRelease) {
			//减速通知
			Message_SkillEffect.create(skillId,target.GetComponentInChildren<BoatControl>().playerId).send();
		}
	}
}
