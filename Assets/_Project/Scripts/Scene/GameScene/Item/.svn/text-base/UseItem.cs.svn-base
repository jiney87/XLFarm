using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class UseItem : MonoBehaviour {

	public long itemUniqueId;
	public ItemData itemData;
	public EffectData effectData;

	// Use this for initialization
	void Start () {
		//init (selfBoat, otherBoat, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void init(BoatControl selfBoat,BoatControl otherBoat, long itemUniqueId, ItemData itemData)
	{
		this.itemUniqueId = itemUniqueId;
		this.itemData = itemData;
		effectData = EffectData.getData (itemData.effectId);

		runEffect (selfBoat, otherBoat);
	}

	private void runEffect(BoatControl selfBoat,BoatControl otherBoat)
	{
		//初始位置
		Util.AddChild (gameObject, selfBoat.itemParent);
		Util.resetTransform (transform);

		float delayTime = 0.3f;

		//对效果区分
		switch (effectData.effectType) {
		case 1://向敌人发射火箭
			GetComponentInChildren<MoveToTargetByAxis> ().move (otherBoat.gameObject, delayTime, true, new Vector3 (90, 0, 0),false, hitTarget);
			//GetComponentInChildren<MoveToPositionByAxis> ().move (otherBoat.transform.localPosition, 1f, 0.5f, true, new Vector3 (90, 0, 0), hitTarget);
			break;
		case 2://对自己使用加速
			GetComponentInChildren<RotateSelf_v2>().rotate(effectData.value1,delayTime,null);
			hitTarget();
			break;
		case 3://向敌人发射减速
			GetComponentInChildren<MoveToTargetByAxis> ().move (otherBoat.gameObject, delayTime, false, Vector3.zero,true, hitTarget);
			break;
		case 4://对自己使用无敌
			hitTarget();
			break;
		}
	}

	//击中目标
	private void hitTarget()
	{
		Message_ItemTouchPlayer.create (itemUniqueId).send ();
		runDestroy ();
	}

	private void runDestroy()
	{
		//对效果区分
		switch (effectData.effectType) {
		case 1://向敌人发射火箭
			Destroy (gameObject);
			break;
		case 2://对自己使用加速
			Destroy (gameObject, effectData.value1);
			break;
		case 3://向敌人发射减速
			Destroy (gameObject, effectData.value1);
			break;
		case 4://对自己使用无敌
			Destroy (gameObject, effectData.value1);
			break;
		}
	}
}
