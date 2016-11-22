<<<<<<< .mine
﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity_lt_net;

[RequireComponent(typeof(MessageDispather),typeof(NetMessageDispatcherForUnity),typeof(HttpService))]
public class GameManager : MonoBehaviour {

	public static GameManager instance;

	[Header("版本")]
	public string version = "1.0.0";
	[Header("渠道Id")]
	public string channel="";

	[Header("验证服的IP和端口")]
	public string verifyIp;
	public int verifyPort;

	//验证地址
	[HideInInspector]
	public string verifyAddress = "http://127.0.0.1:8080/verify_server/verify.htm?action=verifyVersion";
	//服务器列表地址
	[HideInInspector]
	public string serversAddress = "http://127.0.0.1:8080/verify_server/verify.htm?action=servers";

	//游戏服的IP和端口
	[HideInInspector]
	public string serverIp;
	[HideInInspector]
	public int serverPort;

	[Header("游戏消息分发器"),HideInInspector]
	public NetMessageDispatcherForUnity gameNetDispatcher;
	[Header("HTTP服务"),HideInInspector]
	public HttpService httpService;
	[Header("聊天消息分发器"),HideInInspector]
	public NetMessageDispatcherForUnity chatNetDispatcher;

	[Header("显示Log")]
	public bool showLog;
	[Header("显示GUI")]
	public bool showGUI;

	private int totalFrame;
	private float totalTime;

	private int ping;
	public static int fps;
	private int inputBytes;
	private int outputBytes;


	void Awake()
	{
		instance = this;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Application.targetFrameRate = 60;
		DontDestroyOnLoad (gameObject);
		hideFlags = HideFlags.NotEditable;
		GameLogger.enableLog = showLog;
	}

