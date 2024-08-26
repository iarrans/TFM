using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollissions : MonoBehaviour
{
    public ParticleSystem lightParticles;

    public bool areParticlesactive = false;

    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ExitTrigger"))
        {
            GameManager.Instance.CheckScaped();
        }
    }

    public void ActivateParticles()
    {
        if (!areParticlesactive)
        {
            areParticlesactive = true;
            StartCoroutine(ActivateParticlesSequence());
        }
    }

    private IEnumerator ActivateParticlesSequence()
    {
        lightParticles.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        lightParticles.gameObject.SetActive(false);
        areParticlesactive = false;
    }
}
