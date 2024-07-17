using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorLightReceptor : MonoBehaviour
{
    [SerializeField]
    public GameObject door;
    [SerializeField]
    public bool doorOpened = false;
    [SerializeField]
    int playerHitCounterNeeded;

    int playerHitCounter = 0;

    void TryOpenDoor()
    {
        if(playerHitCounter >= playerHitCounterNeeded){
            door.SetActive(false);
            doorOpened = true;
            MultiplayerManager.Instance.gameObject.GetComponent<PlayerInputManager>().DisableJoining();
        }     
    }

    public void SetPlayerHitCounter(int counter)
    {
        playerHitCounter = counter;
        TryOpenDoor();
    }
}
