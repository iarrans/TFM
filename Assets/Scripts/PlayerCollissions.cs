using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollissions : MonoBehaviour
{
    public ParticleSystem lightParticles;

    public bool areParticlesactive = false;

    public MeshRenderer characterRenderer;

    public bool isOutlineActive = false;

    public Material outlineMaterial;

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

    public void ActivateLightOutline()
    {
        if (!isOutlineActive)
        {
            isOutlineActive = true;
            StartCoroutine(ActivateOutline());
        }
    }

    private IEnumerator ActivateOutline()
    {
        List<Material> originalMaterials = characterRenderer.materials.ToList();
        originalMaterials.Add(outlineMaterial);
        characterRenderer.SetMaterials(originalMaterials);
        yield return new WaitForSeconds(0.75f);
        originalMaterials.Remove(outlineMaterial);
        characterRenderer.SetMaterials(originalMaterials);
        isOutlineActive = false;
    }
 }
