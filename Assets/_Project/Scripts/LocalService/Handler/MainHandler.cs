using UnityEngine;
using System.Collections;

public class MainHandler : BaseHandler {

	private static MainHandler instance;
	public static MainHandler Instance{
		get{ 
			if (instance == null) {
				instance = new MainHandler ();
			}
			return instance;
		}
	}

	protected override void process (BaseScene baseScene, Message msg)
	{
		MainScene scene = baseScene as MainScene;

		switch (msg.type) {
#region to server
		case MsgType.PveQueue:
			QueueNetHandler.Instance.enterQueue (Constant.SceneType_Pve);
			break;
		case MsgType.PvpQueue:
			QueueNetHandler.Instance.enterQueue (Constant.SceneType_Pvp);
			break;
            case MsgType.Select_Skill://选择技能
            SkillSelectNetHandler.Instance.SelectSkillId();
            break;
#endregion

#region to client
		case MsgType.EnterScene:
			logicEnterScene (scene,msg as Message_EnterScene);
			break;
#endregion
		}
	}

	private void logicEnterScene(MainScene scene,Message_EnterScene msg)
	{
		bool success = msg.success;
		int sceneId = msg.sceneId;

		if (!success) {
			scene.queueError ();
			return;
		}
		GameManager.instance.loadScene (Constant.Scene_Game, havedEnterScene, true);
	}

	private void havedEnterScene()
	{
		PlayerNetHandler.Instance.havedEnterScene ();
	}
}
