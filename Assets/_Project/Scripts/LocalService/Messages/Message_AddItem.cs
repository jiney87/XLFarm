using UnityEngine;
using System.Collections;

public class Message_AddItem : Message {

	public long itemUniqueId;
	public int itemId;
	public float x;
	public float z;

	public static Message_AddItem create(long itemUniqueId,int itemId,float x,float z)
	{
		Message_AddItem msg = new Message_AddItem ();
		msg.type = MsgType.Add_Item;
		msg.itemUniqueId = itemUniqueId;
		msg.itemId = itemId;
		msg.x = x;
		msg.z = z;
		return msg;
	}
}
