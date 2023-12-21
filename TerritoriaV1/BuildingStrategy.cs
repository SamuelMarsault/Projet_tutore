using System;
using System.Collections.Generic;
using Godot;
using TerritoriaV1;

public abstract class BuildingStrategy {
    private TileType[,] tiles;

    public abstract Placeable[,] BuildNewPlaceable(int[] import,
        int[] export, PlaceableFactory factory, 
        TileType[] targetTile,Placeable[,] placeables, int[] resources);


    public abstract int[,] GetExchangesRates();
    public abstract Placeable[,] PlacePlaceable(Placeable[,] placeables,Placeable placeable, TileType targetTile);

    protected bool CanPlaceAtLocation(int x, int y, TileType targetTileType, Placeable[,] placeables)
    {
    if (x < placeables.GetLength(0) && y < placeables.GetLength(1))
    {
        if ((tiles[x, y] != targetTileType) || (placeables[x, y] != null))
        {
            
            return false;
        }
        else
        {
            return true; 
        }
    }
    return false;

    }


    protected bool HasAdjacentPlaceableOfType(int x, int y, PlaceableType type, Placeable[,] placeables)
    {

        if(((x<placeables.GetLength(0)-1)   &&    (placeables[x+1,y] != null)    && (placeables[x+1,y].getPlaceableType() == type)) ||
        ((y<placeables.GetLength(0)-1)  &&  (placeables[x,y+1] != null )    &&  ( placeables[x,y+1].getPlaceableType() == type)) ||
        ((x>0)  &&  (placeables[x-1,y] != null )    &&  ( placeables[x-1,y].getPlaceableType() == type)) ||
        ((y>0)  &&  (placeables[x,y-1] != null )    &&  ( placeables[x,y-1].getPlaceableType() == type)))
        {
            return true;
        }

    return false;
   }

    public bool HasTwoNeighbours(int x, int y, PlaceableType type, Placeable[,] placeables)
{
    int rowCount = placeables.GetLength(0);
    int colCount = placeables.GetLength(1);

    int neighbourCount = 0;

    // Check right neighbor
    if (x < rowCount - 1 && placeables[x + 1, y] != null && placeables[x + 1, y].getPlaceableType() == type)
        neighbourCount++;

    // Check bottom neighbor
    if (y < colCount - 1 && placeables[x, y + 1] != null && placeables[x, y + 1].getPlaceableType() == type)
        neighbourCount++;

    // Check left neighbor
    if (x > 0 && placeables[x - 1, y] != null && placeables[x - 1, y].getPlaceableType() == type)
        neighbourCount++;

    // Check top neighbor
    if (y > 0 && placeables[x, y - 1] != null && placeables[x, y - 1].getPlaceableType() == type)
        neighbourCount++;

    // Check diagonal neighbors
    if (x < rowCount - 1 && y < colCount - 1 && placeables[x + 1, y + 1] != null && placeables[x + 1, y + 1].getPlaceableType() == type)
        neighbourCount++;

    if (x > 0 && y < colCount - 1 && placeables[x - 1, y + 1] != null && placeables[x - 1, y + 1].getPlaceableType() == type)
        neighbourCount++;

    if (x < rowCount - 1 && y > 0 && placeables[x + 1, y - 1] != null && placeables[x + 1, y - 1].getPlaceableType() == type)
        neighbourCount++;

    if (x > 0 && y > 0 && placeables[x - 1, y - 1] != null && placeables[x - 1, y - 1].getPlaceableType() == type)
        neighbourCount++;

    return neighbourCount >= 2;
}




    protected void PlaceRandomly(TileType targetTileType, Placeable placeable, Placeable[,] placeables) {
        var rand = new Random();
        int x = rand.Next(15);
        int y = rand.Next(15);
        if(CanPlaceAtLocation(x, y, targetTileType, placeables))
        {
            placeables[x, y] = placeable;
        }
        else
        {
            PlaceRandomly(targetTileType,placeable,placeables);
        }
    }
    public void SetTiles(TileType[,] tiles) {this.tiles = tiles;}

    public void Destroy(PlaceableType type, Placeable[,] placeables)
    {
        Boolean Destroyed = false; 
        for(int i = 0; i < placeables.GetLength(0) && !Destroyed; i++)
        {
            for(int j = 0; j < placeables.GetLength(1) && !Destroyed; j++)
            {
                if(placeables[i,j] != null && placeables[i,j].getPlaceableType() == type)
                {
                    placeables[i,j] = null;
                    Destroyed = true;
                }
            }
        }
    }
}
