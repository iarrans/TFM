using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<LevelSO> levels;

    public LevelSO currentLevel;

    public void LoadNextScene()
    {
        int levelIndex = levels.IndexOf(currentLevel);

        if (levelIndex + 1 < levels.Count)
        {
            SceneManager.LoadScene(levels[levelIndex + 1].Levelscene);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
