using System.Collections;
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
    private int biomCounter = 0;

    private List<GameObject> gameObjects;
    public static PlaneSpawner Instance = null;
    public int Level = 1;


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
        for (int i = 0; i < 14; i++)
        {
            SpawnRow();

        }
        //StartCoroutine(SpawnTiles());
        //StartCoroutine(DestroyTiles());
    }


    private void SpawnBiome()
    {
        currentBiome = nextBiome ? nextBiome : GameObject.Instantiate(biomePrefab, transform.GetChild(0));
        nextBiome = GameObject.Instantiate(biomePrefab, transform.GetChild(0));
        biomCounter++;
    }


    public void SpawnRow()
    {
        if (rowCounter % 15 == 0 )
        {
            SpawnBiome();
        }
        var row =Instantiate(rowPrefab, currentBiome.transform);
        row.transform.position += transform.forward * 10f * rowCounter;
        row.name = "Row" + rowCounter;
        for (int i = -width / 2; i <= width / 2; i++)
        {
            var go = GameObject.Instantiate(tilePrefab, row.transform);
            gameObjects.Add(go);
            if (Mathf.Abs(i) >= (width - 5) / 2)
            {
                go.GetComponent<FoliageSpawner>().Init(BiomeType.NONE);
            }
            else
            {
                go.GetComponent<FoliageSpawner>().Init(currentBiome.GetComponent<Biome>().BiomeType);

            }
            go.transform.position += transform.right * 10f * i;
            //go.transform.position += transform.forward * 10f * tilePrefab.transform.localScale.z * rowCounter;
        }

        rowCounter++;
       
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
            Level++;
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
