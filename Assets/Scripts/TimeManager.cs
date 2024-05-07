using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI textoTiempo; //texto cuenta atr�s

    public float tiempoTranscurrido;
    public float tiempoRestante; 
    public void Start()
    {
        int minutos = Mathf.Max(Mathf.FloorToInt(tiempoRestante / 60), 0);     //Extraemos los minutos
        int segundos = Mathf.Max(Mathf.FloorToInt(tiempoRestante % 60), 0);    //Extraemos los segundos
        if (segundos >= 10) textoTiempo.text = minutos + ":" + segundos;                    //Si los segundos son mayores o iguales que 10, concatenamos y construimos el texto
        else textoTiempo.text = minutos + ":" + "0" + segundos;
    }

    public void Update()
    {
        //if (DirectorNoteSpawner.instance.areNotesLoaded) {       
        tiempoRestante -= Time.deltaTime; //para saber cu�nto tiempo queda de la canci�n
        tiempoTranscurrido += Time.deltaTime; //para llevar la cuenta de la canci�n, para poder spawnear la nota a tiempo
        //}

        //parseo del tiempo a formato contador
        int minutos = Mathf.Max(Mathf.FloorToInt(tiempoRestante / 60), 0);     //Extraemos los minutos
        int segundos = Mathf.Max(Mathf.FloorToInt(tiempoRestante % 60), 0);    //Extraemos los segundos
        if (segundos >= 10) textoTiempo.text = minutos + ":" + segundos;                    //Si los segundos son mayores o iguales que 10, concatenamos y construimos el texto
        else textoTiempo.text = minutos + ":" + "0" + segundos;                             //Si los segundos son menors que 10, a�adimos un 0 a nuestros segundos, concatenamos

        if (tiempoRestante <= 0 ) //Cuando acaba la canci�n, se considera que el nivel ha terminado.
        {
            textoTiempo.text = "Fin";
       
        }
    }

}
