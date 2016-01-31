using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour 
{
	public GameObject NextLevelButton;
    public bool HideHotspots;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowNextLevelButton()
	{
		NextLevelButton.SetActive(true);	
	}

	public void HideNextLevelButton()
	{
		NextLevelButton.SetActive(false);	
	}
}
