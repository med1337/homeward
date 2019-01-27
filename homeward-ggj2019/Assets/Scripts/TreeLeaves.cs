using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLeaves : MonoBehaviour
{
    [SerializeField] ParticleSystem leaves;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FlockMember>())
        {
            ParticleSystem leaf = Instantiate(leaves, other.transform.position, transform.rotation);

            Destroy(leaf.gameObject, 6);
        }
    }
}
