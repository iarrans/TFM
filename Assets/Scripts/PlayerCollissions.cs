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

    private List<Material> characterOriginalMaterials;

    public CollissionDetector playerCollission;

    private int wallsCollissioned;

    public bool flag;
    public bool lastFlag;

    public AudioClip respawnSound;


    public bool IsCollisioning { get { if (wallsCollissioned > 0) return true; return false; }  }


    private void Start()
    {
        playerCollission.OnObjectEntered += WallCollisionEnter;
        playerCollission.OnObjectExited += WalCollisionExit;
        characterOriginalMaterials = characterRenderer.materials.ToList();
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
            transform.gameObject.SetActive(false);
        }
        else if (other.CompareTag("OutOfBounds"))
        {
            transform.position = MultiplayerManager.Instance.spawnPositions[0].position;
            AudioManager.instance.PlaySFXClip(respawnSound);
        }
    }

    public void ActivateParticles()
    {
        lightParticles.gameObject.SetActive(true);
        characterOriginalMaterials.Add(outlineMaterial);
        characterRenderer.SetMaterials(characterOriginalMaterials);
    }

    public void DeactivatePaticles()
    {
        lightParticles.gameObject.SetActive(false);
        characterOriginalMaterials.Remove(outlineMaterial);
        characterRenderer.SetMaterials(characterOriginalMaterials);
    }
 }
