using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class BaseScene : MonoBehaviour {

	private Canvas canvas;

	void Awake()
	{
		canvas = GetComponentInChildren<Canvas> ();
		awake ();
	}

	// Use this for initialization
	void Start () {
		start ();
	}
	
	// Update is called once per frame
	void Update () {
		update ();
	}

	void OnDestroy()
	{
		gc ();
	}

	/// <summary>
	/// 显示警告弹窗
	/// </summary>
	/// <param name="content">Content.</param>
	/// <param name="callback">Callback.</param>
	public void showPopWarn(string content,UnityAction callback)
	{
		GameObject popGo = ResourcesUtil.loadPopWindow ();
		Util.SetLayer (popGo, gameObject.layer);
		Util.AddChild (popGo, canvas.gameObject);
		Util.resetTransform (popGo.transform);
		popGo.GetComponent<PopWindow> ().popWarn (content, callback);
		popGo.GetComponent<TweenScale> ().tween (Vector3.one * 0.001f, Vector3.one, 0.4f, 0, null);
	}

	/// <summary>
	/// 显示确认弹窗
	/// </summary>
	/// <param name="content">Content.</param>
	/// <param name="sureCallback">Sure callback.</param>
	/// <param name="cancelCallback">Cancel callback.</param>
	public void showPopConfirm(string content,UnityAction sureCallback,UnityAction cancelCallback)
	{
		GameObject popGo = ResourcesUtil.loadPopWindow ();
		Util.SetLayer (popGo, gameObject.layer);
		Util.AddChild (popGo, canvas.gameObject);
		Util.resetTransform (popGo.transform);
		popGo.GetComponent<PopWindow> ().popConfirm (content, sureCallback, cancelCallback);
		popGo.GetComponent<TweenScale> ().tween (Vector3.one * 0.001f, Vector3.one, 0.4f, 0, null);
	}

	protected virtual void awake(){}

	protected virtual void start(){}

	protected virtual void update(){}

	protected virtual void gc(){}
}
