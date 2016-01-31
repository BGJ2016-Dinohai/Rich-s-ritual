using UnityEngine;
using System.Collections;

public class MenuButtonHandler : MonoBehaviour {

	public void StartGame()
    {
        Application.LoadLevel("LevelLoadingScene");
    }

    public void ShowControls()
    {
        Application.LoadLevel("controls");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
