using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public abstract class SearchTarget : MonoBehaviour {

	protected GameObject self;
	private List<GameObject> allTargets = new List<GameObject> ();
	private bool isSearching;
	private UnityAction<int,GameObject,List<GameObject>> callback;
	protected int skillId;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isSearching) {
			searchTarget ();
		}
	}

	protected abstract void searchTarget ();
	protected abstract bool inScope (GameObject self, GameObject target);
	protected abstract List<GameObject> searchOver ();

	/// <summary>
	/// 选取范围内所有目标
	/// </summary>
	/// <returns>The targets.</returns>
	protected List<GameObject> getTargets()
	{
		List<GameObject> targets = new List<GameObject> ();
		for (int k = 0; k < allTargets.Count; k++) {
			GameObject target = allTargets [k];
			if (target == null) {
				continue;
			}
			if (inScope (self, target)) {
				targets.Add (target);
			}
		}
		return targets;
	}

	/// <summary>
	/// 改变外观
	/// </summary>
	/// <param name="target">Target.</param>
	protected void changeSurface(GameObject target)
	{
		MeshRenderer renderer=target.GetComponentInChildren<MeshRenderer>();
		if (renderer != null) {
			renderer.material.SetFloat ("_Outline", .005f);
		}
	}

	/// <summary>
	/// 恢复外观
	/// </summary>
	/// <param name="target">Target.</param>
	protected void recoverSurface(GameObject target)
	{
		MeshRenderer renderer=target.GetComponentInChildren<MeshRenderer>();
		if (renderer != null) {
			renderer.material.SetFloat ("_Outline", 0f);
		}
	}


	#region 供外界调用
	/// <summary>
	/// 初始化
	/// </summary>
	/// <param name="allPlayers">场景里所有玩家</param>
	/// <param name="useRet">是否释放成功回调</param>
	public void init(GameObject self,int skillId,List<GameObject> allPlayers,UnityAction<int,GameObject,List<GameObject>> callback)
	{
		this.self = self;
		this.skillId = skillId;
		this.callback = callback;

		allTargets.Clear ();
		allTargets.AddRange (allPlayers);
		if (allTargets.Contains (self)) {
			allTargets.Remove (self);
		}
	}

	public void startSearch()
	{
		isSearching = true;
	}

	public void endSearch()
	{
		if (isSearching) {
			isSearching = false;
			List<GameObject> targets = searchOver ();
			//通知释放结果
			if (callback != null) {
				callback (skillId, self, targets);
			}
		}
	}
	#endregion
}