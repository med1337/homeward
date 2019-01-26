using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckHunter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform head;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameObject.FindObjectOfType<BirbController>().transform);
        if (Input.GetKeyUp(KeyCode.Space))
        {
            var go = GameObject.Instantiate(bullet, head.transform.position,Quaternion.identity);
            Transform tr = GameObject.FindObjectOfType<BirbController>().transform;
            //tr.position = 

            //var primitive = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //GameObject.Instantiate(primitive, trcp.position, Quaternion.identity);
            var rnd = Random.Range(-5f, 5f);
            //tr.position += Vector3.forward * rnd;
            go.transform.LookAt(tr) ;
            var rigid = go.GetComponent<Rigidbody>();
            rigid.AddForce(transform.forward * 1000f);
            Debug.Log(rnd);
        }
    }
}
