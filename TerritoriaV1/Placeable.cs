using System;
using System.Xml;
using Godot;
using Godot.Collections;
using Godot.NativeInterop;
using TileMap = Godot.TileMap;

namespace TerritoriaV1;

public class Placeable
{
	private PlaceableType placeableType;
	//Défini les types et la quantité de resssources nécéssaire pour produire n output
	private int[] input; 
	//Défini les types et la quantité de resssources produite
	private int[] output;
	//Représente la capacité de production : output * capacite = quantité totale
	private int productionCapacities;

	public Placeable(PlaceableType placeableType, int[] input, int[] output, int productionCapacities)
	{
		this.placeableType = placeableType;
		this.input = input;
		this.output = output;
		this.productionCapacities = productionCapacities;
	}

	public bool ProductResources(int[] availableResources, int[] neededResources)
	{
		int min = 0;
		bool availableRessourceExist = true;
		//Pour chaque ressources en entrée
		for (int i = 0; i < input.Length; i++)
		{
			/*Si -> on n'a pas de minimum, ou qu'on a besoin de cette ressource
			 et qu'elle est en + faible quantité que les autres*/
			if (input[i]!=0 && (min==0 || availableResources[i]/input[i]<min))
			{
				//Alors on définit un nouveau minium
				min = availableResources[i]/input[i];
			}
		}
		//Si le minimum dépasse la capacité de production maximale
		if (min>productionCapacities)
		{
			//Alors le minimum devient la capaité de production
			min = productionCapacities;
		}

		//Et pour chaque ressources en entrée
		for (int i = 0; i < input.Length; i++)
		{
			//On calcule combien on en prend
			int usedResources = min * input[i];
			//Et on les retire des ressources disponibles
			if ((availableResources[i] -= usedResources) < 0){
				availableResources[i] -= usedResources;
				availableRessourceExist = false;
			}
		}
		for (int i = 0; i < output.Length; i++)
		{
			int producedResources = min * output[i];
			availableResources[i] += producedResources;
		}
		return availableRessourceExist ;
	}

	public int[] getResourceNeeds()
	{
		int[] needs = new int[input.Length];
		for (int i = 0; i < input.Length; i++)
		{
			needs[i] = input[i] * productionCapacities;
		}
		return needs;
	}

	public int getProductionCapacity(){
		return productionCapacities;
	}
	public PlaceableType getPlaceableType()
	{
		return placeableType;
	}
}
