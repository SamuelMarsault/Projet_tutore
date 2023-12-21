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
	//Est-ce que le placeable a produit à capacité maximale ?
	private bool maxProduct = true;

	public Placeable(PlaceableType placeableType, int[] input, int[] output, int productionCapacities)
	{
		this.placeableType = placeableType;
		this.input = input;
		this.output = output;
		this.productionCapacities = productionCapacities;
	}
	
	public void ProductResources(int[] availableResources, int[] neededResources)
	{
		int min = productionCapacities;
		maxProduct = false;
		//Pour chaque ressources en entrée
		for (int i = 0; i < input.Length; i++)
		{
			/*Si -> on n'a pas de minimum, ou qu'on a besoin de cette ressource
			 et qu'elle est en + faible quantité que les autres*/
			if (input[i]!=0 && availableResources[i]/input[i]<min)
			{
				//Alors on définit un nouveau minium
				min = availableResources[i]/input[i];
			}
		}

		if (min < 0)
			min = 0;
		else if (min > productionCapacities)
		{
			min = productionCapacities;
		}

		maxProduct = min == productionCapacities;
		
		for (int i = 0; i < input.Length; i++)
		{
			//On calcule combien on en prend
			int usedResources = min * input[i];
			availableResources[i] -= usedResources;
		}
		for (int i = 0; i < output.Length; i++)
		{
			int producedResources = min * output[i];
			availableResources[i] += producedResources;
		}
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
	public int[] getResourceProduction()
	{
		int[] product = new int[output.Length];
		for (int i = 0; i < output.Length; i++)
		{
			product[i] = output[i] * productionCapacities;
		}
		return product;
	}

	public int getProductionCapacity(){
		return productionCapacities;
	}

	public bool getMaxProduct()
	{
		return maxProduct;
	}
	public void setProductionCapacity(int productionCapacities)
	{
		this.productionCapacities = productionCapacities;
	}
	public PlaceableType getPlaceableType()
	{
		return placeableType;
	}

	public int[] getResourceInputs()
    {
        return input;
    }

	public int[] getResourceOutputs()
    {
        return output;
    }
}
