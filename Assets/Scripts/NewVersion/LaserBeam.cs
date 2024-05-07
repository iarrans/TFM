using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LaserBeam
{

    Vector3 pos, dir;
    GameObject laserObject;
    LineRenderer laser;
    List<Vector3> laserIndices = new List<Vector3>();

   public LaserBeam(Vector3 pos, Vector3 dir, Material material)
    {
        this.laser = new LineRenderer();
        this.laserObject = new GameObject();
        this.laserObject.name = "Laser_Beam";
        this.pos = pos;
        this.dir = dir;

        this.laser = this.laserObject.AddComponent<LineRenderer>();
        this.laser.startWidth = 0.1f;
        this.laser.endWidth = 0.1f;
        this.laser.material = material;
        this.laser.startColor = Color.green;
        this.laser.endColor = Color.green;

        CastRay(pos, dir, laser);
    }

    private void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser)
    {
        laserIndices.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50, 1))
        {
            CheckHit(hit, dir, laser);
        }
        else
        {
            laserIndices.Add(ray.GetPoint(50));
            UpdateLaser();
        }
    }

    private void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer lineRenderer)
    {
        if (hitInfo.collider.gameObject.CompareTag("Player"))
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);

            CastRay(pos, dir, laser);
        }
        else if (hitInfo.collider.gameObject.CompareTag("LightReceptor"))
        {
            hitInfo.collider.GetComponent<DoorLightReceptor>().OpenDoor();
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
        else
        {
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
    }

    private void UpdateLaser()
    {
        int count = 0;
        laser.positionCount = laserIndices.Count;

        foreach (Vector3 idx in laserIndices)
        {
            laser.SetPosition(count, idx);
            count++;
        }
    }
}
