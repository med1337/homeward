using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-moveSpeed, 0.0f, 0.0f));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(moveSpeed, 0.0f, 0.0f));
        }

        //turn this in to translate???
        transform.Translate(new Vector3(GyroToUnity(Input.gyro.attitude).z, 0.0f,0.0f));

    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
