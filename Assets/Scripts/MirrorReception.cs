using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;


public struct LaserData
{
    public readonly Vector3 reflectionposition;
    public readonly float angle;
    public readonly bool isActive;
    public readonly string name;

    public LaserData(Vector3 reflectionposition, float angle, bool isActive, string name)
    {
        this.reflectionposition = reflectionposition;
        this.angle = angle;
        this.isActive = isActive;
        this.name = name;
    }
}

public class MirrorReception : MonoBehaviour
{
    public bool ReflectingPlayer1 = false;
    public GameObject laserPrefab; //lleva un componente laser - es decir, script lightcontroller
    public LineRenderer lineRendererPlayer1;
    public LineRenderer lineRendererPlayer2;
    public LayerMask layerMask;
    List<LaserData> laserDataList;
    Dictionary<string, GameObject> LaserByName;

    private void Awake()
    {
        laserDataList = new List<LaserData>();
        LaserByName = new Dictionary<string, GameObject>();
    }

    public void ChangeLaserData(LaserData laser)
    {
        bool alreadyAdded = false;

        for (int i = 0; i < laserDataList.Count; i++)
        {
            if (laserDataList[i].name == laser.name)
            {
                laserDataList[i] = laser;
                alreadyAdded = true;
                break;
            }
        }
        if (!alreadyAdded)
        {
            laserDataList.Add(laser);
            var laserPref = Instantiate(laserPrefab);
            LaserByName.Add(laser.name, laserPref);
        }
    }

    private void Update()
    {
        foreach (LaserData laser in laserDataList)
        {
            var laserObject = LaserByName[laser.name].GetComponent<LightController>();
            if (laser.isActive)
            {
                laserObject.lineRenderer.enabled = true;
                laserObject.lightActive = true;

                var angle = -laser.angle;
                ReflectingPlayer1 = true;          
                laserObject.transform.position = laser.reflectionposition;
                laserObject.transform.rotation = Quaternion.Euler(0, 180 + angle + transform.rotation.y * Mathf.Rad2Deg, 0);

                lineRendererPlayer1.enabled = true;

                Ray ray = new Ray(laser.reflectionposition, laserObject.transform.forward);
                RaycastHit hitInfo;


                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
                {
                    // Actualizar los puntos del Line Renderer
                    lineRendererPlayer1.SetPosition(0, laser.reflectionposition);
                    lineRendererPlayer1.SetPosition(1, hitInfo.point);

                }
                else
                {
                    // Si el rayo no golpea nada, mostrar la línea en la dirección del rayo
                    lineRendererPlayer1.SetPosition(0, laser.reflectionposition);
                    lineRendererPlayer1.SetPosition(1, ray.origin + ray.direction * 100f);
                }

                //Reflejo del rayo.
                laserObject.transform.position = laser.reflectionposition;
            }
            else
            {
                laserObject.lineRenderer.enabled = false;
                laserObject.GetComponent<LightController>().lightActive = false;                
            }
        }
    }
}


