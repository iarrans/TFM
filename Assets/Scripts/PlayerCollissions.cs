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

    public CollissionDetector playerCollission;

    private int wallsCollissioned;

    public bool flag;
    public bool lastFlag;


    public bool IsCollisioning { get { if (wallsCollissioned > 0) return true; return false; }  }


    private void Start()
    {
        playerCollission.OnObjectEntered += WallCollisionEnter;
        playerCollission.OnObjectExited += WalCollisionExit;
    }

    private void Update()
    {
        flag = false;
    }

    private void LateUpdate()
    {
        if (flag != lastFlag)
        {
            Debug.Log("Changed");
            if (flag)
            {
                ActivateParticles();
            }
            else
            {
                DeactivatePaticles();
            }
        }
        lastFlag = flag;
    }

    private void WalCollisionExit()
    {
        wallsCollissioned--;
    }

    private void WallCollisionEnter()
    {
        wallsCollissioned++;
    }

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
        lightParticles.gameObject.SetActive(true);
        List<Material> originalMaterials = characterRenderer.materials.ToList();
        originalMaterials.Add(outlineMaterial);
        characterRenderer.SetMaterials(originalMaterials);
    }

    public void DeactivatePaticles()
    {
        lightParticles.gameObject.SetActive(false);
        List<Material> originalMaterials = characterRenderer.materials.ToList();
        originalMaterials.Remove(outlineMaterial);
        characterRenderer.SetMaterials(originalMaterials);
    }
 }