	// Use this for initialization
	void Start () {
		initVariables ();
		initFramwork ();
		enterGame ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	private GUIStyle style;
	void OnGUI()
	{
		if (!showGUI) {
			return;
		}

		//计算帧率
		totalFrame++;
		totalTime += Time.deltaTime;
		if (totalTime >= 1f) {
			fps = (int)(totalFrame / totalTime);
			totalFrame = 0;
			totalTime = 0;
		}

		if (style == null) {
			style = new GUIStyle ();
			style.normal.textColor = Color.cyan;
			style.fontSize = 30;
		}
		GUI.Label (new Rect (10, 0, 10, 10), "运行时长="+Util.getTimeStrBySecond(Util.getSecondsFromStartup())+"\n"+SystemInfo.graphicsDeviceVersion + "\nPing=" + ping + "\nFps=" + fps + "\n入网流量=" + ((float)inputBytes/1024).ToString("0.00") + "KB\n出网流量=" + ((float)outputBytes/1024).ToString("0.00")+"KB", style);
	}


	void OnDestroy()
	{
		closeTcpService ();
	}

	public string getTrueVerifyAddress()
	{
		return verifyAddress.Replace ("127.0.0.1", verifyIp).Replace ("8080", verifyPort + "");
	}
	public string getTrueServersAddress()
	{
		return serversAddress.Replace ("127.0.0.1", verifyIp).Replace ("8080", verifyPort + "");
	}

	private void initVariables()
	{
		gameNetDispatcher = GetComponent<NetMessageDispatcherForUnity> ();
		httpService = GetComponent<HttpService> ();
	}

	private void initFramwork()
	{
		//注册逻辑层消息执行对象,每个场景可以注册多个handler,但一个handler不能在多个场景注册
		MessageDispather.instance.registe (Constant.Scene_Start,StartHandler.Instance);

		MessageDispather.instance.registe (Constant.Scene_LoadResource, LoadResourceHandler.Instance);

		MessageDispather.instance.registe (Constant.Scene_Login, LoginHandler.Instance);

		MessageDispather.instance.registe (Constant.Scene_Transition, TransitionHandler.Instance);

		MessageDispather.instance.registe (Constant.Scene_Main, MainHandler.Instance);

		MessageDispather.instance.registe (Constant.Scene_Game, PlayerHandler.Instance);
		//更多

		//注册网络层消息执行对象
		gameNetDispatcher.registe (0x1000,UserNetHandler.Instance);
		gameNetDispatcher.registe (0x1001,QueueNetHandler.Instance);
		gameNetDispatcher.registe (0x1002,PlayerNetHandler.Instance);
		gameNetDispatcher.registe (0x1003,MoveNetHandler.Instance);
		//更多

		gameNetDispatcher.registe (0x7FFF,TickNetHandler.create());
	}

	private void enterGame()
	{
		loadScene (Constant.Scene_LoadResource, null, false);
	}

	private void recordInputBytes(int num)
	{
		inputBytes += num;
	}
	private void recordOutputBytes(int num)
	{
		outputBytes += num;
	}
	private void pingChange(int ping)
	{
		this.ping = ping;
	}

	//打开TCP服务
	public bool openTcpService()
	{
		//连接服务器
		if (gameNetDispatcher.TcpHandler.connectServer (serverIp, serverPort, "游戏TCP")) {
			//设置流量监控
			gameNetDispatcher.TcpHandler.setRecordMethod(recordInputBytes,recordOutputBytes);
			//网络出错回调
			gameNetDispatcher.setNetErrorCallback (closeTcpService);
			//超时回调
			TickNetHandler.Instance.setPingTimeoutCallback (closeTcpService);
			//启动心跳
			TickNetHandler.Instance.setPingChange (pingChange);
			TickNetHandler.Instance.startTick ();
			return true;
		}
		return false;
	}

	//关闭TCP服务
	private void closeTcpService()
	{
		//断开连接
		gameNetDispatcher.TcpHandler.disConnectServer ();
		//注销tick检测
		TickNetHandler.Instance.stopTick ();
		//通知断开连接
		MessageDispather.instance.addMessage (Message_DisConnect.create ());
	}

	#region load_scene
	private bool isLoadingScene;
	private UnityAction loadSceneOverCallback;
	public void loadScene(string sceneName,UnityAction callback,bool withTransition)
	{
		if (isLoadingScene) {
			return;
		}
		MessageDispather.instance.releaseScene ();
		ResourcesUtil.releaseAllResources ();

		loadSceneOverCallback = callback;
		if (withTransition) {
			StartCoroutine (asyncLoadSceneWithTransition (sceneName));
		} else {
			StartCoroutine (asyncLoadScene (sceneName));
		}
	}

	private IEnumerator asyncLoadScene(string sceneName)
	{
		//GameLogger.Log ("加载场景");
		isLoadingScene = true;
		AsyncOperation op=SceneManager.LoadSceneAsync (sceneName,LoadSceneMode.Single);
		yield return op;
		isLoadingScene = false;
		loadSceneOver ();
	}

	private IEnumerator asyncLoadSceneWithTransition(string sceneName)
	{
		//GameLogger.Log ("加载切换场景");
		isLoadingScene = true;
		AsyncOperation op=SceneManager.LoadSceneAsync (Constant.Scene_Transition,LoadSceneMode.Single);
		yield return op;

		AsyncOperation targetOp=SceneManager.LoadSceneAsync (sceneName,LoadSceneMode.Single);
		//切换场景进度条
		Message_Transition.create("加载:"+sceneName,targetOp).send();

		yield return targetOp;

		isLoadingScene = false;
		loadSceneOver ();
	}

	private void loadSceneOver()
	{
		sceneName = SceneManager.GetActiveScene ().name;

		MessageDispather.instance.loadScene ();

		GameLogger.Log ("进入场景:"+sceneName);
		if (loadSceneOverCallback != null) {
			loadSceneOverCallback ();
		}
	}
	#endregion

	private string sceneName;
	public string getSceneName()
	{
		if (string.IsNullOrEmpty (sceneName)) {
			sceneName = SceneManager.GetActiveScene ().name;
		}
		return sceneName;
	}
}
=======
﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity_lt_net;

[RequireComponent(typeof(MessageDispather),typeof(NetMessageDispatcherForUnity),typeof(HttpService))]
public class GameManager : MonoBehaviour {

