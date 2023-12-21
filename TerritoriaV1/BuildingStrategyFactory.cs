using TerritoriaV1;

public class BuildingStrategyFactory
{
 
    public BuildingStrategy createPrimaryStrategy(Placeable[,] placeables, TileType[,] tiles)
    {
        return new PrimaryStrat(placeables, tiles);
    }

    public BuildingStrategy createSecondaryStrategy(Placeable[,] placeables, TileType[,] tiles)
    {
        return new SecondaryStrat(placeables, tiles);
    }

    public BuildingStrategy createTertiaryStrategy(Placeable[,] placeables, TileType[,] tiles)
    {
        return new TertiaryStrat(placeables, tiles);
    }
}
