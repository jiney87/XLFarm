using UnityEngine;
using System.Collections;

public class Message_SkillEffect : Message {

	public int skillId;
	public int targetPlayerId;

	public static Message_SkillEffect create(int skillId,int targetPlayerId)
	{
		Message_SkillEffect msg = new Message_SkillEffect ();
		msg.type = MsgType.SkillEffect;
		msg.skillId = skillId;
		msg.targetPlayerId = targetPlayerId;
		return msg;
	}
}