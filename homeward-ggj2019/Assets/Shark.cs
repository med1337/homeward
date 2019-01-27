using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    [SerializeField] SphereCollider colliderRef;
    private Vector3 targetPos;
    private Rigidbody rigid;

    private float turnSpeed = 500.0f;
    private float jumpSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        colliderRef.radius = Random.Range(8, 12);
        jumpSpeed = Random.Range(8, 12);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 targetDirection = targetPos - transform.position;
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * turnSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<FlockMember>())
        {
            rigid.useGravity = true;
            //targetPos = other.transform.position;
            //targetPos.z += Random.Range(50, 70);
            //rigid.AddForce((other.transform.position - transform.position) * jumpSpeed, ForceMode.Impulse);

            rigid.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }
}
