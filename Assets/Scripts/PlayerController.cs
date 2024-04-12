using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed, senstivity;
    private Vector2 move;
    public float maxForce;

    
    
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnPush(InputAction.CallbackContext context)
    {
        Debug.Log("Push");
    }

    public void OnEmitLight(InputAction.CallbackContext context)
    {
        Debug.Log("EmitLight");
    }

    public void OnReflectLight(InputAction.CallbackContext context)
    {
        Debug.Log("ReflectLight");
    }

    private void FixedUpdate()
    {
        //if (GameController.instance.isPlaying)
         Move();
    }

    void Move()
    {

        //Target Velocity
        Vector3 currentVelocity = rb.velocity;

        /*
        if (currentVelocity.x > 0 || currentVelocity.z > 0 || currentVelocity.y > 0)
        {
            transform.GetChild(0).GetComponent<Animator>().SetFloat("Speed", 1);
        }
        else
        {
            transform.GetChild(0).GetComponent<Animator>().SetFloat("Speed", 0);
        }
        */

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
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //Cursor.lockState = CursorLockMode.Locked;
    }

}
