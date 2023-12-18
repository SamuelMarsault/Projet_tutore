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
    GD.Print("tiles : "+ tiles[x,y]); GD.Print("target : "+targetTileType);
    return true; 
    }
}
return false;

}


    protected bool HasAdjacentPlaceableOfType(int x, int y, PlaceableType type, Placeable[,] placeables)
    {

    /*if(placeables == null)
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

    return false;*/


   /* for(int i = 0; i < placeables.GetLength(0); i++)
    {
        for(int j = 0; j < placeables.GetLength(0); j++)
        {
            if(placeables[i,j] != null)
            {
                GD.Print(placeables[i,j] + " "+x+" "+y);
            }
        }
        GD.Print("transition");
    }
    return true;*/

    //GD.Print("type " + type);
    //GD.Print(placeables.GetLength(0));



        if(((x<placeables.GetLength(0)-1)   &&    (placeables[x+1,y] != null)    && (placeables[x+1,y].getPlaceableType() == type)) ||
        ((y<placeables.GetLength(0)-1)  &&  (placeables[x,y+1] != null )    &&  ( placeables[x,y+1].getPlaceableType() == type)) ||
        ((x>0)  &&  (placeables[x-1,y] != null )    &&  ( placeables[x-1,y].getPlaceableType() == type)) ||
        ((y>0)  &&  (placeables[x,y-1] != null )    &&  ( placeables[x,y-1].getPlaceableType() == type)))
        {
            return true;
        }

    return false;
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
}
