using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
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
        var comp = other.GetComponent<Camera>();
        if (comp)
        {
            StartCoroutine(DisableRow());
        }
    }


    private IEnumerator DisableRow()
    {
        PlaneSpawner.Instance.SpawnRow();
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
