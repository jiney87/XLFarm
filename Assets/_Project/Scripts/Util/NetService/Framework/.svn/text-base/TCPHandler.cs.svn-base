using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;
using System.Text;
using System.Linq;

namespace Unity_lt_net
{

	public class TCPHandler : NetMessageDispatcherComponent{

		private string name;

		#region connect
		private const int ThreadSleepTime = 2;
		private const int BufferSize = 2048;

		private byte[] oldBuffer;//断包
		private Socket socket;
		private Queue outputQueue;

		private string getName()
		{
			return "["+name+"]";
		}
		private void printFormatString(string msg)
		{
			Debug.Log("<color=yellow>"+getName()+msg+"</color>");
		}

		public bool connectServer(string host,int port,string name)
		{
			this.name = name;
			try
			{
				if(socket!=null && socket.Connected)
				{
					return true;
				}

				//清除数据
				disConnectServer();

				//连接服务器
				socket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
				socket.Connect(host,port);

				socket.ReceiveBufferSize=BufferSize;
				socket.SendBufferSize=BufferSize;
				socket.NoDelay=true;
				socket.Blocking=true;
				//socket.ReceiveTimeout=0;
				//socket.SendTimeout=0;

				printFormatString("成功连接服务器");

				//开启输入线程
				Thread inputT = new Thread (runInput);
				inputT.IsBackground = true;
				inputT.Start ();

				//开启输出线程
				outputQueue=Queue.Synchronized(new Queue());
				Thread outputT=new Thread(runOutput);
				outputT.IsBackground = true;
				outputT.Start();

				return true;
			}
			catch(Exception e) {
				Debug.LogError (e.Message + "\n" + e.StackTrace);
				return false;
			}
		}

		public void disConnectServer()
		{
			if(socket!=null)
			{
				socket.Close (0);
				socket = null;
				oldBuffer = null;
				//output
				if (outputQueue != null) {
					outputQueue.Clear ();
					outputQueue = null;
				}
			}
		}
		#endregion

		#region input
		private void addInputMessage(byte[] msg)
		{
			if (MsgDispatcher != null) {
				MsgDispatcher.receiveMessage (msg);
			}
		}
		private void runInput()
		{
			printFormatString ("启动输入线程");
			try{
				while(socket!=null && socket.Connected)
				{
					int availNum=socket.Available;
					if(availNum>0)
					{
						//记录流量
						if (recordInputBytes != null) {
							recordInputBytes (availNum);
						}
							
						byte[] curBuffer=new byte[availNum];
						socket.Receive(curBuffer);

						//先把已有的包接上
						if (oldBuffer != null && oldBuffer.Length > 0)
						{
							byte[] tempBuffer = new byte[curBuffer.Length + oldBuffer.Length];
							Array.Copy(oldBuffer, 0, tempBuffer, 0, oldBuffer.Length);
							Array.Copy(curBuffer, 0, tempBuffer, oldBuffer.Length, curBuffer.Length);
							curBuffer = tempBuffer;
							//Debug.Log("粘包",Color.yellow);
						}

						//清掉旧包
						oldBuffer=null;

						//多个消息的处理
						while(true)
						{
							if(curBuffer==null || curBuffer.Length==0)
							{
								curBuffer = null;
								break;
							}

							//字节组的长度不够一个int
							if (curBuffer.Length < ByteArray.INT_SIZE) {
								oldBuffer = curBuffer;
								break;
							}

							//判断包长度
							ByteArray ba=new ByteArray(curBuffer);
							int msgLength=ba.readInt();
							int packageLength=curBuffer.Length;
							if(packageLength<msgLength)
							{
								oldBuffer=curBuffer;
								//Debug.Log("断包",Color.yellow);
								break;
							}
							if(packageLength>=msgLength)
							{
								//获取要用的
								byte[] temp=new byte[msgLength];
								Array.Copy(curBuffer, 0, temp, 0, msgLength);
								//多余的存起来
								oldBuffer=new byte[packageLength-msgLength];
								Array.Copy(curBuffer, msgLength, oldBuffer, 0, packageLength-msgLength);
								//重构字节组
								curBuffer=temp;
								//Debug.Log("粘包和断包",Color.yellow);
							}
							//Debug.Log("成功收包:"+curBuffer.Length,Color.yellow);
							//抛掉第一个int,获取数据
							byte[] msg=new byte[curBuffer.Length-ByteArray.INT_SIZE];
							Array.Copy(curBuffer, ByteArray.INT_SIZE, msg, 0, msg.Length);
							addInputMessage (msg);

							curBuffer=oldBuffer;
							oldBuffer=null;
						}
					}
					else
					{
						Thread.Sleep(ThreadSleepTime);
					}
				}
			}catch(Exception e) {
				errorOccur ("出错了runInput", e);
			}
			printFormatString ("输入线程已关闭");
		}
		#endregion

		#region output
		public void addOutputMessage(NetMessage message)
		{
			if (outputQueue != null) {
				outputQueue.Enqueue (message);
			}
		}

		private void runOutput()
		{
			printFormatString ("启动输出线程");
			try{
				while(socket != null && socket.Connected)
				{
					if(outputQueue.Count>0)
					{
						NetMessage message=(NetMessage)outputQueue.Dequeue();
						processOutPutMessage(message);
					} else {
						Thread.Sleep (ThreadSleepTime);
					}
				}
			}catch(Exception e) {
				errorOccur("出错了runOutput",e);
			}
			printFormatString ("输出线程已关闭");
		}

		private void processOutPutMessage(NetMessage message)
		{
			if (message == null) {
				return;
			}
			int length = message.data.Length + ByteArray.INT_SIZE;
			message.expandInt(length);

			byte[] data = message.data;
			socket.Send (data);

			//记录流量
			if (recordOutputBytes != null) {
				recordOutputBytes (data.Length);
			}
		}
		#endregion

		private void errorOccur(string msg,Exception e)
		{
			Debug.LogError (msg + ":" + e.Message);
			Debug.LogError (msg + ":" + e.StackTrace);
			if (MsgDispatcher != null) {
				MsgDispatcher.notifyNetError ();
			}
		}

		#region record bytes
		private RecordInputBytes recordInputBytes;
		private RecordOutputBytes recordOutputBytes;

		public void setRecordMethod(RecordInputBytes inputMethod,RecordOutputBytes outputMethod)
		{
			recordInputBytes = inputMethod;
			recordOutputBytes = outputMethod;
		}
		#endregion
	}

}