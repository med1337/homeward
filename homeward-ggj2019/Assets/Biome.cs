using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BiomeType
{
    NONE = 0,
    MODERATE = 1,
    LAKE = 2
}

public class Biome : MonoBehaviour
{
    


    public BiomeType BiomeType = BiomeType.MODERATE;
    // Start is called before the first frame update
    void Start()
    {
        if(PlaneSpawner.Instance.Level<2)
        BiomeType = (BiomeType)PlaneSpawner.Instance.Level;
        else
        {
            BiomeType = BiomeType.LAKE;
        }
        //BiomeType = (BiomeType) Random.Range(1, 3);
        //gameObject.name = BiomeType.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
