using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorBehaviour : MonoBehaviour
{
    public GameObject levelButtonPrefab;

    public LevelManager levelManager;

    public GameObject LevelScrollsContent;

    public void Start()
    {
        LoadButtons();
    }


    public void LoadButtons()
    {
        foreach (LevelSO level in levelManager.levels)
        {
            GameObject buttonGO = Instantiate(levelButtonPrefab, LevelScrollsContent.transform);
            LevelButtonBehaviour button = buttonGO.GetComponent<LevelButtonBehaviour>();
            button.level = level;
            button.levelNameText.text = level.levelName;
            button.buttonImage.sprite = level.levelMiniature;
        }
    }
   

}
