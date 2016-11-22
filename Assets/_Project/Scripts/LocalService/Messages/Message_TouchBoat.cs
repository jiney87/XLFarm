using UnityEngine;
using System.Collections;

public class Message_TouchBoat : Message {

	public float selfX;
	public float selfZ;

	public int otherPlayerId;
	public float otherX;
	public float otherZ;

	public static Message_TouchBoat create(float selfX,float selfZ,int otherPlayerId,float otherX,float otherZ)
	{
		Message_TouchBoat msg = new Message_TouchBoat ();
		msg.type = MsgType.TouchBoat;
		msg.selfX = selfX;
		msg.selfZ = selfZ;
		msg.otherPlayerId = otherPlayerId;
		msg.otherX = otherX;
		msg.otherZ = otherZ;
		return msg;
	}
}
