using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollissions : MonoBehaviour
{
    public bool isTouchingMirror;
    public GameObject currentMirror = null;
    public bool rotateUp= false;
    public bool rotateDown = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MirrorUp"))
        {
            isTouchingMirror = true;
            currentMirror = other.gameObject;
            rotateUp = true;
            rotateDown = false;
        }

        if (other.gameObject.CompareTag("MirrorDown"))
        {
            isTouchingMirror = true;
            currentMirror = other.gameObject;
            rotateUp = false;
            rotateDown = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MirrorUp"))
        {
            isTouchingMirror = false;
            currentMirror = null;
            rotateUp = false;
            rotateDown = false;
        }
    }
}
