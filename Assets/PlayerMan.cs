﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMan : MonoBehaviour {

    int x;
    int y;

    public Transform playerSprite;
    public TextAsset moveText;

    public Transform player;

    public float bpm;
    private float offset;
    private string currentSeq;

    private IDictionary<string, string> moveList;
    private IDictionary<char, Vector3> moveVectors;
    private LevelLogic levelLogic;
    private LoadLevel levelLoader;
    private float beat;


    public void instantiatePlayer(Vector3 position, int xPosition, int yPosition)
    {
        x = xPosition;
        y = yPosition;
        player = Instantiate(playerSprite, position, Quaternion.identity) as Transform;
        Debug.Log(string.Format("Instantiated player at ({0},{1})", x, y));
    }

    // Use this for initialization
    void Start () {
        currentSeq = "";
        beat = 60.0f / bpm;
        offset = Time.time % (beat);

        // Translation of directions to move vectors
        moveList = new Dictionary<string, string>();
        moveVectors = new Dictionary<char, Vector3>();
        moveVectors['u'] = new Vector3(0, 1, 0);
        moveVectors['d'] = new Vector3(0, -1, 0);
        moveVectors['l'] = new Vector3(-1, 0, 0);
        moveVectors['r'] = new Vector3(1, 0, 0);

        string moves = moveText.text.Replace("\r","");
        foreach(string line in moves.Split('\n'))
        {
            string[] mt = line.Split(':');
            moveList[mt[1]] = mt[0];
        }
        levelLoader = GetComponent<LoadLevel>();
		levelLogic = levelLoader.GetLevelController ();
    }

    private char getMyKeyCode()
    {
        char retVal = '\0';
        if (Input.GetButtonDown("Voodoo1")) retVal = '1';
        if (Input.GetButtonDown("Voodoo2")) retVal = '2';
        if (Input.GetButtonDown("Voodoo3")) retVal = '3';
        if (Input.GetButtonDown("Voodoo4")) retVal = '4';

        return retVal;
    }

    private bool moving = false;
    private float tween = 0;
    private string pattern;
    private Vector3 oldPos;
    void move(string p = null)
    {
        if(p != null)
        {
            moving = true;
            pattern = p;
            oldPos = player.position;
        }

        if (levelLogic.isDeadly(x, y))
        {
            Debug.Log("Restart lvl");
            LevelManager.reloadLevel();
            return;
        }

        if (pattern.Length > 0)
        {

            var moveSpec = moveVectors[pattern[0]];
            int newX = x + (int)moveSpec.x;
            int newY = y - (int)moveSpec.y;

            tween += Time.deltaTime;
        	float smooth;
            
			Debug.Log(string.Format("Trying: x{0} y{1} tile: {2}", newX, newY, levelLogic.getTile(newX, newY)));
            
			if (!levelLogic.canWalk(newX, newY))
            {   
                /*
                Debug.Log(string.Format("Cant walk there: x{0} y{1} tile: {2}", newX, newY, levelLogic.getTile(newX, newY)));
                oldPos = player.position;
                pattern = pattern.Substring(1);
                if (pattern == "")
                {
                    moving = false;
                }
                return;*/
                smooth = smoothbumpstep(0.0f, 1.0f, tween);
            }
            
			else
            {
                smooth = smoothstep(0.0f, 1.0f, tween);
            }

            player.position = oldPos + moveSpec * smooth * levelLoader.gridSize;
            

            if (tween > 1)
            {
                tween = 0;
                if (levelLogic.canWalk(newX, newY))
                {
                    x = newX;
                    y = newY;
                }
                oldPos = player.position;
                pattern = pattern.Substring(1);
                if (pattern == "")
                {
                    moving = false;
                }
            }
        }
    }

    private float smoothstep(float edge0, float edge1, float x)
    {
        x = Mathf.Clamp((x - edge0) / (edge1 - edge0), 0.0f, 1.0f);
        return x * x * (3 - 2 * x);
    }

    private float smoothbumpstep(float edge0, float edge1, float x)
    {
        x = Mathf.Clamp((x - edge0) / (edge1 - edge0), 0.0f, 1.0f);
        return /*x - x * x; */0.125f * (Mathf.Cos(2 * Mathf.PI * (x + 0.5f)) + 1);
    }


    bool isHit;
    bool canHit;

	void Update ()
    {
        if (moving)
        {
            move();
            return;
        }

        if (levelLogic.isKey(x, y))
        {
            levelLogic.pickedUpKey();
            GameObject keyObject = GameObject.Find("key(Clone)");
            Instantiate(levelLoader.floorTile, keyObject.transform.position, Quaternion.identity);
            GameObject.Destroy(keyObject);
            levelLogic.replaceTile(x, y, "g");
        }

        if (levelLogic.isGoal(x, y))
        {
            LevelManager.nextLevel();
        }

        if (levelLogic.isDeadly(x, y))
        {
            Debug.Log("Restart lvl");
            LevelManager.reloadLevel();
        }

        float modtime = (1.0f / beat * (((Time.time+offset) - beat / 2) % beat));
        bool hitWindow = !(modtime > 0.75f || modtime < 0.25f);
        

        if (!Input.anyKeyDown){ return; }
        if (!(getMyKeyCode() >= '1')) { return; }

        if (!hitWindow)
        {
            Debug.Log("Miss");
            currentSeq = "";
            return;
        }

        if (currentSeq.Length == 4){
        }

        currentSeq += getMyKeyCode();

        Debug.Log(string.Format("Hit: {0} Seq: {1} Length: {2}", getMyKeyCode(), currentSeq, currentSeq.Length));
        if (currentSeq.Length == 4)
        {
            Debug.Log(string.Format("Length: {0} Seq: {1}", currentSeq.Length, currentSeq));
            string value;
            if (moveList.TryGetValue(currentSeq, out value))
            {
                Debug.Log(string.Format("Move like this: {0}", value));
                move(value);
            }
            currentSeq = "";
        }
    }
}
