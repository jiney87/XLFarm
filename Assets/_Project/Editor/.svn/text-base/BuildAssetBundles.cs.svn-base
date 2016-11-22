using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using UnityEditorInternal;

/// <summary>
/// Build asset bundles.
/// @LiTao
/// </summary>
public class BuildAssetBundles : MonoBehaviour {

	//private string resRootPath = Application.dataPath + "/_BundleResources/";
	private const string ResRootPath = "Assets/_BundleResources/";
	private const string BundlePath = "Assets/_AssetBundles/";

	[MenuItem ("Assets/Build AssetBundles (Android)")]
	static void BuildAllAssetBundlesForAndroid ()
	{
		ClearConsole ();
		//清除掉resource下的bundleName
		clearBundleName("Assets/_Project/Resources/");

		//设置bundleNames
		setBundleNames ();

		//导出各平台bundle
		BuildAllAssetBundles (BuildTarget.Android);

		EditorUtility.DisplayDialog ("导出AssetBundles (Android)", "导出完成！", "确定");
	}

	[MenuItem ("Assets/Build AssetBundles (Android + iOS)")]
	static void BuildAllAssetBundlesForAllPlatform ()
	{
		ClearConsole ();
		//清除掉resource下的bundleName
		clearBundleName("Assets/_Project/Resources/");

		//设置bundleNames
		setBundleNames ();

		//导出各平台bundle
		BuildAllAssetBundles (BuildTarget.Android);
		BuildAllAssetBundles (BuildTarget.iOS);

		EditorUtility.DisplayDialog ("导出AssetBundles  (Android + iOS)", "导出完成！", "确定");
	}



	/*************************************************************************************************/
	/************************************** 设置各类资源的bundleName **********************************/
	/*************************************************************************************************/
	private static void setBundleNames()
	{
		//EditorUtility.DisplayProgressBar ("设置资源的bundleName", "准备设置", 0);

		/*****************************************************************************************/
		/****************************** 设置UI的bundleName ***************************************/
		/*****************************************************************************************/

		//经测试,如果trueColor的图片分开导bundle,那么导出的bundle跟原图大小差不多,最终导出来的总体积很大,所以按图集导出,效果还是很可以滴

		//按图集导出,文件夹名即为图集的名字
		setDirectoryBundleName (ResRootPath+"UI/", null);

		//单个文件导出
		//setFileBundleNames (ResRootPath+"UI/", null);

		/*****************************************************************************************/
		/****************************** 设置prefab的bundleName ***********************************/
		/*****************************************************************************************/

		//EditorUtility.CollectDependencies ();//收集依赖方法
		//这里依赖的资源都在Model下,所以直接给Model下的所有资源设置bundleName,省得去找依赖了
		//依赖的资源应该单独设置bundleName,防止一个依赖资源被导出到多个bundle中
		setFileBundleNames(ResRootPath+"Model/",null);
		setFileBundleNames(ResRootPath+"Prefab/",null);
	}

	private static void clearBundleName(string path)
	{
		path = correctPath (path);
		string[] filePaths = Directory.GetFiles (path);
		if (filePaths != null && filePaths.Length > 0) {
			for (int k = 0; k < filePaths.Length; k++) {
				setBundleName (filePaths[k], null, null);
			}
		}

		string[] directoryPaths = Directory.GetDirectories (path);
		if (directoryPaths != null && directoryPaths.Length > 0) {
			for (int k = 0; k < directoryPaths.Length; k++) {
				clearBundleName (directoryPaths [k]);
			}
		}
	}

