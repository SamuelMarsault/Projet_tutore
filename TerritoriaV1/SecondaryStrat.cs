using Godot;
using System;
using System.Collections.Generic;
using TerritoriaV1;

public partial class SecondaryStrat : BuildingStrategy
{
    private Placeable[][] placeables;
    private TileType[][] tiles;

    public SecondaryStrat(Placeable[][] placeables, TileType[][] tiles)
    {
        this.placeables = placeables;
        this.tiles = tiles;
    }

    public List<Placeable> BuildNewPlaceable(int[] totalResources, int[] neededResources, Placeable[][] placeables, PlaceableFactory factory)
    {
        // si on a plus de glace et de houblon que ce que l'on consomme
        if((neededResources[(int)ResourceType.HOP]*1.5 < totalResources[(int)ResourceType.HOP]) && (neededResources[(int)ResourceType.ICE]*1.5< totalResources[(int)ResourceType.ICE]))
        {
            factory.CreateBeerUsine();  // update les autres truc (cf primary)
        }

        if(neededResources[(int)ResourceType.BEER] < totalResources[(int)ResourceType.BEER]) // le joueur a interet a exporter ses bieres si il veut pas qu'on construisent des bars partout
            {
                if(totalResources[(int)ResourceType.WOOD] > 10)
                {
                    factory.CreateBar();
                    totalResources[(int)ResourceType.WOOD] -=10;
                }
            }

        while(totalResources[(int)ResourceType.WOOD] > 10)  // on dépense tout le bois en maison lol ( )
        {
            factory.CreateHouse();
            totalResources[(int)ResourceType.WOOD] -= 10;
        }

        return null;
    }
}
