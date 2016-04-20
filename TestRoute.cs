using UnityEngine;
using System.Collections;
using System;

public class TestRoute : MonoBehaviour {

	public static GameObject[,] GameLayout = new GameObject[11,8];
	public int[] CoOrdinates;
	public GameObject[] StartPipes;
	public bool notAtEnd = true;
	public int Direction = 1;
	public bool currentPipe = true;
	public int score = 0;
	
	// Use this for initialization
	void Start () {
		StartPipes = new GameObject[4];
		StartPipes[0] =  GameObject.Find("TRUESTART (0)");
		MapObject(StartPipes[0]);
		MapObject(GameObject.Find("TRUEEND (0)"));
		StartPipes[1] = GameObject.Find("TRUESTART (1)");
		MapObject(StartPipes[1]);
		StartPipes[2] = GameObject.Find("FALSESTART (0)");
		MapObject(StartPipes[2]);
		MapObject(GameObject.Find("FALSEEND (0)"));
		StartPipes[3] = GameObject.Find("FALSESTART (1)");
		MapObject(StartPipes[3]);
		MapObject(GameObject.Find("FALSEEND (1)"));
		
	}
	
	void OnMouseDown()
	{
		foreach (GameObject StartPipe in StartPipes)
		{
			CoOrdinates = FindCoordinates(StartPipe);
			Debug.Log("New Start Check from " + StartPipe.name + " at " + CoOrdinates[0] + "," + CoOrdinates[1]);
			string sub = StartPipe.name.Substring(0, 2);
			currentPipe = (sub == "TR");		
			Direction = 1;
			notAtEnd = true;
			FromStartPipe();
		}
		if (score == 3)
		{
			Debug.Log("Success!");
		}
		else Debug.Log("Try Again");
	}
	
	void FromStartPipe()
	{
		StartPipe();
		do
		{
			try
			{
				idObject(GameLayout[CoOrdinates[0],CoOrdinates[1]].name);
			}
			catch
			{
				notAtEnd = false;
				Debug.Log("No Pipe Found");
			}
		}		
		while (notAtEnd);
	}
	
	public static void MapObject(GameObject Object)
	{
		int x = Convert.ToInt32(Math.Floor(Object.transform.position.x));
		int y = -1 - Convert.ToInt32(Math.Floor(Object.transform.position.y));
		GameLayout[x,y] = Object;
	}
	
	public static void MapObject(GameObject Object, bool tall)
	{
		int x = Convert.ToInt32(Math.Floor(Object.transform.position.x));
		int y = -1 - Convert.ToInt32(Math.Floor(Object.transform.position.y));
		if (tall)
		{
			y++;
		}
		GameLayout[x,y] = Object;		
	}
	
	public static int [] FindCoordinates(GameObject Object)
	{
		int x = Convert.ToInt32(Math.Floor(Object.transform.position.x));
		int y = -1 - Convert.ToInt32(Math.Floor(Object.transform.position.y));
		int[] CoOrds = new int[] {x,y};
		return (CoOrds);
	}
	
	void idObject(string ObjName)
	{
		Debug.Log("New Move to " + ObjName + " at " + CoOrdinates[0] + "," + CoOrdinates[1]);
		string sub = ObjName.Substring(0, 2);
		switch (sub)
		{
			case "LR": LeftRight(); break;
			case "UD": UpDown(); break;
			case "LU": LeftUp(); break;
			case "LD": LeftDown(); break;
			case "UR": UpRight(); break;
			case "DR": DownRight(); break;
			case "NO": currentPipe = !currentPipe; LeftRight(); break;
			case "TR": 
				if (currentPipe)
				{
					notAtEnd = false; 
					Debug.Log("Win");
					score++;
					break;	
				}
				else
				{
					notAtEnd = false; 
					Debug.Log("Fail");
					break;
				}
			case "FA": 
				if (!currentPipe)
				{
					notAtEnd = false; 
					Debug.Log("Win");
					score++;
					break;
				}
				else
				{
					notAtEnd = false; 
					Debug.Log("Fail");
					break;
				}
			case "AN":
				{ 
					And(); 
					break;
				}
			case "OR":
				{ 
					Or(); 
					break;
				}
			default: notAtEnd = false; break;
		}
	}
	
