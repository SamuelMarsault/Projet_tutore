using Godot;
using Godot.Collections;

namespace TerritoriaV1;

public abstract class Placeable
{
	protected Vector2 position;

	abstract public void productResources(Dictionary<ResourceType, int> availableResources,
		Dictionary<ResourceType, int> neededResources);

	abstract public Godot.Collections.Dictionary<ResourceType, int> getAvailableResources();
	abstract public Godot.Collections.Dictionary<ResourceType, int> getResourceNeeds();
	public Vector2 getPosition()
	{
		return position;
	}
}
