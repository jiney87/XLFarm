using UnityEngine;
using System.Collections;

public class Message_UseItemReturn : Message {

	public int playerId;//使用者
	public long itemUniqueId;
	public int itemId;

	public static Message_UseItemReturn create(int playerId,long itemUniqueId,int itemId)
	{
		Message_UseItemReturn msg = new Message_UseItemReturn ();
		msg.type = MsgType.UseItemReturn;
		msg.playerId = playerId;
		msg.itemUniqueId = itemUniqueId;
		msg.itemId = itemId;
		return msg;
	}
}
