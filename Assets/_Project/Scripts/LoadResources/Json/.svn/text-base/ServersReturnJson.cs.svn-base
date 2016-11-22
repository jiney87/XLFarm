using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class ServersReturnJson {

	public List<ServerInfo> servers;

	public bool validate()
	{
		return servers != null && servers.Count > 0;
	}
}

[Serializable]
public class ServerInfo
{
	public int id;
	public string serverName;
	public string ip;
	public int port;
	public string des;
	public int status;
}