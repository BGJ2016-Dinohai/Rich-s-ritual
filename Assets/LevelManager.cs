using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{

    public TextAsset[] levels;

    public static TextAsset[] sLevelList;
    public static int levelIndex;

    // Use this for initialization
    void Start()
    {
        sLevelList = levels;
        levelIndex = 0;
    }

    public static void nextLevel(int level = -1)
    {
        if(level > -1)
        {
            levelIndex = level;
        }

        if(levelIndex >= sLevelList.Length)
        {
            Application.LoadLevel("mainmenu");
        }
        Debug.Log(string.Format("Loading level: {0}", levelIndex));
        LoadLevel.nextLevel = sLevelList[levelIndex];
        levelIndex++;
        Application.LoadLevel("LevelLoadingScene");
    }

    public static void reloadLevel()
    {
        LoadLevel.nextLevel = sLevelList[levelIndex-1];
        Application.LoadLevel("LevelLoadingScene");
    }
}
