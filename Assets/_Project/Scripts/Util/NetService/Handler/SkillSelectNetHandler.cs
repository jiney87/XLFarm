/************************************************************************************
 * Copyright (c) 2016 XunLing CORPORATION All Rights Reserved.
 * CLR版本： 4.0.30319.42000
 *机器名称：DESKTOP-4NN1JQC
 *公司名称：讯灵科技
 *命名空间：Assets._Project.Scripts.Util.NetService.Handler
 *文件名：  SkillSelectNetHandler
 *版本号：  V1.0.0.0
 *唯一标识：7daacd71-08c4-4033-8a30-a010e35c8306
 *创建人：  Yang zhenkun
 *创建时间：2016/9/27 16:01:16
 *描述：技能选择
 *
/************************************************************************************/

using UnityEngine;
using System.Collections;
using Unity_lt_net;

public class SkillSelectNetHandler : NetHandler
{

    private static SkillSelectNetHandler instance;
    public static SkillSelectNetHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SkillSelectNetHandler();
            }
            return instance;
        }
    }

    //================================================== 指令 for server ==============================================//
    private const int Command_Select_Skill = 0x0008;
    //================================================== 指令 for client ==============================================//
    private const int Notify_Select_Skill = 0x1010;
    //================================================== 指令结束= ====================================================//

    protected override void process(int command, ByteArray data)
    {
        switch (command)
        {
            case Notify_Select_Skill:
                fuction_Notify_Skill(data);
                break;
        }
    }

    public override void update(float deltaTime)
    {

    }
    #region send to client
    private void fuction_Notify_Skill(ByteArray ba)
    {
        int notify_number = ba.readInt();
        //int x = StringUtil.getFloat(ba.readUTF());
        //如果是技能返回成功的话
        Debug.Log("playerId == " + notify_number);
        if (notify_number == 1)
        {
            if (PlayerPrefs.GetInt("is_pvp_games") == 1)
            {
                UserInfo.sceneType = Constant.SceneType_Pvp;
                Message.create(MsgType.PvpQueue).send();

            }
            else
            {
                UserInfo.sceneType = Constant.SceneType_Pve;
                Message.create(MsgType.PveQueue).send();
            }
        }
        
    }
    #endregion

    #region send to server
    public void SelectSkillId()
    {
        //通知移动
        ByteArray ba = new ByteArray();
        ba.writeInt(Command_Select_Skill);
        ba.writeInt(PlayerPrefs.GetInt("skill_id_0"));
        ba.writeInt(PlayerPrefs.GetInt("skill_id_1"));
        sendMessage(Command_Select_Skill, ba);
    }
    #endregion
}
