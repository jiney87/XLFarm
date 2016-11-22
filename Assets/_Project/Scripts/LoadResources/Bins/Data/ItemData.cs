using UnityEngine;
using System.Collections.Generic;

public class ItemData : PropertyReader {
	
	public int id{ get; set;}
	public string name{ get; set;}
	public int effectId{ get; set;}
	public string icon{ get; set;}
	public string prefab_path{ get; set;}
	public string prefab_use{ get; set;}
	public string des{ get; set;}


	private static Dictionary<int,ItemData> data=new Dictionary<int, ItemData>();
	
	public void addData()
	{
		data.Add (id, this);
	}
	public void resetData(){
		data.Clear ();
	}
	public void parse(string[] ss){}

	public static ItemData getData(int itemId)
	{
		if (!data.ContainsKey (itemId)) {
			GameLogger.Log ("找不到item:" + itemId);
			return null;
		}
		return data [itemId];
	}
}
