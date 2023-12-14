using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using TerritoriaV1;

public partial class PrimaryStrat : BuildingStrategy
{
    private Placeable[,] placeables;
    private TileType[,] tiles;
    public PrimaryStrat(Placeable[,] placeables, TileType[,] tiles)
    {
        this.placeables = placeables;
        this.tiles = tiles;
    }

    public List<Placeable> BuildNewPlaceable(int[] totalResources, int[] neededResources, Placeable[,] placeables, PlaceableFactory factory)
    {
        List<Placeable> newPlaceables = new List<Placeable>();
            if(neededResources[(int)ResourceType.HOP] > totalResources[(int)ResourceType.HOP])
            {
                if(totalResources[(int)ResourceType.WOOD] > 10)//si on a assez pour construire, on peut plutot tester ca dans Create
                {
                newPlaceables.Add(factory.CreateField());
                GD.Print("j'ai crée un champ !");
                totalResources[(int)ResourceType.WOOD] -=10; // je met a jour manuellement les valeurs, on peux peut etre l'automatiser 
                }
            }

            if(neededResources[(int)ResourceType.ICE] > totalResources[(int)ResourceType.ICE])
            {
                if(totalResources[(int)ResourceType.WOOD] > 10)
                {
                   /* Create(PlaceableType.ICE_USINE,factory);
                    newPlaceables = placeables;
                    totalResources[(int)ResourceType.WOOD] -=10;*/
                    GD.Print("j'ai crée une usine a glacon !");
                }
            }

               if(neededResources[(int)ResourceType.BEER] < totalResources[(int)ResourceType.BEER]) // je pensais avoir oublier bar et beerusine dans cette strat, donc je les ai rajouté, mais en fait il sont dans la strat suivante, il semblerais donc qu'il y a avait une raison pour ne pas en creer a ce moment 
            {
                if(totalResources[(int)ResourceType.WOOD] > 10)
                {
                    factory.CreateBar();
                    totalResources[(int)ResourceType.WOOD] -=10;
                    GD.Print("j'ai crée un bar !");
                }
            }

            if(neededResources[(int)ResourceType.ICE] < totalResources[(int)ResourceType.ICE] && neededResources[(int)ResourceType.HOP] < totalResources[(int)ResourceType.HOP])
            {
                factory.CreateBeerUsine();
                 totalResources[(int)ResourceType.WOOD] -=10;
                 GD.Print("j'ai crée une usine a biere!");
            }

            if(neededResources[(int)ResourceType.WOOD] > totalResources[(int)ResourceType.WOOD])    // on devrait la créer a chaque tour meme si on as assez de bois 
            {
                newPlaceables.Add(factory.CreateSawmill());
                GD.Print("j'ai crée une scierie !");
            }
        //newPlaceables.Add(factory.CreateHouse()); 
        foreach (Placeable placeable in newPlaceables) Console.WriteLine(placeable.getPlaceableType());
        return newPlaceables;
    }

    public int[,] GetExchangesRates()
    {
        int[,] exchangesRates = new[,]
        {
            { 3, 2, 3, 6 }, //import
            { 1, 1, 1, 6 } //export
        };
        return exchangesRates;
    }

    private void Create(PlaceableType placeable, PlaceableFactory factory)
{
    switch (placeable)
    {
        case PlaceableType.HOUSE:
            PlaceHouse(factory);
            break;
        case PlaceableType.FIELD:
            PlaceField(factory);
            break;
        case PlaceableType.ICE_USINE:
            PlaceIceUsine(factory);
            break;
        case PlaceableType.SAWMILL:
            PlaceSawmill(factory);
            break;
        case PlaceableType.BAR:
            PlaceBar(factory);
            break;
        case PlaceableType.BEER_USINE:
            PlaceBeerUsine(factory);
            break;
        default:
            break;
    }
}

