using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class Blur_radial : MonoBehaviour
{
	[Header("径向模糊材质")]
	public Material blur_radial_mat;
	
	public float fSampleDist;
	public float fSampleStrength;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		blur_radial_mat.SetFloat("fSampleDist",fSampleDist);
		blur_radial_mat.SetFloat("fSampleStrength",fSampleStrength);
		
		int rtW = source.width;
		int rtH = source.height;

		RenderTexture buffer = RenderTexture.GetTemporary(rtW, rtH, 0);
		Graphics.Blit(source,buffer,blur_radial_mat);
		Graphics.Blit(buffer,destination);
		RenderTexture.ReleaseTemporary(buffer);
		
		
		
		//Graphics.Blit(source,destination,blur_radial_mat);
	}
}

