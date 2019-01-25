using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyCamera : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    private Vector3 displacement; 

    private void Start()
    {
        displacement = new Vector3(target.transform.position.x - transform.position.x,
                                   target.transform.position.y - transform.position.y,
                                   target.transform.position.z - transform.position.z);
    }
    void FixedUpdate()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = new Vector3(0, 
                                            target.transform.position.y - displacement.y,
                                            target.transform.position.z - displacement.z);

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
