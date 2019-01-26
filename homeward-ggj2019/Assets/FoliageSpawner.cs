﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoliageSpawner : MonoBehaviour
{

    [SerializeField] private MeshCollider collider;
    [SerializeField] private List<GameObject> buildingPrefabs;
    [SerializeField] private List<GameObject> fieldPrefabs;
    [SerializeField] private List<GameObject> treePrefabs;
    [SerializeField] private List<GameObject> foliagePrefabs;
    private bool stop;
    private List<GameObject> trees = new List<GameObject>();
    //public Tile currentTile;
    int counter = 0;
    int spawned = 0;

    public bool spawnTrees = false;
    public bool spawnHills = false;
    // Use this for initialization
    void Start()
    {
        if (spawnTrees)
        {
            StartCoroutine(SpawnBuildingCR());
        }

        if (spawnHills)
        {
           // StartCoroutine(SpawnFieldCR());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            Debug.Log("CR");
            StartCoroutine(SpawnMaximumTrees());
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            StartCoroutine(SpawnBuildingCR());
            Debug.Log("CR");
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            foreach (var item in trees)
            {
                Destroy(item);
            }
            trees.Clear();
        }
    }

    IEnumerator SpawnBuildingCR()
    {
        spawned = 0;
        counter = 0;
        stop = false;
        var chance = Random.Range(0, 100);
        Debug.Log(chance);
        if (chance < 95)
        {
            SpawnTrees();
            yield break;
        }
        while (!stop)
        {
            if (counter > 0 || spawned > 0)
            {
                SpawnTrees();
                yield break;
            }
            var prefab = buildingPrefabs[Random.Range(0, buildingPrefabs.Count)];

            StartCoroutine(Spawn(prefab));
            yield return new WaitForSeconds(Random.Range(0f, 0.01f));
        }

    }

    public void SpawnTrees()
    {
        StartCoroutine(SpawnMaximumTrees());
    }

    public void SpawnFields()
    {
        StartCoroutine(SpawnFieldCR());
    }

    IEnumerator SpawnFieldCR()
    {
        spawned = 0;
        counter = 0;
        stop = false;
        while (!stop)
        {
            if (counter > 0 || spawned > 0)
            {
                StartCoroutine(SpawnMaximumTrees());
                yield break;
            }
            SpawnField();
            yield return new WaitForSeconds(Random.Range(0f, 0.01f));
        }

    }

    IEnumerator SpawnMaximumTrees()
    {
        counter = 0;
        spawned = 0;
        stop = false;
        var maxTrees = Random.Range(0, 3);

        if (maxTrees < 2)
        {
            StartCoroutine(SpawnGrass());
            yield break;
        }
        while (!stop)
        {
            if (counter > maxTrees/2)
            {
                StartCoroutine(SpawnGrass());
                yield break;
            }
            SpawnRandomTree();
            yield return new WaitForSeconds(Random.Range(0f, 0.01f));
        }

    }
    void SpawnField()
    {

        var prefab = fieldPrefabs[Random.Range(0, fieldPrefabs.Count)];
        StartCoroutine(Spawn(prefab));

    }

    IEnumerator SpawnGrass()
    {
        counter = 0;
        spawned = 0;
        stop = false;
        while (!stop)
        {
            if (counter > 3)
            {
                yield break;
            }
            SpawnRandomFoliage();
            yield return new WaitForSeconds(Random.Range(0f, 0.01f));
            //yield return new WaitForSeconds(Random.Range(0f, 0.1f));
        }
        yield return null;
    }

    void SpawnRandomFoliage()
    {
        var prefab = foliagePrefabs[Random.Range(0, foliagePrefabs.Count)];
        StartCoroutine(Spawn(prefab));

    }


    void SpawnRandomTree()
    {
        var prefab = treePrefabs[Random.Range(0, treePrefabs.Count)];
        StartCoroutine(Spawn(prefab));

    }

   

    IEnumerator Spawn(GameObject prefab)
    {
        var bounds = collider.sharedMesh.bounds;
        //var objbounds = prefab.GetComponent<SpawnedObject>().myCollider.bounds.extents ;
        //Debug.Log(objbounds);
        var readyToSpawn = true;
        var position = Vector3.zero;
        readyToSpawn = true;
        var newX = Random.Range(bounds.min.x, bounds.max.x );
        var newZ = Random.Range(bounds.min.z, bounds.max.z);
        var newY = bounds.max.y * 2;

        position = new Vector3(newX, newY, newZ) + transform.position;

        position = collider.ClosestPoint(position);

        //RaycastHit[] hits = Physics.BoxCastAll(position, objbounds, Vector3.one);
        RaycastHit[] hits = Physics.SphereCastAll(new Ray(position, new Vector3(1, 1, 1)), 0.5f);
        foreach (var item in hits)
        {
            if (item.collider.gameObject.layer == prefab.gameObject.layer)
            {
                readyToSpawn = false;
                counter++;
                yield break;
            }

        }

        //Ray ray = new Ray(position+Vector3.up*1000,Vector3.down);
        //RaycastHit hit = new RaycastHit();
        
        //if(Physics.Raycast(ray,out hit))
        //{
        //    if(!hit.collider.isTrigger)
        //        position = hit.point - Vector3.up*0.2f;
        //}



        var go = Instantiate(prefab, transform.GetChild(0));
        go.transform.position = position;
        var randomY = Random.Range(0f, 180f);
        go.transform.Rotate(Vector3.up,randomY);
        yield return new WaitForEndOfFrame();
        //go.transform.localScale = go.transform.localScale * Random.Range(0.6f, 1.6f);
        //if (currentTile)
        //    go.GetComponent<SpawnedObject>().audioSource = currentTile.audioSource;
        //go.GetComponent<SpawnedObject>().PlaySound();
        //go.GetComponent<SpawnedObject>().desiredScale = Vector3.one * Random.Range(0.8f, 1.2f);
        spawned++;
        trees.Add(go);
        //go.transform.SetParent(transform);
    }


    public void DestroyTile(Transform _transform)
    {
        StartCoroutine(WaitForDistance(_transform));
    }


    private IEnumerator WaitForDistance(Transform _transform)
    {
        var timer = Time.time;
        var distance = Vector3.Distance(_transform.position, transform.position);
        while (distance < 25f)
        {
            distance = Vector3.Distance(_transform.position, transform.position);
            yield return null;
        }
        Debug.Log(Time.time - timer);
        GetComponentInParent<PlaneSpawner>().SpawnTile();
        Destroy(gameObject);
    }

}
