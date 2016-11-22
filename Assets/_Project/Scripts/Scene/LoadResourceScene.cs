using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using Unity_lt_net;

public class LoadResourceScene : BaseScene {


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


	private const string FirstRunApp = "firstRunApp";
	private const string ResManager = "res_manager.bin";
	//平台名字
	private const string String_Android = "Android";
	private const string String_iOS = "iOS";
	//bundle名字
	private const string String_AssetBundleName = "asset_bundles";


	private static string PersistentDataPath;
	private static string StreamingAssetsPath;

	private string packagePath;
	private string resourcePath;

	protected override void awake ()
	{
		base.awake ();
	}

	protected override void start ()
	{
		base.start ();


		PersistentDataPath = Application.persistentDataPath + "/";
		StreamingAssetsPath = Application.streamingAssetsPath + "/";

		step = LoadStep.To_CopyBins;
		setDes ("开始加载");
		setPercent (0);

		initGame ();
	}

	private void initGame()
	{
		errorOccur = false;
		StopAllCoroutines ();
		StartCoroutine (loadResource ());
	}

	private LoadStep step;
	private bool errorOccur;
	private IEnumerator loadResource()
	{
		GameLogger.Log ("资源加载开始",Color.cyan);

		//===========================游戏第一次运行时,StreamingAssets文件复制到persistData===========================//
		if (!errorOccur && step == LoadStep.To_CopyBins) {
			GameLogger.Log("begin To_CopyBins");

			setDes("复制资源文件");
			setPercent (10);

			if (PlayerPrefs.GetInt (FirstRunApp, 0) == 0) {
				yield return copyStreamingAssetsFiles ();
			}
			step = LoadStep.To_Verify;
			GameLogger.Log("end To_CopyBins");
		}

		//============================================向服务器对比包版本号===========================================//
		if (!errorOccur && step == LoadStep.To_Verify) {
			GameLogger.Log("begin To_Verify");

			setDes ("验证游戏版本");
			setPercent (20);

			HttpReturn httpReturn = HttpReturn.create ();
			yield return requestVerify (httpReturn);
			switch (httpReturn.code) {
			case HttpErrorCode.Success:
				//验证的结果有2种:1.大版本一致下载资源文件,2.大版本不一致需要下载游戏包
				VerifyReturnJson json = JsonUtility.FromJson<VerifyReturnJson> (httpReturn.response);
				if (!json.verifyOk ()) {
					step = LoadStep.To_DownloadPackage;
					packagePath = json.packagePath;
				} else {
					step = LoadStep.To_DownloadVersionFile;
					resourcePath = json.resourcePath;
				}
				break;
			default:
				netError ();
				errorOccur = true;
				break;
			}
			GameLogger.Log("end To_Verify");
		}

		//=================================================下载新包=================================================//
		if (!errorOccur && step == LoadStep.To_DownloadPackage) {
			GameLogger.Log("begin To_DownloadPackage");

			setDes ("下载最新版游戏包");
			setPercent (20);

			downloadPackage ();
			errorOccur = true;
			GameLogger.Log("end To_DownloadPackage");
		}

		//=============================================下载资源版本文件=============================================//
		if (!errorOccur && step == LoadStep.To_DownloadVersionFile) {
			GameLogger.Log("begin To_DownloadVersionFile");

			setDes ("下载版本文件");
			setPercent (30);

			FileDownloadReturn fdr = FileDownloadReturn.create ();
			yield return downloadResManager (fdr);
			if (fdr.isDownloadSuccess ()) {
				step = LoadStep.To_CompareVersionFile;
			} else {
				netError ();
				errorOccur = true;
			}
			GameLogger.Log("end To_DownloadVersionFile");
		}

		//====================================比较资源文件版本,获取要下载的文件列表===================================//
		List<ResourceFileInfo> needDownloadFiles = new List<ResourceFileInfo> ();
		if (!errorOccur && step == LoadStep.To_CompareVersionFile) {
			GameLogger.Log("begin To_CompareVersionFile");

			setDes ("验证资源");
			setPercent (40);

			yield return compareResManager (needDownloadFiles);
			//比较结束，删除临时文件
			FileUtil.deleteFile (PersistentDataPath + ResManager);

			step = LoadStep.To_DownloadResourceFile;

			GameLogger.Log ("需要下载资源文件个数:" + needDownloadFiles.Count);

			GameLogger.Log("end To_CompareVersionFile");
		}

		//===============================================下载资源文件===============================================//
		if (!errorOccur && step == LoadStep.To_DownloadResourceFile) {
			GameLogger.Log("begin To_DownloadResourceFile");

			setDes ("下载资源文件");
			setPercent (50);

			if (needDownloadFiles.Count > 0) {
				FileDownloadReturn fdr = FileDownloadReturn.create ();
				yield return downloadResourceFiles (needDownloadFiles, fdr);

				if (fdr.isDownloadSuccess ()) {
					step = LoadStep.To_LoadBins;
				} else {
					netError ();
					errorOccur = true;
				}
			} else {
				step = LoadStep.To_LoadBins;
			}

			GameLogger.Log("end To_DownloadResourceFile");
		}

		//===============================================加载资源文件===============================================//
		if (!errorOccur && step == LoadStep.To_LoadBins) {
			GameLogger.Log("begin To_LoadBins");

			setDes ("加载数据文件");
			setPercent (90);

			yield return readAllBins ();
			step = LoadStep.To_RequestServers;
			GameLogger.Log("end To_LoadBins");
		}

		//==============================================请求服务器列表==============================================//
		if (!errorOccur && step == LoadStep.To_RequestServers) {
			GameLogger.Log("begin To_RequestServers");

			setDes ("连接服务器");
			setPercent (100);

			HttpReturn httpReturn = HttpReturn.create ();
			yield return requestServers (httpReturn);
			switch (httpReturn.code) {
			case HttpErrorCode.Success:
				//解析服务器列表,默认取第一个登录
				ServersReturnJson json = JsonUtility.FromJson<ServersReturnJson> (httpReturn.response);
				if (json.validate()) {
					//TODO
					ServerInfo server = json.servers [0];
					GameManager.instance.serverIp = server.ip;
					GameManager.instance.serverPort = server.port;
				} else {
					netError ();
					errorOccur = true;
				}
				break;
			default:
				netError ();
				errorOccur = true;
				break;
			}
			GameLogger.Log("end To_RequestServers");
		}
		//=================================================加载结束=================================================//
		GameLogger.Log ("资源加载结束",Color.cyan);
		if (!errorOccur) {

			setAssetBundleInfo ();

			enterGame ();
		}
	}

