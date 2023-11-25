using Godot;
using System;
using System.Collections.Generic;
using TerritoriaV1;

public partial class Trader : Node, VillageObserver
{
	private List<ResourceTradeUnit> resourceTradeUnits = new ();
	private Godot.Collections.Dictionary<ResourceType, int> resources;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Node container = this.GetNode("Control/MarginContainer/VBoxContainer");
		/*foreach (HBoxContainer node in container.GetChildren())
		{
			foreach (ResourceTradeUnit resourceTradeUnit in node.GetChildren())
			{
				resourceTradeUnits.Add(resourceTradeUnit);
				Action myAction = () => { TotalChanged(resourceTradeUnit.GetTotal()); };
				resourceTradeUnit.Connect(ResourceTradeUnit.SignalName.TotalChanged,Callable.From(myAction));
			}
		}*/
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void TotalChanged(int total)
	{
		foreach (ResourceTradeUnit resourceTradeUnit in resourceTradeUnits)
		{
			Console.WriteLine("coucou");
			resourceTradeUnit.SetExportMax(total);
		}
	}
	
	public void reactToResourcesChange(Godot.Collections.Dictionary<ResourceType, int> resources)
	{
		this.resources = resources;
	}

	public void reactToPlaceableChange(List<Placeable> placeables)
	{
		
	}

	public void reactToTilesChange(TileType[][] tiles)
	{
		
	}
}
