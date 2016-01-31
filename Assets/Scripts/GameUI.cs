using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour 
{
	public GameObject NextLevelButton;
    public bool HideHotspots;
    // The assumed standard radius of a hotspot, used to normalize the frequency
    // change for all hotspots in the szene, so the drop of doesn't depend on 
    // scaling.
    public float normalizeRadius = 1f;

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
