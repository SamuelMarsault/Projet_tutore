using System;
using System.Collections.Generic;
using TerritoriaV1;

public interface BuildingStrategy {
    public List<Placeable> BuildNewPlaceable(int[] totalResources,
        int[] neededResources, Placeable[][] placeables, PlaceableFactory factory);

}
