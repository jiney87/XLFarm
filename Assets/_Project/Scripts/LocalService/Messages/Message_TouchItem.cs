using UnityEngine;
using System.Collections;

public class Message_TouchItem : Message {

	public long itemUniqueId;

	public static Message_TouchItem create(long itemUniqueId)
	{
		Message_TouchItem msg = new Message_TouchItem ();
		msg.type = MsgType.TouchItem;
		msg.itemUniqueId = itemUniqueId;
		return msg;
	}
}
