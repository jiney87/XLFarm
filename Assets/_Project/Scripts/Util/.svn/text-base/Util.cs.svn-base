using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Util {

	public static bool isListEmpty(IList list)
	{
		return list == null || list.Count == 0;
	}

	public static Vector2 getHorizontalPos(Vector3 pos)
	{
		return new Vector2 (pos.x, pos.z);
	}

	public static float getSecondsFromStartup()
	{
		return Time.realtimeSinceStartup;
	}

	public static string getPlayerPrefs(string key)
	{
		return PlayerPrefs.GetString (key, "");
	}

	public static void setPlayerPrefs(string key,string value)
	{
		PlayerPrefs.SetString (key, value);
	}

	public static void resetTransform(Transform tf)
	{
		tf.localPosition = Vector3.zero;
		tf.localRotation = Quaternion.identity;
		tf.localScale = Vector3.one;
	}

	public static T getSceneUI<T>() where T :BaseScene
	{
		GameObject[] gos = SceneManager.GetActiveScene ().GetRootGameObjects ();
		for (int k = 0; k < gos.Length; k++) {
			if (gos [k].name.Equals ("UIRoot")) {
				return gos [k].GetComponent<T> ();
			}
		}
		return null;
	}

	public static T getOrAddComponent<T>(GameObject go) where T :MonoBehaviour
	{
		T t = go.GetComponent<T> ();
		if (t == null) {
			t = go.AddComponent<T> ();
		}
		return t;
	}

	public static T CreateObjectByType<T>(Type type)
	{
		try
		{
			if (type == null)
				return default(T);
			else
				return (T)Activator.CreateInstance(type);
		}
		catch (Exception e)
		{
			GameLogger.LogError ("CreateObjectByType<"+type+">出错,"+e.StackTrace);
			return default(T);
		}
	}

	public static double Radian2Angle(float radian)
	{
		return radian * 180 / Math.PI;
	}

	public static double Angle2Radian(float angle)
	{
		return angle / 180 * Math.PI;
	}

	/// <summary>
	/// 设置模型所在层
	/// </summary>
	/// <param name="parent">Parent.</param>
	/// <param name="layer">Layer.</param>
	public static void SetModelsInLayer(GameObject parent, int layer)
	{
		Transform[] allTransform = parent.GetComponentsInChildren<Transform>(true);
		foreach (Transform trans in allTransform)
		{
			trans.gameObject.layer = layer;
		}
	}

	// 将字符串根据分号解为整型数组
	public static List<int> SpliteToIntList(string target)
	{
		if (target == null) target = string.Empty;

		List<int> intList = new List<int>();
		string[] array = target.Split(';');

		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].Equals(string.Empty))
			{
				intList.Add(int.Parse(array[i]));
			}
		}

		return intList;
	}

	public static List<int> SpliteToIntList(string target, params char[] separator)
	{
		if (target == null) target = string.Empty;

		List<int> intList = new List<int>();
		string[] array = target.Split(separator);

		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].Equals(string.Empty))
			{
				intList.Add(int.Parse(array[i]));
			}
		}

		return intList;
	}

	public static List<T> SpliteToEnumList<T>(string target) where T : struct
	{
		if (target == null) target = string.Empty;

		List<T> intList = new List<T>();
		string[] array = target.Split(';');

		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Equals(string.Empty))
			{
				array[i] = "0";
			}

			Type enumType = typeof(T);

			intList.Add((T)Enum.Parse(enumType, array[i], true));
		}

		return intList;
	}

	public static List<float> SpliteToFloatList(string target)
	{
		if (target == null) target = string.Empty;

		List<float> floatList = new List<float>();
		string[] array = target.Split(';');

		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].Equals(string.Empty))
			{
				floatList.Add(float.Parse(array[i]));
			}
		}

		return floatList;
	}

	public static Vector3 SpliteToVector3(string target)
	{
		string[] array = target.Split(';');

		if (array != null && array.Length == 3)
		{
			return new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
		}
		else
		{
			GameLogger.Log("SpliteToVector3 is error!, target = " + target, Color.yellow);
		}

		return Vector3.zero;
	}

	// 将字符串分解
	public static List<string> SpliteToStrList(string target)
	{
		string[] array = target.Split(';');

		return new List<string>(array);
	}

	public static List<string> SpliteToStrList(string target, params char[] separator)
	{
		string[] array = target.Split(separator);

		return new List<string>(array);
	}

	//逐个字符转换
	public static List<int> CovertStringToIntList(string target)
	{
		List<int> intList = new List<int>();
		for (int k = 0; k < target.Length; k++)
		{
			intList.Add(CovertInt(target[k] + ""));
		}
		return intList;
	}

	public static int CovertInt(string str)
	{
		int result;
		if (int.TryParse(str, out result))
		{
			return result;
		}
		return 0;
	}

	public static void clearParent(GameObject child)
	{
		child.transform.parent = null;
	}

	// 将某个物体挂在目标物体下
	public static void AddChild(GameObject child, GameObject parent)
	{
		if (child == null || parent == null) return;

		child.transform.SetParent (parent.transform);
	}
    /// <summary>
    ///  将对象作为子对象放到指定对象下
    /// </summary>
    /// <param name="child">子对象</param>
    /// <param name="parent">父对象</param>
    /// <param name="useSelfScale">是否使用自己的缩放</param>
    public static void AttachToParent(GameObject child, GameObject parent, bool useSelfScale = false)
    {
        Vector3 selfScale = child.transform.localScale;
        RectTransform rt = child.GetComponent<RectTransform>();
        if (rt)
        {
            rt.SetParent(parent.transform, false);
        }
        else
        {
            child.transform.parent = parent.transform;
        }

        child.transform.localPosition = Vector3.zero;
        child.transform.localRotation = Quaternion.identity;
        if (!useSelfScale)
        {
            child.transform.localScale = Vector3.one;
        }
        else
        {
            child.transform.localScale = selfScale;
        }
        SetToLayer(child, parent.layer);
    }
    /// <summary>
    ///  改变对象所在层
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="newLayer">目标层</param>
    public static void SetToLayer(GameObject obj, int layer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetToLayer(child.gameObject, layer);
        }
    }

	public static void DesotryGameObjectsWithThisComponent<T>(GameObject[] targets) where T : Component
	{
		List<T> comlist = new List<T>();

		for (int i = targets.Length - 1; i >= 0; i--)
		{
			T[] coms = targets[i].GetComponentsInChildren<T>();
			comlist.AddRange(new List<T>(coms));
		}

		for (int i = 0; i < comlist.Count; i++)
		{
			GameObject.DestroyImmediate(comlist[i].gameObject);
		}
	}

	// 删除物体某个组件
	public static void DestoryComponent<T>(GameObject go) where T : Component
	{
		T Component = go.GetComponentInChildren<T>();
		if (Component != null)
		{
			UnityEngine.Object.Destroy(Component);
		}
	}

	//销毁子物体
	public static void DestoryChilds(GameObject go)
	{
		if (go == null) return;

		int childCount = go.transform.childCount;
		for (int k = 0; k < childCount; k++)
		{
			UnityEngine.Object.DestroyImmediate(go.transform.GetChild(0).gameObject);
		}
	}

	//销毁指定名字的子节点
	public static void DestoryChild(GameObject go, string childName)
	{
		Transform child = go.transform.FindChild(childName);
		if (child != null)
		{
			UnityEngine.Object.DestroyImmediate(child.gameObject);
		}
	}

	//销毁子物体
	public static void DestoryChildsNotImmdiate(GameObject go)
	{
		if (go == null) return;

		int childCount = go.transform.childCount;
		for (int k = 0; k < childCount; k++)
		{
			UnityEngine.Object.Destroy(go.transform.GetChild(k).gameObject);
		}
	}

	public static void SetLayer(GameObject target, int layer, int excludeLayer = -100)
	{
		Transform[] allTransform = target.GetComponentsInChildren<Transform>(true);
		foreach (Transform trans in allTransform)
		{
			if (excludeLayer == trans.gameObject.layer)
				continue;
			trans.gameObject.layer = layer;
		}
	}

	public static void EnableObjCommponent<T>(GameObject go, bool enable) where T : MonoBehaviour
	{
		T target = go.GetComponentInChildren<T>();
		if (target != null)
		{
			MonoBehaviour cp = (MonoBehaviour)target;
			cp.enabled = enable;
		}
	}

	public static void EnableObjCommponents<T>(GameObject go, bool enable) where T : MonoBehaviour
	{
		T[] target = go.GetComponentsInChildren<T>();

		for (int i = 0; i < target.Length; i++)
		{
			MonoBehaviour cp = (MonoBehaviour)target[i];
			cp.enabled = enable;
		}
	}

	//相机坐标转换
	public static Vector3 TransformPos(Camera fromCamera, Vector3 fromPos, Camera toCamera, bool clearZ = true)
	{
		if (fromCamera == null || toCamera == null)
		{
			GameLogger.LogError("有个相机为空");
			return Vector3.zero;
		}
		Vector3 temp = fromCamera.WorldToScreenPoint(fromPos);
		Vector3 uiPos = toCamera.ScreenToWorldPoint(temp);
		if (clearZ)
		{
			return new Vector3(uiPos.x, uiPos.y, 0);
		}
		return uiPos;
	}

	// 将颜色转为ngui string
	public static string ConvertColorToFomateCode(Color color)
	{
		return "[" + string.Format("{0,2:X2}", (int)(color.r * 255))
			+ string.Format("{0,2:X2}", (int)(color.g * 255))
			+ string.Format("{0,2:X2}", (int)(color.b * 255)) + "]";
	}

	public static string ConvertColorString(string msg, Color color)
	{
		return ConvertColorToFomateCode(Color.red) + msg + "[-]";
	}

	public static Transform FindTargetChildInHierarchy(Transform t, string name)
	{
		foreach (Transform c in t)
		{
			if (c.gameObject.name == name)
			{
				return c;
			}
		}

		Transform r;
		foreach (Transform c in t)
		{
			r = FindTargetChildInHierarchy(c, name);
			if (r != null)
			{
				return r;
			}
		}
		return null;
	}

	/// <summary>
	/// Gets the time string by second.
	/// </summary>
	/// <returns>The time string by second.</returns>
	/// <param name="second">秒数</param>
	public static string getTimeStrBySecond(float second)
	{
		//yyyy-MM-dd HH:mm:ss
		return new DateTime((long)(second * 10000000)).ToString("HH:mm:ss:ff");//毫秒：c#是fff，java是SSS
	}

	private static bool inLayer(int selfLayer,params int[] layers)
	{
		if(layers==null)
		{
			return false;
		}
		for(int k=0;k<layers.Length;k++)
		{
			if(selfLayer==layers[k])
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// 获取从0001年1月1日0点到现在的100毫微秒数(实际就是100纳秒)
	/// </summary>
	/// <returns>The current ticks.</returns>
	public static long getCurrentTicks()
	{
		return DateTime.Now.Ticks;
	}

	/// <summary>
	/// 获取经历的时间，单位秒
	/// </summary>
	/// <returns>The time offset.</returns>
	/// <param name="ticksFrom">Ticks from.</param>
	/// <param name="ticksTo">Ticks to.</param>
	public static float getTimeOffset(long ticksFrom, long ticksTo)
	{
		//Debug.Log("ticksTo-ticksFrom:"+(ticksTo-ticksFrom));
		return (ticksTo - ticksFrom) / 10000000f;
	}

	public static void RemoveItemFromDictionary<T1, T2>(Dictionary<T1, T2> dictData, T1 key)
	{
		if (dictData.ContainsKey(key))
		{
			dictData.Remove(key);
		}
	}

//	public static void tweenAlpha(UIWidget widget, float alphaFrom, float alphaTo, float during, float delay, EventDelegate.Callback callback)
//	{
//		widget.alpha = alphaFrom;
//		TweenAlpha ta = widget.gameObject.AddMissingComponent<TweenAlpha>();
//		ta.from = alphaFrom;
//		ta.to = alphaTo;
//		ta.duration = during;
//		ta.delay = delay;
//		ta.tweenFactor = 0;
//		ta.ignoreTimeScale = false;
//		ta.enabled = true;
//		EventDelegate.Set(ta.onFinished, callback);
//	}
//
//	public static void tweenPosition(GameObject go, Vector3 posFrom, Vector3 posTo, float during, float delay, EventDelegate.Callback callback)
//	{
//		go.transform.localPosition = posFrom;
//		TweenPosition ta = go.AddMissingComponent<TweenPosition>();
//		ta.from = posFrom;
//		ta.to = posTo;
//		ta.duration = during;
//		ta.delay = delay;
//		ta.tweenFactor = 0;
//		ta.ignoreTimeScale = false;
//		ta.enabled = true;
//		EventDelegate.Set(ta.onFinished, callback);
//	}
//
//	public static void tweenFillAmount(UISprite sp, float from, float to, float during, float delay, EventDelegate.Callback callback)
//	{
//		sp.gameObject.AddMissingComponent<TweenFillAmount>().tween(from, to, during, delay, callback);
//	}

	/// <summary>
	/// 获取一个transform下的指定名字的后辈节点
	/// </summary>
	/// <returns>The child.</returns>
	/// <param name="root">Root.</param>
	/// <param name="childName">Child name.</param>
	public static Transform findChildByName(Transform root, string name)
	{
		Transform[] tfs=root.GetComponentsInChildren<Transform>();
		for(int k=0;k<tfs.Length;k++)
		{
			if(tfs[k].name.Equals(name))
			{
				return tfs[k];
			}
		}
		return null;
	}

	public static void PlayParticleSystem(GameObject obj, bool isPlay)
	{
		if (obj == null)
			return;

		if (obj.activeSelf == isPlay)
			return;

		obj.SetActive(isPlay);
		ParticleSystem[] allParticle = obj.GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < allParticle.Length; i++)
		{
			if (isPlay)
				allParticle[i].Play();
			else
				allParticle[i].Stop();
		}
	}

	public static Transform FindChildWithName(Transform root, string findName)
	{
		if (root.name.Equals(findName))
			return root;
		for (int i = 0; i < root.childCount; i++)
			FindChildWithName(root.GetChild(i), findName);
		return null;
	}

	/// <summary>
	/// 重新指定物体的shader，用于bundle内加载的物体
	/// </summary>
	public static void ResetGameObjectShader(GameObject targetObj)
	{
		Renderer[] renders = targetObj.GetComponentsInChildren<Renderer>();

		for (int i = 0; i < renders.Length; i++)
		{
			renders[i].receiveShadows = false;

			Material[] mats = renders[i].materials;

			for (int j = 0; j < mats.Length; j++)
			{
				if (mats[j].shader != null)
				{
					Shader targetShader = Shader.Find(mats[j].shader.name);
					if (targetShader != null)
					{
						mats[j].shader = targetShader;
					}
				}
			}
		}
	}

	public static void ResetPhysics(GameObject player)
	{
		// 移动时，把物理组件关了，完事再开
		Cloth[] _cloths = player.GetComponentsInChildren<Cloth>();

		for (int i = 0; i < _cloths.Length; i++)
		{
			_cloths[i].enabled = false;
		}

		for (int i = 0; i < _cloths.Length; i++)
		{
			_cloths[i].enabled = true;
		}
	}

	public static void destoryListGameObject(List<GameObject> gos)
	{	
		for (int k = gos.Count - 1; k >= 0; k--) {
			GameObject go = gos [k];
			gos.Remove (go);
			GameObject.Destroy (go);
		}
	}
}