using UnityEngine;
using System.Collections;

public class Message_LoginReturn : Message {

	public int playerId;

	public static Message_LoginReturn create(int playerId)
	{
		Message_LoginReturn msg = new Message_LoginReturn ();
		msg.type = MsgType.Login_Return;
		msg.playerId = playerId;
		return msg;
	}
}
