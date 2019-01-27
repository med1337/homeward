using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : MonoBehaviour
{
    private Vector3 mouthPos;

    [SerializeField] GameObject rockPrefab;

    [SerializeField] float rockSpeed = 1.0f;


    private float timer = 0.0f;
    [SerializeField] float rockSpawnDelay = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        mouthPos = transform.position;
        mouthPos.y += 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > rockSpawnDelay)
        {
            timer = 0.0f;
            SpawnRock();
        }
    }

    void SpawnRock()
    {
        GameObject rock = Instantiate(rockPrefab, mouthPos, Random.rotation);
        rock.transform.localScale *= Random.Range(0.8f, 1.2f);

        Vector3 dir = Vector3.up;
        
        dir.x += Random.Range(-0.5f, 0.5f);
        dir.z += Random.Range(-0.5f, 0.5f);

        rock.GetComponent<Rigidbody>().AddForce(dir * rockSpeed, ForceMode.Impulse);
    }
}
