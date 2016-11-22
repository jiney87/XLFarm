using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransitionScene : BaseScene {

	#region des
	[Header("加载描述")]
	public Text desText;
	[Header("加载进度")]
	public Text percentText;
	[Header("前景条")]
	public Image foreImage;

	private void setDes(string msg)
	{
		desText.text = msg;
	}
	private void setPercent(float percent)
	{
		percentText.text = percent.ToString ("0.00") + "%";
		foreImage.fillAmount = percent / 100;
	}
	#endregion


	private Message_Transition msg;

	public void init(Message_Transition msg)
	{
		this.msg = msg;
		setDes (msg.des);
	}

	protected override void update ()
	{
		base.update ();
		if (msg != null) {
			setPercent (msg.op.progress*100);
		}
	}
}
