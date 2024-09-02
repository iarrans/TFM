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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            obstacleScript.canMove = !other.transform.parent.GetComponent<PlayerCollissions>().IsCollisioning;
        }
    }
}
