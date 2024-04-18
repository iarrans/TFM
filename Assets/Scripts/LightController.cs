using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;


public class LightController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform characterAim;
    public LayerMask layerMask;
    public bool lightActive = false;
    public bool lightOnMirror = false;
    public int playerNumber;

    private void Awake()
    {
        lineRenderer = characterAim.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if(lightActive) {
            lineRenderer.enabled = true;
            // Lanzar un rayo desde la posición del personaje hacia adelante
            Ray ray = new Ray(characterAim.position, characterAim.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
            {
                // Actualizar los puntos del Line Renderer
                lineRenderer.SetPosition(0, characterAim.position);
                lineRenderer.SetPosition(1, hitInfo.point);
                if (hitInfo.transform.gameObject.CompareTag("Mirror"))
                {
                    lightOnMirror = true;
                    if (playerNumber == 0) {

                        var angle = Vector3.SignedAngle(hitInfo.normal, ray.direction, Vector3.up);
                        GameManager.Instance.MirrorPlayer1Light(hitInfo.point, angle, true);
                    } 
                }
            }
            else
            {
                // Si el rayo no golpea nada, mostrar la línea en la dirección del rayo
                lightOnMirror = false;
                lineRenderer.SetPosition(0, characterAim.position);
                lineRenderer.SetPosition(1, ray.origin + ray.direction * 100f);
            }
        }
        else
        {
            lineRenderer.enabled = false;
            lightOnMirror = false;
        }     
    }
    
}


