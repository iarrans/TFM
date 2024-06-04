using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsManager : MonoBehaviour
{
    public static ControlsManager Instance;

    public TimeManager timeManager;

    public List<GameObject> Player1Characters;

    public List<GameObject> Player2Characters;

    public GameObject currentPlayer1Character;

    public GameObject currentPlayer2Character;

    public int CurrentPlayer1Index;

    public int CurrentPlayer2Index;

    private void Awake()
    {
        Instance = this;

        //Var inicialitated
        currentPlayer1Character = Player1Characters[0];
        currentPlayer2Character = Player2Characters[0];
        CurrentPlayer1Index = 0;
        CurrentPlayer2Index = 0;
    }

    //Controls
    public void SwitchCharacterPlayer1(InputAction.CallbackContext context)
    {

        //Para comprobar si hay que dar la vuelta a la lista
        if(CurrentPlayer1Index + 1 >= Player1Characters.Count)
        {
            CurrentPlayer1Index = 0;
        }
        else
        {
            CurrentPlayer1Index++;
        }
        currentPlayer1Character = Player1Characters[CurrentPlayer1Index];

        Debug.Log("Changed player 1 char to " + currentPlayer1Character.name);

    }

    public void SwitchCharacterPlayer2(InputAction.CallbackContext context)
    {
        if (CurrentPlayer2Index + 1 >= Player2Characters.Count)
        {
            CurrentPlayer2Index = 0;
        }
        else
        {
            CurrentPlayer2Index++;
        }
        currentPlayer2Character = Player2Characters[CurrentPlayer2Index];

        Debug.Log("Changed player 2 char to " + currentPlayer2Character.name);
    }

    public void OnMove1(InputAction.CallbackContext context)
    {
        Debug.Log("Move");
        currentPlayer1Character.GetComponent<PlayerControls>().move = context.ReadValue<Vector2>();
    }

    public void OnRotate1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Rotate");
            currentPlayer1Character.GetComponent<PlayerControls>().CharacterRotate();
        }
    }

    public void OnMove2(InputAction.CallbackContext context)
    {
        Debug.Log("Move");
        currentPlayer2Character.GetComponent<PlayerControls>().move = context.ReadValue<Vector2>();
    }

    public void OnRotate2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Rotate");
            currentPlayer2Character.GetComponent<PlayerControls>().CharacterRotate();
        }
    }




    public void LevelFailed()
    {
        UIManager.Instance.LevelFailed();
    }

}
