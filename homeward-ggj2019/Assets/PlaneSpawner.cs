﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;

    private int counter = 0;

    private List<GameObject> gameObjects;
    // Start is called before the first frame update
    void Start()
    {
        gameObjects = new List<GameObject>();
        StartCoroutine(SpawnTiles());
        //StartCoroutine(DestroyTiles());
    }


    private IEnumerator DestroyTiles()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            DestroyTile();
            yield return new WaitForSeconds(1f);
        }
    }


    private void DestroyTile()
    {

        Destroy(gameObjects[0]);
        gameObjects.RemoveAt(0);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            foreach (var o in gameObjects)
            {
               
                o.GetComponent<FoliageSpawner>();
            }

        }
    }


    private IEnumerator SpawnTiles()
    {
        while (counter <20)
        {
            SpawnTile();
                yield return null;
        }
    }

    public void SpawnTile()
    {
        var go = GameObject.Instantiate(tilePrefab, transform);
        gameObjects.Add(go);
        go.transform.position += transform.forward * 10f *tilePrefab.transform.localScale.z* counter;
        counter++;


    }
}
