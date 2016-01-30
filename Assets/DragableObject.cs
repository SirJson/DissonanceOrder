using UnityEngine;
using System.Collections;

public delegate void DragStart();
public delegate void DragStop();

public class DragableObject : MonoBehaviour 
{
    private float distance;
    private bool dragging;
    public DragStart OnDragStart;
    public DragStop OnDragStop; 

    public bool Dragging
    {
	    get { return dragging; }
    }
	
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
        OnDragStart();
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
    }
    
    void OnMouseUp()
    {
        OnDragStop();
        dragging = false;
    }
}
