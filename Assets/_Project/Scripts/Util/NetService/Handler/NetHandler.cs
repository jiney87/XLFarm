using UnityEngine;
using System.Collections;
using Unity_lt_net;

public abstract class NetHandler : BaseNetHandler {

	protected override void processCommand (int command, ByteArray data)
	{
		int errorCode = data.readInt ();
		if (errorCode != ErrorCode.ErrorCode_0x0000) {
			Message_ErrorCode.create (errorCode).send ();
			GameLogger.LogError("出现错误码:"+errorCode);
			return;
		}
		process (command, data);
	}

	protected abstract void process (int command, ByteArray data);
	//public abstract void update (float deltaTime);
}
