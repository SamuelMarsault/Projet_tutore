using System.Collections.Generic;
using Godot;

namespace TerritoriaV1;

public interface VillageObserver
{
    public void reactToResourcesChange(Godot.Collections.Dictionary<ResourceType, int> resources);
    public void reactToPlaceableChange(List<Placeable> placeables);
    public void reactToTilesChange(TileType[][] tiles);
}