	public static GameManager instance;

	[Header("版本")]
	public string version = "1.0.0";
	[Header("渠道Id")]
	public string channel="";

	[Header("验证服的IP和端口")]
	public string verifyIp;
	public int verifyPort;

	//验证地址
	[HideInInspector]
	public string verifyAddress = "http://127.0.0.1:8080/verify_server/verify.htm?action=verifyVersion";
	//服务器列表地址
	[HideInInspector]
	public string serversAddress = "http://127.0.0.1:8080/verify_server/verify.htm?action=servers";

	//游戏服的IP和端口
	[HideInInspector]
	public string serverIp;
	[HideInInspector]
	public int serverPort;

	[Header("游戏消息分发器"),HideInInspector]
	public NetMessageDispatcherForUnity gameNetDispatcher;
	[Header("HTTP服务"),HideInInspector]
	public HttpService httpService;
	[Header("聊天消息分发器"),HideInInspector]
	public NetMessageDispatcherForUnity chatNetDispatcher;

	[Header("显示Log")]
	public bool showLog;
	[Header("显示GUI")]
	public bool showGUI;

	private int totalFrame;
	private float totalTime;

	private int ping;
	public static int fps;
	private int inputBytes;
	private int outputBytes;


	void Awake()
	{
		instance = this;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Application.targetFrameRate = 60;
		DontDestroyOnLoad (gameObject);
		hideFlags = HideFlags.NotEditable;
		GameLogger.enableLog = showLog;
	}

