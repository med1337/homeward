using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    EMPTY = 0,
    FOLIAGE = 1,
    FOREST =2,
    PLAINS = 3,
    VILLAGE = 4,
    CITY = 5,
    MOUNTAINS = 6,
    BARRIER = 7
}

public class Tile : MonoBehaviour
{
    [SerializeField] private List<Transform> raycastPoints = new List<Transform>();
    
    public TileType TileType = TileType.EMPTY;


    public int emptyNeightbours = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ScanNeighbours()
    {
        if (TileType == TileType.BARRIER) return;
        emptyNeightbours = 0;
        foreach (var raycastPoint in raycastPoints)
        {
            var hits = Physics.SphereCastAll(raycastPoint.position, 1f, raycastPoint.transform.forward,1);
            foreach (var raycastHit in hits)
            {
                var comp = raycastHit.transform.GetComponent<Tile>();
                if (comp && comp != this && comp.TileType!=TileType.BARRIER)
                    emptyNeightbours++;
            }
        }
    }

}
