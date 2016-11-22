using UnityEngine;
using System.Collections;

public class Message_MoveToServer : Message {

	public int playerId;
	public float x;
	public float z;
	public float speed;

	public static Message_MoveToServer create(MsgType type,int playerId,float x,float z,float speed)
	{
		Message_MoveToServer msg = new Message_MoveToServer ();
		msg.type = type;
		msg.playerId = playerId;
		msg.x = x;
		msg.z = z;
		msg.speed = speed;
		return msg;
	}
}
