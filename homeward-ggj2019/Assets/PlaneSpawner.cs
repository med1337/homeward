using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject biomePrefab;
    private GameObject currentBiome;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private int width = 5;
    [SerializeField] private int biomlength = 15;
    private int rowCounter = 0;
    private int biomCounter = 0;

    private List<GameObject> gameObjects;
    // Start is called before the first frame update
    void Start()
    {
        gameObjects = new List<GameObject>();
        SpawnRow();
        //StartCoroutine(SpawnTiles());
        //StartCoroutine(DestroyTiles());
    }


    private void SpawnBiome()
    {
        currentBiome = GameObject.Instantiate(biomePrefab, transform.GetChild(0));
        biomCounter++;
    }


    private void SpawnRow()
    {
        if (rowCounter % 15 == 0 )
        {
            SpawnBiome();
        }
        var row =Instantiate(biomePrefab, currentBiome.transform);
        row.name = "Row" + rowCounter;
        for (int i = -width / 2; i <= width / 2; i++)
        {
            var go = GameObject.Instantiate(tilePrefab, row.transform);
            gameObjects.Add(go);
            go.transform.position += transform.right * 10f * i;
            go.transform.position += transform.forward * 10f * tilePrefab.transform.localScale.z * rowCounter;
        }

        rowCounter++;
        if (rowCounter % 15 == 0)
        {
            
        }
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
        if(Input.GetKeyUp(KeyCode.Return))
            SpawnRow();
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
