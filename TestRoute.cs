using UnityEngine;
using System.Collections;
using System;

public class TestRoute : MonoBehaviour {

	public static GameObject[,] GameLayout = new GameObject[11,8];
	public int[] CoOrdinates;
	public GameObject tStart1;
	public GameObject tEnd1;
	public GameObject tStart2;
	public GameObject tEnd2;
	public GameObject fStart1;
	public GameObject fEnd1;
	public GameObject fStart2;
	public GameObject fEnd2;
	public GameObject[] StartPipes;
	public bool notAtEnd = true;
	public int Direction = 1;
	public bool currentPipe = true;
	
	// Use this for initialization
	void Start () {
		tStart1 = GameObject.Find("TRUESTART (0)");
		MapObject(tStart1);
		tEnd1 = GameObject.Find("TRUEEND (0)");
		MapObject(tEnd1);
		tStart2 = GameObject.Find("TRUESTART (1)");
		MapObject(tStart2);
		fStart1 = GameObject.Find("FALSESTART (0)");
		MapObject(fStart1);
		fEnd1 = GameObject.Find("FALSEEND (0)");
		MapObject(fEnd1);
		fStart2 = GameObject.Find("FALSESTART (1)");
		MapObject(fStart2);
		fEnd2 = GameObject.Find("FALSEEND (1)");
		MapObject(fEnd2);
		StartPipes = new GameObject[4];
		StartPipes[0] = tStart1;
		StartPipes[1] = tStart2;
		StartPipes[2] = fStart1;
		StartPipes[3] = fStart2;
	}
	
	void OnMouseDown()
	{
		foreach (GameObject StartPipe in StartPipes)
		{
			Debug.Log("Current Check");
			Debug.Log(StartPipe.name);
			CoOrdinates = FindCoordinates(StartPipe);
			string sub = StartPipe.name.Substring(0, 2);
			currentPipe = (sub == "TR");		
			Direction = 1;
			FromStartPipe();
		}
	}
	
	void FromStartPipe()
	{
		string Output;
		StartPipe();
		do
		{
			try
			{
				Output = GameLayout[CoOrdinates[0],CoOrdinates[1]].name + "(" + CoOrdinates[0] + "," + CoOrdinates[1] + ")";
				Debug.Log(Output);
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
	
	public static int [] FindCoordinates(GameObject Object)
	{
		int x = Convert.ToInt32(Math.Floor(Object.transform.position.x));
		int y = -1 - Convert.ToInt32(Math.Floor(Object.transform.position.y));
		int[] CoOrds = new int[] {x,y};
		string Output = x + "," + y;
		Debug.Log(Output);
		return (CoOrds);
	}
	
	void idObject(string ObjName)
	{
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
					break;
				}
				else
				{
					notAtEnd = false; 
					Debug.Log("Fail");
					break;
				}	
			default: notAtEnd = false; break;
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
