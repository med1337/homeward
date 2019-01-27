using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollection : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float destroyAfterSeconds = 5.0f;
    void Start()
    {
        Destroy(this.gameObject, destroyAfterSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
