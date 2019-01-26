using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float EdgeDistance = 5;
    public float TurnRotation = 60;
    public int Responsiveness = 2;
    public int speed = 10;
    float forceApplied = 0;
    int moveForce = 0;
    int prevMoveForce = 0;
    Vector3 currentAngle;
    Vector3 targetAngle;
    private Rigidbody rigid;
    private Vector2 fingerUpPosition;
    private Vector2 fingerDownPosition;
    public float extraPower = 1.0f;
    public float desiredX = 0.0f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }


    void Update()
    {
        
          
        if (Input.touches.Length == 0)
        {
            targetAngle = new Vector3(0, 0, 0);
            forceApplied = 0;
        }
        else
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                fingerUpPosition = Input.touches[0].position;
                fingerDownPosition = Input.touches[0].position;
            }

            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                //desiredX = (0.5f - fingerDownPosition.x / Screen.width) * -40f;
                fingerDownPosition = Input.touches[0].position;
                DetectSwipe();
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {

                forceApplied = 0;
            }


            //else if (touch.phase == TouchPhase.Stationary)
            //{
            //    forceApplied = 0.5f - touch.position.x / Screen.width;
            //    forceApplied *= -100f;
            //    Debug.Log(forceApplied);
            //}
        }
        if (Input.GetKey(KeyCode.A))
            forceApplied = -1;
        if (Input.GetKey(KeyCode.D))
            forceApplied = 1;

        if (transform.position.x < -20 )
        {
            forceApplied = 1;
        }
        else if (transform.position.x > 20)
        {
            forceApplied = -1;
        }
    }

    void FixedUpdate()
    {
        //Set target velocity dependent on input 
        //forceApplied = 0;



        //float distance = transform.position.x - desiredX;
        //rigid.AddForce(transform.right * distance * power);

        ////if (Input.touchCount > 0)
        ////{
        ////    if (Input.touches[0].position.x > Screen.width / 2)
        ////        forceApplied = 100;
        ////    if (Input.touches[0].position.x < Screen.width / 2)
        ////        forceApplied = -100;
        ////}

       

        //Slowly increment towards target velocity
        //if (forceApplied < moveForce)
        //    moveForce -= Responsiveness;
        //if (forceApplied > moveForce)
        //    moveForce += Responsiveness;

        //Rotate dependent on direction
        if (forceApplied > 0)
            targetAngle = new Vector3(0, 0, TurnRotation );
        else if (forceApplied < 0)
            targetAngle = new Vector3(0, 0, -TurnRotation );
       

        currentAngle = new Vector3(
        Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime * extraPower),
        180,
        Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime * extraPower));
        transform.eulerAngles = currentAngle;

        //Cap movement at edges
        //if (transform.position.x < EdgeDistance)
        //    moveForce += Responsiveness*2;
        //if (transform.position.x > -EdgeDistance)
        //    moveForce -= Responsiveness*2;

        //Apply updated velocity    
        var fdt = forceApplied * Time.deltaTime * extraPower;
        rigid.AddForce(Vector3.right * fdt * 1000f);
        //rigid.velocity = new Vector3(forceApplied , rigid.velocity.y, rigid.velocity.z);
    }



    void DetectSwipe()
    {
        //Check if Vertical swipe
        if (verticalMove() > 20f && verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if (fingerDownPosition.y - fingerUpPosition.y > 0)//up swipe
            {
                OnSwipeUp();
            }
            else if (fingerDownPosition.y - fingerUpPosition.y < 0)//Down swipe
            {
                OnSwipeDown();
            }
            fingerUpPosition = fingerDownPosition;
        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > 20f && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (fingerDownPosition.x - fingerUpPosition.x > 0)//Right swipe
            {
                float power = fingerDownPosition.x - fingerUpPosition.x;
                OnSwipeRight(Mathf.Abs(power));
            }
            else if (fingerDownPosition.x - fingerUpPosition.x < 0)//Left swipe
            {
                float power = fingerDownPosition.x - fingerUpPosition.x;
                OnSwipeLeft(Mathf.Abs(power));
            }
            fingerUpPosition = fingerDownPosition;
        }

        //No Movement at-all
        else
        {

        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    void OnSwipeUp()
    {
        Debug.Log("Swipe UP");
    }

    void OnSwipeDown()
    {
        Debug.Log("Swipe Down");
    }

    void OnSwipeLeft(float power)
    {
        extraPower = 1;
        if (power > 100)
        {
            extraPower = 3;
        }
        forceApplied = -1;
    }

    void OnSwipeRight(float power)
    {
        extraPower = 1;
        if (power > 100)
        {
            extraPower = 3;
        }
        forceApplied = 1;
    }
}
