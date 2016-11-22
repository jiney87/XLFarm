using UnityEngine;
using System.Collections;

public class Message_MoveToClient : Message {

	public int playerId;
	public float x;
	public float z;
	public float speed;

	public static Message_MoveToClient create(int playerId,float x,float z,float speed)
	{
		Message_MoveToClient msg = new Message_MoveToClient ();
		msg.type = MsgType.Move_ToClient;
		msg.playerId = playerId;
		msg.x = x;
		msg.z = z;
		msg.speed = speed;
		return msg;
	}
}
