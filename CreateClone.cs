using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

public class CreateClone : MonoBehaviour {

	public GameObject prefab;
	
	void OnMouseDown() 
	{
		Instantiate(prefab, Vector3.zero, Quaternion.identity);
	}
}
