using UnityEngine;
using System.Collections;

public class Rhythm : MonoBehaviour {

    private float beat;
	// Use this for initialization
	void Start () {
        beat = 60.0f/122.08f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpriteRenderer r = GetComponent<SpriteRenderer>();
        float modtime = 1-(1.0f/beat * (Time.time%beat));
        r.color = new Color(1, 1, 1, modtime);
    }
}
