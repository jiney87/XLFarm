﻿using UnityEngine;
using System.Collections;

public class Constant  {

	#region 常量
	//屏幕宽高,代替Screen.width、Screen.height
	public const int ScreenWidth=1280;
	public const int ScreenHeight=720;

	public const string Username = "username";
	public const string Password = "password";
	#endregion

	#region scene
	public const string Scene_Start = "Start";//初始场景
	public const string Scene_LoadResource = "LoadResource";//加载资源场景
	public const string Scene_Login = "Login";//登录场景
	public const string Scene_Main = "Main";//主场景
	public const string Scene_Game = "Game";//游戏场景


	public const string Scene_Transition = "Transition";//过渡场景
	#endregion


	#region layer
	public const string Layer_Item = "Item";
	public const string Layer_UseItem = "UseItem";
	public const string Layer_Boat = "Boat";
	#endregion

	#region game
	public const int SceneType_Pve=1;
	public const int SceneType_Pvp=2;
	#endregion
}

public enum LoadStep:int
{
	To_CopyBins,//复制streamingAssets下的bin文件
	To_Verify,//验证大版本
	To_DownloadPackage,//下载游戏包
	To_DownloadVersionFile,//下载资源版本文件
	To_CompareVersionFile,//比较资源版本文件
	To_DownloadResourceFile,//下载资源文件
	To_LoadBins,//加载资源文件
	To_RequestServers,//请求服务器列表
}

public enum MsgType:int
{
	Login,
	Login_Return,
	Tick,
    Select_Skill,//技能选择
	PveQueue,
	PvpQueue,
	EnterScene,//进入游戏场景
	AddSelf,
	AddPlayer,
	BeginGame,
	SceneException,
	GameOver,
	Move_ToServer,
	Move_ToClient,
	Add_Item,
	TouchItem,
	TouchBoat,
	RemoveItem,
	AchieveItem,
	UseItem,
	UseItemReturn,
	UseSkill,
	UseSkillReturn,
	SkillEffect,
	PlayerExitScene,
	ItemTouchPlayer,
	ChangeMaxSpeed,
	BoatQuote,
	BoatQuoteOver,
	ChangeSpeed,


	Transition,
	DisConnect,
	ErrorCode,
}


public enum ItemType:int
{
	Type1_Huojian=1,//火箭
	Type2_SpeedAdd=2,//加速
	Type3_SpeedMinus=3,//减速
	Type4_Invincible=4,//无敌
	Type5_Block=5,//障碍物
}