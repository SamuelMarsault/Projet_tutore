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
	
	/// <summary>
	/// Initialise les composants d'échanges
	/// </summary>
	public override void _Ready()
	{
		Control containerParent = this.GetNode<Control>("Control");
		this.control = containerParent;
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

	/// <summary>
	/// Setter sur la visibilité
	/// </summary>
	/// <param name="vis">Si oui ou non on peut le voir</param>
	public void SetVisibility(bool vis){
		this.control.Visible = vis;
	}
	
	/// <summary>
	/// Signale à toutes les unités d'échanges de changer leur total
	/// </summary>
	/// <param name="total">Nouveau total</param>
	public void TotalChanged(int total)
	{
		foreach (ResourceTradeUnit resourceTradeUnit in resourceTradeUnits)
		{
			resourceTradeUnit.SetExportMax(total);
		}
	}
	
	/// <summary>
	/// Redéfini le max des unités d'échanges en fonction des nouvelles ressources
	/// </summary>
	/// <param name="resources">Les nouvelles ressources</param>
	public void ReactToResourcesChange(int[] resources)
	{
		int max = 100;
		foreach (int res in resources)
			while (max < res)
				max += 100;
		foreach (ResourceTradeUnit resourceTradeUnit in resourceTradeUnits)
			resourceTradeUnit.SetExportMax(max);
	}

	/// <summary>
	/// Réagi au changement des placeables, ici ne fait rien
	/// </summary>
	/// <param name="placeables">Les Placeable du village</param>
	public void ReactToPlaceableChange(Placeable[,] placeables)
	{
		return;
	}

	/// <summary>
	/// Reagi aux changements du sol, ici ne fait rien
	/// </summary>
	/// <param name="tiles">Le sol du village</param>
	/// <returns>Le tableau de besoin en ressources</returns>
	public void ReactToTilesChange(TileType[,] tiles)
	{
		return;
	}

	/// <summary>
	/// Si le bouton est pressé, récupère tous les flux et les envoie pour le jouer le tour
	/// </summary>
	private void _on_button_pressed()
	{
		int[] import = new int[Enum.GetNames(typeof(ResourceType)).Length-1];
		int[] export = new int[Enum.GetNames(typeof(ResourceType)).Length-1];
		int[] money = new int[Enum.GetNames(typeof(ResourceType)).Length-1];
		for (int i = 0; i < resourceTradeUnits.Count; i++)
		{
			export[i] = resourceTradeUnits[i].GetExportValue();
			money[i] = resourceTradeUnits[i].GetExportValue() * exchangesRates[1,i]; 
			import[i] = resourceTradeUnits[i].GetImportValue();
			money[i] = money[i] - resourceTradeUnits[i].GetImportValue() * exchangesRates[0,i];
		}
		parent.nextTurn(export, import, money);
	}

	/// <summary>
	/// Réagi aux ressources manquantes, ici ne fait rien
	/// </summary>
	/// <param name="missingResources">Les Placeable du village</param>
	public void ReactToImpossibleTransaction(int[] missingResources)
	{
		
	}

	/// <summary>
	/// Réagi au changement des taux de change
	/// </summary>
	/// <param name="exchangesRates">Les Placeable du village</param>
	public void ReactToExchangesRatesChange(int[,] exchangesRates)
	{
		this.exchangesRates = exchangesRates;
		for (int i = 0; i < resourceTradeUnits.Count; i++)
		{
			int[] newRates = { exchangesRates[0,i],exchangesRates[1,i] };
			resourceTradeUnits[i].SetExchangeRate(newRates);
		}
	}
}
