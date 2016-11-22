using UnityEngine;
using System.Collections;

public class Message_ItemTouchPlayer : Message {

	public long itemUniqueId;

	public static Message_ItemTouchPlayer create(long itemUniqueId)
	{
		Message_ItemTouchPlayer msg = new Message_ItemTouchPlayer ();
		msg.type = MsgType.ItemTouchPlayer;
		msg.itemUniqueId = itemUniqueId;
		return msg;
	}
}
