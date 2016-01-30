using UnityEngine;
using System.Collections.Generic;

public class Bar : MonoBehaviour 
{
	public int MaxTones = 3;
	//public AudioSource[] Tones;
	public Hotspot[] Hotspots;
	
	// Use this for initialization
	void Start () {
	}
	
	public void Play() 
	{
		foreach(var tone in Hotspots)
		{
			if(tone == null) continue;
			tone.Play();
		}
	}

	public void Stop()
	{
		foreach(var tone in Hotspots)
		{
			if(tone == null) continue;
			tone.Stop();
		}
	}
}
