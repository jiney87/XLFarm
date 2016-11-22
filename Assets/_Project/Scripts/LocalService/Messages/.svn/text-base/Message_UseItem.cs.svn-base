using UnityEngine;
using System.Collections;

public class Message_UseItem : Message {

	public long itemUniqueId;
	public int itemId;

	public static Message_UseItem create(long itemUniqueId,int itemId)
	{
		Message_UseItem msg = new Message_UseItem ();
		msg.type = MsgType.UseItem;
		msg.itemUniqueId = itemUniqueId;
		msg.itemId = itemId;
		return msg;
	}
}
