using UnityEngine;
using System.Collections;

public class StartHandler : BaseHandler {

	private static StartHandler instance;
	public static StartHandler Instance{
		get{ 
			if (instance == null) {
				instance = new StartHandler ();
			}
			return instance;
		}
	}

	protected override void process (BaseScene baseScene, Message msg)
	{

	}

}
