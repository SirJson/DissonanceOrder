using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NextLevelButton : MonoBehaviour 
{
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPress()
	{
		var nextLevel = Function.GetClassFromScene<LevelSettings>().NextLevel;
		SceneManager.LoadScene(nextLevel);	
	}
}
