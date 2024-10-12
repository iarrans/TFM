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
    [SerializeField]
    AudioClip doorOpeningSound;

    int playerHitCounter = 0;

    void TryOpenDoor()
    {
        if(playerHitCounter >= playerHitCounterNeeded && !doorOpened){
            door.SetActive(false);
            doorOpened = true;
            AudioManager.instance.PlaySFXClip(doorOpeningSound);
            AudioManager.instance.PlayOpenDoorsMusic();
            MultiplayerManager.Instance.gameObject.GetComponent<PlayerInputManager>().DisableJoining();
            GameManager.Instance.ActiveAlarms();
        }     
    }

    public void SetPlayerHitCounter(int counter)
    {
        playerHitCounter = counter;
        TryOpenDoor();
    }
}
