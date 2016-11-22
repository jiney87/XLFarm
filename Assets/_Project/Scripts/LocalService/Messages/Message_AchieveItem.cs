using UnityEngine;
using System.Collections;

public class Message_AchieveItem : Message {

	public long itemUniqueId;
	public int itemId;

	public static Message_AchieveItem create(long itemUniqueId,int itemId)
	{
		Message_AchieveItem msg = new Message_AchieveItem ();
		msg.type = MsgType.AchieveItem;
		msg.itemUniqueId = itemUniqueId;
		msg.itemId = itemId;
		return msg;
	}
}
