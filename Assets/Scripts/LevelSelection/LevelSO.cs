using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelSO", menuName = "Levels/LevelSO", order = 1)]
public class LevelSO : ScriptableObject
{
    public int Levelscene;

    public int levelOrder;

    public string levelName;

    public Sprite levelMiniature;
}
