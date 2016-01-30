using UnityEngine;
using System;  // Needed for Math

public enum SignaleType 
{
	Sin,
	Square,
	Sawtooth,
	Triangle,
	Pulse
}

public class ToneGenerator : MonoBehaviour
{
	// un-optimized version
	public double Frequency = 440;
	public double BaseFrequency;
	public int Note = 0;
	public double Gain = 0.05;
	public bool Noise = false;
	public SignaleType Type;

	private double phase;
	private double sampling_frequency = 48000;
	private double blah = 0;
	private System.Random random;
	private float test;

	void Start()
	{
		random = new System.Random();
		Frequency = Frequency * Math.Pow(Math.Pow(2.0,1.0/12.0),Note);
		BaseFrequency = Frequency;
		sampling_frequency = AudioSettings.outputSampleRate;
	}

	void Update()
	{
	}

	void GenerateSin(ref float[] data, int channels, double t)
	{
		for (var i = 0; i < data.Length; i = i + channels)
		{
			phase = phase + t;
			data[i] = (float)(Gain*Math.Sin(2.0*Math.PI*phase));

			if (channels == 2) data[i + 1] = data[i];
			if (phase > 32768 * Math.PI) phase = 0;
		}
	}

	void GenerateSquare(ref float[] data, int channels, double t)
	{
		for (var i = 0; i < data.Length; i = i + channels)
		{
			phase = phase + t;
			data[i] = (float)(Gain*Math.Sign(Math.Sin(2.0*Math.PI*phase)));

			if (channels == 2) data[i + 1] = data[i];
			if (phase > 32768 * Math.PI) phase = 0;
		}
	}

	void GenerateSawtooth(ref float[] data, int channels, double t)
	{
		for (var i = 0; i < data.Length; i = i + channels)
		{
			phase = phase + t;
			data[i] = (float)(Gain*2f*(phase-Math.Floor(phase+0.5)));

			if (channels == 2) data[i + 1] = data[i];
			if (phase > 32768 * Math.PI) phase = 0;
		}
	}

	void GenerateTriangle(ref float[] data, int channels, double t)
	{
		for (var i = 0; i < data.Length; i = i + channels)
		{
			phase = phase + t;
			data[i] = (float)(Gain*(1-4*Math.Abs(Math.Round(phase-0.25)-(phase-0.25))));

			if (channels == 2) data[i + 1] = data[i];
			if (phase > 32768 * Math.PI) phase = 0;
		}
	}

	void GeneratePulse(ref float[] data, int channels, double t)
	{
		for (var i = 0; i < data.Length; i = i + channels)
		{
			phase = phase + t;
			data[i] = (float)(Gain*((Math.Abs(Math.Sin(2*Math.PI*phase)) < 1.0 - 10E-3) ? (0) : (1)));

			if (channels == 2) data[i + 1] = data[i];
			if (phase > 32768 * Math.PI) phase = 0;
		}
	}


	void OnAudioFilterRead(float[] data, int channels)
	{
		var t = Frequency / sampling_frequency;

		switch(Type) {
			case SignaleType.Square:
				GenerateSquare(ref data, channels, t);
			break;
			case SignaleType.Sin:
				GenerateSin(ref data, channels, t);
			break;
			case SignaleType.Sawtooth:
				GenerateSawtooth(ref data, channels, t);
			break;
			case SignaleType.Triangle:
				GenerateTriangle(ref data, channels, t);
			break;
			case SignaleType.Pulse:
				GeneratePulse(ref data, channels, t);
			break;
		}
	}
} 
