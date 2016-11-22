using UnityEngine;
using System.Collections;

public class Message_GameOver : Message {

	public int sequence;
	public int gold;
	public int exitTime;

	public static Message_GameOver create(int sequence,int gold,int exitTime)
	{
		Message_GameOver msg = new Message_GameOver ();
		msg.type = MsgType.GameOver;
		msg.sequence = sequence;
		msg.gold = gold;
		msg.exitTime = exitTime;
		return msg;
	}
}
