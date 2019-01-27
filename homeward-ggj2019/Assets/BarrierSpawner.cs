public class BarrierSpawner : Spawner
{
    public void Populate(BiomeType _biomeType)
    {
        GetComponent<Tile>().TileType = TileType.BARRIER;
        SpawnRandomGameObject(_biomeType,1);
    }
}