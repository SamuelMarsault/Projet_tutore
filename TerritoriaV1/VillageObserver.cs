using System.Collections.Generic;
using Godot;

namespace TerritoriaV1;

public interface VillageObserver
{
    public void ReactToResourcesChange(int[] resources);
    public void ReactToPlaceableChange(Placeable[][] placeables);
    public void ReactToTilesChange(TileType[][] tiles);
}