using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMan : MonoBehaviour {

    public Transform player;
    public Sprite playerSprite;
    public TextAsset moveText;

    private Transform ply;
    private float spriteHeight;
    private string currentSeq;

    private IDictionary<string, string> moveList;
    private IDictionary<char, Vector3> moveVectors;

    // Use this for initialization
    void Start () {
        currentSeq = "";
        float spriteHeight = playerSprite.bounds.size.y;
        ply = Instantiate(player) as Transform;

        // oh jesus
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
            oldPos = ply.position;
        }

        tween += Time.deltaTime;
        float smooth = smoothstep(0.0f, 1.0f, tween);
        if (pattern.Length > 0)
        {
            ply.position = oldPos + moveVectors[pattern[0]] * smooth;
        }


        if (tween > 1)
        {
            tween = 0;
            oldPos = ply.position;
            pattern = pattern.Substring(1);
            if(pattern == "")
            {
                moving = false;
            }
        }
    }

    private float smoothstep(float edge0, float edge1, float x)
    {
        x = Mathf.Clamp((x - edge0) / (edge1 - edge0), 0.0f, 1.0f);
        return x * x * (3 - 2 * x);
    }


	void Update ()
    {
        if (moving)
        {
            move();
            return;
        }

        if (!Input.anyKeyDown){ return; }
        if (currentSeq.Length == 4)
        {
            currentSeq = "";
        }
        if (getMyKeyCode() >= '1')
        {
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
            }
        }
    }
}
