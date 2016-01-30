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

    // Use this for initialization
    void Start () {
        currentSeq = "";
        float spriteHeight = playerSprite.bounds.size.y;
        ply = Instantiate(player) as Transform;

        // oh jesus
        moveList = new Dictionary<string, string>();

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


	void Update () {
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
                }
            }
        }
    }
}
