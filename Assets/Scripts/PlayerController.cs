using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject CharBody;
    public float speed, senstivity;
    private Vector2 move;
    public float maxForce;
    public LightController LightController;
    
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
        Debug.Log("Push");
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
    }

}
