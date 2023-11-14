using System;
using System.Collections.Generic;
using Godot;
using TerritoriaV1;

public class BuildingGrowthStrategy : BuildingStrategy
{
    private TileType[][] tiles;

    public BuildingGrowthStrategy(TileType[][] tiles)
    {
        this.tiles = tiles;
    }
    public List<Placeable> buildNewPlaceable(Godot.Collections.Dictionary<ResourceType, int> totalResources, double fulfilementOfNeeds, List<Placeable> placeables, PlaceableFactory factory)
    {
        List<Placeable> newPlaceables = new List<Placeable>();
        if (fulfilementOfNeeds>=0.95)
        {
            //3 maisons + 1 unité de production + 1 nouvelle ressource 
        }
        else if (fulfilementOfNeeds>=0.80)
        {
            //2 maisons + 1 nouvelle ressource + 1 unité de prod
        }
        else if (fulfilementOfNeeds>=0.60)
        {
            //2 maison + 1 unité de prod
        }
        else if (fulfilementOfNeeds>=0.50)
        {
            //2 maison
        }
        else if (fulfilementOfNeeds>=0.25)
        {
            //1 maison
        }
        return null;
    }

    public Vector2 seekCompatibleTile(PlaceableType newPlaceableType,List<Placeable> currentPlaceables, List<Placeable> newPlaceables)
    {
        for (int i = currentPlaceables.Count - 1; i >= 0; i--)
        {
            if (currentPlaceables[i].getPlaceableType().Equals(newPlaceableType))
            {
                
            }
        }
        return Vector2.Down;
    }
}
