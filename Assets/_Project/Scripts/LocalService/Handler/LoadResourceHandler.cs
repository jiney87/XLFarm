using UnityEngine;
using System.Collections;

public class LoadResourceHandler : BaseHandler {

	private static LoadResourceHandler instance;
	public static LoadResourceHandler Instance{
		get{ 
			if (instance == null) {
				instance = new LoadResourceHandler ();
			}
			return instance;
		}
	}

	protected override void process (BaseScene baseScene, Message msg)
	{

	}

}
