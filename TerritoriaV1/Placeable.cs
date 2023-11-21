using Godot;
using Godot.Collections;

namespace TerritoriaV1;

public abstract class Placeable
{
	private Vector2I position;
	private PlaceableType placeableType;

	private int ID;

	public Placeable(Vector2I position, PlaceableType placeableType, int ID)
	{
		this.position = position;
		this.placeableType = placeableType;
		this.ID = ID;
	}

	abstract public void productResources(Dictionary<ResourceType, int> availableResources,
		Dictionary<ResourceType, int> neededResources);

	abstract public Godot.Collections.Dictionary<ResourceType, int> getAvailableResources();
	abstract public Godot.Collections.Dictionary<ResourceType, int> getResourceNeeds();
	public Vector2I getPosition()
	{
		return position;
	}
	public PlaceableType getPlaceableType()
	{
		return placeableType;
	}
	public int getID()
	{
		return ID;
	}
}
