using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreenTransitions : MonoBehaviour {

    public Sprite[] sprites;
    public string nextSceneName;
    public float splashLength;
    private SpriteRenderer spriteRenderer;
    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
	}
	
	// Update is called once per frame
	void Update () {
        int index = (int)(Time.time/splashLength);
        if(index >= sprites.Length)
        {
            SceneManager.LoadScene(nextSceneName);
            return;
        }
        spriteRenderer.sprite = sprites[index];
    }
}
