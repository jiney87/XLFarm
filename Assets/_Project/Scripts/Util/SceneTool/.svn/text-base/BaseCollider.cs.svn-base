using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider),typeof(Rigidbody))]
public abstract class BaseCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected abstract void touchTarget (GameObject target);


	void OnCollisionEnter(Collision collision)
	{
		touchTarget(collision.gameObject);
	}

	void OnCollisionStay(Collision collision)
	{
		OnCollisionEnter(collision);
	}
	void OnCollisionExit(Collision collision)
	{
		OnCollisionEnter(collision);
	}
	void OnTriggerEnter(Collider other){}//当进入触发器
	void OnTriggerExit(Collider other){}//当退出触发器
	void OnTriggerStay(Collider other){}//当逗留触发器
}
