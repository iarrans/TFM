using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;
    public AudioSource mainMenuAudio;
    public AudioSource sfxMenu;

    [SerializeField] AudioClip buttonSound;

    private void Start()
    {
        MainUtils.BGMVolume = bgmSlider.value;
        MainUtils.SFXVolume = sfxSlider.value;
    }

    public void MoveToScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void ChangeBGMVolume()
    {
        MainUtils.BGMVolume = bgmSlider.value;
        mainMenuAudio.volume = bgmSlider.value;
    }

    public void ChangeSFXVolume()
    {
        MainUtils.SFXVolume = sfxSlider.value;
        sfxMenu.volume = sfxSlider.value;
    }

    public void PlayButtonSound()
    {
        sfxMenu.clip = buttonSound;
        sfxMenu.Play();
    }
}
