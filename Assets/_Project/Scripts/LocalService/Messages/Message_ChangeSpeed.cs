using UnityEngine;
using System.Collections;

public class Message_ChangeSpeed : Message {

	public bool isAdd;
	public float changeValue;

	public static Message_ChangeSpeed create(bool isAdd,float changeValue)
	{
		Message_ChangeSpeed msg = new Message_ChangeSpeed ();
		msg.type = MsgType.ChangeSpeed;
		msg.isAdd = isAdd;
		msg.changeValue = changeValue;
		return msg;
	}
}
