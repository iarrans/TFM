using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static UIManager Instance;

    public GameObject gameplayCanvas;

    public GameObject failedLevelCanvas;

    private void Awake()
    {
        Instance = this;
        failedLevelCanvas.SetActive(false);
        gameplayCanvas.SetActive(true);
    }
   public void LevelFailed()
   {
        gameplayCanvas.SetActive(false);
        failedLevelCanvas.SetActive(true);
   }

    public void RestartScene()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene);
    }
}
