using Godot;
using System;
using System.Collections.Generic;
using TerritoriaV1;

public partial class Trader : Node, VillageObserver
{
	private int[,] exchangesRates;
	private Control control;
	[Export] private GameManager parent;

	private List<ResourceTradeUnit> resourceTradeUnits = new ();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Control containerParent = this.GetNode<Control>("Control");
		this.control = containerParent;
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

	public void setVisibility(){
		this.control.Visible = true;
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
		int max = 100;
		foreach (int res in resources)
			while (max < res)
				max += 100;
		foreach (ResourceTradeUnit resourceTradeUnit in resourceTradeUnits)
			resourceTradeUnit.SetExportMax(max);
	}

	public void ReactToPlaceableChange(Placeable[,] placeables)
	{
		
	}

	public void ReactToTilesChange(TileType[,] tiles)
	{
		
	}
	private void _on_button_pressed()
	{
		int[] import = new int[Enum.GetNames(typeof(ResourceType)).Length-1];
		int[] export = new int[Enum.GetNames(typeof(ResourceType)).Length-1];
		int[] money = new int[Enum.GetNames(typeof(ResourceType)).Length-1];
		for (int i = 0; i < resourceTradeUnits.Count; i++)
		{
			export[i] = resourceTradeUnits[i].GetExportValue();
			money[i] += resourceTradeUnits[i].GetExportValue() * exchangesRates[1,i]; 
			import[i] = resourceTradeUnits[i].GetImportValue();
			money[i] -= resourceTradeUnits[i].GetImportValue() * exchangesRates[0,i];
		}
		/*
		for (int i = 0; i < export.Length; i++)
		{
			GD.Print("export : "+export[i]);
		}
		for (int i = 0; i < import.Length; i++)
		{
			GD.Print("import : "+import[i]);
		}
		for (int i = 0; i < import.Length; i++)
		{
			GD.Print("money : "+money[i]);
		}
		*/
		parent.nextTurn(export, import, money);
	}

	public void ReactToImpossibleTransaction(int[] missingRessources)
	{
		
	}

	public void ReactToExchangesRatesChange(int[,] exchangesRates)
	{
		this.exchangesRates = exchangesRates;
		for (int i = 0; i < resourceTradeUnits.Count; i++)
		{
			int[] newRates = { exchangesRates[0,i],exchangesRates[1,i] };
			//Console.WriteLine("coucou : "+this.exchangesRates[1,i]+" "+exchangesRates[0,i]);
			resourceTradeUnits[i].SetExchangeRate(newRates);
		}
	}
}
