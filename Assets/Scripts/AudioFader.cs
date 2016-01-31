using UnityEngine;
using System.Collections;

public class AudioFader : MonoBehaviour 
{
	private AudioSource src;

	// Use this for initialization
	void Start () 
	{
		src = GetComponent<AudioSource>();
	}
	
	// Upte is called once per frame
	void Update () {
	
	}

	private IEnumerator DoFade(float speed)
	{
		while(src.volume > 0)
		{
			src.volume -= speed;
			yield return new WaitForFixedUpdate();
		}
	}

	public void Fade(float speed)
	{
		StartCoroutine(DoFade(speed));
	}
}
