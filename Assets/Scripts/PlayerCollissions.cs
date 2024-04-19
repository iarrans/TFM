using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollissions : MonoBehaviour
{
    public bool isTouchingMirror;
    public GameObject currentMirror = null;

    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mirror"))
        {
            isTouchingMirror = true;
            currentMirror = collision.gameObject;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mirror"))
        {
            isTouchingMirror = false;
            currentMirror = null;
        }
    }
}
