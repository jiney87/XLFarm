using UnityEngine;
using System.Collections;

public class Message_BeginGame : Message {

	public static Message_BeginGame create(MsgType type)
	{
		Message_BeginGame msg = new Message_BeginGame ();
		msg.type = type;
		return msg;
	}
}
