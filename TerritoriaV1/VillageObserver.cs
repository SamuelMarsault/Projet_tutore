namespace TerritoriaV1;

public interface VillageObserver
{
    public void ReactToResourcesChange(int[] resources);
    public void ReactToExchangesRatesChange(int[,] exchangesRates);
    public void ReactToPlaceableChange(Placeable[,] placeables);
    public void ReactToTilesChange(TileType[,] tiles);
    public void ReactToImpossibleTransaction(int[] missingRessources);
}