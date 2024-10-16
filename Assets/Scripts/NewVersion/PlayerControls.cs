using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    Rigidbody rb;
    public float speed, senstivity;
    public Vector2 move;
    public float maxForce;
    public Transform body;
    public int playerNumber;
    public MirrorType mirrorType;

    public AudioClip rotationSound;
    public LayerMask wallLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        body = transform.GetChild(1);
    }

    public void PlayerJoined(PlayerInput input)
    {
        Debug.Log("Player joined!");
        PlayerSettings(input.gameObject);
    }

    private void PlayerSettings(GameObject playerGO)
    {
        MultiplayerManager.Instance.PlayerAdded(playerGO);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.playingLevel)
        {
            Debug.Log("Move");
            move = context.ReadValue<Vector2>();
        }
    }

    void Move()
    {

        //Target Velocity
        Vector3 currentVelocity = rb.velocity;

        Vector3 targetVelocity = new Vector3(move.x, 0, move.y);
        targetVelocity *= speed;

        //Dieccion
        targetVelocity = transform.TransformDirection(targetVelocity);

        //Calculate Forces
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        //Limit force
        Vector3.ClampMagnitude(velocityChange, maxForce);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (context.performed && GameManager.Instance.playingLevel)
        {
            Debug.Log("Rotate");
            AudioManager.instance.PlaySFXClip(rotationSound);
            CharacterRotate();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (UIManager.Instance != null && GameManager.Instance.playingLevel)
        {
            if(!UIManager.Instance.pauseScreen.activeSelf) UIManager.Instance.PauseGameplay();
            else UIManager.Instance.RestoreGameplay();
        }
    }

    public void CharacterRotate()
    {
        float degreeNumber = 0;
        switch (mirrorType)
        {
            case MirrorType.TYPE1:
                degreeNumber = 90;
                break;
            case MirrorType.TYPE2:
                degreeNumber = 60;
                break;
            case MirrorType.TYPE3:
                degreeNumber = 45;
                break;
            case MirrorType.TYPE4:
                degreeNumber = 30;
                break;
        }

        body.Rotate(0,0.0f, degreeNumber, Space.Self);
    }

}

public enum MirrorType{

    TYPE1,TYPE2, TYPE3, TYPE4

}
