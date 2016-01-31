using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Rhythm : MonoBehaviour {
    public float bpm;
    private float beat;
    private float offset;
	// Use this for initialization
	void Start () {
        beat = 60.0f/bpm;
        offset = Time.time % (beat);

    }
	
	// Update is called once per frame
	void Update ()
    {
        Image r = GetComponent<Image>();
        float modtime = 1-(1.0f/beat * ((Time.time+offset)%beat));
        r.color = new Color(1, 1, 1, modtime);
    }
}