	// Use this for initialization
	void Start () {
		initVariables ();
		initFramwork ();
		enterGame ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	private GUIStyle style;
	void OnGUI()
	{
		if (!showGUI) {
			return;
		}

		//计算帧率
		totalFrame++;
		totalTime += Time.deltaTime;
		if (totalTime >= 1f) {
			fps = (int)(totalFrame / totalTime);
			totalFrame = 0;
			totalTime = 0;
		}

		if (style == null) {
			style = new GUIStyle ();
			style.normal.textColor = Color.cyan;
			style.fontSize = 30;
		}
		GUI.Label (new Rect (10, 0, 10, 10), "运行时长="+Util.getTimeStrBySecond(Util.getSecondsFromStartup())+"\n"+SystemInfo.graphicsDeviceVersion + "\nPing=" + ping + "\nFps=" + fps + "\n入网流量=" + ((float)inputBytes/1024).ToString("0.00") + "KB\n出网流量=" + ((float)outputBytes/1024).ToString("0.00")+"KB", style);
	}


	void OnDestroy()
	{
		closeTcpService ();
	}

	public string getTrueVerifyAddress()
	{
		return verifyAddress.Replace ("127.0.0.1", verifyIp).Replace ("8080", verifyPort + "");
	}
	public string getTrueServersAddress()
	{
		return serversAddress.Replace ("127.0.0.1", verifyIp).Replace ("8080", verifyPort + "");
	}

	private void initVariables()
	{
		gameNetDispatcher = GetComponent<NetMessageDispatcherForUnity> ();
		httpService = GetComponent<HttpService> ();
	}

	private void initFramwork()
	{
		//注册逻辑层消息执行对象,每个场景可以注册多个handler,但一个handler不能在多个场景注册
		MessageDispather.instance.registe (Constant.Scene_Start,StartHandler.Instance);

		MessageDispather.instance.registe (Constant.Scene_LoadResource, LoadResourceHandler.Instance);

		MessageDispather.instance.registe (Constant.Scene_Login, LoginHandler.Instance);

		MessageDispather.instance.registe (Constant.Scene_Transition, TransitionHandler.Instance);

		MessageDispather.instance.registe (Constant.Scene_Main, MainHandler.Instance);

		MessageDispather.instance.registe (Constant.Scene_Game, PlayerHandler.Instance);
		//更多

		//注册网络层消息执行对象
		gameNetDispatcher.registe (0x1000,UserNetHandler.Instance);
		gameNetDispatcher.registe (0x1001,QueueNetHandler.Instance);
		gameNetDispatcher.registe (0x1002,PlayerNetHandler.Instance);
		gameNetDispatcher.registe (0x1003,MoveNetHandler.Instance);
        //加入技能
        gameNetDispatcher.registe(0x0008, SkillSelectNetHandler.Instance);
		//更多

		gameNetDispatcher.registe (0x7FFF,TickNetHandler.create());
	}

	private void enterGame()
	{
		loadScene (Constant.Scene_LoadResource, null, false);
	}

	private void recordInputBytes(int num)
	{
		inputBytes += num;
	}
	private void recordOutputBytes(int num)
	{
		outputBytes += num;
	}
	private void pingChange(int ping)
	{
		this.ping = ping;
	}

	//打开TCP服务
	public bool openTcpService()
	{
		//连接服务器
		if (gameNetDispatcher.TcpHandler.connectServer (serverIp, serverPort, "游戏TCP")) {
			//设置流量监控
			gameNetDispatcher.TcpHandler.setRecordMethod(recordInputBytes,recordOutputBytes);
			//网络出错回调
			gameNetDispatcher.setNetErrorCallback (closeTcpService);
			//超时回调
			TickNetHandler.Instance.setPingTimeoutCallback (closeTcpService);
			//启动心跳
			TickNetHandler.Instance.setPingChange (pingChange);
			TickNetHandler.Instance.startTick ();
			return true;
		}
		return false;
	}

	//关闭TCP服务
	private void closeTcpService()
	{
		//断开连接
		gameNetDispatcher.TcpHandler.disConnectServer ();
		//注销tick检测
		TickNetHandler.Instance.stopTick ();
		//通知断开连接
		MessageDispather.instance.addMessage (Message_DisConnect.create ());
	}

	#region load_scene
	private bool isLoadingScene;
	private UnityAction loadSceneOverCallback;
	public void loadScene(string sceneName,UnityAction callback,bool withTransition)
	{
		if (isLoadingScene) {
			return;
		}
		MessageDispather.instance.releaseScene ();
		ResourcesUtil.releaseAllResources ();

		loadSceneOverCallback = callback;
		if (withTransition) {
			StartCoroutine (asyncLoadSceneWithTransition (sceneName));
		} else {
			StartCoroutine (asyncLoadScene (sceneName));
		}
	}

	private IEnumerator asyncLoadScene(string sceneName)
	{
		//GameLogger.Log ("加载场景");
		isLoadingScene = true;
		AsyncOperation op=SceneManager.LoadSceneAsync (sceneName,LoadSceneMode.Single);
		yield return op;
		isLoadingScene = false;
		loadSceneOver ();
	}

	private IEnumerator asyncLoadSceneWithTransition(string sceneName)
	{
		//GameLogger.Log ("加载切换场景");
		isLoadingScene = true;
		AsyncOperation op=SceneManager.LoadSceneAsync (Constant.Scene_Transition,LoadSceneMode.Single);
		yield return op;

		AsyncOperation targetOp=SceneManager.LoadSceneAsync (sceneName,LoadSceneMode.Single);
		//切换场景进度条
		Message_Transition.create("加载:"+sceneName,targetOp).send();

		yield return targetOp;

		isLoadingScene = false;
		loadSceneOver ();
	}

	private void loadSceneOver()
	{
		sceneName = SceneManager.GetActiveScene ().name;

		MessageDispather.instance.loadScene ();

		GameLogger.Log ("进入场景:"+sceneName);
		if (loadSceneOverCallback != null) {
			loadSceneOverCallback ();
		}
	}
	#endregion

	private string sceneName;
	public string getSceneName()
	{
		if (string.IsNullOrEmpty (sceneName)) {
			sceneName = SceneManager.GetActiveScene ().name;
		}
		return sceneName;
	}
}
>>>>>>> .r315
