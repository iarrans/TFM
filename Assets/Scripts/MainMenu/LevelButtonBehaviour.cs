using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonBehaviour : MonoBehaviour
{
    public LevelSO level;

    public TextMeshProUGUI levelNameText;

    public Image buttonImage;

    public void LoadButtonLevel()
    {
        SceneManager.LoadScene(level.Levelscene);
    }
}
