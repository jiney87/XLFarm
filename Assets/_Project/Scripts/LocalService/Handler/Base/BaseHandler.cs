using UnityEngine;
using System.Collections;

public abstract class BaseHandler {

	public BaseScene scene;

	public void setScene(BaseScene scene)
	{
		this.scene = scene;
	}
	public void releaseScene ()
	{
		scene = null;
	}

	public void process(Message msg)
	{
		if (msg.type == MsgType.ErrorCode) {
			int errorCode = (msg as Message_ErrorCode).errorCode;
			processErrorCode (scene, errorCode);
			return;
		} else if (msg.type == MsgType.DisConnect) {
			processDisConnect (scene);
		}

		process (scene, msg);
	}

	protected abstract void process (BaseScene baseScene, Message msg);

	private void processDisConnect(BaseScene baseScene)
	{
		baseScene.showPopWarn ("与服务器连接中断", goToLoginScene);
	}

	private void goToLoginScene()
	{
		GameManager.instance.loadScene (Constant.Scene_Login, null, true);
	}

	/// <summary>
	/// 处理错误码
	/// </summary>
	/// <param name="baseScene">Base scene.</param>
	/// <param name="errorCode">Error code.</param>
	private void processErrorCode(BaseScene baseScene,int errorCode)
	{
		switch (errorCode) {
		case ErrorCode.ErrorCode_0x0001:
			baseScene.showPopWarn ("账号不存在", null);
			break;
		case ErrorCode.ErrorCode_0x0002:
			baseScene.showPopWarn ("密码错误", null);
			break;
		case ErrorCode.ErrorCode_0x0003:
			baseScene.showPopWarn ("账号已冻结", null);
			break;
		case ErrorCode.ErrorCode_0x0004:
			baseScene.showPopWarn ("玩家不在线", null);
			break;
        case ErrorCode.ErrorCode_0x0005:
            baseScene.showPopWarn("金币不足", null);
            break;
        case ErrorCode.ErrorCode_0x0006:
            baseScene.showPopWarn("技能未开启，请选择其它技能", null);
            break;
		}
	}
}
