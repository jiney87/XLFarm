using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

/// <summary>
/// 搜索附近目标,圆形范围
/// </summary>
public class SearchSelf : SearchTarget {

	protected override bool inScope (GameObject self, GameObject target)
	{
		return true;
	}

	protected override void searchTarget ()
	{

	}

	protected override List<GameObject> searchOver ()
	{
		List<GameObject> targets = new List<GameObject> ();
		targets.Add (self);
		return targets;
	}
}
