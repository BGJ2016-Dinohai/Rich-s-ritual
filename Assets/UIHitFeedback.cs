using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHitFeedback : MonoBehaviour {

    public Sprite great;
    public Sprite miss;

    public float bpm;
    private float offset;
    private float beat;

    // Use this for initialization
    void Start () {
        beat = 60.0f / bpm;
        offset = Time.time % (beat);
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

    // Update is called once per frame
    void Update ()
    {

        Image r = GetComponent<Image>();

        r.color = new Color(255f, 255f, 255f, r.color.a - Time.deltaTime*3);

        if (!Input.anyKeyDown) { return; }
        if (!(getMyKeyCode() >= '1')) { return; }

        float modtime = (1.0f / beat * (((Time.time + offset) - beat / 2) % beat));
        bool hitWindow = !(modtime > 0.75f || modtime < 0.25f);

        if (hitWindow)
        {
            r.sprite = great;
        }
        else
        {
            r.sprite = miss;
        }

        r.color = new Color(255, 255, 255, 1);

    }
}
