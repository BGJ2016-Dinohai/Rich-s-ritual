using UnityEngine;
using System.Collections;
using System;
using System.Text;

public class Movement : MonoBehaviour {
    public TextAsset text;
    public float moveSpeed = 0.1f;

    Vector2 moveVector;

	// Use this for initialization
	void Start () {
        Debug.Log(Application.dataPath);
        string csvText = text.text.Replace("\r", "");

        var lineNumber = 0;
        foreach(string line in csvText.Split('\n'))
        {            
            var columnNumber = 0;
            foreach (string column in line.Split(','))
            {
                addTile(columnNumber, lineNumber, column);
                ++columnNumber;
            }
            ++lineNumber;
        }
  
	}

    private void addTile(int xPosition, int yPosition, string tileName)
    {
        var tile = LookUpTile(tileName);
        Debug.Log(string.Format("Should make tile of type {0} at <{1},{2}>", tile, xPosition, yPosition));
    }

    private string LookUpTile(string tileName)
    {
        Debug.Log(tileName == null);

        string retVal = "ERROR";
        switch(tileName)
        {
            case "s": retVal = "Start"; break;
            case "m": retVal = "Mål"; break;
            case "n": retVal = "Nøkkel"; break;
            case "d": retVal = "Dør"; break;
            case "v": retVal = "Vegg"; break;
            case "l": retVal = "Lava"; break;
            case "g": retVal = "Gulv"; break;
            case "0": retVal = "Utenfor"; break;
            case "": retVal = "Utenfor"; break;
            case "\r":retVal = "SLASH R ! ;_;"; break;
            case null: retVal = "NULL"; break;
            default: throw new Exception(string.Format("tile specifier {0} {1} is not valid", tileName, Convert.ToInt32(tileName.ToCharArray()[0])));
        }
        return retVal;
    }
    // Update is called once per frame
    void Update () {
        moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(moveVector * moveSpeed);
	}
}
