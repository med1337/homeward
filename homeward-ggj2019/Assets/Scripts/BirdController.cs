﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float EdgeDistance = 5;
    public float TurnRotation = 30;
    public int Responsiveness = 2;
    public int speed = 10; 
    int forceApplied = 0; 
    int moveForce = 0;
    int prevMoveForce = 0;
    Vector3 currentAngle;
    Vector3 targetAngle;
    private Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //Set target velocity dependent on input 
        forceApplied = 0;
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].position.x > Screen.width / 2)
                forceApplied = 100;
            if (Input.touches[0].position.x < Screen.width / 2)
                forceApplied = -100;
        }

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
            targetAngle = new Vector3(0, 0, TurnRotation);
        else if (moveForce < -10)
            targetAngle = new Vector3(0, 0, -TurnRotation);
        else
            targetAngle = new Vector3(0, 0, 0);

        currentAngle = new Vector3(
        Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime),
        180,
        Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime));
        transform.eulerAngles = currentAngle;

        //Cap movement at edges
        if (transform.position.x < EdgeDistance)
            moveForce += Responsiveness*2;
        if (transform.position.x > -EdgeDistance)
            moveForce -= Responsiveness*2;

        //Apply updated velocity        
        rigid.velocity = new Vector3(moveForce/10, rigid.velocity.y, rigid.velocity.z);
    }
}
