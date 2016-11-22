using UnityEngine;
using System.Collections;
using Unity_lt_net;

public class Message_EnterScene : Message {

	public bool success;
	public int sceneId;

	public static Message_EnterScene create(ByteArray ba)
	{
		Message_EnterScene msg = new Message_EnterScene ();
		msg.type = MsgType.EnterScene;
		
		msg.success = ba.readBoolean ();//排队成功
		msg.sceneId = ba.readInt ();//场景Id
		return msg;
	}
}
