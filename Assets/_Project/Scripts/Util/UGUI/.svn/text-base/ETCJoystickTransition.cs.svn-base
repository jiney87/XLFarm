using UnityEngine;
using System.Collections.Generic;

public class ETCJoystickTransition : MonoBehaviour {

	public delegate void OnMoveStartDelegate();
	public delegate void OnMoveDelegate(Vector2 delta);
	public delegate void OnMoveEndDelegate();

	public List<OnMoveStartDelegate> moveStartList=new List<OnMoveStartDelegate>();
	public List<OnMoveDelegate> moveList=new List<OnMoveDelegate>();
	public List<OnMoveEndDelegate> moveEndList=new List<OnMoveEndDelegate>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMoveStart()
	{
		for (int k = 0; k < moveStartList.Count; k++) {
			moveStartList [k] ();
		}
	}

	public void OnMove(Vector2 delta)
	{
		for (int k = 0; k < moveList.Count; k++) {
			moveList [k] (delta);
		}
	}

	public void OnMoveEnd()
	{
		for (int k = 0; k < moveEndList.Count; k++) {
			moveEndList [k] ();
		}
	}
}
