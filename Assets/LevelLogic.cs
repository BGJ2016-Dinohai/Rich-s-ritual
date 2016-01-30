using UnityEngine;
using System.Collections;

public class LevelLogic : MonoBehaviour {

    string[][] level;
    private bool hasKey;

    public void setLevel(string[][] level)
    {
        this.level = level;
    }

    public void createEmptyLevel(int width, int height)
    {
        level = new string[width][];
        for(int rowNr = 0; rowNr < width; ++rowNr)
        {
            level[rowNr] = new string[height];          
        }
    }

    public void setTile(int xPosition, int yPosition, string tile)
    {
        level[xPosition][yPosition] = tile;
    }

	// Use this for initialization
	void Start () {
        createEmptyLevel(16, 16);
	}
	
    public void pickedUpKey()
    {
        this.hasKey = true;
    }

    public bool canWalk(int x, int y)
    {
        bool retVal = false;
        switch(level[x][y])
        {
            case "":
            case "g":
            case "l":
            case "n":
            case "s":
            case "m":
                retVal = true; break;
            case "d":
                retVal = hasKey; break;
            default:
                retVal = false; break;
        }
        return retVal;
    }

    public bool isDeadly(int x, int y)
    {
        return level[x][y].Equals("l");
    }


	// Update is called once per frame
	void Update () {
	
	}
}
