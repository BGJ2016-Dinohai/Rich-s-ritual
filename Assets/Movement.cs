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
  
	}
    
    // Update is called once per frame
    void Update () {
        moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(moveVector * moveSpeed);
	}
}
