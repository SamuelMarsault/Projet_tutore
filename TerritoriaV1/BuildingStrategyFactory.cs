using Godot;
using System;
using System.Runtime.InteropServices;
using TerritoriaV1;

public partial class BuildingStrategyFactory
{
 
    public BuildingStrategy createPrimaryStrategy(TileType[,] tiles)
    {
        return new PrimaryStrat(tiles);
    }

    public BuildingStrategy createSecondaryStrategy(TileType[,] tiles)
    {
        return new SecondaryStrat(tiles);
    }

    public BuildingStrategy createTertiaryStrategy(TileType[,] tiles)
    {
        return new TertiaryStrat(tiles);
    }
}
