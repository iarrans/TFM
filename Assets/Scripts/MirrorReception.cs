using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class MirrorReception : MonoBehaviour
{
    public bool ReflectingPlayer1 = false;
    public Transform player1Reflecter;
    public Transform player2Reflecter;
    public LineRenderer lineRendererPlayer1;
    public LineRenderer lineRendererPlayer2;
    public LayerMask layerMask;

    public void ReflectLightPlayer1(Vector3 reflectionposition, float angle, bool isPositive)
    {
        //Debug.Log("Angle is " + angle);
        if(!ReflectingPlayer1) StartCoroutine(LightReflection1(reflectionposition, angle, isPositive));
    }

    private IEnumerator LightReflection1(Vector3 reflectionposition, float angle, bool isPositive)
    {
        angle = -angle;
        ReflectingPlayer1 = true;
        player1Reflecter.position = reflectionposition;

        player1Reflecter.transform.rotation = Quaternion.Euler(0, 180 + angle, 0);

        //player1Reflecter.transform.rotation = Quaternion.Euler(0, 180 - angle, 0);
             

        //yield return new WaitForSeconds(0.2f);
        lineRendererPlayer1.enabled = true;

        Ray ray = new Ray(reflectionposition, player1Reflecter.forward);
        RaycastHit hitInfo;


        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
        {
            // Actualizar los puntos del Line Renderer
            lineRendererPlayer1.SetPosition(0, reflectionposition);
            lineRendererPlayer1.SetPosition(1, hitInfo.point);

        }
        else
        {
            // Si el rayo no golpea nada, mostrar la línea en la dirección del rayo
            lineRendererPlayer1.SetPosition(0, reflectionposition);
            lineRendererPlayer1.SetPosition(1, ray.origin + ray.direction * 100f);
        }

        //Reflejo del rayo.
        player1Reflecter.position = reflectionposition;

        while (GameManager.Instance.Player1Light.lightOnMirror){          
            yield return new WaitForSeconds(0.5f);
        }

        ReflectingPlayer1 = false;
        lineRendererPlayer1.enabled = false;
        yield return null;
    }
}


