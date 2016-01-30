using UnityEngine;
using System.Collections;


public class Grid : MonoBehaviour {

    public Transform walkable;
    public Transform lava;
    public Sprite tileSprite;

    // Use this for initialization
    void Start () {
        
        float spriteHeight = tileSprite.bounds.size.y;
        for (int j = 0; j < 10; j++)
        {
            for(int i = 0; i < 10; i++)
            {
                Transform clone = Instantiate(walkable) as Transform;
                clone.Translate(i * spriteHeight, j * spriteHeight, 0.0f);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
