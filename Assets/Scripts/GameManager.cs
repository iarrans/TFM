using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public TimeManager timeManager;

    public LayerMask RaycastLayers;


    private void Awake()
    {
        Instance = this;
    }

    public void LevelFailed()
    {
        UIManager.Instance.LevelFailed();
    }
}
