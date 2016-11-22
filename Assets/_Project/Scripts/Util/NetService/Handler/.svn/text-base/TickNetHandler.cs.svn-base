using UnityEngine;
using System.Collections.Generic;
using System;
using Unity_lt_net;

public class TickNetHandler : NetHandler {

	private static TickNetHandler instance;
	public static TickNetHandler Instance
	{
		get{ 
			if (instance == null) {
				instance= new TickNetHandler ();
			}
			return instance;
		}
	}



	//to server
	private const int CommandTick=0x0000;
	//to client
	private const int NotifyTick=0x1000;

	private Queue<float> tickQueue = new Queue<float> ();

	private PingChange pingChange;
	public void setPingChange(PingChange pingChange)
	{
		this.pingChange = pingChange;
	}

	private PingTimeoutCallback pingTimeoutCallback;
	public void setPingTimeoutCallback(PingTimeoutCallback pingTimeoutCallback)
	{
		this.pingTimeoutCallback = pingTimeoutCallback;
	}

	protected override void process (int command, ByteArray ba)
	{
		switch (command) {
		case NotifyTick:
			//GameLogger.Log ("收到tick返回");
			if (tickQueue.Count > 0) {
				float sendTime = tickQueue.Dequeue ();
				int ping = (int)((Util.getSecondsFromStartup () - sendTime) * 1000);
				if (pingChange != null) {
					pingChange (ping);
				}
			}
			break;
		}
	}


	#region tick
	private bool running;
	//发送心跳间隔,单位秒
	private const int TickDiff = 3;
	private float curTime;
	//发送心跳接收间隔,单位秒,超过这个时间认为已经与服务器断开连接
	private const int DisConnectDiff = 5;

	public override void update (float deltaTime)
	{
		if (!running) {
			return;
		}

		curTime += deltaTime;
		if (curTime > TickDiff) {
			curTime = 0;
			sendMessage (CommandTick, null);
			tickQueue.Enqueue (Util.getSecondsFromStartup ());
			//GameLogger.Log ("发送TICK");
		}

		//判断连接中断
		if (tickQueue.Count > 0) {
			float sendTime = tickQueue.Peek ();
			if (Util.getSecondsFromStartup () - sendTime >= DisConnectDiff) {
				//Debug.Log ("时间差:" + (Util.getSecondsFromStartup () - sendTime));
				if (pingTimeoutCallback != null) {
					pingTimeoutCallback ();
				}
			}
		}
	}
	#endregion

	public void startTick()
	{
		tickQueue.Clear ();
		running = true;
	}

	public void stopTick()
	{
		tickQueue.Clear ();
		running = false;
	}

	public static TickNetHandler create()
	{
		GameLogger.Log ("开启TICK");
		TickNetHandler handler= new TickNetHandler ();
		handler.running = false;
		instance = handler;
		return handler;
	}
}