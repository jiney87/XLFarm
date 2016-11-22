using UnityEngine;
using System.Collections;

public class MainScene : BaseScene {

	public GameObject tipsGo;
    private bool is_pvp_games = false;
    //选择技能界面
    public GameObject select_skill_go;

	// Use this for initialization
	protected override void start ()
	{
		hideTips ();
	}

	public void onClickPVE()
	{
        is_pvp_games = false;
        select_skill_go.SetActive(true);
        //UserInfo.sceneType = Constant.SceneType_Pve;
        //Message.create (MsgType.PveQueue).send ();
        //tipsGo.SetActive (true);
	}

	public void onClickPVP()
	{
        is_pvp_games = true;
        select_skill_go.SetActive(true);
        //UserInfo.sceneType = Constant.SceneType_Pvp;
        //Message.create (MsgType.PvpQueue).send ();
        //tipsGo.SetActive (true);
	}
    public void OnClickGame()
    {
        BoatSkillMenuPanel tep_panel = select_skill_go.GetComponent<BoatSkillMenuPanel>();

        if (!tep_panel.isSelectSkillNumber())
        {
            this.showPopWarn("请选择两个技能后进入游戏", null);
            return;
        }
        if (is_pvp_games)
        {
            PlayerPrefs.SetInt("is_pvp_games",1);
        }
        else
            PlayerPrefs.SetInt("is_pvp_games", 0);
        //发送请求
        Message_SelectSkillBoat.create(MsgType.Select_Skill).send();
        tipsGo.SetActive(true);
    }

	public void queueError()
	{
		showPopWarn ("排队出错", hideTips);
	}

	private void hideTips()
	{
		tipsGo.SetActive (false);
        select_skill_go.SetActive(false);
	}
}
