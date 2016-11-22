using UnityEngine;
using System.Collections.Generic;

public class Message_UseSkillReturn : Message {

	public int releaserPlayerId;
	public int skillId;
	public List<int> targetPlayerIds;

	public static Message_UseSkillReturn create(int releaserPlayerId,int skillId,List<int> targetPlayerIds)
	{
		Message_UseSkillReturn msg = new Message_UseSkillReturn ();
		msg.type = MsgType.UseSkillReturn;
		msg.releaserPlayerId = releaserPlayerId;
		msg.skillId = skillId;
		msg.targetPlayerIds = targetPlayerIds;
		return msg;
	}
}
