using UnityEngine;
using System.Collections.Generic;

public class Message_PlayerExitScene : Message {

	public int exitorPlayerId;

	public static Message_PlayerExitScene create(int exitorPlayerId)
	{
		Message_PlayerExitScene msg = new Message_PlayerExitScene ();
		msg.type = MsgType.PlayerExitScene;
		msg.exitorPlayerId = exitorPlayerId;
		return msg;
	}
}
