using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{

    public float tiempoTranscurrido;

    public void Update()
    {
        tiempoTranscurrido += Time.deltaTime; 
    }

}
