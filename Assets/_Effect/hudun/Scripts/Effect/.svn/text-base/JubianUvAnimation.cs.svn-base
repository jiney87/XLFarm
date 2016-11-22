using UnityEngine;
using System.Collections;

public class JubianUvAnimation : MonoBehaviour {
	public float xTiling = 1;
	public float yTiling = 1;
	public float xOffset = 0;
	public float yOffset = 0;
	public float xScrollSpeed = 1;
	public float yScrollSpeed = 1;

	private Material ownMatrial = null;

	// Use this for initialization
	void Start () {
		if (GetComponent<Renderer>() != null) ownMatrial = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		if (ownMatrial != null) {
		ownMatrial.mainTextureScale = new Vector2 (xTiling, yTiling);
		float x_offset = xOffset + Time.time * xScrollSpeed;
		float y_offset = yOffset + Time.time * yScrollSpeed;
		ownMatrial.mainTextureOffset = new Vector2 (x_offset - Mathf.Floor(x_offset), y_offset - Mathf.Floor(y_offset));
		}
	}
}
