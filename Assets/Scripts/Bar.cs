using UnityEngine;
using System.Collections;

public class Bar : MonoBehaviour 
{
	public int MaxTones = 3;
	public double Frequency;
	public SignaleType Signale;
    public bool Completed = true;
    public float ToneOffset = 0.1f;

	//public AudioSource[] Tones;
	public Hotspot[] Hotspots;
	
	// Use this for initialization
	void Start () {
	}

	IEnumerator PlayDelayed()
	{
		foreach(var tone in Hotspots)
		{
			if(tone == null) continue;
			yield return new WaitForSeconds(ToneOffset);
			tone.Frequency = Frequency;
			tone.Signale = Signale;
			tone.Play();
		}
	}
    
    public void Update()
    {
	    Completed = true;
        foreach(var tone in Hotspots)
		{
            Completed &= tone.Valid;
        }
    }
	
	public void Play() 
	{
        if(Completed) PlayAll();
        else StartCoroutine(PlayDelayed());
	}
    
    private void PlayAll()
    {
        foreach(var tone in Hotspots)
		{
			if(tone == null) continue;
			tone.Frequency = Frequency;
			tone.Signale = Signale;
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
