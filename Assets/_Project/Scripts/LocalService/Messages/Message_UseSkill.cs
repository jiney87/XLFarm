using UnityEngine;
using System.Collections.Generic;

public class Message_UseSkill : Message {

	public int skillId;
	public List<int> targetPlayerIds;

	public static Message_UseSkill create(int skillId,List<int> targetPlayerIds)
	{
		Message_UseSkill msg = new Message_UseSkill ();
		msg.type = MsgType.UseSkill;
		msg.skillId = skillId;
		msg.targetPlayerIds = targetPlayerIds;
		return msg;
	}
}
