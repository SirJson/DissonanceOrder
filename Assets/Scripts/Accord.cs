using UnityEngine;
using System.Collections;

public class Accord : MonoBehaviour 
{
	public AudioSource srcA, srcB, srcC;
	public AudioSource srcD, srcE, srcF;
	
	private bool active = false;
	private float timer = 0.5f;
	
	// Use this for initialization
	void Start () {
		srcA.Play();
		srcB.Play();
		srcC.Play();
		
		srcD.Play();
		srcE.Play();
		srcF.Play();
	}
	
	public void GroupA()
	{
		srcA.Play();
		srcB.Play();
		srcC.Play();
		
		srcD.Stop();
		srcE.Stop();
		srcF.Stop();
	}
	
	public void GroupB()
	{
		srcD.Play();
		srcE.Play();
		srcF.Play();
		
		srcA.Stop();
		srcB.Stop();
		srcC.Stop();
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer <= 0) {
			active = !active;
			if(active) GroupA();
			else GroupB();
			timer = 0.5f;
		}
	}
}
