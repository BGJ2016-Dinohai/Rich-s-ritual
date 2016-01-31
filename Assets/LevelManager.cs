using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{

    public TextAsset[] levels;

    private static TextAsset[] sLevelList;
    private static int levelIndex;
    private static bool active;

    // Use this for initialization
    void Start()
    {
        sLevelList = levels;
        levelIndex = 0;
        active = false;
    }

    public static void nextLevel(int level = -1)
    {
        active = true;
        if (level > -1)
        {
            levelIndex = level;
        }

        if(levelIndex >= sLevelList.Length)
        {
            Application.LoadLevel("mainmenu");
        }
        Debug.Log(string.Format("Loading level: {0}", levelIndex));
        levelIndex++;
        Application.LoadLevel("LevelLoadingScene");
    }

    public static bool isActive()
    {
        return active;
    }

    public static TextAsset getCurrentLevel()
    {
        return sLevelList[levelIndex-1];
    }

    public static void reloadLevel()
    {
        Application.LoadLevel("LevelLoadingScene");
    }
}
