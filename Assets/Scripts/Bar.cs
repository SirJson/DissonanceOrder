using UnityEngine;
using System.Collections;

public enum ToneValue
{
    C4,
    Cis4,
    D4,
    Dis4,
    E4,
    F4,
    Fis4,
    G4,
    Gis4,
    A4,
    Ais4,
    B4,
    
    C5,
    Cis5,
    D5,
    Dis5,
    E5,
    F5,
    Fis5,
    G5,
    Gis5,
    A5,
    Ais5,
    B5
}

public static class ToneValueStorage
{
    public static double[] data = {
        261.63,
        277.18,
        293.66,
        311.13,
        329.63,
        349.23,
        369.99,
        392.00,
        415.30,
        440,
        466.16,
        493.88,
        
        523.25,
        554.37,
        587.33,
        622.25,
        659.25,
        698.46,
        739.99,
        783.99,
        830.61,
        880.00,
        932.33,
        987.77
    };
}

public class Bar : MonoBehaviour 
{
	public int MaxTones = 3;
    [HideInInspector]
    public ToneValue Tone;
	public SignaleType Signale;
    public bool Valid = true;
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
			tone.Frequency = ToneValueStorage.data[(int)Tone];
			tone.Signale = Signale;
			tone.Play();
		}
	}
    
    public void Update()
    {
	    Valid = true;
        Completed = true;
        
        foreach(var tone in Hotspots)
		{
            Valid &= tone.Valid;
            Completed &= tone.Completed;
        }
        
        foreach(var tone in Hotspots)
        {
            if(Completed) tone.SetGreen(); else tone.SetWhite();
        }
    }
	
	public void Play() 
	{
        if(Valid) PlayAll();
        else StartCoroutine(PlayDelayed());
	}
    
    private void PlayAll()
    {
        foreach(var tone in Hotspots)
		{
			if(tone == null) continue;
			tone.Frequency = ToneValueStorage.data[(int)Tone];
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
