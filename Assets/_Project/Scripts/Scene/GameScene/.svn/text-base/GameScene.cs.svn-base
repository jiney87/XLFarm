using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameScene : BaseScene {

	public ETCJoystickTransition transition;
	public FollowTarget_V2 cameraFollow;

	private BoatControl self;
	private List<BoatControl> enemys = new List<BoatControl> ();

	public GameObject boatParentGo;
	public GameObject itemParentGo;

	[Header("UI")]
	public GameUI ui;
	public MapUI mapUI;

	private Dictionary<long,GameObject> itemGos = new Dictionary<long, GameObject> ();

	[Header("场景类型")]
	public int sceneType;


	protected override void awake ()
	{
		base.awake ();
		sceneType = UserInfo.sceneType;
	}

	public void createSelf(Message_AddPlayer msg)
	{
		GameObject selfGo = ResourcesUtil.loadBoat ();
		selfGo.name = "boat_self";
		Util.AddChild (selfGo, boatParentGo);
		Util.resetTransform (selfGo.transform);
		//赋值
		self=selfGo.GetComponentInChildren<BoatControl> ();
		self.init (msg, false);

		SkillControl skillControl = selfGo.GetComponentInChildren<SkillControl> ();
		skillControl.init (this);
		ui.init (msg.name, msg.skillId1, msg.skillId2, skillControl);
		mapUI.loadPoint (self);

		//注册摇杆
		transition.moveList.Add (self.GetComponentInChildren<BoatControl>().onMove);
		//相机跟踪
		cameraFollow.target=self.transform;
	}

	public void createEnemy(Message_AddPlayer msg)
	{
		GameObject enemyGo = ResourcesUtil.loadBoat ();
		enemyGo.name = "boat_enemy";
		Util.AddChild (enemyGo, boatParentGo);
		Util.resetTransform (enemyGo.transform);
		//赋值
		BoatControl enemy=enemyGo.GetComponentInChildren<BoatControl> ();
		enemy.init (msg, true);

		mapUI.loadPoint (enemy);
		//关闭碰撞控制
		Destroy (enemy.GetComponentInChildren<BoatColliderControl> ());

		enemys.Add (enemy);
	}

	public List<GameObject> getAllplayers()
	{
		List<GameObject> allPlayers = new List<GameObject> ();
		allPlayers.Add (self.gameObject);
		for (int k = 0; k < enemys.Count; k++) {
			if (enemys [k] != null) {
				allPlayers.Add (enemys [k].gameObject);
			}
		}
		return allPlayers;
	}

	public void beginGame()
	{
		ui.beginGame ();
		self.beginGame ();
		for (int k = 0; k < enemys.Count; k++) {
			if (enemys [k] != null) {
				enemys [k].beginGame ();
			}
		}
	}

	public void playerMove(Message_MoveToClient msg)
	{
		int playerId = msg.playerId;
		BoatControl posControl = getBoatControl (playerId);
		posControl.onMove (msg.x, msg.z, msg.speed);
	}

	private BoatControl getBoatControl(int playerId)
	{
		if (playerId == self.playerId) {
			return self;
		} else {
			for (int k = 0; k < enemys.Count; k++) {
				if (enemys [k] != null && enemys[k].playerId==playerId) {
					return enemys [k];
				}
			}
			return null;
		}
	}

	//场景加入道具
	public void addItem(Message_AddItem msg)
	{
		ItemData itemData=ItemData.getData(msg.itemId);
		GameObject itemGo=ResourcesUtil.loadGameObject (itemData.prefab_path);
		Util.AddChild (itemGo, itemParentGo);
		Util.resetTransform (itemGo.transform);
		itemGo.GetComponent<PathItem> ().init (msg);
		itemGos.Add (msg.itemUniqueId, itemGo);
	}

	//场景移除道具
	public void removeItem(Message_RemoveItem msg)
	{
		if (!itemGos.ContainsKey (msg.itemUniqueId)) {
			return;
		}
		GameObject itemGo=itemGos[msg.itemUniqueId];
		itemGos.Remove (msg.itemUniqueId);
		Destroy (itemGo);
	}

	//玩家获得道具
	public void achieveItem(Message_AchieveItem msg)
	{
		//更新UI
		ui.achieveItem(msg);
	}

	//使用道具
	public void useItem(Message_UseItemReturn msg)
	{
		BoatControl releaserBoat = getBoatControl (msg.playerId);

		if (isSelf (msg.playerId)) {
			//移除UI上对应的道具
			ui.useItemUI(msg.itemUniqueId);
		}

		ItemData itemData = ItemData.getData (msg.itemId);
		GameObject useItemGo = ResourcesUtil.loadGameObject (itemData.prefab_use);

		//TODO 缺少目标
		//useItemGo.GetComponentInChildren<UseItem> ().init (releaserBoat, null, msg.itemUniqueId, itemData);
	}

	//有玩家使用技能
	public void useSkill(Message_UseSkillReturn msg)
	{
		BoatControl releaser = getBoatControl (msg.releaserPlayerId);

		if (isSelf (msg.releaserPlayerId)) {
			ui.useSkill (msg.skillId);
		}

		List<GameObject> targets = new List<GameObject> ();
		for (int k = 0; msg.targetPlayerIds != null && k < msg.targetPlayerIds.Count; k++) {
			targets.Add (getBoatControl (msg.targetPlayerIds [k]).gameObject);
		}

		releaser.GetComponent<SkillControl> ().releaseSkill (isSelf (msg.releaserPlayerId), msg.skillId, targets);
	}

	private bool isSelf(int playerId)
	{
		return playerId == UserInfo.playerId;
	}

	public void changeMaxSpeed(Message_ChangeMaxSpeed msg)
	{
		self.maxZSpeed = msg.maxSpeed;
		if (msg.isAdd) {
			self.zSpeed = self.maxZSpeed;
		} else {
			if (self.zSpeed > self.maxZSpeed) {
				self.zSpeed = self.maxZSpeed;
			}
		}
	}

	public void boatQuote(Message_BoatQuote msg)
	{
		BoatControl boat = getBoatControl (msg.playerId);
		boat.quote (msg.quoteTime);
	}

	public void boatQuoteOver(Message_BoatQuoteOver msg)
	{
		BoatControl boat = getBoatControl (msg.playerId);
		boat.quoteOver ();
	}

	public void changeBoatSpeed(Message_ChangeSpeed msg)
	{
		float speed = self.zSpeed;
		if (msg.isAdd) {
			speed += msg.changeValue;
		} else {
			speed -= msg.changeValue;
		}
		self.zSpeed = Mathf.Clamp (speed, self.minZSpeed, self.maxZSpeed);
	}

	public void playerExitScene(Message_PlayerExitScene msg)
	{
		BoatControl boat = getBoatControl (msg.exitorPlayerId);
		if (boat != null) {
			boat.stopMove ();
		}
	}

	#region game over
	public void showResult(Message_GameOver msg)
	{
		int sequence = msg.sequence;
		int gold = msg.gold;
		int exitTime = msg.exitTime;

		GameLogger.Log ("名次:"+sequence + "", Color.cyan);
		//弹窗
		showPopWarn("your sequence is "+sequence+",\nand you got "+gold+" gold",switchToMain);
		//关闭碰撞 TODO

		//注销摇杆
		transition.moveList.Remove (self.GetComponentInChildren<BoatControl>().onMove);
		//取消相机跟踪
		cameraFollow.target=null;

		self.gameOver();
		for (int k = 0; k < enemys.Count; k++) {
			enemys[k].gameOver ();
		}
	}

	public void sceneException(Message_SceneException msg)
	{
		//弹窗
		showPopWarn("玩家不齐,退回主界面",switchToMain);
	}

	private void switchToMain()
	{
		GameManager.instance.loadScene (Constant.Scene_Main, null, true);
	}
	#endregion

	protected override void gc ()
	{
		base.gc ();
		itemGos.Clear ();
		itemGos = null;
	}


	public void testUI()
	{
		showPopWarn ("测试弹窗", null);
	}
}
