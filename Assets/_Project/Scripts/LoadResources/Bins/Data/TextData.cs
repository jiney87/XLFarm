using UnityEngine;
using System.Collections.Generic;

public class TextData : PropertyReader {
	
	public int id{get;set;}
	public string chinese{get;set;}
	
	private static Dictionary<int,TextData> data=new Dictionary<int, TextData>();
	
	public void addData()
	{
		data.Add(id,this);
	}
	public void resetData(){
		data.Clear();
	}
	public void parse(string[] ss){}
	
	public static TextData getData(int id)
	{
		return data [id];
	}

    public static string getText(int id)
    {
		return data [id].chinese;
    }
	
}
