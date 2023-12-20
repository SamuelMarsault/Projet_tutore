using System.Collections.Generic;
using TerritoriaV1;

public class SecondaryStrat : BuildingStrategy
{

    public SecondaryStrat(Placeable[,] placeables,TileType[,] tiles)
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
            if(totalResources[(int)ResourceType.WOOD] > 10)
                {
                    newPlaceables.Add(factory.CreateBeerUsine());
                    totalResources[(int)ResourceType.WOOD] -=10;
                }
        }

        if(neededResources[(int)ResourceType.BEER] < resourcesBeforeProduct[(int)ResourceType.BEER]) // le joueur a interet a exporter ses bieres si il veut pas qu'on construisent des bars partout
        {
                if(totalResources[(int)ResourceType.WOOD] > 10)
                {
                    newPlaceables.Add(factory.CreateBar());
                    totalResources[(int)ResourceType.WOOD] -=10;
                }
        }

        for(int i = 0; i < 5 && (totalResources[(int)ResourceType.WOOD] > 10); i++)  // on dépense tout le bois en maison lol ( )
        {
            newPlaceables.Add(factory.CreateHouse());
            totalResources[(int)ResourceType.WOOD] -= 10;
        }
        foreach (Placeable placeable in newPlaceables)
        {
            PlacePlaceable(placeables,placeable, targetTile[placeable.getPlaceableType().GetHashCode()]);
        }

        Destroy(PlaceableType.SAWMILL,placeables);
        Destroy(PlaceableType.SAWMILL,placeables);
        Destroy(PlaceableType.FIELD,placeables);
        Destroy(PlaceableType.ICE_USINE,placeables);

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

     override public Placeable[,] PlacePlaceable(Placeable[,] placeables,Placeable placeable, TileType targetTile)
     {
        
            if(placeable == null)
            {
                //GD.Print("placeable est nulle");
            }
            if(placeables == null)
            {
                //GD.Print("placeables est nulle");
            }
            bool notPlaced = true;
            for (int i = 0; i < placeables.GetLength(0) && notPlaced; i++)
            {
                for (int j = 0; j < placeables.GetLength(1) && notPlaced; j++)
                {
                    if ((HasAdjacentPlaceableOfType(i, j, PlaceableType.TRAIN_STATION, placeables)||HasAdjacentPlaceableOfType(i, j, PlaceableType.RAIL, placeables)) && CanPlaceAtLocation(i, j, targetTile, placeables))
                    {
                        placeables[i, j] = placeable;
                        notPlaced = false;
                    }
                }
            }
               if (notPlaced)
                {
                    PlaceRandomly(targetTile, placeable, placeables);
                }

        return placeables;
    }
}
