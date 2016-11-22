using UnityEngine;
using System.Collections.Generic;

public class PlayerHandler : BaseHandler {

	private static PlayerHandler instance;
	public static PlayerHandler Instance{
		get{ 
			if (instance == null) {
				instance = new PlayerHandler ();
			}
			return instance;
		}
	}

	protected override void process(BaseScene baseScene,Message msg)
	{
		GameScene scene = baseScene as GameScene;

		switch (msg.type) {
#region send to client
		case MsgType.AddSelf:
			scene.createSelf (msg as Message_AddPlayer);
			break;
		case MsgType.AddPlayer:
			scene.createEnemy (msg as Message_AddPlayer);
			break;
		case MsgType.BeginGame:
			scene.beginGame();
			break;
		case MsgType.GameOver:
			scene.showResult (msg as Message_GameOver);
			break;
		case MsgType.SceneException:
			scene.sceneException(msg as Message_SceneException);
			break;
		case MsgType.Move_ToClient:
			scene.playerMove (msg as Message_MoveToClient);
			break;
		case MsgType.Add_Item:
			scene.addItem(msg as Message_AddItem);
			break;
		case MsgType.RemoveItem:
			scene.removeItem(msg as Message_RemoveItem);
			break;
		case MsgType.AchieveItem:
			scene.achieveItem(msg as Message_AchieveItem);
			break;
		case MsgType.UseItemReturn:
			scene.useItem(msg as Message_UseItemReturn);
			break;
		case MsgType.UseSkillReturn:
			scene.useSkill(msg as Message_UseSkillReturn);
			break;
		case MsgType.ChangeMaxSpeed:
			scene.changeMaxSpeed(msg as Message_ChangeMaxSpeed);
			break;
		case MsgType.BoatQuote:
			scene.boatQuote(msg as Message_BoatQuote);
			break;
		case MsgType.BoatQuoteOver:
			scene.boatQuoteOver(msg as Message_BoatQuoteOver);
			break;
		case MsgType.ChangeSpeed:
			scene.changeBoatSpeed(msg as Message_ChangeSpeed);
			break;
		case MsgType.PlayerExitScene:
			scene.playerExitScene(msg as Message_PlayerExitScene);
			break;
#endregion

#region send to server
		case MsgType.Move_ToServer:
			MoveNetHandler.Instance.moveTo (msg as Message_MoveToServer);
			break;
		case MsgType.TouchItem:
			PlayerNetHandler.Instance.touchItem(msg as Message_TouchItem);
			break;
		case MsgType.TouchBoat:
			PlayerNetHandler.Instance.touchBoat(msg as Message_TouchBoat);
			break;
		case MsgType.UseItem:
			PlayerNetHandler.Instance.useItem(msg as Message_UseItem);
			break;
		case MsgType.ItemTouchPlayer:
			PlayerNetHandler.Instance.itemTouchPlayer(msg as Message_ItemTouchPlayer);
			break;
		case MsgType.UseSkill:
			PlayerNetHandler.Instance.useSkill(msg as Message_UseSkill);
			break;
		case MsgType.SkillEffect:
			PlayerNetHandler.Instance.useSkillEffect(msg as Message_SkillEffect);
			break;
#endregion
		}
	}
}
