using UnityEngine;
using System.Collections;

public class Message_Login : Message {

	public string username;
	public string password;

	public static Message_Login create(MsgType type,string username,string password)
	{
		Message_Login msg = new Message_Login ();
		msg.type = type;
		msg.username = username;
		msg.password = password;
		return msg;
	}
}