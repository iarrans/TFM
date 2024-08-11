using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObstacle : MonoBehaviour
{
    public Transform[] waypoints; // Array de waypoints
    public float speed = 5f; // Velocidad de movimiento
    private int currentWaypointIndex = 0; // Índice del waypoint actual

    void Update()
    {
        // Si no hay waypoints, no hacer nada
        if (waypoints.Length == 0)
            return;

        // Mover el GameObject hacia el waypoint actual
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        //Cond: si raycast (< tamanyo de pj) da con pared en alguna dir y hay pj en el trigger, se cambia al siguiente para evitar empujarlo

        /*
        
         
        Ray ray = new Ray(transform.position, targetWaypoint.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50, layers))
        {
            CheckHit(hit, dir, laser);
        }

         */

        // Verificar si el GameObject ha llegado al waypoint actual
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
