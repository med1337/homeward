﻿using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject biomePrefab;
    [SerializeField] private GameObject rowPrefab;
    private GameObject currentBiome;
    private GameObject nextBiome;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private int width = 7;
    [SerializeField] private int biomlength = 15;
    private int rowCounter = 0;
    public int biomCounter = 0;

    private List<GameObject> gameObjects;
    public static PlaneSpawner Instance = null;
    public int Level = 0;


    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (Instance == null)

            //if not, set instance to this
            Instance = this;

        //If instance already exists and it's not this:
        else if (Instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);


    }

    // Start is called before the first frame update
    void Start()
    {
        gameObjects = new List<GameObject>();
        SpawnBiome();
        //StartCoroutine(SpawnTiles());
        //StartCoroutine(DestroyTiles());
    }


    public void SpawnBiome()
    {
        currentBiome = GameObject.Instantiate(biomePrefab, transform.GetChild(0));
        currentBiome.GetComponent<Biome>().Init(width, rowPrefab, tilePrefab);
        nextBiome = currentBiome;
        biomCounter++;
    }


    public void PopulateNextBiome()
    {
        Level++;
        nextBiome = GameObject.Instantiate(biomePrefab, transform.GetChild(0));
        nextBiome.transform.position += transform.forward * 150 * (Level-1);
        nextBiome.GetComponent<Biome>().Init(width, rowPrefab, tilePrefab);
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
    }


    private IEnumerator SpawnTiles()
    {
        while (rowCounter <15)
        {
            SpawnTile();
                yield return null;
        }
    }

    public void SpawnTile()
    {
        for (int i = -width/2; i <= width/2; i++)
        {
            var go = GameObject.Instantiate(tilePrefab, transform);
            gameObjects.Add(go);
            go.transform.position += transform.right * 10f * i;
            go.transform.position += transform.forward * 10f * tilePrefab.transform.localScale.z * rowCounter;
        }

        rowCounter++;


    }
}
