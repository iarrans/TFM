using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class MirrorReception : MonoBehaviour
{
    public bool ReflectingPlayer1 = false;
    public GameObject mirrorAim;
    
    public void ReflectLightPlayer1(Vector3 reflectionposition, float angle)
    {
        if(!ReflectingPlayer1) StartCoroutine(LightReflection(reflectionposition, angle,0));
    }

    private IEnumerator LightReflection(Vector3 reflectionposition, float angle, int player)
    {
        ReflectingPlayer1 = true;
        yield return new WaitForSeconds(0.2f);
        while(GameManager.Instance.Player1Light.lightOnMirror){

            //Reflejo del rayo.
            yield return new WaitForSeconds(1);
        }
        ReflectingPlayer1 = false;
        yield return null;
    }
}


