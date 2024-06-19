using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI textoTiempo; //texto cuenta atrás

    public float tiempoTranscurrido;

    public void Start()
    {
        int minutos = Mathf.Max(Mathf.FloorToInt(tiempoTranscurrido / 60), 0);     //Extraemos los minutos
        int segundos = Mathf.Max(Mathf.FloorToInt(tiempoTranscurrido % 60), 0);    //Extraemos los segundos
        if (segundos >= 10) textoTiempo.text = minutos + ":" + segundos;                    //Si los segundos son mayores o iguales que 10, concatenamos y construimos el texto
        else textoTiempo.text = minutos + ":" + "0" + segundos;
    }

    public void Update()
    {
        tiempoTranscurrido += Time.deltaTime; 

        //parseo del tiempo a formato contador
        int minutos = Mathf.Max(Mathf.FloorToInt(tiempoTranscurrido / 60), 0);     //Extraemos los minutos
        int segundos = Mathf.Max(Mathf.FloorToInt(tiempoTranscurrido % 60), 0);    //Extraemos los segundos
        if (segundos >= 10) textoTiempo.text = minutos + ":" + segundos;                    //Si los segundos son mayores o iguales que 10, concatenamos y construimos el texto
        else textoTiempo.text = minutos + ":" + "0" + segundos;                             //Si los segundos son menors que 10, añadimos un 0 a nuestros segundos, concatenamos

    }

}
