using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using Unity_lt_net;

public class PlayerNetHandler : NetHandler {

	private static PlayerNetHandler instance;
	public static PlayerNetHandler Instance
	{
		get{ 
			if (instance == null) {
				instance= new PlayerNetHandler ();
			}
			return instance;
		}
	}

	//================================================== 指令 for server ==============================================//
	private const int Command_HavedEnterScene	=	0x0001;
	private const int Command_UseSkill	=	0x0002;
	private const int Command_UseSkillEffect	=	0x0003;
	private const int Command_TouchItem	=	0x0004;
	private const int Command_UseItem	=	0x0005;
	private const int Command_ItemTouchPlayer	=	0x0006;
	private const int Command_TouchBoat	=	0x0007;
	//================================================== 指令 for client ==============================================//
	private const int Notify_EnterScene	=	0x1000;
	private const int Notify_AddSelf	=	0x1001;
	private const int Notify_AddPlayer	=	0x1002;
	private const int Notify_BeginGame	=	0x1003;
	private const int Notify_GameOver	=	0x1004;
	private const int Notify_SceneException	=	0x1005;
	private const int Notify_AddItem	=	0x1006;
	private const int Notify_RemoveItem	=	0x1007;
	private const int Notify_AchieveItem=	0x1008;
	private const int Notify_UseItem	=	0x1009;
	private const int Notify_UseSkill	=	0x100A;
	private const int Notify_ExitScene	=	0x100B;
	private const int Notify_ChangeMaxSpeed	=	0x100C;
	private const int Notify_BoatQuote	=	0x100D;
	private const int Notify_BoatQuoteOver	=	0x100E;
	private const int Notify_ChangeSpeed =	0x100F;
	//================================================== 指令结束= ====================================================//

	public override void update (float deltaTime)
	{
		//no do
	}

	protected override void process (int command, ByteArray ba)
	{
		switch (command) {
		case Notify_EnterScene:
			Message_EnterScene.create (ba).send ();
			break;
		case Notify_AddSelf:
			Message_AddPlayer.createSelf (ba).send ();
			break;
		case Notify_AddPlayer:
			Message_AddPlayer.create (ba).send ();
			break;
		case Notify_BeginGame:
			Message_BeginGame.create (MsgType.BeginGame).send ();
			break;
		case Notify_GameOver:
			function_Notify_GameOver (ba);
			break;
		case Notify_SceneException:
			function_Notify_SceneException (ba);
			break;
		case Notify_AddItem:
			function_Notify_AddItem (ba);
			break;
		case Notify_RemoveItem:
			function_Notify_RemoveItem (ba);
			break;
		case Notify_AchieveItem:
			funcition_Notify_AchieveItem(ba);
			break;
		case Notify_UseItem:
			function_Notify_UseItem (ba);
			break;
		case Notify_UseSkill:
			function_Notify_UseSkill (ba);
			break;
		case Notify_ExitScene:
			function_Notify_ExitScene (ba);
			break;
		case Notify_ChangeMaxSpeed:
			float maxSpeed = ba.readFloat ();
			bool isAdd = ba.readBoolean ();
			Message_ChangeMaxSpeed.create (maxSpeed, isAdd).send ();
			break;
		case Notify_BoatQuote:
			function_Notify_BoatQuote (ba);
			break;
		case Notify_BoatQuoteOver:
			function_Notify_BoatQuoteOver (ba);
			break;
		case Notify_ChangeSpeed:
			function_Notify_ChangeSpeed (ba);
			break;
		default:
			GameLogger.LogError ("没有这个指令:"+Convert.ToString(command,16));
			break;
		}
	}

	#region process message

	private void function_Notify_AddItem(ByteArray ba)
	{
		long itemUniqueId = ba.readLong ();
		int itemId = ba.readInt ();
		float x = StringUtil.getFloat (ba.readUTF ());
		float z = StringUtil.getFloat (ba.readUTF ());

		Message_AddItem.create (itemUniqueId, itemId, x, z).send ();
	}

