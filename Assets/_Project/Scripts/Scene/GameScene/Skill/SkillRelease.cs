using UnityEngine;
using System.Collections.Generic;

public abstract class SkillRelease : MonoBehaviour {

	[Header("技能描述"),Tooltip("技能描述")]
	public string des;
	//是否已释放过
	public bool haveReleased=false;

	#region 技能实体
	protected bool isSelfRelease;
	protected int skillId;
	protected GameObject releaser;
	protected List<GameObject> targets;
	#endregion

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// 释放技能
	/// </summary>
	/// <param name="self">Self.</param>
	/// <param name="skillId">Skill identifier.</param>
	/// <param name="targets">Targets.</param>
	protected abstract void releaseSkill ();

	public void startReleaseSkill (bool isSelfRelease,int skillId, GameObject releaser, List<GameObject> targets)
	{
		this.isSelfRelease = isSelfRelease;
		this.skillId = skillId;
		this.releaser = releaser;
		this.targets = targets;
		haveReleased = true;

		if (Util.isListEmpty (targets)) {
			return;
		}
		releaseSkill ();
	}

	/// <summary>
	/// 技能结束后应该调用这个方法
	/// </summary>
	public virtual void skillOver ()
	{
		Destroy (gameObject);
	}
}
