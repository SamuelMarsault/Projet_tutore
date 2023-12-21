using TerritoriaV1;

/// <summary>
/// Factory de BuildingStrategy
/// </summary>
public class BuildingStrategyFactory
{
 
    /// <summary>
    /// Construit une PrimaryStrategy
    /// </summary>
    /// <param name="placeables">Les Placeable du village</param>
    /// <param name="tiles">Le sol du village</param>
    public BuildingStrategy createPrimaryStrategy(Placeable[,] placeables, TileType[,] tiles)
    {
        return new PrimaryStrat(placeables, tiles);
    }

    /// <summary>
    /// Construit une SecondaryStrategy
    /// </summary>
    /// <param name="placeables">Les Placeable du village</param>
    /// <param name="tiles">Le sol du village</param>
    public BuildingStrategy createSecondaryStrategy(Placeable[,] placeables, TileType[,] tiles)
    {
        return new SecondaryStrat(placeables, tiles);
    }

    /// <summary>
    /// Construit une TertiaryStrategy
    /// </summary>
    /// <param name="placeables">Les Placeable du village</param>
    /// <param name="tiles">Le sol du village</param>
    public BuildingStrategy createTertiaryStrategy(Placeable[,] placeables, TileType[,] tiles)
    {
        return new TertiaryStrat(placeables, tiles);
    }
}
