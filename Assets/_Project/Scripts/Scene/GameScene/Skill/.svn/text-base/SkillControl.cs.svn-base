using UnityEngine;
using System.Collections.Generic;

public class SkillControl : MonoBehaviour {

	private GameScene gameScene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void init(GameScene gameScene)
	{
		this.gameScene = gameScene;
	}

	#region UI调用
	/// <summary>
	/// 技能冷却结束
	/// </summary>
	/// <param name="skillId">Skill identifier.</param>
	public void cooldown(int skillId)
	{
		if (transform.FindChild ("" + skillId)!=null) {
			return;
		}
		loadSkill (skillId);
	}

	private GameObject loadSkill(int skillId)
	{
		GameObject skillGo = ResourcesUtil.loadSkill (skillId);
		if (skillGo == null) {
			return null;
		}
		skillGo.name = skillId + "";
		//粘上
		Util.AddChild (skillGo, gameObject);
		return skillGo;
	}

	public void onPressDown(int skillId)
	{
		Transform tf = transform.FindChild ("" + skillId);
		if (tf == null) {
			return;
		}
		SearchTarget st = tf.GetComponent<SearchTarget> ();
		st.init (gameObject, skillId, gameScene.getAllplayers (), searchOver);
		st.startSearch ();
	}

	public void onPressUp(int skillId)
	{
		Transform tf = transform.FindChild ("" + skillId);
		if (tf == null) {
			return;
		}
		SearchTarget st = tf.GetComponent<SearchTarget> ();
		st.endSearch ();
	}

	private void searchOver(int skillId,GameObject releaser,List<GameObject> targets)
	{
		if (Util.isListEmpty (targets)) {
			Debug.Log ("searchOver");
			return;
		}

		List<int> targetPlayerIds = new List<int> ();
		for (int k = 0; k < targets.Count; k++) {
			targetPlayerIds.Add (targets [k].GetComponent<BoatControl> ().playerId);
		}

		//给服务器发消息,用来验证是否冷却
		Message_UseSkill.create (skillId,targetPlayerIds).send ();
	}
	#endregion

	public void releaseSkill(bool isSelfRelease,int skillId,List<GameObject> targets)
	{
		Transform tf = transform.FindChild ("" + skillId);
		if (tf == null) {
			if (isSelfRelease) {
				return;
			}
			else {
				GameObject skillGo = loadSkill (skillId);
				if (skillGo == null) {
					return;
				}
				tf = skillGo.transform;
			}
		}

		//解决技能冲突
		solveSkillConflict(skillId);

		SkillRelease release = tf.GetComponent<SkillRelease> ();
		release.startReleaseSkill (isSelfRelease, skillId, gameObject, targets);
	}

	//解决技能冲突
	private void solveSkillConflict(int curSkillId)
	{
		if (curSkillId == 1006) {
			Skill_1007 skill1007 = transform.GetComponentInChildren<Skill_1007> ();
			if (skill1007 != null && skill1007.haveReleased) {
				skill1007.skillOver ();
			}
		}
	}
}