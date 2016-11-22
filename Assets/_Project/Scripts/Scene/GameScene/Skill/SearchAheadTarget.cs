using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

/// <summary>
/// 搜索正前方目标,矩形范围
/// </summary>
public class SearchAheadTarget : SearchTarget {

	[Header("作用范围")]
	public float length = 2;
	public float width=0.5f;

	private List<GameObject> tempTargets = new List<GameObject> ();

	/// <summary>
	/// 指定范围内寻找目标,锁定多个目标
	/// </summary>
	protected override void searchTarget()
	{
		//判断目标是否还在范围内,不在要取消锁定
		if (tempTargets.Count>0) {
			for (int k = 0; k < tempTargets.Count; k++) {
				if(!inScope(self,tempTargets[k]))
				{
					cancelLock (tempTargets[k]);
				}
			}
		}

		//获取范围内的所有目标
		List<GameObject> others=getTargets();
		for (int k = 0; k < others.Count; k++) {
			lockTarget (others [k]);
		}
	}

	protected override bool inScope(GameObject self,GameObject target)
	{
		Vector3 selfPos = self.transform.localPosition;
		Vector3 tempTargetPos = target.transform.localPosition;
		return selfPos.z + length >= tempTargetPos.z && Mathf.Abs (selfPos.x - tempTargetPos.x) <= width;
	}

	protected override List<GameObject> searchOver()
	{
		if (tempTargets.Count == 0) {
			return null;
		}
		//最终目标按距离升序排列
		List<GameObject> temp = new List<GameObject> ();
		for (int k = 0; k < tempTargets.Count; k++) {
			GameObject target = tempTargets [k];
			float distance = getDistance (self, target);
			for (int n = 0; n < temp.Count; n++) {
				float curDistance = getDistance (self, temp [n]);
				if (distance < curDistance) {
					temp.Insert (n, target);
					break;
				}
			}
			if (!temp.Contains (target)) {
				temp.Add (target);
			}

			cancelLock (target);
		}
		tempTargets.Clear ();
		tempTargets.AddRange (temp);

		return tempTargets;
	}

	private float getDistance(GameObject self,GameObject target)
	{
		Vector3 selfPos = self.transform.localPosition;
		Vector3 tempTargetPos = target.transform.localPosition;
		return Mathf.Abs (tempTargetPos.z - selfPos.z);
	}

	private void lockTarget(GameObject target)
	{
		if (tempTargets.Contains (target)) {
			return;
		}
		tempTargets.Add (target);
		//改变外观
		changeSurface(target);
	}

	private void cancelLock(GameObject target)
	{
		recoverSurface (target);
		tempTargets.Remove (target);
	}
}
