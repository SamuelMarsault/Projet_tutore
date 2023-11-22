using Godot;
using Godot.Collections;
using TileMap = Godot.TileMap;

namespace TerritoriaV1;

public abstract class Placeable
{
	private Vector2 position;
	private PlaceableType placeableType;

	public Placeable(Vector2 position, PlaceableType placeableType)
	{
		this.position = position;
		this.placeableType = placeableType;
	}

	abstract public void productResources(Dictionary<ResourceType, int> availableResources,
		Dictionary<ResourceType, int> neededResources);

	abstract public Godot.Collections.Dictionary<ResourceType, int> getAvailableResources();
	abstract public Godot.Collections.Dictionary<ResourceType, int> getResourceNeeds();
	public Vector2 getPosition()
	{
		return position;
	}
	public PlaceableType getPlaceableType()
	{
		return placeableType;
	}
}
