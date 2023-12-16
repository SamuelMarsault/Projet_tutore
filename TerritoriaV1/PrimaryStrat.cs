using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using TerritoriaV1;

public class PrimaryStrat : BuildingStrategy
{
    
    public PrimaryStrat(Placeable[,] placeables, TileType[,] tiles)
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

    private void Create(Placeable[,] placeables,TileType[,] tiles,PlaceableType placeable, PlaceableFactory factory)
{
    switch (placeable)
    {
        case PlaceableType.HOUSE:
            PlaceHouse(placeables,tiles,factory);
            break;
        case PlaceableType.FIELD:
            PlaceField(placeables,tiles,factory);
            break;
        case PlaceableType.ICE_USINE:
            PlaceIceUsine(placeables,tiles,factory);
            break;
        case PlaceableType.SAWMILL:
            PlaceSawmill(placeables,tiles,factory);
            break;
        case PlaceableType.BAR:
            PlaceBar(placeables,tiles,factory);
            break;
        case PlaceableType.BEER_USINE:
            PlaceBeerUsine(placeables,tiles,factory);
            break;
        default:
            break;
    }
}

    private void PlaceHouse(Placeable[,] placeables,TileType[,] tiles,PlaceableFactory factory)
{
    for (int i = 0; i < placeables.GetLength(0); i++)
    {
        for (int j = 0; j < placeables.GetLength(0); j++)
        {
            if (CanPlaceAtLocation(placeables,tiles,i, j, TileType.GRASS) && HasAdjacentPlaceableOfType(placeables,i, j, PlaceableType.HOUSE))
            {
                placeables[i, j] = factory.CreateHouse();
                return;
            }
        }
    }
    PlaceRandomly(placeables,tiles,factory, TileType.GRASS,PlaceableType.HOUSE);
}

private void PlaceField(Placeable[,] placeables,TileType[,] tiles,PlaceableFactory factory)
{
    for (int i = 0; i < placeables.GetLength(0); i++)
    {
        for (int j = 0; j < placeables.GetLength(0); j++)
        {
            if (CanPlaceAtLocation(placeables,tiles,i, j, TileType.GRASS) && HasAdjacentPlaceableOfType(placeables,i, j, PlaceableType.BEER_USINE))
            {
                placeables[i, j] = factory.CreateField();
                return;
            }
        }
    }
    PlaceRandomly(placeables,tiles,factory, TileType.GRASS,PlaceableType.FIELD);
}

private void PlaceIceUsine(Placeable[,] placeables,TileType[,] tiles,PlaceableFactory factory)
{
    for (int i = 0; i < placeables.GetLength(0); i++)
    {
        for (int j = 0; j < placeables.GetLength(0); j++)
        {
            if (CanPlaceAtLocation(placeables,tiles,i, j, TileType.WATER) && (HasAdjacentPlaceableOfType(placeables,i, j, PlaceableType.BEER_USINE )||HasAdjacentPlaceableOfType(placeables,i, j, PlaceableType.ICE_USINE )))
            {
                placeables[i, j] = factory.CreateIceUsine();
                return;
            }
        }
    }
     PlaceRandomly(placeables,tiles,factory, TileType.GRASS,PlaceableType.ICE_USINE);
}

private void PlaceSawmill(Placeable[,] placeables,TileType[,] tiles,PlaceableFactory factory)
{
        for (int i = 0; i < placeables.GetLength(0); i++)
    {
        for (int j = 0; j < placeables.GetLength(0); j++)
        {
            if (CanPlaceAtLocation(placeables,tiles,i, j, TileType.FOREST) && HasAdjacentPlaceableOfType(placeables,i, j, PlaceableType.SAWMILL))
            {
                placeables[i, j] = factory.CreateSawmill();
                return;
            }
        }
    }
    PlaceRandomly(placeables,tiles,factory, TileType.GRASS,PlaceableType.SAWMILL);
}

private void PlaceBar(Placeable[,] placeables,TileType[,] tiles,PlaceableFactory factory)
{
      for (int i = 0; i < placeables.GetLength(0); i++)
    {
        for (int j = 0; j < placeables.GetLength(0); j++)
        {
            if (CanPlaceAtLocation(placeables,tiles,i, j, TileType.GRASS) && HasAdjacentPlaceableOfType(placeables,i, j, PlaceableType.BEER_USINE))
            {
                placeables[i, j] = factory.CreateBar();
                return;
            }
        }
    }
    PlaceRandomly(placeables,tiles,factory, TileType.GRASS,PlaceableType.BAR);
}

private void PlaceBeerUsine(Placeable[,] placeables,TileType[,] tiles,PlaceableFactory factory)
{
      for (int i = 0; i < placeables.GetLength(0); i++)
    {
        for (int j = 0; j < placeables.GetLength(0); j++)
        {
            if (CanPlaceAtLocation(placeables,tiles,i, j, TileType.GRASS) && HasAdjacentPlaceableOfType(placeables,i, j, PlaceableType.BEER_USINE))
            {
                placeables[i, j] = factory.CreateBeerUsine();
                return;
            }
        }
    }
    PlaceRandomly(placeables,tiles,factory, TileType.GRASS,PlaceableType.BEER_USINE);
}

private bool CanPlaceAtLocation(Placeable[,] placeables,TileType[,] tiles,int x, int y, TileType targetTileType)
{
    return placeables[x, y] == null && tiles[x, y] == targetTileType;
}

private bool HasAdjacentPlaceableOfType(Placeable[,] placeables,int x, int y, PlaceableType type)
{
        if (IsValidLocation(placeables, x - 1, y) && placeables[x - 1, y]?.getPlaceableType() == type ||
            IsValidLocation(placeables,x + 1, y) && placeables[x + 1, y]?.getPlaceableType() == type ||
            IsValidLocation(placeables,x, y - 1) && placeables[x, y - 1]?.getPlaceableType() == type ||
            IsValidLocation(placeables,x, y + 1) && placeables[x, y + 1]?.getPlaceableType() == type)
        {
            return true;
        }

    return false;
}

private bool IsValidLocation(Placeable[,] placeables, int x, int y)
{
    return x >= 0 && x < placeables.GetLength(0) && y >= 0 && y < placeables.GetLength(1);
}

private void PlaceRandomly(Placeable[,] placeables,TileType[,] tiles, PlaceableFactory factory, TileType targetTileType, PlaceableType placeable)
{
    var rand = new Random();
    int x;
    int y;
    while (true)
    {
        x = rand.Next(placeables.GetLength(0));
        y = rand.Next(placeables.GetLength(0));
        if (CanPlaceAtLocation(placeables,tiles, x, y, targetTileType))
        {
             switch (placeable)
    {
        case PlaceableType.HOUSE:
            placeables[x, y] = factory.CreateHouse();
            break;
        case PlaceableType.FIELD:
            placeables[x, y] = factory.CreateField();
            break;
        case PlaceableType.ICE_USINE:
            placeables[x, y] = factory.CreateIceUsine();
            break;
        case PlaceableType.SAWMILL:
            placeables[x, y] = factory.CreateSawmill();
            break;
        case PlaceableType.BAR:
            placeables[x, y] = factory.CreateBar();
            break;
        case PlaceableType.BEER_USINE:
            placeables[x, y] = factory.CreateBeerUsine();
            break;
        default:
            break;
        }
            return;
        }
    }
}

}
