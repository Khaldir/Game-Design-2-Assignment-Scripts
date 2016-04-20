using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(BoxCollider2D))]

public class DragDrop : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
	public bool tall;

	void OnMouseDown() 
	{
    offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
	}
	
	void OnMouseUp()
	{
		Vector3 GridPosition = new Vector3(0.0f, 0.0f, 0.0f);
		GridPosition[0] = Convert.ToSingle(RoundToHalf(transform.position[0]));
		if (tall)
		{
			GridPosition[1] = Convert.ToSingle(RoundToHalf(transform.position[1]))-0.5f;
		}
		else GridPosition[1] = Convert.ToSingle(RoundToHalf(transform.position[1]));
		GridPosition[2] = Convert.ToSingle(RoundToHalf(transform.position[2]));
		transform.position = GridPosition;
		TestRoute.MapObject(gameObject);
		TestRoute.FindCoordinates(gameObject);
	}
	
	double RoundToHalf(float number)
	{
		double modifiedNumber = Convert.ToDouble(number) -0.5;
		return(Math.Round(modifiedNumber,0)+0.5);
	}
}

 
