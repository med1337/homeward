﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Serializable]
    public class PrefabList
    {
        public GameObject[] Prefabs;
    }

    [Range(0.01f, 0.99f)] public float density;
    public MeshCollider SpawnAreaCollider;

    [Header("GRASS, LAKE, DESERT, VOLCANO, MOUNTAIN")]
    public PrefabList[] PrefabPerBiome;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public void SpawnRandomGameObject(BiomeType biomeType)
    {
        int index = (int)biomeType - 1;

        if (PrefabPerBiome[index] == null || PrefabPerBiome[index].Prefabs ==null) return;
        if(PrefabPerBiome[index].Prefabs.Length==0) return;
        var go = PrefabPerBiome[index].Prefabs[Random.Range(0, PrefabPerBiome[index].Prefabs.Length)];
        StartCoroutine(Spawn(go));
    }


    IEnumerator Spawn(GameObject prefab, bool maximum = false, int failLimit = 1)
    {
        int failCounter = 0;
        while (failCounter != failLimit)
        {
            var position = Vector3.zero;

            //get bounds of the mesh
            var bounds = SpawnAreaCollider.sharedMesh.bounds;
            //randomize between bounds
            var newX = Random.Range(bounds.min.x, bounds.max.x);
            var newZ = Random.Range(bounds.min.z, bounds.max.z);
            var newY = bounds.max.y * 2;

            //set position
            position = new Vector3(newX, newY, newZ) + transform.position;

            //get closest point on the plane
            position = SpawnAreaCollider.ClosestPoint(position);

            //var sqrX = Mathf.Pow(bounds.max.x - bounds.min.x, 2);
            //var sqrZ = Mathf.Pow(bounds.max.z - bounds.min.z, 2);
            //var radius = Mathf.Sqrt(sqrZ + sqrX) / 2f;
            //Debug.Log(radius);
            //get 
            var so = prefab.GetComponent<SpawnedObject>();

            //cast a ray and check 
            RaycastHit[] hits = Physics.SphereCastAll(new Ray(position, new Vector3(1, 1, 1)), so.radius);
            foreach (var item in hits)
            {
                if (item.collider.gameObject.layer == prefab.gameObject.layer)
                {
                    failCounter++;
                    yield break;
                }

            }


            var go = Instantiate(prefab, transform);
            go.transform.position = position;

            yield return new WaitForEndOfFrame();
        }
        
        ////go.transform.localScale = go.transform.localScale * Random.Range(0.6f, 1.6f);
        ////if (currentTile)
        ////    go.GetComponent<SpawnedObject>().audioSource = currentTile.audioSource;
        ////go.GetComponent<SpawnedObject>().PlaySound();
        ////go.GetComponent<SpawnedObject>().desiredScale = Vector3.one * Random.Range(0.8f, 1.2f);
        //spawned++;
        //if (solid)
        //    spawnedObjects.Add(go);
        ////go.transform.SetParent(transform);
    }

}