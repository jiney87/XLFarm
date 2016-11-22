/************************************************************************************
 * Copyright (c) 2016 XunLing CORPORATION All Rights Reserved.
 * CLR版本： 4.0.30319.42000
 *机器名称：DESKTOP-4NN1JQC
 *公司名称：讯灵科技
 *命名空间：Assets._Project.Scripts.LocalService.Messages
 *文件名：  Message_SelectSkillBoat
 *版本号：  V1.0.0.0
 *唯一标识：f4574687-aca6-4ef7-befc-6aba27f5bfde
 *创建人：  Yang zhenkun
 *创建时间：2016/9/28 10:46:47
 *描述：技能选择
 *
/************************************************************************************/

using UnityEngine;
using System.Collections;

public class Message_SelectSkillBoat :  Message {

    public int skill_id_1;
    public int skill_id_2;
    public static Message_SelectSkillBoat create(MsgType _type)
	{
		Message_SelectSkillBoat msg = new Message_SelectSkillBoat ();
        msg.type = _type;
        msg.skill_id_1 = PlayerPrefs.GetInt("skill_id_0");
        msg.skill_id_2 = PlayerPrefs.GetInt("skill_id_1");
		return msg;
	}
}
