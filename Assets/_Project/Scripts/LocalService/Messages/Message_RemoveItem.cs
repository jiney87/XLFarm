using UnityEngine;
using System.Collections;

public class Message_RemoveItem : Message {

	public long itemUniqueId;

	public static Message_RemoveItem create(long itemUniqueId)
	{
		Message_RemoveItem msg = new Message_RemoveItem ();
		msg.type = MsgType.RemoveItem;
		msg.itemUniqueId = itemUniqueId;
		return msg;
	}
}
