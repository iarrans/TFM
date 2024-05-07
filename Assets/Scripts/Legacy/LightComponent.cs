using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;


public class LightComponent : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public LayerMask layerMask;
    public bool lightActive = false;
    public int playerNumber;
    private string key;
    private Vector3 laserOrigin, laserDirection;
    public LightComponent otherPlayerLight;
    public float laserAngle;
    //private MirrorReception currentMirror;

    private void Awake()
    {
        key = System.Guid.NewGuid().ToString();
    }

    public void ChangeDirection(Vector3 position, Vector3 forward)
    {
        this.laserOrigin = position;
        this.laserDirection = forward;
    }

    void Update()
    {
        if (lightActive)
        {
            lineRenderer.enabled = true;
            // Lanzar un rayo desde la posición del personaje hacia adelante
            Ray ray = new Ray(laserOrigin, laserDirection);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
            {
                // Actualizar los puntos del Line Renderer
                lineRenderer.SetPosition(0, laserOrigin);
                lineRenderer.SetPosition(1, hitInfo.point);
                if (hitInfo.transform.gameObject.CompareTag("Player"))
                {
                    var angle = Vector3.SignedAngle(hitInfo.normal, ray.direction, Vector3.up);
                    LightComponent otherPlayerLight = hitInfo.collider.GetComponent<LightComponent>();
                    otherPlayerLight.lightActive = false;
                }
                else if (hitInfo.transform.gameObject.CompareTag("LightReceptor"))
                {
                    //Función abrir puerta.
                    hitInfo.collider.GetComponent<DoorLightReceptor>().OpenDoor();
                    Debug.Log("Es el light receptor");
                }
                else
                {
                    DeactivateMirrorReflection();
                }
            }
            else
            {
                // Si el rayo no golpea nada, mostrar la línea en la dirección del rayo
                DeactivateMirrorReflection();
                lineRenderer.SetPosition(0, laserOrigin);
                lineRenderer.SetPosition(1, ray.origin + ray.direction * 100f);
            }
        }
        else
        {
            DeactivateMirrorReflection();
            lineRenderer.enabled = false;
        }
    }

    private void DeactivateMirrorReflection()
    {
        //currentMirror?.ChangeLaserData(new LaserData(Vector3.up, 0, false, key));
    }
}


