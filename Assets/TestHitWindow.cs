using UnityEngine;
using System.Collections;

public class TestHitWindow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = 0.2f;
        float beat = 60.0f / 122.08f;
        float modtime = (1.0f / beat * ((Time.time - beat/2) % beat));
        bool hitWindow = !(modtime > 0.75f || modtime < 0.25f);


        SpriteRenderer r = GetComponent<SpriteRenderer>();
        r.color = new Color(1, 1, 1, hitWindow ? 1 : 0);
    }
}
