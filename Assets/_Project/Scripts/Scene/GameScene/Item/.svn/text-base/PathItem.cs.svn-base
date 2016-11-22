using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class PathItem : MonoBehaviour {

	public long itemUniqueId;
	public int itemId;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void init(Message_AddItem msg)
	{
		transform.localPosition = new Vector3 (msg.x, 0, msg.z);
		itemUniqueId = msg.itemUniqueId;
		itemId = msg.itemId;
	}


}
