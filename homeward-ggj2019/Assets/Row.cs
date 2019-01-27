using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Row : MonoBehaviour
{
    public List<GameObject> gameObjects = new List<GameObject>();
    public List<Tile> barrierTiles = new List<Tile>();
    public List<Tile> obstacleTiles = new List<Tile>();
    private BiomeType biomeType = BiomeType.NONE;

    public int index;


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
            StartCoroutine(DisableRow(other.transform));
        }
    }


    private IEnumerator DisableRow(Transform other)
    {
        PlaneSpawner.Instance.SpawnRow(index);
        while (Vector3.Distance(other.position, transform.position)<70f)
        {
            yield return new WaitForSeconds(1f);
        }
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }


    public void SpawnTiles(int width, GameObject tilePrefab, BiomeType _biomeType)
    {
        biomeType = _biomeType;
        for (int i = -width / 2; i <= width / 2; i++)
        {
            var go = GameObject.Instantiate(tilePrefab, transform);
            gameObjects.Add(go);
            go.GetComponent<Tile>().Init(biomeType);
            if (Mathf.Abs(i) >= (width - 5) / 2)
            {
                barrierTiles.Add(go.GetComponent<Tile>());
            }
            else
            {
                //go.GetComponent<FoliageSpawner>().Init(currentBiome.GetComponent<Biome>().BiomeType);

            }
            go.transform.position += transform.right * 10f * i;
            //go.transform.position += transform.forward * 10f * tilePrefab.transform.localScale.z * rowCounter;
        }
    }


    public void SpawnStuff()
    {
        if (index == 14)
        {
            foreach (var obstacleTile in obstacleTiles)
            {

                var transitionGroundSpawner = obstacleTile.GetComponent<TransitionGroundSpawner>();
                transitionGroundSpawner.ReplaceGround(biomeType);
            }
        }

        //barriers
        foreach (var barrierTile in barrierTiles)
        {
            barrierTile.GetComponent<BarrierSpawner>().SpawnRandomGameObject(biomeType);
        }

        //obstacles
        var chance = 25 + FlockManager.Instance.speed;
        var rnd = Random.Range(0, 100);
        if (rnd < chance)
        {
            var rndIndex = Random.Range(0, obstacleTiles.Count);
            obstacleTiles[rndIndex].GetComponent<ObstacleSpawner>().SpawnRandomGameObject(biomeType);
        }

        foreach (var obstacleTile in obstacleTiles)
        {
            var tree = obstacleTile.GetComponent<TreeSpawner>();
            var foliage = obstacleTile.GetComponent<Foliage2Spawner>();
            rnd = Random.Range(0, 100);
            if (rnd < chance)
            {
                switch (biomeType)
                {
                    case BiomeType.NONE:
                        break;
                    case BiomeType.MODERATE:
                        //tile.density = 0.1f;
                        tree.SpawnRandomGameObject(biomeType, 2);
                        break;
                    case BiomeType.LAKE:
                        tree.SpawnRandomGameObject(biomeType, 1);
                        break;
                    case BiomeType.DESERT:
                        //tree.density = 1.5f;
                        tree.SpawnRandomGameObject(biomeType, 1);
                        break;
                    case BiomeType.VOLCANO:
                        break;
                    case BiomeType.MOUNTAIN:
                        tree.SpawnRandomGameObject(biomeType, 1);
                        break;
                    case BiomeType.SNOW:
                        //tree.density = 0.01f;
                        tree.SpawnRandomGameObject(biomeType, 2);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            switch (biomeType)
            {
                case BiomeType.NONE:
                    break;
                case BiomeType.MODERATE:
                    //tile.density = 0.1f;
                    foliage.SpawnRandomGameObject(biomeType, 2);
                    break;
                case BiomeType.LAKE:
                    break;
                case BiomeType.DESERT:
                    //tree.density = 1.5f;
                    break;
                case BiomeType.VOLCANO:
                    foliage.SpawnRandomGameObject(biomeType, 1);
                    break;
                case BiomeType.MOUNTAIN:
                    foliage.SpawnRandomGameObject(biomeType, 3);
                    break;
                case BiomeType.SNOW:
                    //tree.density = 0.01f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        //other shit
        

    }


    public void ScanNeighbours()
    {
        foreach (var o in gameObjects)
        {
            var tile = o.GetComponent<Tile>();
            tile.ScanNeighbours();
            if (tile.emptyNeightbours >= 2)
            {
                obstacleTiles.Add(tile);
            }
        }
    }
}