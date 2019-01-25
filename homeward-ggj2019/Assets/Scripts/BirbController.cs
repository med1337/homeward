using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirbController : MonoBehaviour
{
    public float EdgeDistance = 5;
    public float TurnRotation = 30;
    public int Responsiveness = 2;
    int forceApplied = 0; 
    int moveForce = 0;
    int prevMoveForce = 0;
    Vector3 currentAngle;
    Vector3 targetAngle; 

    void FixedUpdate()
    {
        //Set target velocity dependent on input 
        forceApplied = 0;
        if (Input.GetKey(KeyCode.A))
            forceApplied = -100; 

        if (Input.GetKey(KeyCode.D))
            forceApplied = 100; 

        //Slowly increment towards target velocity
        if (forceApplied < moveForce)
            moveForce -= Responsiveness;
        if (forceApplied > moveForce)
            moveForce += Responsiveness;

        //Rotate dependent on direction
        if (moveForce > 10)
            targetAngle = new Vector3(0, 0, -TurnRotation);
        else if (moveForce < -10)
            targetAngle = new Vector3(0, 0, TurnRotation);
        else
            targetAngle = new Vector3(0, 0, 0);

        currentAngle = new Vector3(
        Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime),
        Mathf.LerpAngle(currentAngle.y, targetAngle.y, Time.deltaTime),
        Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime));
        transform.eulerAngles = currentAngle;

        //Cap movement at edges
        if (transform.position.x < EdgeDistance)
            moveForce += Responsiveness*2;
        if (transform.position.x > -EdgeDistance)
            moveForce -= Responsiveness*2;

        //Apply updated velocity
        GetComponent<Rigidbody>().velocity = new Vector3(moveForce/10, 0, 10);

        //Debug log velocity 
        //if (moveForce != prevMoveForce)
        //{
        //    Debug.Log(moveForce);
        //    prevMoveForce = moveForce; 
        //}
    }
}
