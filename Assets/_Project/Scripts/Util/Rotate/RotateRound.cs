using UnityEngine;
using System.Collections;

public class RotateRound : MonoBehaviour {

	public GameObject middleGo;

	public Vector2 direction
	{
		get{
			return (transform.position-middleGo.transform.position).normalized;
		}
	}

	private float rotateSpeed=1f;
	private bool canRotate;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(canRotate)
		{
			transform.RotateAround(middleGo.transform.position,new Vector3(0,0,1),rotateSpeed*Time.deltaTime);

		}
	}

	public void init(float originAngle,float rotateSpeed)
	{
		originAngle=originAngle%360f;
		transform.RotateAround(middleGo.transform.position,new Vector3(0,0,1),originAngle);
		this.rotateSpeed=rotateSpeed;
		canRotate=true;
	}
}