	/// <summary>
	/// 文件夹下的所有文件共用一个bundleName,适用于UI图片
	/// </summary>
	/// <param name="directoryPath">Directory path.</param>
	/// <param name="bundleVariant">Bundle variant.</param>
	private static void setDirectoryBundleName(string directoryPath,string bundleVariant)
	{
		string[] subDirectoryPaths = Directory.GetDirectories (directoryPath);
		if (subDirectoryPaths != null && subDirectoryPaths.Length > 0) {
			for (int k = 0; k < subDirectoryPaths.Length; k++) {
				string subDirectoryPath = correctPath (subDirectoryPaths [k]);

				//获取bundleName
				string bundleName = subDirectoryPath.Substring(ResRootPath.Length).ToLower();

				string[] filePaths = Directory.GetFiles (subDirectoryPath);
				for (int m = 0; filePaths!=null && m < filePaths.Length; m++) {

					string filePath = filePaths [m];

					//文件设置bundleName
					setBundleName (filePath, bundleName, bundleVariant);
				}
			}
		}
	}

	private static void setFileBundleNames(string path,string bundleVariant)
	{
		//本层
		string[] filePaths = Directory.GetFiles (path);
		if (filePaths != null && filePaths.Length > 0) {
			for (int k = 0; k < filePaths.Length; k++) {
				setFileBundleName (filePaths [k], bundleVariant);
			}
		}

		//下一层
		string[] directroyPaths = Directory.GetDirectories (path);
		if (directroyPaths != null && directroyPaths.Length > 0) {
			for (int k = 0; k < directroyPaths.Length; k++) {
				setFileBundleNames (directroyPaths [k], bundleVariant);
			}
		}
	}

	/// <summary>
	/// 文件设置bundleName
	/// </summary>
	/// <param name="filePath">File path.</param>
	/// <param name="bundleVariant">Bundle variant.</param>
	private static void setFileBundleName(string filePath,string bundleVariant)
	{
		filePath = correctPath (filePath);

		//获取bundleName
		string bundleName = filePath.Substring(ResRootPath.Length).ToLower();
		//去掉扩展名
		bundleName=bundleName.Substring(0,bundleName.LastIndexOf("."));

		//文件设置bundleName
		setBundleName (filePath, bundleName, bundleVariant);
	}


	private static void setBundleName(string filePath,string bundleName,string bundleVariant)
	{
		//过滤掉.meta文件
		if (filePath.EndsWith (".meta")) {
			return;
		}

		Debug.Log ("<b><color=cyan>bundleName:" + bundleName+"</color></b>,filePath:"+filePath);

		AssetImporter importer = AssetImporter.GetAtPath (filePath);
		importer.assetBundleName = bundleName;
		//bundleName非空才可以设置bundleVariant
		if (!string.IsNullOrEmpty (bundleName)) {
			importer.assetBundleVariant = bundleVariant;
		}
	}
	/*************************************************************************************************/
	/************************************** 导出assetBundles *****************************************/
	/*************************************************************************************************/
	private static void BuildAllAssetBundles(BuildTarget buildTarget)
	{
		string temp = "Hahagou";
		switch (buildTarget) {
		case BuildTarget.Android:
			temp = "Android";
			break;
		case BuildTarget.iOS:
			temp = "iOS";
			break;
		}

		string outputPath = BundlePath + temp;
		//删除路径下的所有文件夹和文件
		DeleteContents(outputPath);

		AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles (outputPath, BuildAssetBundleOptions.ChunkBasedCompression, buildTarget);
		EditorApplication.RepaintProjectWindow ();
	}

	private static void DeleteContents(string path)
	{
		string[] directroyPaths = Directory.GetDirectories (path);
		if (directroyPaths != null && directroyPaths.Length > 0) {
			for (int k = 0; k < directroyPaths.Length; k++) {
				Directory.Delete (correctPath (directroyPaths [k]), true);
			}
		}

		string[] filePaths = Directory.GetFiles (path);
		if (filePaths != null && filePaths.Length > 0) {
			for (int k = 0; k < filePaths.Length; k++) {
				File.Delete (correctPath (filePaths [k]));
			}
		}
	}


	private static string correctPath(string path)
	{
		return path.Replace("\\","/");
	}

	/*************************************************************************************************/
	/********************************************* 清除控制台 *****************************************/
	/*************************************************************************************************/
	private static void ClearConsole()
	{
		// This simply does "LogEntries.Clear()" the long way:
		var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
		var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
		clearMethod.Invoke(null, null);
	}
}
