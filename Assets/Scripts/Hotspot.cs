using UnityEngine;
using System.Collections.Generic;

public class Hotspot : MonoBehaviour 
{
	public GameObject Tone;
    [HideInInspector]
	public double Frequency;
    [HideInInspector]
	public SignaleType Signale;
    	private Tone tone;
	private ToneGenerator toneGenerator;
	private AudioSource toneSource;
    [HideInInspector]
    	public bool Valid;
        [HideInInspector]
        public bool Completed;
	
	// Use this for initialization
	void Start () {
	}

	void Update()
	{
		if(toneSource == null) return;
        var dist = (Vector2.Distance(Tone.transform.position, transform.position))*10;
		Debug.DrawLine(transform.position,Tone.transform.position,Color.green);
        if(dist < 3) dist = 0;
        Valid = tone != null;
        Completed = dist == 0 && !tone.Dragging;
		Debug.Log(dist);
		toneGenerator.Frequency = toneGenerator.BaseFrequency + (dist * dist);
	}
	
	public void Play(float start = 0) 
	{
        if(toneSource == null) return;
		if(tone.BlockStop) return;

		toneSource.Play();
		tone.Play();
	}

	public void Stop()
	{
        	if(toneSource == null) return;
		if(tone.BlockStop) return;

		toneSource.Stop();
	}
    
    public void SetGreen()
    {
        if(tone == null) return;
        tone.SetGreen();
    }
    
    public void SetWhite()
    {
        if(tone == null) return;
        tone.SetWhite();
    }

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.layer == (int)LayerID.Tone && other.gameObject == Tone)
		{
			toneSource = other.gameObject.GetComponent<AudioSource>();
            		tone = other.gameObject.GetComponent<Tone>();
			toneGenerator = other.gameObject.GetComponent<ToneGenerator>();
			toneGenerator.SetFrequency(Frequency);
			toneGenerator.Type = Signale;
		}
    }

	void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.layer == (int)LayerID.Tone && other.gameObject == Tone)
		{
			//toneSource.Stop();
            toneGenerator.Frequency = Game.DefaultFrequence;
			toneGenerator.Type = Game.DefaultSignal;
            tone = null;
			toneSource = null;
			toneGenerator = null;


		}
    	}
}
