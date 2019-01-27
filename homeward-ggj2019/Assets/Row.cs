using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    public List<GameObject> gameObjects = new List<GameObject>();

    private BiomeType biomeType = BiomeType.NONE;
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
        //PlaneSpawner.Instance.SpawnRow();
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
                go.GetComponent<BarrierSpawner>().Populate(biomeType);
            }
            else
            {
                //go.GetComponent<FoliageSpawner>().Init(currentBiome.GetComponent<Biome>().BiomeType);

            }
            go.transform.position += transform.right * 10f * i;
            //go.transform.position += transform.forward * 10f * tilePrefab.transform.localScale.z * rowCounter;
        }
    }
}