	private void function_Notify_RemoveItem(ByteArray ba)
	{
		long itemUniqueId = ba.readLong ();
		Message_RemoveItem.create (itemUniqueId).send ();
	}

	private void funcition_Notify_AchieveItem(ByteArray ba)
	{
		long itemUniqueId = ba.readLong ();
		int itemId = ba.readInt ();
		Message_AchieveItem.create (itemUniqueId, itemId).send ();
	}

	private void function_Notify_GameOver(ByteArray ba)
	{
		int sequence = ba.readInt ();
		int gold = ba.readInt ();
		int exitTime = ba.readInt ();
		Message_GameOver.create (sequence, gold, exitTime).send ();
	}

	private void function_Notify_SceneException(ByteArray ba)
	{
		int exitTime = ba.readInt ();
		Message_SceneException.create (exitTime).send ();
	}

	private void function_Notify_UseItem(ByteArray ba)
	{
		int playerId = ba.readInt ();
		long itemUniqueId = ba.readLong ();
		int itemId = ba.readInt ();
		Message_UseItemReturn.create (playerId, itemUniqueId, itemId).send ();
	}

	private void function_Notify_ExitScene(ByteArray ba)
	{
		int exitorPlayerId = ba.readInt ();
		Message_PlayerExitScene.create (exitorPlayerId).send ();
	}

	private void function_Notify_UseSkill(ByteArray ba)
	{
		int releaserPlayerId = ba.readInt ();
		int skillId = ba.readInt ();
		List<int> targetPlayerIds = ba.readIntList ();
		Message_UseSkillReturn.create (releaserPlayerId, skillId, targetPlayerIds).send ();
	}

	private void function_Notify_BoatQuote(ByteArray ba)
	{
		int playerId = ba.readInt ();
		float keepTime = ba.readFloat ();
		Message_BoatQuote.create (playerId,keepTime).send ();
	}

	private void function_Notify_BoatQuoteOver(ByteArray ba)
	{
		int playerId = ba.readInt ();
		Message_BoatQuoteOver.create (playerId).send ();
	}

	private void function_Notify_ChangeSpeed(ByteArray ba)
	{
		bool isAdd = ba.readBoolean ();
		float changeValue = ba.readFloat ();
		Message_ChangeSpeed.create (isAdd, changeValue).send ();
	}
	#endregion

	#region send to server
	public void havedEnterScene()
	{
		ByteArray ba = new ByteArray ();
		sendMessage (Command_HavedEnterScene, ba);
	}

	public void touchItem(Message_TouchItem msg)
	{
		ByteArray ba = new ByteArray ();
		ba.writeLong (msg.itemUniqueId);
		sendMessage (Command_TouchItem,ba);
	}

	public void touchBoat(Message_TouchBoat msg)
	{
		ByteArray ba = new ByteArray ();
		ba.writeFloat (msg.selfX);
		ba.writeFloat (msg.selfZ);
		ba.writeInt (msg.otherPlayerId);
		ba.writeFloat (msg.otherX);
		ba.writeFloat (msg.otherZ);
		sendMessage (Command_TouchBoat, ba);
	}

	public void useItem(Message_UseItem msg)
	{
		ByteArray ba = new ByteArray ();
		ba.writeLong (msg.itemUniqueId);
		sendMessage (Command_UseItem,ba);
	}

	public void itemTouchPlayer(Message_ItemTouchPlayer msg)
	{
		ByteArray ba = new ByteArray ();
		ba.writeLong (msg.itemUniqueId);
		sendMessage (Command_ItemTouchPlayer,ba);
	}

	public void useSkill(Message_UseSkill msg)
	{
		ByteArray ba = new ByteArray ();
		ba.writeInt (msg.skillId);
		ba.writeIntList (msg.targetPlayerIds);
		sendMessage (Command_UseSkill, ba);
	}

	public void useSkillEffect(Message_SkillEffect msg)
	{
		ByteArray ba = new ByteArray ();
		ba.writeInt (msg.skillId);
		ba.writeInt (msg.targetPlayerId);
		sendMessage (Command_UseSkillEffect, ba);
	}
	#endregion
}
