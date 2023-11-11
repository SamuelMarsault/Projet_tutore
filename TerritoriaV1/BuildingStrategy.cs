using System;
using System.Collections.Generic;
using TerritoriaV1;

public interface BuildingStrategy {
    public List<Placeable> buildNewPlaceable(Godot.Collections.Dictionary<ResourceType, int> totalResources, Double fulfilementOfNeeds, List<Placeable> placeables, PlaceableFactory factory);
}