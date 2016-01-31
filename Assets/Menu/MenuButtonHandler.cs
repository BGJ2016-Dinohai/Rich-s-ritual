using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuButtonHandler : MonoBehaviour {

	public void StartGame()
    {
        
        SceneManager.LoadScene("LevelLoadingScene");
    }

    public void ShowControls()
    {
        SceneManager.LoadScene("controls");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
