using UnityEngine;
using System.Collections;
using Unity_lt_net;

public class MoveNetHandler : NetHandler {

	private static MoveNetHandler instance;
	public static MoveNetHandler Instance
	{
		get{ 
			if (instance == null) {
				instance= new MoveNetHandler ();
			}
			return instance;
		}
	}

	//================================================== 指令 for server ==============================================//
	private const int Command_Move		=	0x0003;
	//================================================== 指令 for client ==============================================//
	private const int Notify_Move		=	0x1005;
	//================================================== 指令结束= ====================================================//

	protected override void process (int command, ByteArray ba)
	{
		switch (command) {
		case Notify_Move:
			fuction_Notify_Move (ba);
			break;
		}
	}

	public override void update (float deltaTime)
	{
		//TODO
	}

	#region send to client
	private void fuction_Notify_Move(ByteArray ba)
	{
		int playerId = ba.readInt ();
		float x = StringUtil.getFloat (ba.readUTF ());
		float z = StringUtil.getFloat (ba.readUTF ());
		float speed = StringUtil.getFloat (ba.readUTF ());

		Message_MoveToClient.create (playerId, x, z, speed).send ();
	}
	#endregion

	#region send to server
	public void moveTo(Message_MoveToServer msg)
	{
		//通知移动
		ByteArray ba=new ByteArray();
		ba.writeInt (msg.playerId);
		ba.writeUTF (msg.x + "");
		ba.writeUTF (msg.z + "");
		ba.writeUTF (msg.speed + "");
		sendMessage (Command_Move, ba);
	}
	#endregion
}
