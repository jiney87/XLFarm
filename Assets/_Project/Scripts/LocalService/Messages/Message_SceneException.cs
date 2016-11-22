using UnityEngine;
using System.Collections;

public class Message_SceneException : Message {

	public int exitTime;

	public static Message_SceneException create(int exitTime)
	{
		Message_SceneException msg = new Message_SceneException ();
		msg.type = MsgType.SceneException;
		msg.exitTime = exitTime;
		return msg;
	}
}