	private void netError()
	{
		showPopWarn ("网络出错，点击重试", initGame);
	}

	private void setAssetBundleInfo()
	{
		/***************************************************************************************/
		/*************      Application.persistentDataPath/asset_bundles/Android/    ***********/
		/***************************************************************************************/
		string rootBundlePath = PersistentDataPath + String_AssetBundleName+"/"+String_Android+"/";
		string rootBundleName = String_Android;

		if (Application.platform == RuntimePlatform.Android) {
			rootBundlePath = PersistentDataPath + String_AssetBundleName+"/"+String_Android+"/";
			rootBundleName = String_Android;
		} else if (Application.platform == RuntimePlatform.IPhonePlayer 
			|| Application.platform==RuntimePlatform.OSXEditor) {
			rootBundlePath = PersistentDataPath + String_AssetBundleName+"/"+String_iOS+"/";
			rootBundleName = String_iOS;
		} else {
			rootBundlePath = PersistentDataPath + String_AssetBundleName+"/"+String_Android+"/";
			rootBundleName = String_Android;
		}

		AssetBundleManager.Instance.init (rootBundlePath,rootBundleName);
	}

	private void enterGame()
	{
		GameManager.instance.loadScene (Constant.Scene_Login, testBundle, false);
	}


	private void testBundle()
	{
//		ResourcesUtil.loadGameObject ("Prefab/3D/PathItem/tianshi");
//
//
//		GameObject itemGo = ResourcesUtil.loadItemGameObject ();
//		Util.resetTransform (itemGo.transform);
//		itemGo.GetComponentInChildren<ItemUIControl> ().init (Message_AchieveItem.create (0, 1004));
	}


