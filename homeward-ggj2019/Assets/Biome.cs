using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BiomeType
{
    NONE = 0,
    MODERATE = 1,
    LAKE = 2,
    DESERT = 3,
    VOLCANO = 4,
    MOUNTAIN = 5,

}

public class Biome : MonoBehaviour
{

    public BiomeType BiomeType = BiomeType.MODERATE;
    public List<Row> Rows = new List<Row>();

    public List<Tile> SpawnableTiles = new List<Tile>();


    // Start is called before the first frame update
    void Start()
    {
        //BiomeType = (BiomeType) Random.Range(1, 3);
        //gameObject.name = BiomeType.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        var comp = other.GetComponent<Camera>();
        if (comp)
        {
            PlaneSpawner.Instance.PopulateNextBiome();
        }

    }


    public void Init(int width, GameObject rowPrefab, GameObject tilePrefab)
    {
        BiomeType = (BiomeType)PlaneSpawner.Instance.Level;
        gameObject.name = BiomeType.ToString();
        for (int i = 0; i < 15; i++)
        {
            var row = Instantiate(rowPrefab, transform);
            row.transform.position += transform.forward * 10f * i;
            row.name = "Row" + i;
            var rowComponent =
                row.GetComponent<Row>();
            rowComponent.SpawnTiles(width, tilePrefab, BiomeType);
            Rows.Add(rowComponent);

        }


        //PlaneSpawner.Instance.SpawnBiome();

        foreach (var row in Rows)
        {
            foreach (var rowGameObject in row.gameObjects)
            {
                var tile = rowGameObject.GetComponent<Tile>();
                tile.ScanNeighbours();
                if (tile.emptyNeightbours == 4)
                {
                    SpawnableTiles.Add(tile);
                }
            }
        }

        var chance = 25 + PlaneSpawner.Instance.Level * PlaneSpawner.Instance.Level;
        for (int i = 0; i < 12; i++)
        {
            var rnd = Random.Range(0, 100);
            if (rnd < chance)
            {
                var rndIndex = Random.Range(0, 3);
                var index = (i * 3) + rndIndex;
                SpawnableTiles[index].GetComponent<ObstacleSpawner>().SpawnRandomGameObject(BiomeType);
                //i += Random.Range(0, 7- PlaneSpawner.Instance.Level);
            }
        }

        foreach (var row in Rows)
        {

            foreach (var rowGameObject in row.gameObjects)
            {
                var tile = rowGameObject.GetComponent<TreeSpawner>();
                tile.SpawnRandomGameObject(BiomeType);
            }
        }
    }



    // Update is called once per frame
    void Update()
    {

    }

}
