using UnityEngine;
using System.Collections;
using Unity_lt_net;

public class Message_AddPlayer : Message {

	public int playerId;
	public string name;
	public int skillId1;
	public int skillId2;
	public float x;
	public float z;
	public float xMul;
	public float minX;
	public float maxX;
	public float zSpeed;
	public float zSpeedMul;
	public float maxZSpeed;
	public float minZSpeed;
	public float natureZAcc;


	public static Message_AddPlayer createSelf(ByteArray ba)
	{
		Message_AddPlayer msg = new Message_AddPlayer ();
		msg.type = MsgType.AddSelf;
		readInfo (msg, ba);
		return msg;
	}

	public static Message_AddPlayer create(ByteArray ba)
	{
		Message_AddPlayer msg = new Message_AddPlayer ();
		msg.type = MsgType.AddPlayer;
		readInfo (msg, ba);
		return msg;
	}

	private static void readInfo(Message_AddPlayer msg,ByteArray ba)
	{
		msg.playerId = ba.readInt ();
		msg.name = ba.readUTF ();
		msg.skillId1 = ba.readInt ();
		msg.skillId2 = ba.readInt ();
		msg.x = ba.readFloat ();
		msg.z = ba.readFloat ();
		msg.xMul = ba.readFloat ();
		msg.minX = ba.readFloat ();
		msg.maxX = ba.readFloat ();
		msg.zSpeed = ba.readFloat ();
		msg.zSpeedMul = ba.readFloat ();
		msg.maxZSpeed = ba.readFloat ();
		msg.minZSpeed = ba.readFloat ();
		msg.natureZAcc = ba.readFloat ();
	}
}
