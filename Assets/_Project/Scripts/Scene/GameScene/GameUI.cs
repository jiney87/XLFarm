using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

	[Header("道具")]
	public GameObject itemUIParentGo;
	[Header("两个技能")]
	public SkillUI skill1;
	public SkillUI skill2;

	private List<GameObject> itemUIGos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void init(string name,int skillId1,int skillId2,SkillControl skillControl)
	{
		skill1.init (skillId1, skillControl);
		skill2.init (skillId2, skillControl);
	}

	public void beginGame()
	{
		skill1.beginGame ();
		skill2.beginGame ();
	}

	public void achieveItem(Message_AchieveItem msg)
	{
		if (itemUIGos == null) {
			itemUIGos = new List<GameObject> ();
		}

		while (itemUIGos.Count > 2) {
			GameObject temp = itemUIGos [itemUIGos.Count - 1];
			itemUIGos.Remove (temp);
			Destroy (temp);
		}

		for (int k = 0; k <itemUIGos.Count ; k++) {
			GameObject temp = itemUIGos [k];
			Vector3 from = temp.transform.localPosition;
			Util.getOrAddComponent<TweenPosition> (temp).tween (from, new Vector3 (130 * (k+1), 0, 0), 0.3f, 0, null);
		}


		GameObject itemGo = ResourcesUtil.loadItem ();
		Util.AddChild (itemGo, itemUIParentGo);
		Util.resetTransform (itemGo.transform);
		itemUIGos.Insert (0, itemGo);

		itemGo.GetComponentInChildren<ItemUIControl> ().init (msg);
	}

	//销毁UI
	public void useItemUI(long itemUniqueId)
	{
		for (int k = itemUIGos.Count - 1; k >= 0; k--) {
			GameObject itemUI = itemUIGos [k];
			if (itemUI.GetComponentInChildren<ItemUIControl> ().itemUniqueId == itemUniqueId) {
				itemUIGos.Remove (itemUI);
				Destroy (itemUI);
				break;
			}
		}
	}

	public void useSkill(int skillId)
	{
		if (skill1.skillId == skillId) {
			skill1.startCooldown ();
		}
		if (skill2.skillId == skillId) {
			skill2.startCooldown ();
		}
	}
}
