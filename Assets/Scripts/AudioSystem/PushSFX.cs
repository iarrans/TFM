using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushSFX : MonoBehaviour
{

    public AudioClip pushSound;

    private Rigidbody objectRigidbody;

    public bool audioIsPlaying;

    private void Start()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        audioIsPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (objectRigidbody.velocity != Vector3.zero && audioIsPlaying==false)
        {
            audioIsPlaying = true;
            AudioManager.instance.PlaySFXClip(pushSound);
            StartCoroutine(PauseAudio());
        }
    }

    private IEnumerator PauseAudio()
    {
        yield return new WaitForSeconds(pushSound.length);
        audioIsPlaying = false;
    }
}
