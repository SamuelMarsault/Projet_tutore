using Godot;
using System;
using System.Runtime.InteropServices;
using TerritoriaV1;

public partial class BuildingStrategyFactory
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
