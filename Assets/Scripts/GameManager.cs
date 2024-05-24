using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TimeManager timeManager;

    public List<GameObject> characters;

    public GameObject currentCharacter;

    private void Awake()
    {
        Instance = this;
        currentCharacter = characters[0];
    }
    public void SwitchCharacter1(InputAction.CallbackContext context)
    {
        Debug.Log("Changed to player 1");
        currentCharacter = characters[0];
    }

    public void SwitchCharacter2(InputAction.CallbackContext context)
    {
        Debug.Log("Changed to player 2");
        currentCharacter = characters[1];
    }

    public void SwitchCharacter3(InputAction.CallbackContext context)
    {
        Debug.Log("Changed to player 3");
        currentCharacter = characters[2];
    }

    public void SwitchCharacter4(InputAction.CallbackContext context)
    {
        Debug.Log("Changed to player 4");
        currentCharacter = characters[3];
    }

    public void LevelFailed()
    {
        UIManager.Instance.LevelFailed();
    }

}
