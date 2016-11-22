using UnityEngine;
using System.Collections.Generic;
using System;

public class MessageDispather : MonoBehaviour {

	public static MessageDispather instance;

	private Dictionary<string,List<BaseHandler>> handlers = new Dictionary<string, List<BaseHandler>> ();
	private Queue<Message> queue=new Queue<Message>();

	void Awake()
	{
		instance = this;
		DontDestroyOnLoad (gameObject);
		hideFlags = HideFlags.NotEditable;
	}
	
	// Update is called once per frame
	void Update () {
		while (queue.Count > 0) {
			Message msg = queue.Dequeue ();
			string sceneName = GameManager.instance.getSceneName ();
			if (handlers.ContainsKey (sceneName)) {
				List<BaseHandler> list = handlers [sceneName];
				for (int k = 0; k < list.Count; k++) {
					list [k].process (msg);
				}
			} else {
				GameLogger.LogError ("没有这个场景的handler:" + sceneName);
			}
		}
	}


	/// <summary>
	/// 注册处理器
	/// </summary>
	/// <param name="type">Type.</param>
	/// <param name="handler">Handler.</param>
	public void registe(string sceneName,BaseHandler handler)
	{
		List<BaseHandler> list = null;
		if (!handlers.ContainsKey (sceneName)) {
			list = new List<BaseHandler> ();
			handlers.Add (sceneName, list);
		}
		list = handlers [sceneName];
		if (list.Contains (handler)) {
			return;
		}
		list.Add (handler);
		GameLogger.Log ("注册:" + sceneName + "," + handler.GetType (), Color.green);
	}

	/// <summary>
	/// 消息入队
	/// </summary>
	/// <param name="msg">Message.</param>
	public void addMessage(Message msg)
	{
		queue.Enqueue (msg);
		//GameLogger.Log ("加入消息:" + msg.type);
		//GameLogger.Log ("当前输入消息[logic]个数:"+queue.Count);
	}

	public void releaseScene()
	{
		string sceneName = GameManager.instance.getSceneName ();
		if (handlers.ContainsKey (sceneName)) {
			List<BaseHandler> list = handlers [sceneName];
			for (int k = 0; k < list.Count; k++) {
				list [k].releaseScene ();
			}
		} else {
			GameLogger.LogError ("没有这个场景的handler:" + sceneName);
		}
	}

	public void loadScene()
	{
		string sceneName = GameManager.instance.getSceneName ();
		if (handlers.ContainsKey (sceneName)) {
			List<BaseHandler> list = handlers [sceneName];
			for (int k = 0; k < list.Count; k++) {
				list [k].setScene (Util.getSceneUI<BaseScene> ());
			}
		} else {
			GameLogger.LogError ("没有这个场景的handler:" + sceneName);
		}
	}
}