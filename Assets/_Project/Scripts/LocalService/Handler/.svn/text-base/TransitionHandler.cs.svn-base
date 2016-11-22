using UnityEngine;
using System.Collections;

public class TransitionHandler : BaseHandler {

	private static TransitionHandler instance;
	public static TransitionHandler Instance{
		get{ 
			if (instance == null) {
				instance = new TransitionHandler ();
			}
			return instance;
		}
	}

	protected override void process (BaseScene baseScene, Message msg)
	{
		TransitionScene scene = baseScene as TransitionScene;

		switch (msg.type) {
		case MsgType.Transition:
			scene.init (msg as Message_Transition);
			break;
		}
	}

}
