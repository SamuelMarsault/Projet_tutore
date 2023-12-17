using System;
using System.Collections.Generic;
using Godot;
using TerritoriaV1;

public abstract class BuildingStrategy {
    private TileType[,] tiles;

    public abstract Placeable[,] BuildNewPlaceable(int[] totalResources,
        int[] neededResources, PlaceableFactory factory, 
        TileType[] targetTile,Placeable[,] placeables, int[] resourcesBeforeProduct);


    public abstract int[,] GetExchangesRates();
    public abstract Placeable[,] PlacePlaceable(Placeable[,] placeables,Placeable placeable, TileType targetTile);
    /*{
        bool notPlaced = true;
        for (int i = 0; i < placeables.GetLength(0) && notPlaced; i++)
        {
            for (int j = 0; j < placeables.GetLength(1) && notPlaced; j++)
            {
                if (HasAdjacentPlaceableOfType(i, j, placeable.getPlaceableType(), placeables) && CanPlaceAtLocation(i, j, targetTile, placeables))
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
    }*/

    protected bool CanPlaceAtLocation(int x, int y, TileType targetTileType, Placeable[,] placeables) {
        return placeables[x, y] == null && tiles[x, y] == targetTileType;
    }

    protected bool HasAdjacentPlaceableOfType(int x, int y, PlaceableType type, Placeable[,] placeables){

    if(placeables == null)
    {
        GD.Print("placeables est nulle");
    }

    GD.Print("x : " + x);
    GD.Print("y : " + y);
    GD.Print("type : " + type);
    
    if ((x - 1 >= 0 && placeables[x - 1, y].getPlaceableType() == type) ||
        (x + 1 < placeables.GetLength(0) && placeables[x + 1, y].getPlaceableType() == type) ||
        (y - 1 >= 0 && placeables[x, y - 1].getPlaceableType() == type) ||
        (y + 1 < placeables.GetLength(0) && placeables[x, y + 1].getPlaceableType() == type))
        {
            return true;
        }

    return false;
    }

    protected void PlaceRandomly(TileType targetTileType, Placeable placeable, Placeable[,] placeables) {
        var rand = new Random();
        int x = rand.Next(15);
        int y = rand.Next(15);
        while (CanPlaceAtLocation(x, y, targetTileType, placeables))
        {
            x = rand.Next(placeables.GetLength(0));
            y = rand.Next(placeables.GetLength(1));
        }
        placeables[x, y] = placeable;
    }
    public void SetTiles(TileType[,] tiles) {this.tiles = tiles;}
}
