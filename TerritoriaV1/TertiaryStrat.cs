using Godot;
using System;
using System.Collections.Generic;
using TerritoriaV1;

public class TertiaryStrat : BuildingStrategy
{
    public TertiaryStrat(Placeable[,] placeables,TileType[,] tiles)
    {
        SetTiles(tiles);
    }

    override 
    public Placeable[,] BuildNewPlaceable(int[] totalResources,
        int[] neededResources, PlaceableFactory factory, 
        TileType[] targetTile, Placeable[,] placeables, int[] resourcesBeforeProduct)
    {
        List<Placeable> newPlaceables = new List<Placeable>();
        if(neededResources[(int)ResourceType.HOP] < resourcesBeforeProduct[(int)ResourceType.HOP])
        {
            factory.Destroy(placeables, PlaceableType.FIELD);
        }
        if(neededResources[(int)ResourceType.ICE] < resourcesBeforeProduct[(int)ResourceType.ICE])
        {
            factory.Destroy(placeables, PlaceableType.ICE_USINE);
        }
        if(neededResources[(int)ResourceType.BEER] < resourcesBeforeProduct[(int)ResourceType.BEER])
        {
            factory.Destroy(placeables, PlaceableType.BEER_USINE);
            newPlaceables.Add(factory.CreateBar());
        }

        while(totalResources[(int)ResourceType.WOOD] > 10)  // on dépense tout le bois en maison lol ( )
        {
            newPlaceables.Add(factory.CreateHouse());
            totalResources[(int)ResourceType.WOOD] -= 10;
        }
        foreach (Placeable placeable in newPlaceables)
        {
            PlacePlaceable(placeables,placeable, targetTile[placeable.getPlaceableType().GetHashCode()]);
            Console.WriteLine(targetTile[placeable.getPlaceableType().GetHashCode()]+" "+placeable.getPlaceableType().GetHashCode());
        }
        return placeables;
    }
    override 
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
