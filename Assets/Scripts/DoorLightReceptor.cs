using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLightReceptor : MonoBehaviour
{
    public GameObject door;
    public bool doorOpened = false;

    public void OpenDoor()
    {
        door.SetActive(false);
        doorOpened = true;
    }
}
