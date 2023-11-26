using System;
using System.Collections.Generic;
using TerritoriaV1;

public interface BuildingStrategy {
    public List<Placeable> BuildNewPlaceable(Godot.Collections.Dictionary<ResourceType, int> totalResources,
        double fulfilementOfNeeds, List<Placeable> placeables, PlaceableFactory factory);
}