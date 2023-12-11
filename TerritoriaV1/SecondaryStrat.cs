using Godot;
using System;
using System.Collections.Generic;
using TerritoriaV1;

public partial class SecondaryStrat : BuildingStrategy
{
    public SecondaryStrat()
    {
        
    }

    public List<Placeable> BuildNewPlaceable(int[] totalResources, int[] neededResources, Placeable[,] placeables, PlaceableFactory factory)
    {
        // si on a plus de glace et de houblon que ce que l'on consomme
        if((neededResources[(int)ResourceType.HOP]*1.5 < totalResources[(int)ResourceType.HOP]) && (neededResources[(int)ResourceType.ICE]*1.5< totalResources[(int)ResourceType.ICE]))
        {
            factory.CreateBeerUsine();  // update les autres truc (cf primary)
        }

        while(totalResources[(int)ResourceType.WOOD] > 10)  // on dépense tout le bois en maison lol ( )
        {
            factory.CreateHouse();
            totalResources[(int)ResourceType.WOOD] -= 10;
        }

        return null;
    }
    
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
