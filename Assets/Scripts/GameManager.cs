using DG.Tweening;
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

    public Transform winningCamPosition;


    private void Awake()
    {
        Instance = this;
        scapedChars = 0;
        playingLevel = false;
        Application.targetFrameRate = 60;
        DOTween.Init();
    }

    public void CheckScaped()
    {
        scapedChars++;
        if (scapedChars == requiredPlayers)
        {
            UIManager.Instance.ShowVictoryScreen();
            AudioManager.instance.PlayVictoryMusic();
            Camera.main.transform.DOMove(winningCamPosition.position,3);
            Camera.main.transform.DORotate(winningCamPosition.rotation.eulerAngles, 3);
        }
    }

    public void ShowLevel()
    {
        StartCoroutine(StartLevel());
    }

    public IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.playingLevel = true;
        AudioManager.instance.StartLevelMusic();
        UIManager.Instance.playerJoinScreen.SetActive(false);
    }
}
