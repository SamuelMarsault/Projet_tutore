using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using TerritoriaV1;

public partial class PrimaryStrat : BuildingStrategy
{
    private Placeable[][] placeables;
    private TileType[][] tiles;
    public PrimaryStrat(Placeable[][] placeables, TileType[][] tiles)
    {
        this.placeables = placeables;
        this.tiles = tiles;
    }

    public List<Placeable> BuildNewPlaceable(int[] totalResources, int[] neededResources, Placeable[][] placeables, PlaceableFactory factory)
    {
            if(neededResources[(int)ResourceType.HOP] > totalResources[(int)ResourceType.HOP])
            {
                if(totalResources[(int)ResourceType.WOOD] > 10)//si on a assez pour construire, on peut plutot tester ca dans Create
                {
                factory.CreateField();  // coordonées
                totalResources[(int)ResourceType.WOOD] -=10; // je met a jour manuellement les valeurs, on peux peut etre l'automatiser 
                }
            }

            if(neededResources[(int)ResourceType.ICE] > totalResources[(int)ResourceType.ICE])
            {
                if(totalResources[(int)ResourceType.WOOD] > 10)
                {
                    factory.CreateIceUsine();
                    totalResources[(int)ResourceType.WOOD] -=10;
                }
            }

               if(neededResources[(int)ResourceType.BEER] < totalResources[(int)ResourceType.BEER]) // je pensais avoir oublier bar et beerusine dans cette strat, donc je les ai rajouté, mais en fait il sont dans la strat suivante, il semblerais donc qu'il y a avait une raison pour ne pas en creer a ce moment 
            {
                if(totalResources[(int)ResourceType.WOOD] > 10)
                {
                    factory.CreateBar();
                    totalResources[(int)ResourceType.WOOD] -=10;
                }
            }

            if(neededResources[(int)ResourceType.ICE] < totalResources[(int)ResourceType.ICE] && neededResources[(int)ResourceType.HOP] < totalResources[(int)ResourceType.HOP])
            {
                factory.CreateBeerUsine();
                 totalResources[(int)ResourceType.WOOD] -=10;
            }

            if(neededResources[(int)ResourceType.WOOD] > totalResources[(int)ResourceType.WOOD])    // on devrait la créer a chaque tour meme si on as assez de bois 
            {
                factory.CreateSawmill();
            }
        
        return null;
    }

    private void Create(PlaceableType placeable, PlaceableFactory factory) // construit le placeable demandé dans sa map. le village doit ensuite se debrouiller pour update sa map a lui
    {
        switch(placeable)
        {
            case PlaceableType.HOUSE: 
                for(int i = 0; i < placeables.GetLength(0); i++)
                {
                    for(int j = 0; j < placeables.GetLength(0); j++)
                    {
                        if((placeables[i][j] == null && tiles[i][j] == TileType.GRASS))
                        {
                            if(placeables[i][j-1].getPlaceableType() == PlaceableType.HOUSE || placeables[i][j+1].getPlaceableType() == PlaceableType.HOUSE || placeables[i-1][j].getPlaceableType() == PlaceableType.HOUSE || placeables[i+1][j].getPlaceableType() == PlaceableType.HOUSE )
                            {
                                placeables[i][j] = factory.CreateHouse(); break;
                            }
                        }
                    }
                }
                var rand = new Random();
                int x;
                int y;
                while(true != false)
                {
                    x = rand.Next(placeables.GetLength(0));
                    y = rand.Next(placeables.GetLength(0));
                    if((placeables[x][y] == null && tiles[x][y] == TileType.GRASS))
                    {
                        placeables[x][y] = factory.CreateHouse(); break;
                    }
                }

            default: break;
        }
    }
}
