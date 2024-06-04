using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerControls : MonoBehaviour
{
    Rigidbody rb;
    public float speed, senstivity;
    public Vector2 move;
    public float maxForce;
    public Transform body;
    public int playerNumber;
    public MirrorType mirrorType;
   

    private void FixedUpdate()
    {
            rb = transform.GetComponent<Rigidbody>();
            body = transform.GetChild(0);
        Move();
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

        body.Rotate(0, degreeNumber, 0.0f, Space.Self);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

}

public enum MirrorType{

    TYPE1,TYPE2, TYPE3, TYPE4

}
