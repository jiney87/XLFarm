using UnityEngine;
using System.Collections;

public class LoginHandler : BaseHandler {

	private static LoginHandler instance;
	public static LoginHandler Instance{
		get{ 
			if (instance == null) {
				instance = new LoginHandler ();
			}
			return instance;
		}
	}

	protected override void process (BaseScene baseScene, Message msg)
	{
		LoginScene scene = (LoginScene)baseScene;

		switch (msg.type) {
		case MsgType.Login:
			login (msg as Message_Login);
			break;
		case MsgType.Login_Return:
			loginReturn (msg as Message_LoginReturn);
			break;
		}
	}

	private void login(Message_Login msg)
	{
		string username = msg.username;
		string password = msg.password;
		UserNetHandler.Instance.login (username, password);
	}

	private void loginReturn(Message_LoginReturn msg)
	{
		int playerId = msg.playerId;
		UserInfo.playerId = playerId;
		GameManager.instance.loadScene (Constant.Scene_Main, null, false);
	}
}
