using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResourcesUtil {


	public static void releaseAllResources()
	{
		AssetBundleManager.Instance.releaseAllAssetBundles ();
	}

	/*******************************************************************************************************/
	/******************************************* 通用加载资源 ***********************************************/
	/*******************************************************************************************************/

	public static GameObject loadGameObject(string resourcePath)
	{
		Object obj = Resources.Load (resourcePath);
		if (obj != null) {
			return GameObject.Instantiate (obj) as GameObject;
		}
		return AssetBundleManager.Instance.loadGameObject (resourcePath.ToLower ());
	}

	public static Sprite loadSprite(string resourcePath)
	{
		return AssetBundleManager.Instance.loadSprite (resourcePath.ToLower ());
	}



	/*******************************************************************************************************/
	/******************************************* 一些特别的加载资源 *****************************************/
	/*******************************************************************************************************/

	public static GameObject loadBoat()
	{
		string resourcePath = "Prefab/3D/boat";
		return loadGameObject (resourcePath);
	}

	public static GameObject loadPopWindow()
	{
		string resourcePath = "Prefab/2D/PopWindow";
		return loadGameObject (resourcePath);
	}

	public static GameObject loadMapPoint(bool isEnemy)
	{
		string resourcePath = "Prefab/2D/Map/point_self";
		if (isEnemy) {
			resourcePath = "Prefab/2D/Map/point_other";
		}
		return loadGameObject (resourcePath);
	}

	public static GameObject loadSkill(int skillId)
	{
		SkillData sd = SkillData.getData (skillId);
		if (sd == null) {
			GameLogger.LogError ("技能不存在:" + skillId);
			return null;
		}
		return loadGameObject (sd.skillPrefabPath);
	}

	public static GameObject loadSkillSpwan(string spwanPath)
	{
		return loadGameObject (spwanPath);
	}

	/*******************************************************************************************************/
	/******************************************* 一些没用的加载资源 *****************************************/
	/*******************************************************************************************************/

	public static GameObject loadItem()
	{
		string resourcePath = "Prefab/2D/Item/item";
		return loadGameObject (resourcePath);
	}

	public static GameObject loadExpendEffect()
	{
		string resourcePath = "Prefab/3D/expend";
		return loadGameObject (resourcePath);
	}
}
