using Godot;
using System;
using System.Collections.Generic;
using TerritoriaV1;

public partial class Printer : Node, VillageObserver
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void updateResources(ResourceType[] resources, int[] quantities) {

	}
	public void ReactToResourcesChange(int[] resources)
	{
		
	}

	public void ReactToPlaceableChange(Placeable[][] placeables)
	{
		
	}

	public void ReactToTilesChange(TileType[][] tiles)
	{
		
	}
}
