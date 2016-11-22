using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemUIControl : MonoBehaviour {

	public Image itemImage;
	public long itemUniqueId;
	public int itemId;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void init(Message_AchieveItem msg)
	{
		itemUniqueId = msg.itemUniqueId;
		itemId = msg.itemId;

		//设置图片
		ItemData itemData = ItemData.getData (msg.itemId);
		itemImage.sprite = ResourcesUtil.loadSprite (itemData.icon);

		UGUIEventListener.Get (gameObject).OnPointerClicks.Add (onClick);
	}

	public void onClick(GameObject go,PointerEventData data)
	{
		Message_UseItem.create (itemUniqueId, itemId).send ();
	}
}
