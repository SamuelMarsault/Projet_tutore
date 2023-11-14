using System.Collections.Generic;
using Godot;
using TerritoriaV1;

public class Building : Placeable
{
	private Vector2 position;
	//Défini les types et la quantité de resssources nécéssaire pour produire n output
	private Godot.Collections.Dictionary<ResourceType, int> input; 
	//Défini les types et la quantité de resssources produite
	private Godot.Collections.Dictionary<ResourceType, int> output;
	//Représente la capacité de production : output * capacite = quantité totale
	private int productionCapacities;
	
	public Building(Vector2 position, PlaceableType placeableType, Godot.Collections.Dictionary<ResourceType, int> input,Godot.Collections.Dictionary<ResourceType, int> output, int productionCapacities) : base(position,placeableType)
	{
		this.input = input.Duplicate(true);
		this.output = output.Duplicate(true);
		this.productionCapacities = productionCapacities;
	}
	
	//Produit des ressources en fonction des besoins et ressources disponibles
	public override void productResources(Godot.Collections.Dictionary<ResourceType, int> availableResources, Godot.Collections.Dictionary<ResourceType, int> neededResources)
	{
		int min = 0;
		//Pour chaque ressources en entrée
		foreach (var (type,value) in input)
		{
			//Si la ressources n'est pas dispo
			if (!availableResources.ContainsKey(type))
			{
				//On ne produit rien
				return ;
			}
			if (min==0 || availableResources[type]/value<min)
			{
				min = availableResources[type]/value;
			}
		}
		//Si le minimum dépasse la capacité de production maximale
		if (min>productionCapacities)
		{
			//Alors le minimum devient la capaité de production
			min = productionCapacities;
		}

		//Et pour chaque ressources en entrée
		foreach (var (type,value) in input)
		{
			int usedResources = min * value;
			availableResources[type] -= usedResources;
		}

		foreach (var (type,value) in output)
		{
			int producedResources = min * value;
			if (availableResources.TryAdd(type, min))
				availableResources[type] += producedResources;
		}
	}

	//Renvoi la quantité de ressources dont l'objet à besoin
	public override Godot.Collections.Dictionary<ResourceType, int> getResourceNeeds()
	{
		Godot.Collections.Dictionary<ResourceType, int> resourcesNeeds = new Godot.Collections.Dictionary<ResourceType, int>();
		foreach (var (type,value) in input)
		{ resourcesNeeds.Add(type,value * productionCapacities); }
		return resourcesNeeds;
	}

	//Renvoi la quantité de ressources disponible dans l'objet, ici vide parce que le bâtiment ne stocke rien
	public override Godot.Collections.Dictionary<ResourceType, int> getAvailableResources()
	{
		return new Godot.Collections.Dictionary<ResourceType, int>();
	}
}
