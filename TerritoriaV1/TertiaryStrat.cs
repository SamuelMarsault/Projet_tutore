using Godot;
using System;
using System.Collections.Generic;
using TerritoriaV1;

public partial class TertiaryStrat : BuildingStrategy
{
    
    private Placeable[,] placeables;
    private TileType[,] tiles;
    public TertiaryStrat(Placeable[,] placeables, TileType[,] tiles)
    {
        this.placeables = placeables;
        this.tiles = tiles;
    }

    public List<Placeable> BuildNewPlaceable(int[] totalResources, int[] neededResources, Placeable[,] placeables, PlaceableFactory factory)
    {
        factory.Destroy(PlaceableType.FIELD);   // en gros la deterritorialisation en phase finale c'est transformer des batiments de productions en trucs tertiaire
        factory.Destroy(PlaceableType.ICE_USINE);
        factory.Destroy(PlaceableType.BEER_USINE);
        factory.Destroy(PlaceableType.BAR);

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
            { 1, 1, 2, 6 }, //import
            { 2, 1, 1, 6 } //export
        };
        return exchangesRates;
    }
}
