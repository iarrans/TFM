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
        if (currentLevel.levelOrder + 1 < levels.Count)
        {
            SceneManager.LoadScene(levels[currentLevel.levelOrder + 1].Levelscene);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
