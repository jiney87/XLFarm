using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class PopWindow : MonoBehaviour {

	public Text contentText;
	public Button sureBtn;
	public Button cancelBtn;
	public Button centerBtn;

	private UnityAction sureAction;
	private UnityAction cancelAction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void init()
	{
		contentText.gameObject.SetActive (false);
		sureBtn.gameObject.SetActive (false);
		cancelBtn.gameObject.SetActive (false);
		centerBtn.gameObject.SetActive (false);
	}

	#region popWarn
	public void popWarn(string content,UnityAction callback)
	{
		init ();
		contentText.gameObject.SetActive (true);
		contentText.text = "　　"+content;
		sureAction = callback;
		centerBtn.gameObject.SetActive (true);
		centerBtn.onClick.AddListener (onClickPopWarn);
	}

	private void onClickPopWarn()
	{
		if (sureAction != null) {
			sureAction ();
		}
		Destroy (gameObject);
	}
	#endregion

	#region popConfirm
	public void popConfirm(string content,UnityAction sureCallback,UnityAction cancelCallback)
	{
		init ();
		contentText.gameObject.SetActive (true);
		contentText.text = "　　"+content;

		sureAction = sureCallback;
		sureBtn.gameObject.SetActive (true);
		sureBtn.onClick.AddListener (onClickPopConfirmSure);

		cancelAction = cancelCallback;
		cancelBtn.gameObject.SetActive (true);
		cancelBtn.onClick.AddListener (onClickPopConfirmCancel);
	}

	private void onClickPopConfirmSure()
	{
		if (sureAction != null) {
			sureAction ();
		}
		Destroy (gameObject);
	}

	private void onClickPopConfirmCancel()
	{
		if (cancelAction != null) {
			cancelAction ();
		}
		Destroy (gameObject);
	}

	#endregion

}
