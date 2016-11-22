using UnityEngine;
using System.Collections;
using System;
using System.Text;
using Unity_lt_net;

public class UserNetHandler : NetHandler {

	private static UserNetHandler instance;
	public static UserNetHandler Instance
	{
		get{ 
			if (instance == null) {
				instance = new UserNetHandler ();
			}
			return instance;	
		}
	}


	//================================================== 指令 for server ==============================================//
	private const int Command_Login		=	0x0001;
	private const int Command_Logout	=	0x0FFE;
	//================================================== 指令 for client ==============================================//
	private const int Notify_Login	=	0x1001;
	private const int Notify_Logout	=	0x1FFE;
	//================================================== 指令结束= ====================================================//


	public override void update (float deltaTime)
	{
		//no do
	}

	protected override void process (int command, ByteArray ba)
	{
		switch (command) {
		case Notify_Login:
			function_Notify_Login (ba);
			break;
		case Notify_Logout:
			function_Notify_Logout (ba);
			break;
		default:
			GameLogger.LogError ("没有这个指令:"+Convert.ToString(command,16));
			break;
		}
	}

	#region process message
	private void function_Notify_Login(ByteArray ba)
	{
		int playerId = ba.readInt ();
		Message_LoginReturn.create (playerId).send ();
	}

	private void function_Notify_Logout(ByteArray ba)
	{
//		int playerId = ba.readInt ();
//		UserInfo.removePlayer (playerId);
	}
	#endregion

	#region send to server
	public void login(string username,string password)
	{
		ByteArray ba = new ByteArray ();
		ba.writeUTF (username);
		ba.writeUTF (password);
		sendMessage (Command_Login, ba);
	}
	#endregion
}
