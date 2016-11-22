/************************************************************************************
 * Copyright (c) 2016 XunLing CORPORATION All Rights Reserved.
 * CLR版本： 4.0.30319.42000
 *机器名称：DESKTOP-4NN1JQC
 *公司名称：讯灵科技
 *命名空间：Assets._Project.Scripts.UI.Skill
 *文件名：  BoatSkillMenuPanel
 *版本号：  V1.0.0.0
 *唯一标识：3f120fa8-16a2-41ef-8101-8c60452dbe23
 *创建人：  Yang zhenkun
 *创建时间：2016/9/27 11:10:38
 *描述：选择技能Item
 *
/************************************************************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoatSkillItemPanel : MonoBehaviour
{
    #region 变量
    public Image skill_icon;
    public Text skill_name;
    public Text skill_msg;
    public Text skill_gold_number;
    public SkillData item_skill_data;
    public GameObject select_tect_foreground;
    //是否点选了当前的技能
    public bool is_select_item = false;
         
    
    public delegate void OnSelectItemClick(BoatSkillItemPanel _item);
    public OnSelectItemClick SelectItemback { get; set; }
    #endregion
    // Use this for initialization
	void Start () {
        is_select_item = false;
        setForeground();
	}
	
	// Update is called once per frame
	void Update () {

    }
    #region 外部调用
    //初始化
    public void setInit(SkillData _data)
    {
        item_skill_data = _data;
        skill_gold_number.text = ""+item_skill_data.gold;
        skill_msg.text = item_skill_data.des;
        skill_name.text = item_skill_data.name;
        skill_icon.sprite = ResourcesUtil.loadSprite(item_skill_data.icon);
    }
    public void OnClickButt()
    {
        if (SelectItemback != null)
        {
            is_select_item =!is_select_item;
            SelectItemback(this);
        }
    }
    public void setForeground()
    {
        select_tect_foreground.SetActive(is_select_item);
    }
    #endregion
}
