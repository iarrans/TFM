using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObstacleTrigger : MonoBehaviour
{
    public MovableObstacle obstacleScript;

    private void Start()
    {
        obstacleScript = transform.parent.GetComponent<MovableObstacle>();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            obstacleScript.playerInArea = true;
        }      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            obstacleScript.playerInArea = false;
        }
    }
}
