using UnityEngine;
using System.Collections;

public class DragableObject : MonoBehaviour 
{
    private float distance;
    private bool dragging;
	
	// Update is called once per frame
	void Update () 
    {
	   if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = new Vector2(rayPoint.x,rayPoint.y);
        }
	}
    
    void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
    }
    
    void OnMouseUp()
    {
        dragging = false;
    }
}
