using Godot;
using System;
using System.Collections.Generic;
using TerritoriaV1;

public partial class TertiaryStrat : BuildingStrategy
{
    public TertiaryStrat()
    {
    }

    public List<Placeable> BuildNewPlaceable(int[] totalResources, int[] neededResources, Placeable[,] placeables, PlaceableFactory factory)
    {
        return null;
    }
    public int[,] GetExchangesRates()
    {
        int[,] exchangesRates = new[,]
        {
            { 1, 1, 2, 6 }, //import
            { 2, 1, 1, 6 } //export
        };
        return exchangesRates;
    }
}
