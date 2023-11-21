using System.Collections.Generic;
using Godot;

namespace TerritoriaV1;

public interface VillageObserver
{
    public void reactToResourcesChange(Godot.Collections.Dictionary<ResourceType, int> resources);
    public void reactToPlaceableChange(List<Placeable> placeables);
    public void reactToInitialisePlaceable(List<Placeable> placeables);
    public void reactToTilesChangesTiles(Vector2I setTile, int layer, int ID);

}