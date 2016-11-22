using UnityEngine;
using System.Collections;

public class Message_ErrorCode : Message {

	public int errorCode;

	public static Message_ErrorCode create(int errorCode)
	{
		Message_ErrorCode msg = new Message_ErrorCode ();
		msg.type = MsgType.ErrorCode;
		msg.errorCode = errorCode;
		return msg;
	}
}
