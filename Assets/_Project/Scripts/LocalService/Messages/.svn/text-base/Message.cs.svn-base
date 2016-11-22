using UnityEngine;
using System.Collections;

public class Message
{
	public MsgType type;

	public void send()
	{
		MessageDispather.instance.addMessage (this);
		//GameLogger.Log ("发送消息:" + GetType ());
	}

	public static Message create(MsgType type)
	{
		Message msg = new Message ();
		msg.type = type;
		return msg;
	}
}