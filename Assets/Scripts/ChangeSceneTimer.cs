using UnityEngine;
using System.Collections;

public class ChangeSceneTimer : MonoBehaviour {
    private float elapsedTime;

	// Use this for initialization
	void Start () {
        elapsedTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime > 2.5f)
            Application.LoadLevel("MainMenu");
	}
}