	void And()
	{
		//Check for other link
		if (!GameLayout[CoOrdinates[0], CoOrdinates[1]].GetComponent<gateStatus>().visited)
		{
			Debug.Log("AND Not Yet Visited, end source");
			GameLayout[CoOrdinates[0], CoOrdinates[1]].GetComponent<gateStatus>().status = currentPipe;
			GameLayout[CoOrdinates[0], CoOrdinates[1]].GetComponent<gateStatus>().visited = true;
			notAtEnd = false;
			return;
		}
		else GameLayout[CoOrdinates[0], CoOrdinates[1]].GetComponent<gateStatus>().status = (currentPipe && GameLayout[CoOrdinates[0], CoOrdinates[0]].GetComponent<gateStatus>().status);
				
		//Find out if pipe enters top or bottom
		
		if (FindCoordinates(GameLayout[CoOrdinates[0], CoOrdinates[1]]) == CoOrdinates)	//Current CoOrds = Top Of And
		{
			Debug.Log("Entered the top block of AND");
			LeftDown();
			UpRight();
			Debug.Log("Exiting AND at " + CoOrdinates[0] + "," + CoOrdinates[1]);
		}
		else 			//Other half is above
		{
			Debug.Log("Entered the bottom block of AND");
			LeftRight();
		}
	}
	
	void Or()
	{
		//Check for other link
		if (!GameLayout[CoOrdinates[0], CoOrdinates[1]].GetComponent<gateStatus>().visited)
		{
			Debug.Log("OR Not Yet Visited, end source");
			GameLayout[CoOrdinates[0], CoOrdinates[1]].GetComponent<gateStatus>().status = currentPipe;
			GameLayout[CoOrdinates[0], CoOrdinates[1]].GetComponent<gateStatus>().visited = true;
			notAtEnd = false;
			return;
		}
		else GameLayout[CoOrdinates[0], CoOrdinates[1]].GetComponent<gateStatus>().status = (currentPipe || GameLayout[CoOrdinates[0], CoOrdinates[0]].GetComponent<gateStatus>().status);
				
		//Find out if pipe enters top or bottom
		
		if (FindCoordinates(GameLayout[CoOrdinates[0], CoOrdinates[1]]) == CoOrdinates)	//Current CoOrds = Top Of And
		{
			Debug.Log("Entered the top block of OR");
			LeftDown();
			UpRight();
			Debug.Log("Exiting OR at " + CoOrdinates[0] + "," + CoOrdinates[1]);
		}
		else 			//Other half is above
		{
			Debug.Log("Entered the bottom block of OR");
			LeftRight();
			Debug.Log("Exiting OR at " + CoOrdinates[0] + "," + CoOrdinates[1]);
		}
	}
	
	void StartPipe ()
	{
		CoOrdinates[0]++;
	}
	
	//////////////////////////////
	// Direction				//
	// 0: Coming from Below		//
	// 1: Coming from Left		//
	// 2: Coming from Above		//
	// 3: Coming from Right		//
	//////////////////////////////
		
	void LeftRight ()
	{
		if ((Direction == 1)||(Direction == 3))
		{
			if (Direction == 1)
				CoOrdinates[0]++;
			else
				CoOrdinates[0]--;
		}
		else 
		{
			notAtEnd = false;
			Debug.Log("Fail");
		}
	}
	
	void LeftUp ()
	{
		if ((Direction == 1)||(Direction == 2))
		{
			if (Direction == 1)
			{
				CoOrdinates[1]--;
				Direction = 0;
			}
			else
			{
				CoOrdinates[0]--;
				Direction = 3;
			}
		}
		else 
		{
			notAtEnd = false;
			Debug.Log("Fail");
		}
	}
	
	void LeftDown ()
	{
		if ((Direction == 1)||(Direction == 0))
		{
			if (Direction == 1)
			{
				CoOrdinates[1]++;
				Direction = 2;
			}
			else
			{
				CoOrdinates[0]--;
				Direction = 3;
			}
		}
		else 
		{
			notAtEnd = false;
			Debug.Log("Fail");
		}
	}
	
	void UpRight ()
	{
		if ((Direction == 3)||(Direction == 2))
		{
			if (Direction == 3)
			{
				CoOrdinates[1]--;
				Direction = 0;
			}
			else
			{
				CoOrdinates[0]++;
				Direction = 1;
			}
		}
		else 
		{
			notAtEnd = false;
			Debug.Log("Fail");
		}
	}
	
	void DownRight ()
	{
		if ((Direction == 0)||(Direction == 3))
		{
			if (Direction == 0)
			{
				CoOrdinates[0]++;
				Direction = 1;
			}
			else
			{
				CoOrdinates[0]++;
				Direction = 2;
			}
		}
		else 
		{
			notAtEnd = false;
			Debug.Log("Fail");
		}
	}
	
	void UpDown ()
	{
		if ((Direction == 0)||(Direction == 2))
		{
			if (Direction == 2)
				CoOrdinates[1]++;
			else
				CoOrdinates[1]--;
		}
		else 
		{
			notAtEnd = false;
			Debug.Log("Fail");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
