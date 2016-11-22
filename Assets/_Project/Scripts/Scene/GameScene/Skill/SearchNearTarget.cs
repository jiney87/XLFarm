using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

/// <summary>
/// 搜索附近目标,圆形范围
/// </summary>
public class SearchNearTarget : SearchTarget {

	[Header("作用半径")]
	public float radius=1;

	private GameObject tempTarget;
	private List<GameObject> lockedTargets = new List<GameObject> ();

	/// <summary>
	/// 指定范围内寻找目标,如果有多个目标,轮换着锁定,每个锁定1秒钟
	/// </summary>
	protected override void searchTarget()
	{
		//判断目标是否还在范围内,不在要取消锁定
		if (tempTarget != null) {
			if(!inScope(self,tempTarget))
			{
				cancelLock ();
			}
		}

		//如果目标是空,锁定下一个目标
		if (tempTarget == null) {
			//获取范围内的所有目标
			List<GameObject> others=getTargets();
			if (others != null && others.Count > 0) {
				for (int k = 0; k < others.Count; k++) {
					GameObject other = others [k];
					if (lockedTargets.Contains (other)) {
						continue;
					} else {
						StartCoroutine (lockTarget (other));
						break;
					}
				}
				//如果已经全部锁定过,清空列表重新锁定
				if (tempTarget == null) {
					lockedTargets.Clear ();
					StartCoroutine (lockTarget (others [0]));
				}
			}
		}
	}

	protected override bool inScope(GameObject self,GameObject target)
	{
		Vector2 selfPos=Util.getHorizontalPos(self.transform.localPosition);
		Vector2 tempTargetPos = Util.getHorizontalPos (target.transform.localPosition);
		return Vector2.Distance (selfPos, tempTargetPos) <= radius;
	}

	protected override List<GameObject> searchOver()
	{
		if (tempTarget == null) {
			return null;
		}
		List<GameObject> targets = new List<GameObject> ();
		targets.Add (tempTarget);

		cancelLock ();
		return targets;
	}

	private IEnumerator lockTarget(GameObject target)
	{
		lockedTargets.Add (target);
		tempTarget = target;
		//改变外观
		changeSurface(tempTarget);

		yield return new WaitForSeconds (1);

		//恢复外观
		recoverSurface(tempTarget);

		tempTarget = null;
	}

	private void cancelLock()
	{
		StopAllCoroutines ();
		recoverSurface (tempTarget);
		tempTarget = null;
	}
}
