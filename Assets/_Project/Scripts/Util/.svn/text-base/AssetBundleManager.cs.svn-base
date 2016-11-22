using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AssetBundleManager {

	private static AssetBundleManager instance;
	public static AssetBundleManager Instance
	{
		get{ 
			if(instance==null)
			{
				instance = new AssetBundleManager ();
			}
			return instance;
		}
	}


	private string commonBundlePath;
	private string rootAssetBundleName;


	private AssetBundleManifest rootManifest;

	//开启状态的assetBundles,采用ChunkBasedCompression压缩方式,只占用很少内存,只在场景切换时才会卸载,防止出现材质丢失/游戏切换到后台再切换回来时材质丢失
	private Dictionary<string,AssetBundlePackage> opendBundles=new Dictionary<string, AssetBundlePackage>();



	public void init(string rootBundlePath,string rootBundleName)
	{
		commonBundlePath = rootBundlePath;
		rootAssetBundleName = rootBundleName;
	}

	public void releaseAllAssetBundles()
	{
		//到下一个场景很可能用不到这些bundle了,所以卸掉,虽然它们占了很少的内存
		foreach (KeyValuePair<string,AssetBundlePackage> pair in opendBundles) {
			AssetBundlePackage bundleManager = pair.Value;
			AssetBundle bundle = bundleManager.bundle;
			bundleManager.bundle = null;
			bundle.Unload (false);
		}
		opendBundles.Clear ();
	}

	private void loadRootAssetBundle()
	{
		AssetBundle rootBundle = AssetBundle.LoadFromFile (commonBundlePath + rootAssetBundleName);
		rootManifest = rootBundle.LoadAsset<AssetBundleManifest> ("AssetBundleManifest");
	}

	private AssetBundle loadAssetBundle(string bundleName)
	{
		if (opendBundles.ContainsKey (bundleName)) {
			return opendBundles [bundleName].bundle;
		}

		AssetBundle bundle = AssetBundle.LoadFromFile (commonBundlePath + bundleName);
		if (bundle == null) {
			GameLogger.Log ("找不到bundle:" + commonBundlePath + bundleName, Color.red);
			return null;
		}

		opendBundles.Add (bundleName, AssetBundlePackage.create (bundle));
		GameLogger.Log ("添加bundle:" + bundleName, Color.green);
		return bundle;
	}


	/*******************************************************************************************************/
	/******************************************* 加载资源 ***************************************************/
	/***************************** assetBundle的命名规则：以它要包含的资源的路径为名 **************************/
	/*******************************************************************************************************/

	public GameObject loadGameObject(string goPath)
	{
		if (rootManifest == null) {
			loadRootAssetBundle ();
		}

		//查看依赖的bundle是不是开启状态,不是开启状态的话就开启(虽然我知道这些依赖包会自动被加载，但还是想手动加载，方便管理)
		string[] depends = rootManifest.GetAllDependencies (goPath);
		for (int k = 0; depends != null && k < depends.Length; k++) {
			//加载依赖bundle
			loadAssetBundle(depends [k]);
		}

		//加载自己的bundle
		AssetBundle bundle=loadAssetBundle(goPath);
		//获取goName
		string goName=getSimpleFileName(goPath);
		//加载gameObject
		GameObject tempGo = bundle.LoadAsset<GameObject> (goName);

		//实例化
		return GameObject.Instantiate (tempGo) as GameObject;
	}

	public Sprite loadSprite(string spPath)
	{
		//UI图片没有依赖其他资源,所以不再判断主manifest,直接加载bundle
		string bundleName=getParentPath(spPath);
		//加载bundle
		AssetBundle bundle=loadAssetBundle(bundleName);
		//获取spName
		string spName=getSimpleFileName(spPath);
		//加载图片
		Sprite tmepSp=bundle.LoadAsset<Sprite>(spName);

		//实例化
		return GameObject.Instantiate (tmepSp) as Sprite;
	}


	private static string getSimpleFileName(string fileName)
	{
		int index = fileName.LastIndexOf ("/");
		if (index >= 0) {
			return fileName.Substring (index + 1);
		}
		return fileName;
	}

	private static string getParentPath(string fileName)
	{
		int index = fileName.LastIndexOf ("/");
		if (index >= 0) {
			return fileName.Substring (0, index);
		}
		return fileName;
	}

	private static void lookAllAssetsInBundle(AssetBundle bundle)
	{
		Object[] objs = bundle.LoadAllAssets ();
		foreach (Object obj in objs) {
			GameLogger.Log ("obj:" + obj);
		}
	}
}

public class AssetBundlePackage
{
	public AssetBundle bundle;

	public static AssetBundlePackage create(AssetBundle bundle)
	{
		AssetBundlePackage manager = new AssetBundlePackage ();
		manager.bundle = bundle;
		return manager;
	}
}