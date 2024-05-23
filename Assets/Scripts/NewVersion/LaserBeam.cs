using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class LaserBeam
{
    GameObject laserObject;
    LineRenderer laser;
    List<Vector3> laserPositions = new List<Vector3>();
    int playerHitCounter;

   public LaserBeam(Material material)
    {
        this.laser = new LineRenderer();
        this.laserObject = new GameObject();
        this.laserObject.name = "Laser_Beam";
 
        this.playerHitCounter = 0;

        this.laser = this.laserObject.AddComponent<LineRenderer>();
        this.laser.startWidth = 0.1f;
        this.laser.endWidth = 0.1f;
        this.laser.material = material;
        this.laser.startColor = Color.green;
        this.laser.endColor = Color.green;

        //StartRay(pos, dir, laser);
    }

    public void StartRay(Vector3 pos, Vector3 dir)
    {
        ClearRay();

        CastRayRecursive(pos, dir, laser);
    }

    private void ClearRay()
    {
        laserPositions.Clear();
        playerHitCounter = 0;
    }

    private void CastRayRecursive(Vector3 pos, Vector3 dir, LineRenderer laser)
    {
        laserPositions.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50, 1))
        {
            CheckHit(hit, dir, laser);
        }
        else
        {
            laserPositions.Add(ray.GetPoint(50));
            UpdateLaser();
        }
    }

    private void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer lineRenderer)
    {
        if (hitInfo.collider.gameObject.CompareTag("Player"))
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);

            playerHitCounter++;

            CastRayRecursive(pos, dir, laser);
        }
        else if (hitInfo.collider.gameObject.CompareTag("LightReceptor"))
        {
            hitInfo.collider.GetComponent<DoorLightReceptor>().SetPlayerHitCounter(playerHitCounter);
            laserPositions.Add(hitInfo.point);
            UpdateLaser();
        }
        else
        {
            laserPositions.Add(hitInfo.point);
            UpdateLaser();
        }
    }

    private void UpdateLaser()
    {
        int count = 0;
        laser.positionCount = laserPositions.Count;

        foreach (Vector3 idx in laserPositions)
        {
            laser.SetPosition(count, idx);
            count++;
        }
    }
}

