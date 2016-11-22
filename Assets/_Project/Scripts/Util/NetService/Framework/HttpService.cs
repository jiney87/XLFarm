﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Unity_lt_net
{

	public class HttpService : MonoBehaviour{

		void Awake()
		{
			DontDestroyOnLoad (gameObject);
			hideFlags = HideFlags.NotEditable;
		}

		public IEnumerator httpRequest(string url, Dictionary<string, string> parameters,HttpReturn httpReturn)
	    {
			WWWForm form = new WWWForm ();
			if (parameters != null && parameters.Count>0)
			{
				foreach (KeyValuePair<string, string> kv in parameters)
				{
					form.AddField (kv.Key, kv.Value);
				}
			}

			//请求
			WWW www = new WWW(url,form.data);
			yield return www;

			HttpErrorCode errorCode = HttpErrorCode.Success;
			string response = "";
			if (www.error != null)
			{
				errorCode = HttpErrorCode.Error;
				response = www.error;
			}
			else
			{
				errorCode = HttpErrorCode.Success;
				response = www.text;
			}

			httpReturn.setData (errorCode, response);
	    }
	}

	public class HttpReturn
	{
		public HttpErrorCode code;
		public string response;

		public void setData(HttpErrorCode code,string response)
		{
			this.code = code;
			this.response = response;
		}

		public static HttpReturn create()
		{
			HttpReturn httpReturn = new HttpReturn ();
			httpReturn.code = HttpErrorCode.Create;
			return httpReturn;
		}
	}

	public enum HttpErrorCode:int
	{
		Create=0,
		Success,
		TimeOut,
		Error,
	}

}