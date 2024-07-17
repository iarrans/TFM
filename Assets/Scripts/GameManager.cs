using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public TimeManager timeManager;

    public LayerMask RaycastLayers;

    public int scapedChars;

    public int requiredPlayers = 4;

    public bool playingLevel = true;


    private void Awake()
    {
        Instance = this;
        scapedChars = 0;
    }

    public void CheckScaped()
    {
        scapedChars++;
        if (scapedChars == requiredPlayers)
        {
            UIManager.Instance.ShowVictoryScreen();
        }
    }
}
