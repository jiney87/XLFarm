using UnityEngine;
using System.Collections;

public class Message_Transition : Message {

	public string des;
	public AsyncOperation op;

	public static Message_Transition create(string des,AsyncOperation op)
	{
		Message_Transition msg = new Message_Transition ();
		msg.type = MsgType.Transition;
		msg.des = des;
		msg.op = op;
		return msg;
	}
}
