using UnityEngine;
using System.Collections;

public class Message_ChangeMaxSpeed : Message {

	public float maxSpeed;
	public bool isAdd;

	public static Message_ChangeMaxSpeed create(float maxSpeed,bool isAdd)
	{
		Message_ChangeMaxSpeed msg = new Message_ChangeMaxSpeed ();
		msg.type = MsgType.ChangeMaxSpeed;
		msg.maxSpeed = maxSpeed;
		msg.isAdd = isAdd;
		return msg;
	}
}