	#region To_CopyBins
	private IEnumerator copyStreamingAssetsFiles()
	{
		yield return new WaitForEndOfFrame ();

		List<string> copyFiles = new List<string> (StreamingAssetsCopySetting.getCopyFileNames ());

		//把StreamingAssetsPath下的指定文件复制到PersistentDataPath下
		if(Application.platform == RuntimePlatform.Android)
		{
			FileUtil.copyFolderForAndroid (StreamingAssetsPath, PersistentDataPath, copyFiles);
		}
		else if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FileUtil.copyFolder (StreamingAssetsPath, PersistentDataPath, copyFiles);
		}
		else
		{
			FileUtil.copyFolder (StreamingAssetsPath, PersistentDataPath, copyFiles);
		}

		PlayerPrefs.SetInt (FirstRunApp, 1);
	}
	#endregion

	#region To_Verify
	private IEnumerator requestVerify(HttpReturn httpReturn)
	{
		yield return new WaitForEndOfFrame ();

		string url = GameManager.instance.getTrueVerifyAddress ();
		//version
		string version = GameManager.instance.version;
		//channel
		string channel = GameManager.instance.channel;
		//platform
		string platform = PlatformUtil.getPlatform();

		//request
		Dictionary<string,string> param = new Dictionary<string, string> ();
		param.Add ("version", version);
		param.Add ("channel", channel);
		param.Add ("platform", platform);

		yield return GameManager.instance.httpService.httpRequest (url, param, httpReturn);
	}
	#endregion

	#region To_DownloadPackage
	private void downloadPackage()
	{
		//各平台分别处理 TODO
		if(Application.platform==RuntimePlatform.Android)
		{
			Application.OpenURL(packagePath);
		}
	}
	#endregion

	#region To_DownloadVersionFile
	private IEnumerator downloadResManager(FileDownloadReturn fdr)
	{
		yield return new WaitForEndOfFrame ();
		yield return FileUtil.downLoadFile (resourcePath + ResManager, PersistentDataPath + ResManager, fdr);
	}
	#endregion

	#region To_CompareVersionFile
	private IEnumerator compareResManager(List<ResourceFileInfo> needDownloadFiles)
	{
		yield return new WaitForEndOfFrame ();

		//清除
		needDownloadFiles.Clear ();

		//读取新内容
		List<string> fileNamesNew;
		List<string> fileRivisionsNew;
		List<string> fileSizesNew;
		FileUtil.readResManager (PersistentDataPath + ResManager, out fileNamesNew, out fileRivisionsNew, out fileSizesNew);


		long totalSize = 0;


		//开始比较
		for(int k=0;k<fileNamesNew.Count;k++)
		{
			string fileNameNew = fileNamesNew [k];

			//如果不是本平台的文件,略过
			if (!matchPlatform (fileNameNew)) {
				continue;
			}

			string rivisionNew = fileRivisionsNew [k];
			int fileSizeNew = StringUtil.getInt(fileSizesNew [k]);

			long fileSize = FileUtil.getFileSize (PersistentDataPath + fileNameNew);

			if(fileSize!=fileSizeNew)
			{
				needDownloadFiles.Add (ResourceFileInfo.create (fileNameNew, rivisionNew));
				totalSize += fileSizeNew;
			}
			else
			{
				string rivisionOld = FileUtil.readResourceVersionFile (PersistentDataPath + fileNameNew);
				if(!rivisionNew.Equals(rivisionOld))
				{
					needDownloadFiles.Add (ResourceFileInfo.create (fileNameNew, rivisionNew));
					totalSize += fileSizeNew;
				}
			}
		}

		string downloadSize = ((double)totalSize / 1024 / 1024).ToString ("0.000")+"M";
		GameLogger.Log ("总计下载" + downloadSize, Color.cyan);
	}

	private static bool matchPlatform(string filePath)
	{
		//不是bundle文件,那么是各平台共用的数据文件
		if (filePath.IndexOf (String_AssetBundleName) < 0) {
			return true;
		}
		//是bundle文件,只取各平台自己的资源文件
		if (Application.platform == RuntimePlatform.Android) {
			return filePath.IndexOf (String_Android) >= 0;
		} else if (Application.platform == RuntimePlatform.IPhonePlayer ||
			Application.platform==RuntimePlatform.OSXEditor) {
			return filePath.IndexOf (String_iOS) >= 0;
		} else {
			return filePath.IndexOf (String_Android) >= 0;
		}
	}
	#endregion

	#region To_DownloadResourceFile
	private IEnumerator downloadResourceFiles(List<ResourceFileInfo> fileInfos,FileDownloadReturn fdr)
	{
		int num = fileInfos.Count;

		yield return new WaitForEndOfFrame ();
		while (fileInfos.Count > 0) {
			int index = fileInfos.Count - 1;
			ResourceFileInfo fileInfo = fileInfos [index];
			string fileName = fileInfo.fileName;
			string version = fileInfo.version;

			setPercent (50 + (float)(num - fileInfos.Count) / num * (90 - 50));

			//这里每下一个文件,要在本地生成一个对应的版本文件
			yield return FileUtil.downLoadFile (resourcePath+fileName,PersistentDataPath+fileName,fdr);
			if (fdr.isDownloadSuccess ()) {
				FileUtil.writeResourceVersionFile (PersistentDataPath + fileName, version);
				fileInfos.RemoveAt (index);
			} else {
				GameLogger.Log ("下载失败:" + fileName);
				yield break;
			}
		}
	}
	#endregion

	#region To_LoadBins
	private IEnumerator readAllBins()
	{
		yield return new WaitForEndOfFrame ();

		List<string> binNames;
		List<Type> types;
		List<int> loadWays;

		BinsSetting.setBins (out binNames, out types, out loadWays);
		string curFileName="";
		try
		{
			for(int i=0;i<binNames.Count;i++)
			{
				curFileName=binNames[i];
				Type type=types[i];
				int loadWay=loadWays[i];
				string path=PersistentDataPath+"bins/"+curFileName;
				if(loadWay==1)
				{
					FileUtil.readBinFile1(path, type);
				}
				else
				{
					FileUtil.readBinFile1(path, type);
				}
			}
		}
		catch(Exception e)
		{
			GameLogger.LogError ("读取" + curFileName + "出错");
			GameLogger.LogError(e.Message);
			GameLogger.LogError(e.StackTrace);
		}
	}
	#endregion

	#region To_RequestServers
	private IEnumerator requestServers(HttpReturn httpReturn)
	{
		yield return new WaitForEndOfFrame ();

		string url = GameManager.instance.getTrueServersAddress ();
		//version
		string version = GameManager.instance.version;
		//channel
		string channel = GameManager.instance.channel;
		//platform
		string platform = PlatformUtil.getPlatform();

		//request
		Dictionary<string,string> param = new Dictionary<string, string> ();
		param.Add ("version", version);
		param.Add ("channel", channel);
		param.Add ("platform", platform);

		yield return GameManager.instance.httpService.httpRequest (url, param, httpReturn);
	}
	#endregion
}

public class ResourceFileInfo
{
	public string fileName;
	public string version;

	public static ResourceFileInfo create(string fileName,string version)
	{
		ResourceFileInfo info = new ResourceFileInfo ();
		info.fileName = fileName;
		info.version = version;
		return info;
	}
}