using UnityEngine;
using System.Collections;
using System;

public class LoadLevel : MonoBehaviour {
    public LevelLogic levelController;
	public TextAsset text;
    public Transform wallTile;
    public Transform floorTile;
    public Transform lavaTile;
    public Transform doorTile;
    public Transform goalTile;
    public Transform startTile;
    public Transform keyTile;
    public float gridSize = 0.65f;

	// Use this for initialization
	void Start () {
        if (null == levelController)
        {
            levelController = GetComponent<LevelLogic>();
        }

		Debug.Log(Application.dataPath);

		/* Windows-proofing the text. We assume no OS9 or earlier. *crosses fingers* */
		string csvText = text.text.Replace("\r", "");

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
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(columnNumber / 2 * gridSize, -lineNumber / 2 * gridSize, 0);
	}

	private void addTile(int xPosition, int yPosition, string tileName)
	{
		var tile = LookUpTile(tileName);
		Debug.Log(string.Format("Should make tile of type {0} at <{1},{2}>", tile, xPosition, yPosition));
        Instantiate(tile, new Vector3(xPosition * gridSize, -yPosition * gridSize, 0), Quaternion.identity);
        levelController.setTile(xPosition, yPosition, tileName);
	}
	
	private Transform LookUpTile(string tileName)
	{
        Transform retVal;
		switch(tileName)
		{
		case "s": retVal = startTile; break;
		case "m": retVal = goalTile; break;
		case "n": retVal = keyTile; break;
		case "d": retVal = doorTile; break;
		case "v": retVal = wallTile; break;
		case "l": retVal = lavaTile; break;
		case "g": retVal = floorTile; break;
		case "0": retVal = wallTile; break;
		case "": retVal = floorTile; break;
		default: throw new Exception(string.Format("tile specifier {0} {1} is not valid", tileName, Convert.ToInt32(tileName.ToCharArray()[0])));
		}
		return retVal;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
