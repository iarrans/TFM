using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource BGMsource;

    public float BGMVolume = 1.0f;

    public float SFXVolume = 1.0f;

    public static AudioManager instance;

    [SerializeField] private AudioSource SFXPrefab;


    public AudioClip SolvingBGM;     //Suena durante el nivel
    public AudioClip OpenDoorsBGM;   //Suena cuando se abren las puertas: secuencia de escape
    public AudioClip VictoryBGM;     //Suena en la pantalla de victoria del nivel

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Start()
    {
        StartLevelMusic();
    }

    //Metodos para Background music
    public void StartLevelMusic()
    {
        BGMsource.clip = SolvingBGM;
        BGMsource.volume = BGMVolume;
        BGMsource.Play();
    }

    public void PlayOpenDoorsMusic()
    {
        BGMsource.clip = OpenDoorsBGM;
        BGMsource.Play();
    }

    public void PlayVictoryMusic()
    {
        BGMsource.clip = VictoryBGM;
        BGMsource.Play();
    }

    //Metodos para reproducir los efectos de sonido
    public void PlaySFXClip(AudioClip audioclip)
    {
        //Spawn del GameObject con efecto de sonido. Se crea como hijo del manager del sonido
        AudioSource audioSourceSFX = Instantiate(SFXPrefab, transform);

        //asignar clip de audio
        audioSourceSFX.clip = audioclip;

        //Cambiar a volumen seleccionado por usuario
        audioSourceSFX.volume = SFXVolume;

        //ejecutar sonido
        audioSourceSFX.Play();

        //eliminar objeto al acabar el sonido
        float clipLenght = audioSourceSFX.clip.length;
        Destroy(audioSourceSFX, clipLenght);

    }
}

