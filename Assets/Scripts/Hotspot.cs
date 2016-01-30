using UnityEngine;
using System.Collections.Generic;

public class Hotspot : MonoBehaviour 
{
	public GameObject Tone;
	private ToneGenerator toneGenerator;
	private AudioSource toneSource;
	
	// Use this for initialization
	void Start () {
	}

	void Update()
	{
		if(toneSource == null) return;
        	var dist = (Vector2.Distance(Tone.transform.position, transform.position) - 0.1f)*10;
		Debug.DrawLine(transform.position,Tone.transform.position,Color.green);
        	if(dist < 0) dist = 0;
		Debug.Log(dist);
		toneGenerator.Frequency = toneGenerator.BaseFrequency + (dist * dist);
	}
	
	public void Play() 
	{
        	if(toneSource == null) return;

		toneSource.Play();
	}

	public void Stop()
	{
        	if(toneSource == null) return;

		toneSource.Stop();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.layer == (int)LayerID.Tone && other.gameObject == Tone)
		{
			toneSource = other.gameObject.GetComponent<AudioSource>();
			toneGenerator = other.gameObject.GetComponent<ToneGenerator>();
		}
    }

	void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.layer == (int)LayerID.Tone && other.gameObject == Tone)
		{
			toneSource.Stop();
			toneSource = null;
			toneGenerator = null;
		}
    	}
}
