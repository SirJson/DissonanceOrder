using UnityEngine;
using System.Collections;

public class changeScene : MonoBehaviour
{
    public string levelName;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Debug.Log(1);
        if (levelName == "Exit")
            Application.Quit();
        else if (levelName != "")
            Application.LoadLevel(levelName);
    }
}
