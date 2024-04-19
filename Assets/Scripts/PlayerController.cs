using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject CharBody;
    public float speed, senstivity;
    private Vector2 move;
    public float maxForce;
    public LightController LightController;
    public PlayerCollissions PlayerCollissions;
    public int playerNumber;
    
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        CharacterRotate();
    }


    public void OnPush(InputAction.CallbackContext context)
    {
        //HACER DISTINCIÓN DE 3 COLLIDERS: Middle mirror, up mirror y down mirror. Middle mirror solo desplaza. Up/Down mirror rotan.
        if (PlayerCollissions.isTouchingMirror)
        {
            Vector3 currentMirrorPos = PlayerCollissions.currentMirror.transform.position;
            Vector3 mirrorRotation = PlayerCollissions.currentMirror.transform.eulerAngles;
            if (!PlayerCollissions.rotateUp && !PlayerCollissions.rotateDown)
            {
                if (playerNumber == 1)
                {
                    //mover a -Z
                    PlayerCollissions.currentMirror.transform.position = new Vector3(currentMirrorPos.x, currentMirrorPos.y, currentMirrorPos.z - 0.2f);
                }
                else if (playerNumber == 0)
                {
                    //mover a +Z
                    PlayerCollissions.currentMirror.transform.position = new Vector3(currentMirrorPos.x, currentMirrorPos.y, currentMirrorPos.z + 0.2f);
                }
            }
            //rotación a derecha = -y, rotación a derecha +y
            else if (PlayerCollissions.rotateUp)
            {
                if (playerNumber == 1)
                {
                    //rotar +Y
                    PlayerCollissions.currentMirror.transform.rotation = Quaternion.Euler(mirrorRotation + new Vector3(0,- 0.2f,0));
                }
                else if (playerNumber == 0)
                {
                    //rotar -Y
                    PlayerCollissions.currentMirror.transform.rotation = Quaternion.Euler(mirrorRotation + new Vector3(0,  0.2f, 0));
                }
            }
            else
            {
                if (playerNumber == 1)
                {
                    //rotar -Y
                    PlayerCollissions.currentMirror.transform.rotation = Quaternion.Euler(mirrorRotation + new Vector3(0, 0.2f, 0));
                }
                else if (playerNumber == 0)
                {
                    //rotar +Y
                    PlayerCollissions.currentMirror.transform.rotation = Quaternion.Euler(mirrorRotation + new Vector3(0, - 0.2f, 0));
                }
            }
        }
    }

    public void OnEmitLight(InputAction.CallbackContext context)
    {
        if (context.performed)
            LightController.lightActive = true;
        else if (context.canceled)
            LightController.lightActive = false;
    }

    public void OnReflectLight(InputAction.CallbackContext context)
    {
        if (context.performed)
            LightController.lightActive = true;
        else if (context.canceled)
            LightController.lightActive = false;
    }

    private void FixedUpdate()
    {
        Move();
        LightController.ChangeDirection(transform.position, transform.GetChild(0).GetChild(0).forward);  
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
    
    void CharacterRotate()
    {
        CharBody.transform.Rotate(0, 5.0f, 0.0f, Space.Self);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PlayerCollissions = GetComponent<PlayerCollissions>();
    }

}
