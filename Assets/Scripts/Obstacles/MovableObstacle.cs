using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObstacle : MonoBehaviour
{
    public Transform[] waypoints; // Array de waypoints
    public float speed = 5f; // Velocidad de movimiento
    private int currentWaypointIndex = 0; // Índice del waypoint actual
    public float initialSpeed = 5f;

    public LayerMask characterLayers;
    public LayerMask wallLayers;
    public float margin = 0.5f;

    public bool playerInArea = false;

    void Update()
    {
        // Si no hay waypoints, no hacer nada
        if (waypoints.Length == 0)
            return;

        // Designar waypoint actual
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Mover el GameObject hacia el waypoint actual
        Vector3 direction = targetWaypoint.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        //Cond: si raycast (< tamanyo de pj) da con pared en alguna dir y hay pj en el trigger, se cambia al siguiente para evitar empujarlo

        //Raycast pared, raycast pj. Si distancia a pared <= distancia pj - margen, se cambia de direccion        
        RaycastHit hit;

        Ray wallRay = new Ray(transform.position, transform.position - targetWaypoint.position);
        Debug.Log("-------DIRECTION: " + wallRay.direction);

        float wallDistance = -1;
        if (Physics.Raycast(wallRay, out hit, 50, wallLayers))
        {
            wallDistance = hit.distance;
        }
        if (wallDistance<2f && wallDistance > 0 && playerInArea)
        {
            speed = 0;
        }
        else
        {
            speed = initialSpeed;
        }

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // Cambiar al siguiente waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

    }

    void OnDrawGizmos()
    {
        // Dibujar líneas en la escena para visualizar el recorrido
        if (waypoints.Length > 0)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                if (i < waypoints.Length - 1)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                }
                else
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[0].position);
                }
            }
        }
    }


}
