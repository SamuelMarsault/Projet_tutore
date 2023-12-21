using Godot;
using System;
using System.Collections.Generic;
using TerritoriaV1;

public class TertiaryStrat : BuildingStrategy
{
    public TertiaryStrat(Placeable[,] placeables,TileType[,] tiles)
    {
        SetTiles(tiles);
    }

    override 
    public Placeable[,] BuildNewPlaceable(int[] import,
        int[] export, PlaceableFactory factory, 
        TileType[] targetTile, Placeable[,] placeables, int[] resources)
    {
        GD.Print("on est dans le teritiary là");
        List<Placeable> newPlaceables = new List<Placeable>();
        while(import[(int)ResourceType.WOOD] > 10)  // on dépense tout le bois en maison lol ( )
        {
            newPlaceables.Add(factory.CreateHouse());
            import[(int)ResourceType.WOOD] -= 10;
        }
        foreach (Placeable placeable in newPlaceables)
        {
            PlacePlaceable(placeables,placeable, targetTile[placeable.getPlaceableType().GetHashCode()]);
            //Console.WriteLine(targetTile[placeable.getPlaceableType().GetHashCode()]+" "+placeable.getPlaceableType().GetHashCode());
        }

        Destroy(PlaceableType.BEER_USINE,placeables);

        return placeables;
    }
    override 
    public int[,] GetExchangesRates()
    {
        int[,] exchangesRates = new[,]
        {
            { 1, 1, 2, 6 }, //import
            { 2, 1, 1, 6 } //export
        };
        return exchangesRates;
    }

    override public Placeable[,] PlacePlaceable(Placeable[,] placeables,Placeable placeable, TileType targetTile)
     {
            bool notPlaced = true;
            for (int i = 0; i < placeables.GetLength(0) && notPlaced; i++)
            {
                for (int j = 0; j < placeables.GetLength(1) && notPlaced; j++)
                {
                    if (HasAdjacentPlaceableOfType(i, j, placeable.getPlaceableType(), placeables) && CanPlaceAtLocation(i, j, targetTile, placeables))
                    {  GD.Print(i); GD.Print(j);
                       GD.Print(HasAdjacentPlaceableOfType(i, j, placeable.getPlaceableType(), placeables)); GD.Print(CanPlaceAtLocation(i, j, targetTile, placeables));
                       GD.Print(targetTile); GD.Print(placeable.getPlaceableType());
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