    private void PlaceHouse(PlaceableFactory factory)
{
    for (int i = 0; i < placeables.GetLength(0); i++)
    {
        for (int j = 0; j < placeables.GetLength(0); j++)
        {
            if (CanPlaceAtLocation(i, j, TileType.GRASS) && HasAdjacentPlaceableOfType(i, j, PlaceableType.HOUSE))
            {
                placeables[i, j] = factory.CreateHouse();
                return;
            }
        }
    }
    PlaceRandomly(factory, TileType.GRASS,PlaceableType.HOUSE);
}

private void PlaceField(PlaceableFactory factory)
{
    for (int i = 0; i < placeables.GetLength(0); i++)
    {
        for (int j = 0; j < placeables.GetLength(0); j++)
        {
            if (CanPlaceAtLocation(i, j, TileType.GRASS) && HasAdjacentPlaceableOfType(i, j, PlaceableType.BEER_USINE))
            {
                placeables[i, j] = factory.CreateField();
                return;
            }
        }
    }
    PlaceRandomly(factory, TileType.GRASS,PlaceableType.FIELD);
}

private void PlaceIceUsine(PlaceableFactory factory)
{
    for (int i = 0; i < placeables.GetLength(0); i++)
    {
        for (int j = 0; j < placeables.GetLength(0); j++)
        {
            if (CanPlaceAtLocation(i, j, TileType.WATER) && (HasAdjacentPlaceableOfType(i, j, PlaceableType.BEER_USINE )||HasAdjacentPlaceableOfType(i, j, PlaceableType.ICE_USINE )))
            {
                placeables[i, j] = factory.CreateIceUsine();
                return;
            }
        }
    }
     PlaceRandomly(factory, TileType.GRASS,PlaceableType.ICE_USINE);
}

private void PlaceSawmill(PlaceableFactory factory)
{
        for (int i = 0; i < placeables.GetLength(0); i++)
    {
        for (int j = 0; j < placeables.GetLength(0); j++)
        {
            if (CanPlaceAtLocation(i, j, TileType.FOREST) && HasAdjacentPlaceableOfType(i, j, PlaceableType.SAWMILL))
            {
                placeables[i, j] = factory.CreateSawmill();
                return;
            }
        }
    }
    PlaceRandomly(factory, TileType.GRASS,PlaceableType.SAWMILL);
}

private void PlaceBar(PlaceableFactory factory)
{
      for (int i = 0; i < placeables.GetLength(0); i++)
    {
        for (int j = 0; j < placeables.GetLength(0); j++)
        {
            if (CanPlaceAtLocation(i, j, TileType.GRASS) && HasAdjacentPlaceableOfType(i, j, PlaceableType.BEER_USINE))
            {
                placeables[i, j] = factory.CreateBar();
                return;
            }
        }
    }
    PlaceRandomly(factory, TileType.GRASS,PlaceableType.BAR);
}

private void PlaceBeerUsine(PlaceableFactory factory)
{
      for (int i = 0; i < placeables.GetLength(0); i++)
    {
        for (int j = 0; j < placeables.GetLength(0); j++)
        {
            if (CanPlaceAtLocation(i, j, TileType.GRASS) && HasAdjacentPlaceableOfType(i, j, PlaceableType.BEER_USINE))
            {
                placeables[i, j] = factory.CreateBeerUsine();
                return;
            }
        }
    }
    PlaceRandomly(factory, TileType.GRASS,PlaceableType.BEER_USINE);
}

private bool CanPlaceAtLocation(int x, int y, TileType targetTileType)
{
    return placeables[x, y] == null && tiles[x, y] == targetTileType;
}

private bool HasAdjacentPlaceableOfType(int x, int y, PlaceableType type)
{
        if (IsValidLocation(x - 1, y) && placeables[x - 1, y]?.getPlaceableType() == type ||
            IsValidLocation(x + 1, y) && placeables[x + 1, y]?.getPlaceableType() == type ||
            IsValidLocation(x, y - 1) && placeables[x, y - 1]?.getPlaceableType() == type ||
            IsValidLocation(x, y + 1) && placeables[x, y + 1]?.getPlaceableType() == type)
        {
            return true;
        }

    return false;
}

private bool IsValidLocation(int x, int y)
{
    return x >= 0 && x < placeables.GetLength(0) && y >= 0 && y < placeables.GetLength(1);
}

private void PlaceRandomly(PlaceableFactory factory, TileType targetTileType, PlaceableType placeable)
{
    var rand = new Random();
    int x;
    int y;
    while (true)
    {
        x = rand.Next(placeables.GetLength(0));
        y = rand.Next(placeables.GetLength(0));
        if (CanPlaceAtLocation(x, y, targetTileType))
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
