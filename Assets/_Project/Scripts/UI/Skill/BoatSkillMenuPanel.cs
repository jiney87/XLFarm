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
 *描述：选择技能界面
 *
/************************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoatSkillMenuPanel : MonoBehaviour
{
    #region 变量
    //添加item的父节点
    public GameObject skill_content_bj;
    //提示文字 每场战斗最多使用两个技能
    public GameObject skill_tips_text;
    private float tips_show_number;
    private List<int> select_item_list;
    #endregion
    #region 方法
    // Use this for initialization
    void Start()
    {
        setInit();
        select_item_list = new List<int>();
        skill_tips_text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (tips_show_number > 0)
        {
            tips_show_number -= Time.deltaTime;
            if (tips_show_number <= 0)
            {
                skill_tips_text.SetActive(false);
            }
        }
    }
    //初始化当前的界面
    public void setInit()
    {
        Debug.Log("初始化技能信息");
        for (int i = 0; i < SkillData.getDataLength(); i++ )
        {
            var go = ResourcesUtil.loadGameObject("Prefab/2D/Skill/Boat_Skill_Item_Panel");
            if (go)
            {
                BoatSkillItemPanel page = go.GetComponent<BoatSkillItemPanel>();
                page.setInit(SkillData.getData(1001+i));
                page.SelectItemback = setSelectSkillItem;
            }
            Util.AttachToParent(go, skill_content_bj);
        }
        skill_content_bj.GetComponent<RectTransform>().sizeDelta = new Vector2(SkillData.getDataLength() * 215 + 20,skill_content_bj.GetComponent<RectTransform>().sizeDelta.y);
        //skill_content_bj.GetComponent<RectTransform>().localPosition = new Vector3(0, 0,0);
    }
    //返回按钮
    public void OnReturnClickButt()
    {
 
    }
    //是否拥有技能
    public bool isSelectSkillNumber()
    {
        if (select_item_list.Count < 2)
        {
            return false;
        }
        for (int i = 0; i < select_item_list.Count && i < 2; i++)
        {
            if (select_item_list[i] < 0)
                return false;
            else
                PlayerPrefs.SetInt("skill_id_" + i, select_item_list[i]);
        }
            return true;
    }
    //接收当前选择的技能
    public void setSelectSkillItem(BoatSkillItemPanel _item)
    {
        if (_item.is_select_item)
        {
            if (select_item_list.Equals(_item.item_skill_data.id))
            {
                
            }
            else if (select_item_list.Count < 2)
            {
                select_item_list.Add(_item.item_skill_data.id);
            }
            else
            {
                _item.is_select_item = false;
                for (int i = 0; i < select_item_list.Count; i++)
                {
                    if (select_item_list[i] == -1)
                    {
                        select_item_list[i] = _item.item_skill_data.id;
                        _item.is_select_item = true;
                        break;
                    }
                }
                //如果是没有能选上 提示只能选择最多两个技能
                if (!_item.is_select_item)
                {
                    skill_tips_text.SetActive(true);
                    tips_show_number = 1.0f;
                }
            }
        }
        else {
            for (int i = 0; i < select_item_list.Count; i++)
            {
                if (select_item_list[i] == _item.item_skill_data.id)
                {
                    select_item_list[i] = -1;
                }
            }
        }
        //Debug.Log(select_item_list.Count);
        _item.setForeground();
        
    }
    #endregion
}
