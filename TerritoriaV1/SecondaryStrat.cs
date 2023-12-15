using Godot;
using System;
using System.Collections.Generic;
using TerritoriaV1;

public class SecondaryStrat : BuildingStrategy
{

    public SecondaryStrat(TileType[,] tiles)
    {
        SetTiles(tiles);
    }
    override 
    public Placeable[,] BuildNewPlaceable(int[] totalResources,
        int[] neededResources, PlaceableFactory factory, 
        TileType[] targetTile, Placeable[,] placeables, int[] resourcesBeforeProduct)
    {
        List<Placeable> newPlaceables = new List<Placeable>();
        // si on a plus de glace et de houblon que ce que l'on consomme
        if((neededResources[(int)ResourceType.HOP]*1.5 < resourcesBeforeProduct[(int)ResourceType.HOP]) && (neededResources[(int)ResourceType.ICE]*1.5< totalResources[(int)ResourceType.ICE]))
        {
            newPlaceables.Add(factory.CreateBeerUsine());
        }

        if(neededResources[(int)ResourceType.BEER] < resourcesBeforeProduct[(int)ResourceType.BEER]) // le joueur a interet a exporter ses bieres si il veut pas qu'on construisent des bars partout
            {
                if(totalResources[(int)ResourceType.WOOD] > 10)
                {
                    newPlaceables.Add(factory.CreateBar());
                    totalResources[(int)ResourceType.WOOD] -=10;
                }
            }

        while(totalResources[(int)ResourceType.WOOD] > 100)  // on dépense tout le bois en maison lol ( )
        {
            newPlaceables.Add(factory.CreateHouse());
            totalResources[(int)ResourceType.WOOD] -= 100;
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
            { 1, 2, 2, 6 }, //import
            { 1, 1, 1, 6 } //export
        };
        return exchangesRates;
    }
}
