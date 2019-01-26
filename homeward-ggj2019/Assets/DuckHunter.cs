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
        if (FlockManager.Instance.leader)
        {
            var leader = FlockManager.Instance.leader.transform;
            transform.LookAt(leader);
            if (Input.GetKeyUp(KeyCode.Space))
            {
                var go = GameObject.Instantiate(bullet, head.transform.position, Quaternion.identity);
                var newGo = new GameObject();
                newGo.transform.rotation = leader.rotation;

                var rnd = Random.Range(-5f, 5f); ;
                newGo.transform.position = leader.position + leader.transform.forward * rnd ;
                
                go.transform.LookAt(newGo.transform);
                var rigid = go.GetComponent<Rigidbody>();
                rigid.AddForce(transform.forward * 500f);
                Debug.Log(rnd);
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        //Ray weaponRay = new Ray(head.transform.position, FlockManager.Instance.leader.transform.position-head.transform.position);
        //RaycastHit[] hitInfo = Physics.RaycastAll(head.transform.position, head.transform.forward, 1000);
        //if (hitInfo.Length <=2)
        //{

            Gizmos.color = Color.green;
        //}
        //else
        //{
        //    Gizmos.color = Color.red;
        //}
        if(FlockManager.Instance.leader)
        Gizmos.DrawLine(head.transform.position, FlockManager.Instance.leader.transform.position);

    }


}
