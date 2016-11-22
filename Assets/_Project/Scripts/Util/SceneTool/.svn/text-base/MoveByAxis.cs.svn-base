using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public enum Axis
{
	x,y,z
}

public class MoveByAxis : MonoBehaviour {

	[Header("偏移轴")]
	public Axis axis=Axis.y;
	[Header("偏移幅度")]
	public float offsetMul=1;
	[Header("偏移曲线")]
	public AnimationCurve curve;
	[Header("是否旋转")]
	public bool rotate;
	[Header("曲线执行完毕自销毁")]
	public bool selfDestory;
	[Header("作用于上级节点")]
	public bool isForParent;
	[Header("可以运行")]
	public bool canRun;

	protected UnityAction callback;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected Transform getTf()
	{
		if (isForParent) {
			return transform.parent;
		}
		return transform;
	}

}
