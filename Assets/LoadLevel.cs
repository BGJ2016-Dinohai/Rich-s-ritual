using UnityEngine;
using System.Collections;
using System;

public class LoadLevel : MonoBehaviour {
	public LevelLogic levelController;
	public PlayerMan playerController;

	public TextAsset levelDescription;

	public Transform wallTile;
	public Transform floorTile;
	public Transform lavaTile;
	public Transform doorTile;
	public Transform goalTile;
	public Transform startTile;
	public Transform keyTile;
	public Transform noTile;
	public float gridSize = 0.65f;

	public GameObject player;

	public LevelLogic GetLevelController()
	{
		return levelController;
	}

	// Use this for initialization    
	void Start ()
	{
		if (null == levelController)
		{
			levelController = new LevelLogic();
			levelController.createEmptyLevel(16, 16);
		}
		playerController = GetComponent<PlayerMan>();

		//Debug.Log(Application.dataPath);


		/* Windows-proofing the text. We assume no OS9 or earlier. *crosses fingers* */
		string csvText = levelDescription.text.Replace("\r", "");

		var lineNumber = 0;
		var columnNumber = 0;
		foreach (string line in csvText.Split('\n'))
		{
		columnNumber = 0;
		foreach (string column in line.Split(','))
		{
		addTile(columnNumber, lineNumber, column);
		++columnNumber;
		}
		++lineNumber;
		}
		GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(columnNumber / 2 * gridSize, -lineNumber / 2 * gridSize, -2);
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().orthographicSize = Math.Max((float)columnNumber / 2 * gridSize / 16 * 9, (float)lineNumber / 2 * gridSize);
	}

	private void addTile(int xPosition, int yPosition, string tileName)
	{
		var tile = LookUpTile(tileName);
		Vector3 vector = new Vector3(xPosition * gridSize, -yPosition * gridSize, 0);
		if(tile == startTile)
	{
		vector.z = -1;
		playerController.instantiatePlayer(vector, xPosition, yPosition);            
	}
	if (tile != noTile)
	{
		Instantiate(tile, vector, Quaternion.identity);
	}
		levelController.setTile(xPosition, yPosition, tileName);
		//Debug.Log(tile.GetComponent<Renderer>().bounds.size.x);
	}

	private Transform LookUpTile(string tileName)
	{
		Transform retVal;
		if(tileName.Equals("s"))
		{
			retVal = startTile;
		}    
			else if(tileName.Equals("m"))
		{
			retVal = goalTile;
		}    
		else if(tileName.Equals("n"))
		{
			retVal = keyTile;
		}    
		else if(tileName.Equals("d"))
		{
			retVal = doorTile;
		}    
		else if(tileName.Equals("v"))
		{
			retVal = wallTile;
		}    
		else if(tileName.Equals("l"))
		{
			retVal = lavaTile;
		}    
		else if(tileName.Equals("g"))
		{
			retVal = floorTile;
		}    
		else if(tileName.Equals("0"))
		{
			retVal = noTile;
		}    
		else if(tileName.Equals(""))
		{
			retVal = floorTile;
		}    
		else
		{
			throw new Exception(string.Format("tile specifier {0} {1} is not valid", tileName, Convert.ToInt32(tileName.ToCharArray()[0])));
		}

		return retVal;
	}

	// Update is called once per frame
	void Update ()
	{

	}
}
