using UnityEngine;
using System.Collections.Generic;

public class SkillData : PropertyReader {

	public int id{ get; set; }
	public string name{ get; set; }
	public int gold{ get; set; }
	public float cooldown{ get; set; }
	public string icon{ get; set; }
	public string skillPrefabPath{ get; set; }
	public string skillEffect{ get; set; }
	public string spawnPrefabPath{ get; set; }
	public int effect{ get; set; }
	public string des{ get; set; }

	
	private static Dictionary<int,SkillData> data = new Dictionary<int, SkillData> ();
	
	public void addData()
	{
		data.Add(id,this);
	}
	public void resetData(){
		data.Clear();
	}
	public void parse(string[] ss){}
	
	public static SkillData getData(int id)
	{
		return data[id];
	}
    public static int getDataLength()
    {
        return  data.Count;
    }
	
}
