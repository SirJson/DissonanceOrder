using UnityEngine;
using System.Collections;

public class Tracker : MonoBehaviour 
{
	public Bar[] Bars;
	public GameUI UI;
	public float TimePerAccord = 0.5f;
	public float PauseBetweenAccords = 0.1f;

	private float timer;
	private int accordIdx = 0;

	// Use this for initialization
	void Start () {
		timer = TimePerAccord+PauseBetweenAccords;
		PlayNextAccord();
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer -= Time.deltaTime;
		if(timer <= PauseBetweenAccords) StopAccord();
		if(timer <= 0)
		{
			PlayNextAccord();
			timer = TimePerAccord+PauseBetweenAccords;
		}	

		var gameCompleted = Bars.Length >= 1;
		foreach(var bar in Bars)
		{
			gameCompleted &= bar.Completed;
		}

		if (gameCompleted) {
			UI.ShowNextLevelButton ();
		} else
			UI.HideNextLevelButton ();
	}

	void PlayNextAccord()
	{
		StopAccord();
		Bars[accordIdx].Play();
		accordIdx++;
		if(accordIdx >= Bars.Length) accordIdx = 0;
	}

	void StopAccord()
	{
		var prevAccordIdx = accordIdx - 1;
		if(prevAccordIdx <= -1) prevAccordIdx = Bars.Length - 1;
		Bars[prevAccordIdx].Stop();
	}

}
