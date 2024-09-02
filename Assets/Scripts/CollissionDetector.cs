using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollissionDetector : MonoBehaviour
{
    public event Action OnObjectEntered;
    public event Action OnObjectExited;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            OnObjectEntered?.Invoke();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            OnObjectExited?.Invoke();
        }
    }


}
