using Godot;
using System;
using System.Collections.Generic;
using TerritoriaV1;

public partial class Trader : Node, VillageObserver
{

	private int[] resources;
	[Export] private GameManager parent;

	private List<ResourceTradeUnit> resourceTradeUnits = new ();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Node container = this.GetNode("Control/MarginContainer/VBoxContainer");
		foreach (HBoxContainer node in container.GetChildren())
		{
			foreach (ResourceTradeUnit resourceTradeUnit in node.GetChildren())
			{
				resourceTradeUnits.Add(resourceTradeUnit);
				Action myAction = () => { TotalChanged(resourceTradeUnit.GetTotal()); };
				resourceTradeUnit.Connect(ResourceTradeUnit.SignalName.TotalChanged,Callable.From(myAction));
			}
		}
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
	
	public void ReactToResourcesChange(int[] resources)
	{
		this.resources = resources;
	}

	public void ReactToPlaceableChange(Placeable[][] placeables)
	{
		
	}

	public void ReactToTilesChange(TileType[][] tiles)
	{
		
	}
	private void _on_button_pressed()
	{
		int total = 0;
		int[] import = new int[Enum.GetNames(typeof(ResourceType)).Length];
		int[] export = new int[Enum.GetNames(typeof(ResourceType)).Length];
		for (int i = 0; i < resourceTradeUnits.Count; i++)
		{
			export[i] = resourceTradeUnits[i].GetExportValue();
			import[i] = resourceTradeUnits[i].GetImportValue();
			total += export[i] - import[i];
		}
		parent.nextTurn(export, import, total);
	}
}
