﻿using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class Tone : MonoBehaviour 
{
	public bool BlockStop { get; private set; }
	public AudioMixerGroup MasterGroup, HighlightedGroup;
    private DragableObject obj;
    
	// Use this for initialization
	void Start () {
	   obj = GetComponent<DragableObject>();
       	obj.OnDragStart += OnDragStart;
       	obj.OnDragStop += OnDragStop;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnDragStart()
    {
		var src = GetComponent<AudioSource> ();
		src.outputAudioMixerGroup.audioMixer.SetFloat ("ToneVolume", -20f);
		src.outputAudioMixerGroup = HighlightedGroup;
		src.Play();

	BlockStop = true;
    }
    
    void OnDragStop()
    {
		var src = GetComponent<AudioSource> ();
		src.outputAudioMixerGroup.audioMixer.SetFloat ("ToneVolume", 0.0f);
		src.outputAudioMixerGroup = MasterGroup;
        src.Stop();
	BlockStop = false;
    }

	public void Play()
	{
		iTween.PunchScale(gameObject, Vector3.one, 1.0f);
		
	}
}

