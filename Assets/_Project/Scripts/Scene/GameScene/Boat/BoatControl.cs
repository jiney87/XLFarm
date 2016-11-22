using UnityEngine;
using System.Collections.Generic;

public class BoatControl : MonoBehaviour {

	[HideInInspector]
	public int playerId;

	[Header("X位移倍率")]
	public float xMul=1;

	//[SeparateProperty("=======================")]

	[Header("Z当前速度")]
	public float zSpeed;
	[Header("Z油门（摇杆）加速倍率")]
	public float zSpeedMul=1;
	[Header("Z最大速度")]
	public float maxZSpeed = 3f;
	[Header("Z最小速度")]
	public float minZSpeed = 0;
	[Header("Z自然加速度")]
	public float natureZAcc=0.3f;
	[Header("道具生成位置")]
	public GameObject itemParent;

	public bool isEnemy;
	private bool canSelfMove;
	private bool canReceivePos;
	private bool canSendPos;

	private Transform tf;
	private float targetSpeed;

	//X位移最小值
	private float minX=-1.3f;
	//X位移最大值
	private float maxX=1.3f;

	private float sendTime;
	private const float SendDiff = 0.050f;
	private Vector3 lastPos=Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isEnemy && canSelfMove) {
			zSpeed += natureZAcc * Time.deltaTime;
			zSpeed = Mathf.Clamp (zSpeed, minZSpeed, maxZSpeed);

			Vector3 pos = tf.localPosition;
			pos += new Vector3 (0, 0, zSpeed) * Time.deltaTime;

			tf.localPosition = pos;
		}

		//send
		if (canSendPos) {
			sendTime -= Time.deltaTime;
			if (sendTime <= 0) {
				sendTime = SendDiff;
				if (tf.localPosition != lastPos) {
					lastPos = tf.localPosition;
					Message_MoveToServer.create (MsgType.Move_ToServer, playerId, tf.localPosition.x, tf.localPosition.z, zSpeed).send ();
				}
			}
		}

		if (isEnemy && canSelfMove) {
			tf.localPosition += new Vector3 (0, 0, targetSpeed * Time.deltaTime);
		}
	}

	void LateUpdate()
	{
		
	}

	public void onMove(Vector2 delta)
	{
		if (!canSelfMove) {
			return;	
		}

		Vector3 pos = tf.localPosition;
		pos.x += -delta.y * Time.deltaTime * xMul;
		pos.x = Mathf.Clamp (pos.x, minX, maxX);
		tf.localPosition = pos;

		zSpeed += delta.x * Time.deltaTime * zSpeedMul;
	}

	public void onMove(float x,float z,float speed)
	{
		if (!canReceivePos) {
			return;	
		}

		targetSpeed = speed;
		//实际位置与当前位置折中
		Vector3 temp = new Vector3 (x, tf.localPosition.y, Mathf.Lerp (tf.localPosition.z, z, 0.1f));
		temp.x = Mathf.Clamp (temp.x, minX, maxX);
		tf.localPosition = temp;

		//通信延迟1帧,客户端发送延迟1帧,本端设置位置延迟1帧
		//Vector3 newPos = new Vector3 (x, 0, z + 2 * Time.deltaTime);
		//Vector3 oldPos = new Vector3 (tf.localPosition.x, 0, tf.localPosition.z);
	}

	public void init(Message_AddPlayer msg,bool isEnemy)
	{
		tf = transform;

		playerId = msg.playerId;
		tf.localPosition = new Vector3 (msg.x, 0, msg.z);
		xMul = msg.xMul;
		minX = msg.minX;
		maxX = msg.maxX;
		zSpeed = msg.zSpeed;
		zSpeedMul = msg.zSpeedMul;
		maxZSpeed = msg.maxZSpeed;
		minZSpeed = msg.minZSpeed;
		natureZAcc = msg.natureZAcc;
		this.isEnemy = isEnemy;

		canSelfMove = false;
		canReceivePos = false;
		canSendPos = false;
	}

	public void beginGame()
	{
		if (!isEnemy) {
			canSelfMove = true;
			canSendPos = true;
			canReceivePos = false;
		} else {
			canSelfMove = true;
			canSendPos = false;
			canReceivePos = true;
		}
	}

	//受伤效果
	public void hurtEffect()
	{
		GetComponentInChildren<BoatFlashControl> ().flash (0.3f, Color.white);
	}

	public void gameOver()
	{
		if (!isEnemy) {
			canSendPos = false;
		} else {
			canReceivePos = false;
		}
	}

	public void stopMove()
	{
		canSelfMove = false;
		canSendPos = false;
		canReceivePos = false;
	}

	//翻船
	public void quote(float keepTime)
	{
		canSelfMove = false;
		if (!isEnemy) {
			canSendPos = false;
		} else {
			canReceivePos = false;
		}
		GetComponentInChildren<BoatQuote> ().rotate (keepTime, 0, null);
		GameLogger.Log ("开始翻船",Color.yellow);
	}

	public void quoteOver()
	{
		canSelfMove = true;
		if (!isEnemy) {
			canSendPos = true;
			zSpeed = 0;
		} else {
			canReceivePos = true;
			targetSpeed = 0;
			GetComponentInChildren<Collider> ().enabled = true;
		}
		GameLogger.Log ("翻船结束",Color.yellow);
	}

	public float Xpercent{
		get{ 
			return Mathf.InverseLerp (minX, maxX, tf.localPosition.x);
		}
	}

	public float Zpercent{
		get{ 
			return Mathf.InverseLerp (0, 100, tf.localPosition.z);
		}
	}
}
