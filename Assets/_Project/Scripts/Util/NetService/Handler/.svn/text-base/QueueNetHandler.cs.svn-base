using UnityEngine;
using System.Collections;
using Unity_lt_net;

public class QueueNetHandler : NetHandler {

	private static QueueNetHandler instance;
	public static QueueNetHandler Instance
	{
		get{ 
			if (instance == null) {
				instance= new QueueNetHandler ();
			}
			return instance;
		}
	}

	//================================================== 指令 for server ==============================================//
	private const int Command_Queue		=	0x0000;
	//================================================== 指令 for client ==============================================//

	//================================================== 指令结束= ====================================================//

	protected override void process (int command, ByteArray data)
	{
		switch (command) {
			//TODO
		}
	}

	public override void update (float deltaTime)
	{
		//TODO
	}

	#region send to client

	#endregion

	#region send to server
	public void enterQueue(int queueType)
	{
		ByteArray ba = new ByteArray ();
		ba.writeInt (queueType);
		sendMessage (Command_Queue, ba);
	}
	#endregion
}
