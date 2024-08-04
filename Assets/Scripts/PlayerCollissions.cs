using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollissions : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ExitTrigger"))
        {
            GameManager.Instance.CheckScaped();
        }
    }
}
