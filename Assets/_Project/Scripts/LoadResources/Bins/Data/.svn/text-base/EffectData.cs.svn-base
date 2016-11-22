using UnityEngine;
using System.Collections.Generic;

public class EffectData : PropertyReader {
	
	public int id{ get; set;}
	public int effectType{ get; set;}
	public float value1{ get; set;}
	public float value2{ get; set;}
	public float value3{ get; set;}


	private static Dictionary<int,EffectData> data = new Dictionary<int, EffectData> ();
	
	public void addData()
	{
		data.Add (id, this);
	}
	public void resetData(){
		data.Clear ();
	}
	public void parse(string[] ss){}

	public static EffectData getData(int itemEffectId)
	{
		if (!data.ContainsKey (itemEffectId)) {
			GameLogger.Log ("找不到ItemEffectData:" + itemEffectId);
			return null;
		}
		return data [itemEffectId];
	}
}
