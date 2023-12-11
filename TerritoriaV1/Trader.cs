using Godot;
using System;
using System.Collections.Generic;
using TerritoriaV1;

public partial class Trader : Node, VillageObserver
{

	private int[] resources;
	private int[,] exchangesRates;
	[Export] private GameManager parent;

	private List<ResourceTradeUnit> resourceTradeUnits = new ();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print(parent.Name);
		Node container = this.GetNode("Control/MarginContainer/VBoxContainer");
		foreach (Node node in container.GetChildren())
		{
			if (node.IsClass("HBoxContainer"))
			{
				foreach (ResourceTradeUnit resourceTradeUnit in node.GetChildren())
				{
					resourceTradeUnits.Add(resourceTradeUnit);
					Action myAction = () => { TotalChanged(resourceTradeUnit.GetTotal()); };
					resourceTradeUnit.Connect(ResourceTradeUnit.SignalName.TotalChanged,Callable.From(myAction));
				}
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
			//Console.WriteLine("coucou");
			resourceTradeUnit.SetExportMax(total);
		}
	}
	
	public void ReactToResourcesChange(int[] resources)
	{
		this.resources = resources;
	}

	public void ReactToPlaceableChange(Placeable[,] placeables)
	{
		
	}

	public void ReactToTilesChange(TileType[,] tiles)
	{
		
	}
	private void _on_button_pressed()
	{
		int[] import = new int[Enum.GetNames(typeof(ResourceType)).Length];
		int[] export = new int[Enum.GetNames(typeof(ResourceType)).Length];
		for (int i = 0; i < resourceTradeUnits.Count; i++)
		{
			export[i] = resourceTradeUnits[i].GetExportValue();
			import[ResourceType.MONEY.GetHashCode()] += resourceTradeUnits[i].GetExportValue() * exchangesRates[0,i]; 
			import[i] = resourceTradeUnits[i].GetImportValue();
			export[ResourceType.MONEY.GetHashCode()] += resourceTradeUnits[i].GetImportValue() * exchangesRates[1,i];
		}
		Console.WriteLine("import : ");
		for (int i = 0; i < import.Length; i++)
		{
			Console.WriteLine(Enum.GetValues(typeof(ResourceType)).GetValue(i)+" : "+import[i]);
		}
		Console.WriteLine("export : ");
		for (int i = 0; i < export.Length; i++)
		{
			Console.WriteLine(Enum.GetValues(typeof(ResourceType)).GetValue(i)+" : "+export[i]);
		}
		parent.nextTurn(export, import);
	}

	public void ReactToImpossibleTransaction()
	{
		
	}

	public void ReactToExchangesRatesChange(int[,] exchangesRates)
	{
		this.exchangesRates = exchangesRates;
		for (int i = 0; i < resourceTradeUnits.Count; i++)
		{
			int[] newRates = { exchangesRates[0,i],exchangesRates[1,i] };
			Console.WriteLine("coucou : "+this.exchangesRates[1,i]+" "+exchangesRates[0,i]);
			resourceTradeUnits[i].SetExchangeRate(newRates);
		}
	}
}
