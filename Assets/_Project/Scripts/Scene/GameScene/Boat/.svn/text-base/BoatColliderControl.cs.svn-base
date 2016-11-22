using UnityEngine;
using System.Collections;

public class BoatColliderControl : BaseCollider {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	protected override void touchTarget(GameObject target)
	{
		GameLogger.Log (name + "进入碰撞区域:" + target.name);

		Collider collider = target.GetComponent<Collider> ();
		//关闭碰撞体
		collider.enabled = false;

		//碰撞道具
		if (target.layer == LayerMask.NameToLayer (Constant.Layer_Item)) {

			PathItem item = target.GetComponentInChildren<PathItem> ();
			Message_TouchItem.create (item.itemUniqueId).send ();
		} 
//		else if (target.layer == LayerMask.NameToLayer (Constant.Layer_Boat)) {
//
//			float self_x=transform.localPosition.x;
//			float self_z=transform.localPosition.z;
//
//			int otherPlayerId = collider.GetComponentInChildren<BoatControl> ().playerId;
//			float other_x=collider.transform.localPosition.x;
//			float other_z=collider.transform.localPosition.z;
//
//			Message_TouchBoat.create (self_x, self_z, otherPlayerId, other_x, other_z).send ();
//		}
	}
}
