using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Unity_lt_net
{

	public class NetMessageDispatcherForUnity : MonoBehaviour {

		// Use this for initialization
		void Awake()
		{
			hideFlags = HideFlags.NotEditable;
			DontDestroyOnLoad (gameObject);

			queue = Queue.Synchronized (new Queue ());
			registeTCPHandler ();
		}

		// Update is called once per frame
		void Update () {

			//通知网络出错
			if (netErrorOccur) {
				netErrorOccur = false;
				if (netErrorCallback != null) {
					netErrorCallback ();
				}
			}

			while(queue!=null && queue.Count>0)
			{
				byte[] data=(byte[])queue.Dequeue();
				processMessage(data);
			}

			for (int k = 0; k < handlerList.Count; k++) {
				handlerList [k].update (Time.deltaTime);
			}
		}

		private Dictionary<int,BaseNetHandler> handlers = new Dictionary<int, BaseNetHandler> ();
		private List<BaseNetHandler> handlerList = new List<BaseNetHandler> ();
		private Dictionary<Type,int> handlerTypes = new Dictionary<Type, int> ();

		private Queue queue;


		private TCPHandler tcpHandler;
		public TCPHandler TcpHandler{
			get{
				return tcpHandler;
			}
		}

		private void registeTCPHandler()
		{
			tcpHandler = new TCPHandler ();
			tcpHandler.setMsgDispatcher (this);
		}


		public void registe(int type,BaseNetHandler handler)
		{
			handlers.Add (type, handler);
			handlerList.Add (handler);
			handlerTypes.Add (handler.GetType (), type);
			handler.setMsgDispatcher (this);
			GameLogger.Log ("注册:" + handler.GetType (), Color.yellow);
		}

		public void unRegiste<T>() where T:BaseNetHandler
		{
			int type = getHandlerType<T> ();
			if (!handlers.ContainsKey(type)) {
				return;
			}

			BaseNetHandler handler = handlers [type];
			handlers.Remove (type);
			handlerList.Remove (handler);
			handlerTypes.Remove (typeof(T));
			GameLogger.Log ("注销:" + handler.GetType (), Color.yellow);
		}

		public T getHandler<T>() where T:BaseNetHandler
		{
			int type = getHandlerType<T> ();
			if (!handlers.ContainsKey (type)) {
				return null;
			}
			return handlers [type] as T;
		}

		public int getHandlerType<T>() where T:BaseNetHandler
		{
			if (!handlerTypes.ContainsKey (typeof(T))) {
				return 0;
			}
			return handlerTypes [typeof(T)];
		}

		public int getHandlerType(BaseNetHandler handler)
		{
			Type t = handler.GetType ();
			if (!handlerTypes.ContainsKey (t)) {
				return 0;
			}
			return handlerTypes [t];
		}

		public void sendMessage(NetMessage msg)
		{
			TcpHandler.addOutputMessage (msg);
		}

		public void receiveMessage(byte[] data)
		{
			queue.Enqueue (data);
		}

		private void processMessage(byte[] data)
		{
			ByteArray ba = new ByteArray (data);
			int command = ba.readInt ();
			int key = getType(command);
			if (handlers.ContainsKey (key)) {
				BaseNetHandler logic = handlers [key];
				logic.processMessage (data);
			} else {
				GameLogger.LogError ("没有这个processer:"+ command);
			}
		}
		
		private int getType(int command)
		{
			return (int)(0xFFFF0000&command)>>16;
		}

		#region NetError
		private NetErrorCallback netErrorCallback;
		public void setNetErrorCallback(NetErrorCallback netErrorCallback)
		{
			this.netErrorCallback = netErrorCallback;
		}
		private bool netErrorOccur;
		public void notifyNetError()
		{
			netErrorOccur = true;
		}
		#endregion
	}

}