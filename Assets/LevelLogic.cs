using UnityEngine;
using System.Collections;

public class LevelLogic : MonoBehaviour {
    private int width;
    private int height;

    string[][] level;
    private bool hasKey;
    public void setLevel(string[][] level)
    {
        this.level = level;
    }

    public void createEmptyLevel(int width, int height)
    {
        this.width = width;
        this.height = height;
        level = new string[width][];
        for(int rowNr = 0; rowNr < width; ++rowNr)
        {
            level[rowNr] = new string[height];

            for(int i = 0; i < height; ++i)
            {
                level[rowNr][i] = "0";
            }          
        }
    }

    public void setTile(int xPosition, int yPosition, string tile)
    {
        bool error = false;
        string errorMsg = "Errors:";
        if(null == tile)
        {
            error = true;
            errorMsg += " tile argument is null";
        }
        if(xPosition < 0)
        {
            errorMsg += " xPosition is negative";
            error = true;
        }
        if(xPosition >= width)
        {
            errorMsg += string.Format(" xPosition is too large, was {0} but width is {1}", xPosition, width);
            error = true;
        }
        if (yPosition < 0)
        {
            errorMsg += " yPosition is negative";
            error = true;
        }
        if (yPosition >= width)
        {
            errorMsg += string.Format(" yPosition is too large, was {0} but width is {1}", yPosition, height);
            error = true;
        }

        if (error)
        {
            throw new System.Exception(errorMsg);
        }
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
                retVal = true; break;
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
