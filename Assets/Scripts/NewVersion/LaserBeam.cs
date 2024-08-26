using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{
    GameObject laserObject;
    LineRenderer laser;
    List<Vector3> laserPositions = new List<Vector3>();
    int playerHitCounter;
    public LayerMask layers;
    int lastCount = 2;

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

        layers = GameManager.Instance.RaycastLayers;

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

        if (Physics.Raycast(ray, out hit, 50, layers))
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

            //Llamamos al metodo activar particulas, limitada por una corrutina con espera para no comprobar todo el rato
            if (hitInfo.transform.parent != null)
            {
                hitInfo.transform.parent.GetComponent<PlayerCollissions>().ActivateParticles();
                hitInfo.transform.parent.GetComponent<PlayerCollissions>().ActivateLightOutline();
            }
            else
            {
                hitInfo.transform.GetComponent<PlayerCollissions>().ActivateParticles();
                hitInfo.transform.GetComponent<PlayerCollissions>().ActivateLightOutline();
            }

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

        //para que se lance sonido cada vez que la luz rebota en un jugador
        if (laserPositions.Count > lastCount)
        {
            AudioManager.instance.RaySFX();
        }
        lastCount = laserPositions.Count;
    }
}

