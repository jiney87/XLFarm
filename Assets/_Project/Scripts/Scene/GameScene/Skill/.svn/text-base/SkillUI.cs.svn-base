using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillUI : MonoBehaviour {

	public TweenImageFillAmount cover;
	public Image skillImage;
	public int skillId;
	public SkillControl skillControl;
	private bool isCooldownStatus;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void init(int skillId,SkillControl skillControl)
	{
		this.skillId = skillId;
		this.skillControl = skillControl;

		//设置图片
		SkillData sd=SkillData.getData(skillId);
		skillImage.sprite = ResourcesUtil.loadSprite (sd.icon);

		UGUIEventListener.Get (gameObject).OnPointerDowns.Add (onPressDown);
		UGUIEventListener.Get (gameObject).OnPointerUps.Add (onPressUp);

		isCooldownStatus = false;
	}

	public void beginGame()
	{
		startCooldown ();
	}

	public void startCooldown()
	{
		SkillData sd=SkillData.getData(skillId);
		cover.tween (sd.cooldown, cooldownOver);
		isCooldownStatus = true;
	}

	private void cooldownOver()
	{
		isCooldownStatus = false;
		//冷却结束
		skillControl.cooldown (skillId);
	}

	private void onPressDown(GameObject go,PointerEventData data)
	{
		if (isCooldownStatus) {
			return;
		}
		//选取目标开始
		skillControl.onPressDown (skillId);
	}

	private void onPressUp(GameObject go,PointerEventData data)
	{
		if (isCooldownStatus) {
			return;
		}
		//选取目标结束
		skillControl.onPressUp(skillId);
	}
}
