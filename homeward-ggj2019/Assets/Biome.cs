using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum BiomeType
{
    NONE = 0,
    MODERATE = 1,
    LAKE = 2,
    DESERT = 3,
    VOLCANO = 4,
    MOUNTAIN = 5,
    SNOW=6

}

public class Biome : MonoBehaviour
{

    public BiomeType BiomeType = BiomeType.MODERATE;
    public List<Row> Rows = new List<Row>();

    public List<Tile> SpawnableTiles = new List<Tile>();

    private int width;
    private GameObject tilePrefab;
    private int rowcounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        //BiomeType = (BiomeType) Random.Range(1, 3);
        //gameObject.name = BiomeType.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        //var comp = other.GetComponent<Camera>();
        //if (comp)
        //{
        //    PlaneSpawner.Instance.PopulateNextBiome();
        //    switch (BiomeType)
        //    {
        //        case BiomeType.NONE:
        //            break;
        //        case BiomeType.MODERATE:
        //            FlockManager.Instance.SetFlyingHeight(7f);
        //            break;
        //        case BiomeType.LAKE:
        //            FlockManager.Instance.SetFlyingHeight(4f);
        //            break;
        //        case BiomeType.DESERT:
        //            FlockManager.Instance.SetFlyingHeight(4f);
        //            break;
        //        case BiomeType.VOLCANO:
        //            FlockManager.Instance.SetFlyingHeight(4f);
        //            break;
        //        case BiomeType.MOUNTAIN:
        //            FlockManager.Instance.SetFlyingHeight(14f);
        //            break;
        //        case BiomeType.SNOW:
        //            FlockManager.Instance.SetFlyingHeight(15f);
        //            break;
        //    }
        //}

    }



    public void Init(int width, GameObject rowPrefab, GameObject tilePrefab, bool firstBiome = false)
    {
        BiomeType = (BiomeType)PlaneSpawner.Instance.Level;
        gameObject.name = BiomeType.ToString();
        for (int i = 0; i < 15; i++)
        {
            var row = Instantiate(rowPrefab, transform);
            row.transform.position += transform.forward * 10f * i;
            row.name = "Row" + i;
            var rowComponent =row.GetComponent<Row>();
            rowComponent.index = i;
            rowComponent.SpawnTiles(width, tilePrefab, BiomeType);
            Rows.Add(rowComponent);

        }
        //PlaneSpawner.Instance.SpawnBiome();
        
        foreach (var row in Rows)
        {
            row.ScanNeighbours();
            //foreach (var rowGameObject in row.gameObjects)
            //{
            //    var tile = rowGameObject.GetComponent<Tile>();
            //    tile.ScanNeighbours();
            //    if (tile.emptyNeightbours >= 2)
            //    {
            //        SpawnableTiles.Add(tile);
            //    }
            //}
        }


        if (firstBiome)
        {
            foreach (var row in Rows)
            {
                PopulateRow();
            }
        }
        //if (firstBiome)
        //{
        //    var chance = 10 + FlockManager.Instance.speed;
        //    for (int i = 0; i < 15; i++)
        //    {
        //        var rnd = Random.Range(0, 100);
        //        if (rnd < chance)
        //        {
        //            var rndIndex = Random.Range(0, 5);
        //            var index = (i * 5) + rndIndex;
        //            SpawnableTiles[index].GetComponent<ObstacleSpawner>().SpawnRandomGameObject(BiomeType);
        //            //i += Random.Range(3, 5);
        //        }
        //    }

        //    foreach (var row in Rows)
        //    {

        //        foreach (var rowGameObject in row.gameObjects)
        //        {
        //            if (rowGameObject.GetComponent<Tile>().TileType != TileType.BARRIER)
        //            {
        //                var rnd = Random.Range(0, 100);
        //                if (rnd < chance)
        //                {
        //                    var tree = rowGameObject.GetComponent<TreeSpawner>();
        //                    var foliage = rowGameObject.GetComponent<Foliage2Spawner>();
        //                    switch (BiomeType)
        //                    {
        //                        case BiomeType.NONE:
        //                            break;
        //                        case BiomeType.MODERATE:
        //                            //tile.density = 0.1f;
        //                            tree.SpawnRandomGameObject(BiomeType, 2);
        //                            foliage.SpawnRandomGameObject(BiomeType, 5);
        //                            break;
        //                        case BiomeType.LAKE:
        //                            break;
        //                        case BiomeType.DESERT:
        //                            //tree.density = 1.5f;
        //                            tree.SpawnRandomGameObject(BiomeType, 1);
        //                            break;
        //                        case BiomeType.VOLCANO:
        //                            break;
        //                        case BiomeType.MOUNTAIN:
        //                            tree.SpawnRandomGameObject(BiomeType, 1);
        //                            foliage.SpawnRandomGameObject(BiomeType, 3);
        //                            break;
        //                        case BiomeType.SNOW:
        //                            //tree.density = 0.01f;
        //                            tree.SpawnRandomGameObject(BiomeType, 2);
        //                            break;
        //                        default:
        //                            throw new ArgumentOutOfRangeException();
        //                    }
        //                }
        //            }



        //        }
        //    }
        //}

      
    }


    private IEnumerator InitCoroutine()
    {
        yield return null;
     
    }


    // Update is called once per frame
    void Update()
    {

    }


    public void PopulateRow()
    {
        if (rowcounter < Rows.Count)
        {
            var row = Rows[rowcounter];
            row.SpawnStuff();
            rowcounter++;

        }
    }
}
