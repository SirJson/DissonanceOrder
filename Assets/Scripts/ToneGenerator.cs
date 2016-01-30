using UnityEngine;
using System;  // Needed for Math

public class ToneGenerator : MonoBehaviour
{
	// un-optimized version
	public double Frequency = 440;
	public int Note = 0;
	public double Gain = 0.05;

	private double increment;
	private double phase;
	private double sampling_frequency = 48000;

	void Start()
	{
		Frequency = Frequency * Math.Pow(Math.Pow(2.0,1.0/12.0),Note);	
	}

	void GenerateSin(ref float[] data, int channels)
	{
		// update increment in case frequency has changed
		increment = Frequency * 2.0 * Math.PI / sampling_frequency;
		for (var i = 0; i < data.Length; i = i + channels)
		{
			phase = phase + increment;
			// this is where we copy audio data to make them “available” to Unity
			data[i] = (float)(Gain*Math.Sin(phase));
			// if we have stereo, we copy the mono data to each channel
			if (channels == 2) data[i + 1] = data[i];
			if (phase > 2 * Math.PI) phase = 0;
		}
	}

	void OnAudioFilterRead(float[] data, int channels)
	{
		GenerateSin(ref data,channels);
	}
} 
