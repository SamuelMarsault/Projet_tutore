using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using TerritoriaV1;

public class PrimaryStrat : BuildingStrategy
{
    
    public PrimaryStrat(TileType[,] tiles)
    {
        SetTiles(tiles);
    }
    override 
    public Placeable[,] BuildNewPlaceable(int[] totalResources,
        int[] neededResources, PlaceableFactory factory, 
        TileType[] targetTile, Placeable[,] placeables, int[] resourcesBeforeProduct)
    {
        List<Placeable> newPlaceables = new List<Placeable>();
            if(neededResources[(int)ResourceType.HOP] > resourcesBeforeProduct[(int)ResourceType.HOP])
            {
                if(totalResources[(int)ResourceType.WOOD] > 10)//si on a assez pour construire, on peut plutot tester ca dans Create
                {
                newPlaceables.Add(factory.CreateField());
                totalResources[(int)ResourceType.WOOD] -=10; // je met a jour manuellement les valeurs, on peux peut etre l'automatiser 
                }
            }

            if(neededResources[(int)ResourceType.ICE] > resourcesBeforeProduct[(int)ResourceType.ICE])
            {
                if(totalResources[(int)ResourceType.WOOD] > 10)
                {
                    newPlaceables.Add(factory.CreateIceUsine());
                    totalResources[(int)ResourceType.WOOD] -=10;
                }
            }

            if(neededResources[(int)ResourceType.WOOD] > resourcesBeforeProduct[(int)ResourceType.WOOD])    // on devrait la créer a chaque tour meme si on as assez de bois 
            {
                newPlaceables.Add(factory.CreateSawmill());
            }
        //newPlaceables.Add(factory.CreateHouse());
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
            { 3, 2, 3, 6 }, //import
            { 1, 1, 1, 6 } //export
        };
        return exchangesRates;
    }
}
