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
	public float Tolerance = 3.0f;
	public float StepScalar = 0.9f;
    [HideInInspector]
    public SpriteRenderer HotspotIndicator;

	// Use this for initialization
	void Start () {
        HotspotIndicator = this.GetComponent<SpriteRenderer>();
        bool hideHotspots = GameObject.Find("GameUI").GetComponent<GameUI>().HideHotspots;
        HotspotIndicator.enabled = !hideHotspots;
	}

	void Update()
	{
		if (toneSource == null) {
			Tone.GetComponent<AudioSource> ().volume = 1.0f - (1.0f / 23.0f * Vector2.Distance (transform.position, Tone.transform.position));
			return;
		}

		var dist = (Vector2.Distance(Tone.transform.position, transform.position))*10;
		Debug.DrawLine(transform.position,Tone.transform.position,Color.green);
		if(dist < Tolerance) dist = 0;
		var step = (dist > 0) ? toneGenerator.BaseFrequency * StepScalar : 0;
		Valid = tone != null;
		Completed = dist == 0 && !tone.Dragging;
		toneGenerator.Frequency = toneGenerator.BaseFrequency + (dist * dist) + step;
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
			toneSource.volume = 1.0f;
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
			toneSource.volume = 1.0f;
			toneGenerator.SetFrequency(Game.DefaultFrequence);
			toneGenerator.Type = Game.DefaultSignal;
			tone = null;
			toneSource = null;
			toneGenerator = null;


		}
	}
}
