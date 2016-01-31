using UnityEngine;
using System.Collections;

public class DistanceLogger : MonoBehaviour 
{
    public Transform a,b;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	   Debug.Log(Vector2.Distance(a.position,b.position));
	}
}
